//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prueba.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura
    {
        public int Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public bool Activo { get; set; }
        public int ClienteId { get; set; }
        public int PaqueteId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Paquete Paquete { get; set; }
    }
}
