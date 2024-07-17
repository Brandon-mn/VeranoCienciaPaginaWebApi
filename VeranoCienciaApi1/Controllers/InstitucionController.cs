using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class InstitucionController : ControllerBase
    {
        private readonly VeranoContext _context;

        public InstitucionController(VeranoContext context)
        {
            _context = context;
        }

        private bool InstitucionExiste(int id)
        {
            return _context.Institucion.Any(e => e.idInstitucion == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasInstituciones()
        {
            var instituciones = await _context.Institucion
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(instituciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Institucion>> GetInstitucionPorId(int id)
        {
            var institucion = await _context.Institucion.FindAsync(id);

            if (institucion == null)
            {
                return NotFound();
            }

            return institucion;
        }

        [HttpPost("CreateInstitucion")]
        public async Task<ActionResult<Institucion>> CreateInstitucion(string nombre, string abreviatura, int idEstado, byte estatus)
        {
            var nuevaInstitucion = new Institucion
            {
                nombre = nombre,
                abreviatura = abreviatura,
                idEstado = idEstado,
                estatus = (byte)estatus
            };

            _context.Institucion.Add(nuevaInstitucion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInstitucionPorId), new { id = nuevaInstitucion.idInstitucion }, nuevaInstitucion);
        }

        [HttpPut("UpdateInstitucion/{id}")]
        public async Task<IActionResult> UpdateInstitucion(int id, string nombre, string abreviatura, int idEstado, byte estatus)
        {
            var institucion = await _context.Institucion.FindAsync(id);

            if (institucion == null)
            {
                return NotFound();
            }

            institucion.nombre = nombre;
            institucion.abreviatura = abreviatura;
            institucion.idEstado = idEstado;
            institucion.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitucionExiste(id))
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

        [HttpDelete("DeleteInstitucion/{id}")]
        public async Task<IActionResult> DeleteInstitucion(int id)
        {
            var institucion = await _context.Institucion.FindAsync(id);
            if (institucion == null)
            {
                return NotFound();
            }

            institucion.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitucionExiste(id))
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

