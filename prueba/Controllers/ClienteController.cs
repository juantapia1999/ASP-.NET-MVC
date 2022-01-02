using prueba.Models;
using prueba.Models.TableViewModel;
using prueba.Models.FormsViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prueba.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<ClienteTableViewModel> list = null;
            using (var db = new pruebaEntities())
            {
                list = (
                    from c in db.Cliente
                    join p in db.Pais
                    on c.PaisId equals p.Id
                    join e in db.Empresa
                    on c.EmpresaId equals e.Id
                    into clienteEmpresa
                    from cliEmp in clienteEmpresa.DefaultIfEmpty()
                    where c.Activo == true
                    select new ClienteTableViewModel
                    {
                        Apellido = c.Apellido,
                        //DescripcionEmpresa=data_B.Descripcion,
                        DescripcionEmpresa = cliEmp.Descripcion,
                        DescripcionPais = p.Descripcion,
                        Direccion = c.Direccion,
                        Dni = c.Dni,
                        //IdEmpresa=data_B.Id,
                        IdEmpresa = cliEmp.Id,
                        IdPais = p.Id,
                        Nombre = c.Nombre,
                        Telefono = c.Telefono,
                        Id = c.Id
                    }
                    ).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Empresas = GetEmpresaList();
            ViewBag.Paises = GetPaisList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Empresas = GetEmpresaList();
                ViewBag.Paises = GetPaisList();
                return View(model);
            }

            using (var db = new pruebaEntities())
            {
                Cliente oCliente = new Cliente();
                oCliente.Activo = true;
                oCliente.Apellido = model.Apellido;
                oCliente.Direccion = model.Direccion;
                oCliente.Dni = Convert.ToDouble(model.Dni);
                if (model.IdEmpresa == 0)
                {
                    oCliente.EmpresaId = null;
                }
                else
                {
                    oCliente.EmpresaId = model.IdEmpresa;
                }
                oCliente.Nombre = model.Nombre;
                oCliente.PaisId = model.IdPais;
                oCliente.Telefono = model.Telefono;

                db.Cliente.Add(oCliente);
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Cliente/"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ClienteViewModel model = new ClienteViewModel();
            using (var db = new pruebaEntities())
            {
                var oCliente = db.Cliente.Find(id);
                model.Apellido = oCliente.Apellido;
                model.Direccion = oCliente.Direccion;
                model.Dni = oCliente.Dni;
                model.IdEmpresa = oCliente.EmpresaId;
                model.IdPais = oCliente.PaisId;
                model.Nombre = oCliente.Nombre;
                model.Telefono = oCliente.Telefono;

                ViewBag.ThisEmpresa = oCliente.EmpresaId;
                ViewBag.ThisPais = oCliente.PaisId;
            }
            ViewBag.Empresas = GetEmpresaList();
            ViewBag.Paises = GetPaisList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ThisEmpresa = model.IdEmpresa;
                ViewBag.ThisPais = model.IdPais;

                ViewBag.Empresas = GetEmpresaList();
                ViewBag.Paises = GetPaisList();
                return View(model);
            }

            using (var db = new pruebaEntities())
            {
                var oCliente = db.Cliente.Find(model.Id);
                oCliente.Apellido = model.Apellido;
                oCliente.Direccion = model.Direccion;
                oCliente.Dni = model.Dni;
                if (model.IdEmpresa == 0)
                {
                    oCliente.EmpresaId = null;
                }
                else
                {
                    oCliente.EmpresaId = model.IdEmpresa;
                }
                oCliente.Nombre = model.Nombre;
                oCliente.PaisId = model.IdPais;
                oCliente.Telefono = model.Telefono;

                db.Entry(oCliente).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Cliente/"));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db=new pruebaEntities())
            {
                var oCliente = db.Cliente.Find(id);
                oCliente.Activo = false;

                db.Entry(oCliente).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }

        private List<EmpresaTableViewModel> GetEmpresaList()
        {
            List<EmpresaTableViewModel> list = null;
            using (var db = new pruebaEntities())
            {
                list = (
                    from e in db.Empresa
                    where e.Activo == true
                    select new EmpresaTableViewModel
                    {
                        Descripcion = e.Descripcion,
                        Id = e.Id
                    }
                    ).ToList();
            }
            return list;
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
    }
}