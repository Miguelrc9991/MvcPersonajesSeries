using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPersonajesSeries.Models
{
    public class Serie
    {
        public int IdSerie { get; set; }
        public string NombreSerie { get; set; }
        public string Imagen { get; set; }
        public double Puntuacion { get; set; }
        public int Año { get; set; }
    }
}
