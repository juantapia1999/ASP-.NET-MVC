using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba.Models.FormsViewModel
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }
        [Required]
        public double Cuit { get; set; }
        [Required]
        public int RazonSocialId { get; set; }
        [Required]
        [Display(Name = "Nombre de Empresa")]
        [StringLength(30, ErrorMessage = "cantidad invalida de caracteres", MinimumLength = 3)]
        public string Descripcion { get; set; }
    }
}