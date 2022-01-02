using prueba.Models;
using prueba.Models.FormsViewModel;
using prueba.Models.TableViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prueba.Controllers
{
    public class PaqueteController : Controller
    {
        // GET: Paquete
        public ActionResult Index()
        {
            List<PaqueteTableViewModel> list = null;
            using (var db = new pruebaEntities())
            {
                list = (
                    from d in db.Paquete
                    join p1 in db.Pais
                    on d.PaisId equals p1.Id
                    join p2 in db.Provincia
                    on d.ProvinciaId equals p2.Id
                    where d.Activo == true
                    select new PaqueteTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id,
                        Precio = d.Precio,
                        CantDias = d.CantDias,
                        FechaViaje = d.FechaViaje,
                        Estado = d.Estado,
                        Lugares = d.Lugares,
                        DescripcionPais = p1.Descripcion,
                        DescripcionProvincia = p2.Descripcion,
                        Nacional = d.Nacional,
                        Cuotas = d.Cuotas,
                        Impuesto = d.Impuesto,
                        Cotizacion = d.Cotizacion,
                        Visa = d.Visa
                    }
                    ).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Paises = GetPaisList();
            ViewBag.Provincias = GetProvinciaList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(PaqueteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Paises = GetPaisList();
                ViewBag.Provincias = GetProvinciaList();
                return View(model);
            }

            using (var db = new pruebaEntities())
            {
                Paquete oPaquete = new Paquete();
                oPaquete.Descripcion = model.Descripcion;
                oPaquete.Cotizacion = Convert.ToDouble(model.Cotizacion, CultureInfo.InvariantCulture);
                oPaquete.CantDias = model.CantDias;
                oPaquete.Cuotas = model.Cuotas;
                oPaquete.FechaViaje = Convert.ToDateTime(model.FechaViaje);
                oPaquete.Lugares = model.Lugares;
                oPaquete.Impuesto = Convert.ToDouble(model.Impuesto, CultureInfo.InvariantCulture);

                if (model.Nacional == 1)
                {
                    oPaquete.Nacional = true;
                }
                else
                {
                    oPaquete.Nacional = false;
                }

                oPaquete.PaisId = model.IdPais;
                oPaquete.ProvinciaId = model.IdProvincia;
                oPaquete.Precio = Convert.ToDouble(model.Precio, CultureInfo.InvariantCulture);

                if (model.Visa == 1)
                {
                    oPaquete.Visa = true;
                }
                else
                {
                    oPaquete.Visa = false;
                }

                oPaquete.Estado = true;
                oPaquete.Activo = true;

                db.Paquete.Add(oPaquete);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Paquete/"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PaqueteViewModel model = new PaqueteViewModel();
            using (var db = new pruebaEntities())
            {
                var oPaquete = db.Paquete.Find(id);
                model.CantDias = oPaquete.CantDias;
                model.Cotizacion = (oPaquete.Cotizacion.ToString()).Replace(",", ".");
                model.Cuotas = oPaquete.Cuotas;
                model.Descripcion = oPaquete.Descripcion;

                if (oPaquete.Estado == true)
                {
                    model.Estado = 1;
                }
                else
                {
                    model.Estado = 0;
                }
                model.FechaViaje = oPaquete.FechaViaje.ToString("yyyy-MM-dd");
                model.Impuesto = (oPaquete.Impuesto.ToString()).Replace(",", ".");
                model.Lugares = oPaquete.Lugares;

                if (oPaquete.Nacional == true)
                {
                    model.Nacional = 1;
                }
                else
                {
                    model.Nacional = 0;
                }
                model.Precio = (oPaquete.Precio.ToString()).Replace(",", ".");
                if (oPaquete.Visa == true)
                {
                    model.Visa = 1;
                }
                else
                {
                    model.Visa = 0;
                }

                model.IdPais = oPaquete.PaisId;
                model.IdProvincia = oPaquete.ProvinciaId;

                ViewBag.ThisPais = oPaquete.PaisId;
                ViewBag.ThisProvincia = oPaquete.ProvinciaId;
                ViewBag.ThisRegion = oPaquete.Nacional;
                ViewBag.ThisVisa = oPaquete.Visa;
                ViewBag.ThisEstado = oPaquete.Estado;
            }
            ViewBag.Paises = GetPaisList();
            ViewBag.Provincias = GetProvinciaList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PaqueteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ThisPais = model.IdPais;
                ViewBag.ThisProvincia = model.IdProvincia;
                if (model.Nacional == 1)
                {
                    ViewBag.ThisRegion = true;
                }
                else
                {
                    ViewBag.ThisRegion = false;
                }
                if (model.Visa == 1)
                {
                    ViewBag.ThisVisa = true;
                }
                else
                {
                    ViewBag.ThisVisa = false;
                }
                if (model.Estado == 1)
                {
                    ViewBag.ThisEstado = true;
                }
                else
                {
                    ViewBag.ThisEstado = false;
                }
                ViewBag.Paises = GetPaisList();
                ViewBag.Provincias = GetProvinciaList();
                return View(model);
            }
            using (var db = new pruebaEntities())
            {
                var oPaquete = db.Paquete.Find(model.Id);
                oPaquete.CantDias = model.CantDias;
                oPaquete.Cotizacion = Convert.ToDouble(model.Cotizacion, CultureInfo.InvariantCulture);
                oPaquete.Cuotas = model.Cuotas;
                oPaquete.Descripcion = model.Descripcion;

                if (model.Estado == 1)
                {
                    oPaquete.Estado = true;
                }
                else
                {
                    oPaquete.Estado = false;
                }
                oPaquete.FechaViaje = Convert.ToDateTime(model.FechaViaje);
                oPaquete.Impuesto = Convert.ToDouble(model.Impuesto, CultureInfo.InvariantCulture);
                oPaquete.Lugares = model.Lugares;

                if (model.Nacional == 1)
                {
                    oPaquete.Nacional = true;
                }
                else
                {
                    oPaquete.Nacional = false;
                }
                oPaquete.Precio = Convert.ToDouble(model.Precio, CultureInfo.InvariantCulture);
                if (model.Visa == 1)
                {
                    oPaquete.Visa = true;
                }
                else
                {
                    oPaquete.Visa = false;
                }

                oPaquete.PaisId = model.IdPais;
                oPaquete.ProvinciaId = model.IdProvincia;

                db.Entry(oPaquete).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Paquete/"));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new pruebaEntities())
            {
                var oPaquete = db.Paquete.Find(id);
                oPaquete.Activo = false;

                db.Entry(oPaquete).State = EntityState.Modified;
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
                    select new PaisTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id
                    }
                     ).ToList();
            }
            return list;
        }

        private List<ProvinciaTableViewModel> GetProvinciaList()
        {
            List<ProvinciaTableViewModel> list = null;
            using (pruebaEntities db = new pruebaEntities())
            {
                list = (
                    from d in db.Provincia
                    where d.Activo == true
                    select new ProvinciaTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id
                    }
                    ).ToList();
            }
            return list;
        }

        [HttpGet]
        public JsonResult ProvinciaList(int id)
        {
            List<ProvinciaTableViewModel> list = new List<ProvinciaTableViewModel>();
            using (var db = new pruebaEntities())
            {
                list = (
                    from d in db.Provincia
                    where d.Activo == true && d.PaisId == id
                    select new ProvinciaTableViewModel
                    {
                        Descripcion = d.Descripcion,
                        Id = d.Id
                    }
                    ).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}