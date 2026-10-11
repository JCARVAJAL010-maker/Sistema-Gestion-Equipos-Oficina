using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEquipos.API.Data;
using SistemaGestionEquipos.API.DTOs;
using SistemaGestionEquipos.API.Models;

namespace SistemaGestionEquipos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquiposController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> Get()
        {
            return await _context.Equipos
                .Include(e => e.Empleado)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipo>> Get(int id)
        {
            var equipo = await _context.Equipos
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipo == null)
                return NotFound();

            return equipo;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EquipoDto dto)
        {
            if (dto.EmpleadoId.HasValue &&
                !await _context.Empleados.AnyAsync(e => e.Id == dto.EmpleadoId))
                return BadRequest("El empleado no existe.");

            var equipo = new Equipo
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Estado = dto.Estado,
                EmpleadoId = dto.EmpleadoId
            };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get),
                new { id = equipo.Id }, equipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EquipoDto dto)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
                return NotFound();

            if (dto.EmpleadoId.HasValue &&
                !await _context.Empleados.AnyAsync(e => e.Id == dto.EmpleadoId))
                return BadRequest("El empleado no existe.");

            equipo.Nombre = dto.Nombre;
            equipo.Marca = dto.Marca;
            equipo.Modelo = dto.Modelo;
            equipo.Estado = dto.Estado;
            equipo.EmpleadoId = dto.EmpleadoId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
                return NotFound();

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}