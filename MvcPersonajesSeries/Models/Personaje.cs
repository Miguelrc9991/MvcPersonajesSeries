using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPersonajesSeries.Models
{
    public class Personaje
    {
        public int IdPersonaje { get; set; }
        public string NombrePersonaje { get; set; }
        public string Imagen { get; set; }
        public int IdSerie { get; set; }
    }
}
