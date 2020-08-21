using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class MedicoEspecialidad
    {
        [Key]
        public int IdMedicoEspecialidad { get; set; }
        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int? EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public double? PrecioEspecialidad { get; set; }
        public string? DescripcionEspecialidad { get; set; }
        public bool Estado { get; set; }

    }
}
