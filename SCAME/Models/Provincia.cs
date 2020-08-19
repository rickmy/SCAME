using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Provincia
    {
        [Key]
        public int Id { get; set; }
        public string NombreProvincia { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        public bool Estado { get; set; } = true;
        List<Canton> Catones { get; set; } = new List<Canton>();

        public void AñadirCanton(Canton canton)
        {
            this.Catones.Add(canton);
        }
    }
}