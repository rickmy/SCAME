using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    #region Cita
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        [Display(Name = "Fecha de la cita")]
        public DateTime FechaCita { get; set; }
        [Display(Name = "Médico")]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        [Display(Name = "Motivo de la cita")]
        public string MotivoCita { get; set; }
        [Display(Name = "Motivo de la anulación")]
        public string MotivoAnulacion { get; set; }
        [Display(Name = "Especialidad")]
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        [Display(Name = "Consultorio")]
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
        [Display(Name = "Hora de atención")]
        public int HorasAtencionId { get; set; }
        public HorasAtencion HorasAtencion { get; set; }
        [Display(Name = "Estado de la cita")]
        public string EstadoCita { get; set; }
        public bool Estado { get; set; }
    }
    #endregion
    #region HomeCalendario
    public class HomeCalendario
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Start { get; set; }
        public bool AllDay { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
    } 
    #endregion
}
