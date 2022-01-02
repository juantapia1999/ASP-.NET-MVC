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
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            List<EmpresaTableViewModel> list = null;
            using (var db = new pruebaEntities())
            {
                list = (
                    from d in db.Empresa
                    join r in db.RazonSocial
                    on d.RazonSocialId equals r.Id
                    where d.Activo == true
                    select new EmpresaTableViewModel
                    {
                        Cuit = d.Cuit,
                        Descripcion = d.Descripcion,
                        Id = d.Id,
                        IdRazonSocial = r.Id,
                        DescripcionRazonSocial = r.Descripcion
                    }
                    ).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.RazonesSociales = GetRazonSocialList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(EmpresaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RazonesSociales = GetRazonSocialList();
                return View(model);
            }
            using (var db = new pruebaEntities())
            {
                Empresa oEmpresa = new Empresa();
                oEmpresa.Activo = true;
                oEmpresa.Cuit = Convert.ToDouble(model.Cuit);
                oEmpresa.Descripcion = model.Descripcion;
                oEmpresa.RazonSocialId = model.RazonSocialId;

                db.Empresa.Add(oEmpresa);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Empresa/"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmpresaViewModel model = new EmpresaViewModel();
            using(var db = new pruebaEntities())
            {
                var oEmpresa = db.Empresa.Find(id);
                model.Id = oEmpresa.Id;
                model.Cuit = oEmpresa.Cuit;
                model.Descripcion = oEmpresa.Descripcion;
                model.RazonSocialId = oEmpresa.RazonSocialId;

                ViewBag.ThisRazon = oEmpresa.RazonSocialId;
                ViewBag.RazonesSociales = GetRazonSocialList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmpresaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ThisRazon = model.RazonSocialId;
                ViewBag.RazonesSociales = GetRazonSocialList();
                return View(model);
            }

            using(var db = new pruebaEntities())
            {
                var oEmpresa = db.Empresa.Find(model.Id);
                oEmpresa.Descripcion = model.Descripcion;
                oEmpresa.Cuit = Convert.ToDouble(model.Cuit);
                oEmpresa.RazonSocialId = model.RazonSocialId;

                db.Entry(oEmpresa).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Empresa/"));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using(var db = new pruebaEntities())
            {
                var oEmpresa = db.Empresa.Find(id);
                oEmpresa.Activo = false;
                db.Entry(oEmpresa).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }

        private List<RazonSocialTableViewModel> GetRazonSocialList()
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
            return list;
        }
    }
}