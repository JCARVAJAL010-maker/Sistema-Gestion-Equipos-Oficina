using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEquipos.API.Data;
using SistemaGestionEquipos.API.DTOs;
using SistemaGestionEquipos.API.Models;

namespace SistemaGestionEquipos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            return await _context.Empleados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

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

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = empleado.Id }, empleado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmpleadoDto dto)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return NotFound();

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Departamento = dto.Departamento;
            empleado.Correo = dto.Correo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return NotFound();

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}