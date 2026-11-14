
using SistemaGestionEquipos.Domain.Entities;

namespace SistemaGestionEquipos.Application.Contract
{
    public interface IEquipoService
    {
        Task<List<Equipo>> ObtenerTodosAsync();

        Task<Equipo?> ObtenerPorIdAsync(int id);

        Task<Equipo> AgregarAsync(Equipo equipo);

        Task<bool> ActualizarAsync(int id, Equipo equipo);

        Task<bool> EliminarAsync(int id);
    }
}
