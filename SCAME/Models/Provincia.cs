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
    }
}