using Microsoft.EntityFrameworkCore;
using SistemaGestionEquipos.API.Models;

namespace SistemaGestionEquipos.API.Data
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