using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        public string NombrePais { get; set; }
        public bool Estado { get; set; } = true;
        List<Provincia> Provincias { get; set; } = new List<Provincia>();


        public void AñadirProvincia(Provincia provincia)
        {
            this.Provincias.Add(provincia);
        }

        
    }


}