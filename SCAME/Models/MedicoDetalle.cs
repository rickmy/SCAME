using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class MedicoDetalle
    {
        [Key]
        public int IdMedicoDetalle { get; set; }
        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int? EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public double? PrecioEspecialidad { get; set; }
        public string? DescripcionEspecialidad { get; set; }
        public int? TurnoId { get; set; }
        public Turno Turno { get; set; }
        public int? ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
        public bool Estado { get; set; }

    }
}
