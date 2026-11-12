
using SistemaGestionEquipos.Domain.Entities;

namespace SistemaGestionEquipos.Infrastructure.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>
    {
        public EmpleadoRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
