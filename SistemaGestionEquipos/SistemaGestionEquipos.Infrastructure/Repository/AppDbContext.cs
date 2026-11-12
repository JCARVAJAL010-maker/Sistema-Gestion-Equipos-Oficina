
using Microsoft.EntityFrameworkCore;
using SistemaGestionEquipos.Domain.Entities;

namespace SistemaGestionEquipos.Infrastructure.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
    }
}

