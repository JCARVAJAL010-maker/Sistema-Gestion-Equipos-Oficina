
using SistemaGestionEquipos.Application.Contract;
using SistemaGestionEquipos.Domain.Entities;
using SistemaGestionEquipos.Infrastructure.Repository;

namespace SistemaGestionEquipos.Application.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly EquipoRepository _equipoRepository;
        private readonly EmpleadoRepository _empleadoRepository;

        public EquipoService(
            EquipoRepository equipoRepository,
            EmpleadoRepository empleadoRepository)
        {
            _equipoRepository = equipoRepository;
            _empleadoRepository = empleadoRepository;
        }

        public async Task<List<Equipo>> ObtenerTodosAsync()
        {
            return await _equipoRepository.ObtenerConEmpleadosAsync();
        }

        public async Task<Equipo?> ObtenerPorIdAsync(int id)
        {
            return await _equipoRepository.ObtenerConEmpleadoPorIdAsync(id);
        }

        public async Task<Equipo> AgregarAsync(Equipo equipo)
        {
            ValidarEquipo(equipo);
            await ValidarEmpleadoAsignadoAsync(equipo.EmpleadoId);

            await _equipoRepository.AgregarAsync(equipo);

            return equipo;
        }

        public async Task<bool> ActualizarAsync(int id, Equipo equipo)
        {
            var equipoExistente =
                await _equipoRepository.ObtenerPorIdAsync(id);

            if (equipoExistente == null)
                return false;

            ValidarEquipo(equipo);
            await ValidarEmpleadoAsignadoAsync(equipo.EmpleadoId);

            equipoExistente.Nombre = equipo.Nombre.Trim();
            equipoExistente.Marca = equipo.Marca.Trim();
            equipoExistente.Modelo = equipo.Modelo.Trim();
            equipoExistente.Estado = equipo.Estado.Trim();
            equipoExistente.EmpleadoId = equipo.EmpleadoId;

            await _equipoRepository.ActualizarAsync(equipoExistente);

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var equipo =
                await _equipoRepository.ObtenerPorIdAsync(id);

            if (equipo == null)
                return false;

            await _equipoRepository.EliminarAsync(equipo);

            return true;
        }

        private void ValidarEquipo(Equipo equipo)
        {
            if (string.IsNullOrWhiteSpace(equipo.Nombre))
                throw new ArgumentException(
                    "El nombre del equipo es obligatorio.");

            if (equipo.Nombre.Length > 100)
                throw new ArgumentException(
                    "El nombre no puede superar 100 caracteres.");

            if (string.IsNullOrWhiteSpace(equipo.Marca))
                throw new ArgumentException(
                    "La marca del equipo es obligatoria.");

            if (equipo.Marca.Length > 50)
                throw new ArgumentException(
                    "La marca no puede superar 50 caracteres.");

            if (string.IsNullOrWhiteSpace(equipo.Modelo))
                throw new ArgumentException(
                    "El modelo del equipo es obligatorio.");

            if (equipo.Modelo.Length > 100)
                throw new ArgumentException(
                    "El modelo no puede superar 100 caracteres.");

            if (string.IsNullOrWhiteSpace(equipo.Estado))
                throw new ArgumentException(
                    "El estado del equipo es obligatorio.");

            if (equipo.Estado.Length > 50)
                throw new ArgumentException(
                    "El estado no puede superar 50 caracteres.");
        }

        private async Task ValidarEmpleadoAsignadoAsync(int? empleadoId)
        {
            if (!empleadoId.HasValue)
                return;

            var empleado = await _empleadoRepository
                .ObtenerPorIdAsync(empleadoId.Value);

            if (empleado == null)
                throw new ArgumentException(
                    "El empleado asignado no existe.");
        }
    }
}
