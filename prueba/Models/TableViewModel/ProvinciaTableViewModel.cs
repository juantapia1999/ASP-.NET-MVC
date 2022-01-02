using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prueba.Models.TableViewModel;

namespace prueba.Models.TableViewModel
{
    public class ProvinciaTableViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public int IdPais { get; set; }
        public string DescripcionPais { get; set; }
    }
}