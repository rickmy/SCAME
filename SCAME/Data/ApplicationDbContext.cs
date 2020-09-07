using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCAME.Models;

namespace SCAME.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Consultorio> Consultorio { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Canton> Canton { get; set; }
        public DbSet<MedicoDetalle> MedicoDetalle { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<Sexo> Sexos { get; set; }

        internal Task<IdentityResult> SaveChangesAsync(Consultorio consultorio)
        {
            throw new NotImplementedException();
        }

        public DbSet<SCAME.Models.Paciente> Paciente { get; set; }

        public DbSet<SCAME.Models.HorasAtencion> HorasAtencion { get; set; }
    }
}
