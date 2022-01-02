using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba.Models.FormsViewModel
{
    public class PaisViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nombre de Pais")]
        [StringLength(100,ErrorMessage ="cantidad invalida de caracteres",MinimumLength =3)]
        public string Descripcion { get; set; }
    }
}