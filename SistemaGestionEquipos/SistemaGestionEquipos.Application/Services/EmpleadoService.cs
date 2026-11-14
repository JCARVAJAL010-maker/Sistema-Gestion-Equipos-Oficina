
using System.Net.Mail;
using SistemaGestionEquipos.Application.Contract;
using SistemaGestionEquipos.Domain.Entities;
using SistemaGestionEquipos.Infrastructure.Repository;

namespace SistemaGestionEquipos.Application.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly EmpleadoRepository _empleadoRepository;

        public EmpleadoService(EmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<List<Empleado>> ObtenerTodosAsync()
        {
            return await _empleadoRepository.ObtenerTodosAsync();
        }

        public async Task<Empleado?> ObtenerPorIdAsync(int id)
        {
            return await _empleadoRepository.ObtenerPorIdAsync(id);
        }

        public async Task<Empleado> AgregarAsync(Empleado empleado)
        {
            ValidarEmpleado(empleado);

            await _empleadoRepository.AgregarAsync(empleado);

            return empleado;
        }

        public async Task<bool> ActualizarAsync(int id, Empleado empleado)
        {
            var empleadoExistente =
                await _empleadoRepository.ObtenerPorIdAsync(id);

            if (empleadoExistente == null)
                return false;

            ValidarEmpleado(empleado);

            empleadoExistente.Nombre = empleado.Nombre.Trim();
            empleadoExistente.Apellido = empleado.Apellido.Trim();
            empleadoExistente.Departamento = empleado.Departamento.Trim();
            empleadoExistente.Correo = empleado.Correo.Trim();

            await _empleadoRepository.ActualizarAsync(empleadoExistente);

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var empleado =
                await _empleadoRepository.ObtenerPorIdAsync(id);

            if (empleado == null)
                return false;

            await _empleadoRepository.EliminarAsync(empleado);

            return true;
        }

        private void ValidarEmpleado(Empleado empleado)
        {
            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                throw new ArgumentException(
                    "El nombre del empleado es obligatorio.");

            if (empleado.Nombre.Length > 100)
                throw new ArgumentException(
                    "El nombre no puede superar 100 caracteres.");

            if (string.IsNullOrWhiteSpace(empleado.Apellido))
                throw new ArgumentException(
                    "El apellido del empleado es obligatorio.");

            if (empleado.Apellido.Length > 100)
                throw new ArgumentException(
                    "El apellido no puede superar 100 caracteres.");

            if (string.IsNullOrWhiteSpace(empleado.Departamento))
                throw new ArgumentException(
                    "El departamento es obligatorio.");

            if (empleado.Departamento.Length > 100)
                throw new ArgumentException(
                    "El departamento no puede superar 100 caracteres.");

            if (string.IsNullOrWhiteSpace(empleado.Correo))
                throw new ArgumentException(
                    "El correo electrónico es obligatorio.");

            if (empleado.Correo.Length > 254)
                throw new ArgumentException(
                    "El correo electrónico es demasiado largo.");

            if (!MailAddress.TryCreate(
                empleado.Correo.Trim(), out var correo) ||
                correo.Address != empleado.Correo.Trim() ||
                !correo.Host.Contains('.'))
            {
                throw new ArgumentException(
                    "El correo electrónico no tiene un formato válido.");
            }
        }
    }
}
