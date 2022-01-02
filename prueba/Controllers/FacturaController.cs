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
    public class FacturaController : Controller
    {
        // GET: Factura
        public ActionResult Index()
        {
            List<FacturaTableViewModel> list = null;
            using (var db = new pruebaEntities())
            {
                list = (
                    from f in db.Factura
                    join c in db.Cliente
                    on f.ClienteId equals c.Id
                    join p in db.Paquete
                    on f.PaqueteId equals p.Id
                    join p1 in db.Pais
                    on p.PaisId equals p1.Id
                    join p2 in db.Provincia
                    on p.ProvinciaId equals p2.Id
                    select new FacturaTableViewModel
                    {
                        ApellidoCliente = c.Apellido,
                        NombreCliente = c.Nombre,
                        DescripcionPaquete = p.Descripcion,
                        ClienteId = c.Id,
                        PaqueteId = p.Id,
                        Fecha = f.Fecha,
                        DniCliente = c.Dni,
                        Id = f.Id,
                        Monto = f.Monto,
                        DescripcionPais = p1.Descripcion,
                        DescripcionProvincia = p2.Descripcion
                    }
                    ).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Clientes = GetClienteList();
            ViewBag.Paquetes = GetPaqueteList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(VentaViewModel mod)
        {
            Paquete oPaquete = null;
            Cliente oCliente = null;
            using (var db = new pruebaEntities())
            {
                oPaquete = db.Paquete.Find(mod.IdPaquete);
            }
            using (var db = new pruebaEntities())
            {
                oCliente = db.Cliente.Find(mod.IdCliente);
            }

            using (var db = new pruebaEntities())
            {
                Factura oFactura = new Factura();
                oFactura.Fecha = DateTime.Now;
                if (oPaquete.Nacional)
                {
                    oFactura.Monto = (oPaquete.Precio + oPaquete.Impuesto);
                }
                else
                {
                    oFactura.Monto = (((oPaquete.Precio * oPaquete.Cotizacion) * oPaquete.Impuesto) / 100) + (oPaquete.Precio * oPaquete.Cotizacion);
                }

                oFactura.PaqueteId = oPaquete.Id;
                oFactura.ClienteId = oCliente.Id;
                db.Factura.Add(oFactura);
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Factura/"));
        }


        private List<ClienteTableViewModel> GetClienteList()
        {
            List<ClienteTableViewModel> list = new List<ClienteTableViewModel>();
            using (var db = new pruebaEntities())
            {
                list = (
                    from c in db.Cliente
                    where c.Activo == true
                    select new ClienteTableViewModel
                    {
                        Apellido = c.Apellido,
                        Nombre = c.Nombre,
                        Id = c.Id,
                        Dni = c.Dni,
                        IdEmpresa = c.EmpresaId
                    }
                    ).ToList();
            }
            return list;
        }

        private List<PaqueteTableViewModel> GetPaqueteList()
        {
            List<PaqueteTableViewModel> list = new List<PaqueteTableViewModel>();
            using (var db = new pruebaEntities())
            {
                list = (
                    from p in db.Paquete
                    where p.Activo == true && p.Estado == true
                    select new PaqueteTableViewModel
                    {
                        Cotizacion = p.Cotizacion,
                        Impuesto = p.Impuesto,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Nacional = p.Nacional,
                        Id = p.Id
                    }
                    ).ToList();
            }
            return list;
        }
    }
}