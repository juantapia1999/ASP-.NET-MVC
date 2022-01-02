using prueba.Models;
using prueba.Models.FormsViewModel;
using prueba.Models.TableViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace prueba.Controllers
{
    public class ProvinciaController : Controller
    {
        // GET: Provincia
        public ActionResult Index()
        {
            List<ProvinciaTableViewModel> list = null;
            using (pruebaEntities db = new pruebaEntities())
            {
                list = (
                    from d in db.Provincia
                    join p in db.Pais
                    on d.PaisId equals p.Id
                    where d.Activo == true
                    select new ProvinciaTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id,
                        IdPais = p.Id,
                        DescripcionPais = p.Descripcion
                    }
                     ).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Paises = GetPaisList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProvinciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Paises = GetPaisList();
                return View(model);
            }

            using (var db = new pruebaEntities())
            {
                Provincia oProvincia = new Provincia();
                oProvincia.Descripcion = model.Descripcion;
                oProvincia.PaisId = model.PaisId;
                oProvincia.Activo = true;
                db.Provincia.Add(oProvincia);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Provincia/"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProvinciaViewModel model = new ProvinciaViewModel();
            using (pruebaEntities db = new pruebaEntities())
            {
                var oProvincia = db.Provincia.Find(id);
                model.Descripcion = oProvincia.Descripcion;
                model.Id = oProvincia.Id;
                model.PaisDescripcion = oProvincia.Pais.Descripcion;

                ViewBag.ThisPais = oProvincia.PaisId;
            }

            /*List<PaisTableViewModel> list = null;
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
            ViewBag.Paises = list;*/
            ViewBag.Paises = GetPaisList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProvinciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                /*List<PaisTableViewModel> list = null;
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
                ViewBag.Paises = list;*/
                ViewBag.Paises = GetPaisList();
                return View(model);
            }
            using (var db = new pruebaEntities())
            {
                var oProvincia = db.Provincia.Find(model.Id);
                oProvincia.Descripcion = model.Descripcion;
                oProvincia.PaisId = model.PaisId;

                db.Entry(oProvincia).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Provincia/"));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new pruebaEntities())
            {
                var oProvincia = db.Provincia.Find(id);
                oProvincia.Activo = false;

                db.Entry(oProvincia).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }

        private List<PaisTableViewModel> GetPaisList()
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
            return list;
        }
    }

}