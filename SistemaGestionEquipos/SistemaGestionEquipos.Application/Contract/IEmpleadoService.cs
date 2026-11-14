
using SistemaGestionEquipos.Domain.Entities;

namespace SistemaGestionEquipos.Application.Contract
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> ObtenerTodosAsync();

        Task<Empleado?> ObtenerPorIdAsync(int id);

        Task<Empleado> AgregarAsync(Empleado empleado);

        Task<bool> ActualizarAsync(int id, Empleado empleado);

        Task<bool> EliminarAsync(int id);
    }
}
