
using Microsoft.AspNetCore.Mvc;
using SistemaGestionEquipos.API.DTOs;
using SistemaGestionEquipos.Domain.Entities;
using SistemaGestionEquipos.Infrastructure.Repository;

namespace SistemaGestionEquipos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoRepository _empleadoRepository;

        public EmpleadosController(EmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            return await _empleadoRepository.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await _empleadoRepository.ObtenerPorIdAsync(id);

            if (empleado == null)
                return NotFound();

            return empleado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmpleadoDto dto)
        {
            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Departamento = dto.Departamento,
                Correo = dto.Correo
            };

            await _empleadoRepository.AgregarAsync(empleado);

            return CreatedAtAction(nameof(Get),
                new { id = empleado.Id }, empleado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmpleadoDto dto)
        {
            var empleado = await _empleadoRepository.ObtenerPorIdAsync(id);

            if (empleado == null)
                return NotFound();

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Departamento = dto.Departamento;
            empleado.Correo = dto.Correo;

            await _empleadoRepository.ActualizarAsync(empleado);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _empleadoRepository.ObtenerPorIdAsync(id);

            if (empleado == null)
                return NotFound();

            await _empleadoRepository.EliminarAsync(empleado);

            return NoContent();
        }
    }
}
