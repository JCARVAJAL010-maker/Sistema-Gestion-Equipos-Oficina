

using Microsoft.AspNetCore.Mvc;
using SistemaGestionEquipos.Application.Contract;
using SistemaGestionEquipos.Domain.Entities;
using SistemaGestionEquipos.Application.Dtos;

namespace SistemaGestionEquipos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            return await _empleadoService.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await _empleadoService.ObtenerPorIdAsync(id);

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

            try
            {
                await _empleadoService.AgregarAsync(empleado);

                return CreatedAtAction(nameof(Get),
                    new { id = empleado.Id }, empleado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmpleadoDto dto)
        {
            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Departamento = dto.Departamento,
                Correo = dto.Correo
            };

            try
            {
                var actualizado =
                    await _empleadoService.ActualizarAsync(id, empleado);

                if (!actualizado)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _empleadoService.EliminarAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
