using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SCAME.Models
{
    public class Discapacidad
    {
        [Key]
        [DisplayName("Id")]
        public int IdDiscapacidad { get; set; }
        [DisplayName("Nombre discapacidad")]
        public string NombreDiscapacidad { get; set; }
    }
}
