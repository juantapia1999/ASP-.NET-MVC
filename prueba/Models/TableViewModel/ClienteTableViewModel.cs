using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba.Models.TableViewModel
{
    public class ClienteTableViewModel
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public double Telefono { get; set; }
        public double Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public int IdPais { get; set; }
        public string DescripcionPais { get; set; }
        public int? IdEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
    }
}