using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba.Models.TableViewModel
{
    public class EmpresaTableViewModel
    {
        public int Id { get; set; }
        public double Cuit { get; set; }
        public bool Activo { get; set; }
        public int IdRazonSocial { get; set; }
        public string DescripcionRazonSocial { get; set; }
        public string Descripcion { get; set; }
    }
}