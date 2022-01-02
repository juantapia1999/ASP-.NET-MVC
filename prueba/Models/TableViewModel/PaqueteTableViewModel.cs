using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba.Models.TableViewModel
{
    public class PaqueteTableViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int CantDias { get; set; }
        public DateTime FechaViaje { get; set; }
        public bool Estado { get; set; }
        public int Lugares { get; set; }
        public string DescripcionPais { get; set; }
        public int IdPais { get; set; }
        public string DescripcionProvincia { get; set; }
        public int IdProvincia { get; set; }
        public bool Nacional { get; set; }
        public double Cotizacion { get; set; }
        public bool Visa { get; set; }
        public double Impuesto { get; set; }
        public int Cuotas { get; set; }
    }
}