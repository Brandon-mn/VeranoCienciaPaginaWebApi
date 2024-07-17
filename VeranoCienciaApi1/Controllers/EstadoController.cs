using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly VeranoContext _context;

        public EstadoController(VeranoContext context)
        {
            _context = context;
        }

        private bool EstadoExiste(int id)
        {
            return _context.Estado.Any(e => e.idEstado == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosEstados()
        {
            var estados = await _context.Estado
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstadoPorId(int id)
        {
            var estado = await _context.Estado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        [HttpPost("CreateEstado")]
        public async Task<ActionResult<Estado>> CreateEstado(string nombre, byte estatus)
        {
            var nuevoEstado = new Estado
            {
                nombre = nombre,
                estatus = estatus
            };

            _context.Estado.Add(nuevoEstado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstadoPorId), new { id = nuevoEstado.idEstado }, nuevoEstado);
        }

        [HttpPut("UpdateEstado/{id}")]
        public async Task<IActionResult> UpdateEstado(int id, string nombre, byte estatus)
        {
            var estado = await _context.Estado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            estado.nombre = nombre;
            estado.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("DeleteEstado/{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            estado.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
