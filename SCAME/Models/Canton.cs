using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Canton
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre del cantón")]
        public string NombreCanton { get; set; }
        [Display(Name = "Provincia")]
        public int ProvinciaId { get; set; }        
        public Provincia Provincia { get; set; }
        public bool Estado { get; set; } = true;
    }
}