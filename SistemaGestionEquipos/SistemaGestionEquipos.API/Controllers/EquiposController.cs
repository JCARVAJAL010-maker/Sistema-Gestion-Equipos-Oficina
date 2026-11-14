

using Microsoft.AspNetCore.Mvc;
using SistemaGestionEquipos.Application.Contract;
using SistemaGestionEquipos.Application.Dtos;
using SistemaGestionEquipos.Domain.Entities;

namespace SistemaGestionEquipos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly IEquipoService _equipoService;

        public EquiposController(IEquipoService equipoService)
        {
            _equipoService = equipoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> Get()
        {
            return await _equipoService.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipo>> Get(int id)
        {
            var equipo = await _equipoService.ObtenerPorIdAsync(id);

            if (equipo == null)
                return NotFound();

            return equipo;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EquipoDto dto)
        {
            var equipo = new Equipo
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Estado = dto.Estado,
                EmpleadoId = dto.EmpleadoId
            };

            try
            {
                await _equipoService.AgregarAsync(equipo);

                return CreatedAtAction(
                    nameof(Get),
                    new { id = equipo.Id },
                    equipo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EquipoDto dto)
        {
            var equipo = new Equipo
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Estado = dto.Estado,
                EmpleadoId = dto.EmpleadoId
            };

            try
            {
                var actualizado =
                    await _equipoService.ActualizarAsync(id, equipo);

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
            var eliminado = await _equipoService.EliminarAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
