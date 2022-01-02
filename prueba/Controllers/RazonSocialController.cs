using prueba.Models;
using prueba.Models.FormsViewModel;
using prueba.Models.TableViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prueba.Controllers
{
    public class RazonSocialController : Controller
    {
        // GET: RazonSocial
        public ActionResult Index()
        {
            List<RazonSocialTableViewModel> list = null;
            using (var db = new pruebaEntities())
            {
                list = (
                    from d in db.RazonSocial
                    where d.Activo == true
                    select new RazonSocialTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id
                    }
                    ).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RazonSocialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new pruebaEntities())
            {
                RazonSocial oRazonSocial = new RazonSocial();
                oRazonSocial.Descripcion = model.Descripcion;
                oRazonSocial.Activo = true;
                db.RazonSocial.Add(oRazonSocial);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/RazonSocial"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            RazonSocialViewModel model = new RazonSocialViewModel();

            using (var db = new pruebaEntities())
            {
                var oRazonSocial = db.RazonSocial.Find(id);
                model.Descripcion = oRazonSocial.Descripcion;
                model.Id = oRazonSocial.Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RazonSocialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new pruebaEntities())
            {
                var oRazonSocial = db.RazonSocial.Find(model.Id);
                oRazonSocial.Descripcion = model.Descripcion;

                db.Entry(oRazonSocial).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/RazonSocial/"));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new pruebaEntities())
            {
                var oRazonSocial = db.RazonSocial.Find(id);
                oRazonSocial.Activo = false;

                db.Entry(oRazonSocial).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }
    }
}