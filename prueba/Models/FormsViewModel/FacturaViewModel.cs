using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba.Models.FormsViewModel
{
    public class FacturaViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public bool Activo { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public double DniCliente { get; set; }
        public string ApellidoCliente { get; set; }

        [Required]
        public int PaqueteId { get; set; }
        public string DescripcionPaquete { get; set; }
        public double PrecioPaquete { get; set; }
        public double CotizacionPaquete { get; set; }
        public double Impuesto { get; set; }
        public int NacionalPaquete { get; set; }
    }
}