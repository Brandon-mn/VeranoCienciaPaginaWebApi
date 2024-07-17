using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        private readonly VeranoContext _context;

        public ModuloController(VeranoContext context)
        {
            _context = context;
        }

        private bool ModuloExiste(int id)
        {
            return _context.Modulo.Any(e => e.idmodulo == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosModulos()
        {
            var modulos = await _context.Modulo
                .Where(m => m.estatus == 1)
                .ToListAsync();
            return Ok(modulos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Modulo>> GetModuloPorId(int id)
        {
            var modulo = await _context.Modulo.FindAsync(id);

            if (modulo == null)
            {
                return NotFound();
            }

            return modulo;
        }

        [HttpPost("CreateModulo")]
        public async Task<ActionResult<Modulo>> CreateModulo(
            string nombre,
            string descripcion,
            string ruta,
            byte estatus)
        {
            var nuevoModulo = new Modulo
            {
                nombre = nombre,
                descripcion = descripcion,
                ruta = ruta,
                estatus = estatus
            };

            _context.Modulo.Add(nuevoModulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModuloPorId), new { id = nuevoModulo.idmodulo }, nuevoModulo);
        }

        [HttpPut("UpdateModulo/{id}")]
        public async Task<IActionResult> UpdateModulo( int id,string nombre,string descripcion,
            string ruta,
            byte estatus)
        {
            var modulo = await _context.Modulo.FindAsync(id);

            if (modulo == null)
            {
                return NotFound();
            }

            modulo.nombre = nombre;
            modulo.descripcion = descripcion;
            modulo.ruta = ruta;
            modulo.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloExiste(id))
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

        [HttpDelete("DeleteModulo/{id}")]
        public async Task<IActionResult> DeleteModulo(int id)
        {
            var modulo = await _context.Modulo.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            modulo.estatus = 0; 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloExiste(id))
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
