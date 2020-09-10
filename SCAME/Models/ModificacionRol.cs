using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class ModificacionRol
    {
        [Display(Name = "Nombre del Rol")]
        public string RolName { get; set; }
        [Display(Name = "Id del Rol")]
        public string RolId { get; set; }
        [Display(Name = "Aumentar")]
        public string[] AumentarIds { get; set; }
        [Display(Name = "Quitar")]
        public string[] QuitarIds { get; set; }

    }
}