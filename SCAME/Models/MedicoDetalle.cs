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
        [Display(Name = "Médico")]
        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
        [Display(Name = "Especilidad")]
        public int? EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        [Display(Name = "Precio de la consulta")]
        public double? PrecioEspecialidad { get; set; }
        [Display(Name = "Descripción")]
        public string? DescripcionEspecialidad { get; set; }
        [Display(Name = "Turno")]
        public int? TurnoId { get; set; }
        public Turno Turno { get; set; }
        [Display(Name = "Consultorio")]
        public int? ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
        public bool Estado { get; set; }

    }
}
