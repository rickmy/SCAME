using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaCita { get; set; }
        public string MedicoId { get; set; }
        public Medico Medico { get; set; }
        public string MotivoCita { get; set; }
        public string MotivoAnulacion { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public string ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
        public int HorasAtencionId { get; set; }
        public HorasAtencion HorasAtencion { get; set; }
        public string EstadoCita { get; set; }
        public bool  Estado { get; set; }
    }
}
