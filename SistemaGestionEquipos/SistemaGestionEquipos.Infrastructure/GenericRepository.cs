
using Microsoft.EntityFrameworkCore;

namespace SistemaGestionEquipos.Infrastructure.Repository
{
    public class GenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> ObtenerTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AgregarAsync(T entidad)
        {
            await _context.Set<T>().AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(T entidad)
        {
            _context.Set<T>().Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(T entidad)
        {
            _context.Set<T>().Remove(entidad);
            await _context.SaveChangesAsync();
        }
    }
}
