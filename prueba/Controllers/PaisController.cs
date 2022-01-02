using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prueba.Models;
using prueba.Models.TableViewModel;
using prueba.Models.FormsViewModel;
using System.Data.Entity;

namespace prueba.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        public ActionResult Index()
        {
            List<PaisTableViewModel> list = null;
            using (pruebaEntities db = new pruebaEntities())
            {
                list = (
                    from d in db.Pais
                    where d.Activo == true
                    select new PaisTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id
                    }
                     ).ToList();
            }

            /*string[] pais = { "hola", "mundo", "como", "va :)" };
            ViewBag.Pais = pais;*/
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PaisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new pruebaEntities())
            {
                Pais oPais = new Pais();
                oPais.Descripcion = model.Descripcion;
                oPais.Activo = true;
                db.Pais.Add(oPais);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Pais/"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PaisViewModel model = new PaisViewModel();

            using (var db = new pruebaEntities())
            {
                var oPais = db.Pais.Find(id);
                model.Descripcion = oPais.Descripcion;
                model.Id = oPais.Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PaisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new pruebaEntities())
            {
                var oPais = db.Pais.Find(model.Id);
                oPais.Descripcion = model.Descripcion;

                db.Entry(oPais).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Pais/"));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new pruebaEntities())
            {
                var oPais = db.Pais.Find(id);
                oPais.Activo = false;

                db.Entry(oPais).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }
    }
}