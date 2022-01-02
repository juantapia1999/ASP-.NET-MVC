using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba.Models.FormsViewModel
{
    public class RazonSocialViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(50, ErrorMessage = "cantidad invalida de caracteres", MinimumLength = 3)]
        public string Descripcion { get; set; }
    }
}