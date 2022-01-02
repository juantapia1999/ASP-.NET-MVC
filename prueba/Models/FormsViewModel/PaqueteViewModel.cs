using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba.Models.FormsViewModel
{
    public class PaqueteViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de Paquete")]
        [StringLength(50, ErrorMessage = "cantidad invalida de caracteres", MinimumLength = 3)]
        public string Descripcion { get; set; }
        [Required]
        public string Precio { get; set; }
        [Required]
        public int CantDias { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        //public DateTime FechaViaje { get; set; }
        public string FechaViaje { get; set; }
        [Required]
        public int Estado { get; set; }
        [Required]
        public int Lugares { get; set; }
        public string DescripcionPais { get; set; }
        public int IdPais { get; set; }
        public string DescripcionProvincia { get; set; }
        public int IdProvincia { get; set; }
        [Required]
        public int Nacional { get; set; }
        [Required]
        public string Cotizacion { get; set; }
        [Required]
        public int Visa { get; set; }
        [Required]
        public string Impuesto { get; set; }
        [Required]
        public int Cuotas { get; set; }
    }
}