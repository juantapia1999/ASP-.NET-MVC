using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba.Models.FormsViewModel
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        [StringLength(50, ErrorMessage = "cantidad invalida de caracteres", MinimumLength = 4)]
        public string Direccion { get; set; }       
        public double Telefono { get; set; }
        [Required]
        public double Dni { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "cantidad invalida de caracteres", MinimumLength = 3)]
        public string Apellido { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "cantidad invalida de caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        [Required]
        public int IdPais { get; set; }
        public string DescripcionPais { get; set; }
        public int? IdEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
    }
}