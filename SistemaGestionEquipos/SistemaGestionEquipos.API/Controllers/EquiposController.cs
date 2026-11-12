
using Microsoft.AspNetCore.Mvc;
using SistemaGestionEquipos.API.DTOs;
using SistemaGestionEquipos.Domain.Entities;
using SistemaGestionEquipos.Infrastructure.Repository;

namespace SistemaGestionEquipos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly EquipoRepository _equipoRepository;
        private readonly EmpleadoRepository _empleadoRepository;

        public EquiposController(
            EquipoRepository equipoRepository,
            EmpleadoRepository empleadoRepository)
        {
            _equipoRepository = equipoRepository;
            _empleadoRepository = empleadoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> Get()
        {
            return await _equipoRepository.ObtenerConEmpleadosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipo>> Get(int id)
        {
            var equipo = await _equipoRepository.ObtenerConEmpleadoPorIdAsync(id);

            if (equipo == null)
                return NotFound();

            return equipo;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EquipoDto dto)
        {
            if (dto.EmpleadoId.HasValue)
            {
                var empleado = await _empleadoRepository
                    .ObtenerPorIdAsync(dto.EmpleadoId.Value);

                if (empleado == null)
                    return BadRequest("El empleado no existe.");
            }

            var equipo = new Equipo
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Estado = dto.Estado,
                EmpleadoId = dto.EmpleadoId
            };

            await _equipoRepository.AgregarAsync(equipo);

            return CreatedAtAction(nameof(Get),
                new { id = equipo.Id }, equipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EquipoDto dto)
        {
            var equipo = await _equipoRepository.ObtenerPorIdAsync(id);

            if (equipo == null)
                return NotFound();

            if (dto.EmpleadoId.HasValue)
            {
                var empleado = await _empleadoRepository
                    .ObtenerPorIdAsync(dto.EmpleadoId.Value);

                if (empleado == null)
                    return BadRequest("El empleado no existe.");
            }

            equipo.Nombre = dto.Nombre;
            equipo.Marca = dto.Marca;
            equipo.Modelo = dto.Modelo;
            equipo.Estado = dto.Estado;
            equipo.EmpleadoId = dto.EmpleadoId;

            await _equipoRepository.ActualizarAsync(equipo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipo = await _equipoRepository.ObtenerPorIdAsync(id);

            if (equipo == null)
                return NotFound();

            await _equipoRepository.EliminarAsync(equipo);

            return NoContent();
        }
    }
}
