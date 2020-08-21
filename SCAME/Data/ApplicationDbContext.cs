﻿using System;
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
        public DbSet<SCAME.Models.Paciente> Paciente { get; set; }
        public DbSet<SCAME.Models.Consultorio> Consultorio { get; set; }

        internal Task<IdentityResult> SaveChangesAsync(Consultorio consultorio)
        {
            throw new NotImplementedException();
        }

        public DbSet<SCAME.Models.Cita> Cita { get; set; }

        public DbSet<SCAME.Models.Medico> Medicos { get; set; }

        public DbSet<SCAME.Models.Especialidad> Especialidad { get; set; }

        public DbSet<SCAME.Models.Pais> Pais { get; set; }
        public DbSet<SCAME.Models.Provincia> Provincia { get; set; }
        public DbSet<SCAME.Models.Canton> Canton { get; set; }
        public DbSet<SCAME.Models.MedicoEspecialidad> ConsultorioDetalle { get; set; }

    }
}
