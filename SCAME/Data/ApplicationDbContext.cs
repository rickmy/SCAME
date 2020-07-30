using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
