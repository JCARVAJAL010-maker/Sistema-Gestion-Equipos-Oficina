

using Microsoft.EntityFrameworkCore;
using SistemaGestionEquipos.Domain.Entities;

namespace SistemaGestionEquipos.Infrastructure.Repository
{
    public class EquipoRepository : GenericRepository<Equipo>
    {
        public EquipoRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<Equipo>> ObtenerConEmpleadosAsync()
        {
            return await _context.Equipos
                .Include(e => e.Empleado)
                .ToListAsync();
        }

        public async Task<Equipo?> ObtenerConEmpleadoPorIdAsync(int id)
        {
            return await _context.Equipos
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
