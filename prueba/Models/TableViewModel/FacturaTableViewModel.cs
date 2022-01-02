using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba.Models.TableViewModel
{
    public class FacturaTableViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public bool Activo { get; set; }


        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public double DniCliente { get; set; }
        public string ApellidoCliente { get; set; }


        public string DescripcionPaquete { get; set; }
        public int PaqueteId { get; set; }   
        
        public string DescripcionPais { get; set; }
        public string DescripcionProvincia { get; set; }
    }
}