using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre de la especialidad")]
        public string NombreEspecialidad { get; set; }
        public List<MedicoDetalle> MedicoDetalles { get; set; }
        //[NotMapped]
        //public MedicoDetalle MedicoDetalle { get; set; }
        public bool Estado { get; set; } = true;
    }
}
