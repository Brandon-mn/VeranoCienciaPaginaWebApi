using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : Controller
    {
        
        private readonly VeranoContext _context;
        public AreaController(VeranoContext context)
        {
            _context = context;
        }
        // GET: api/Alumno
        [HttpGet]
        public async Task<IActionResult> GetTodasAreas()
        {
            var alumnos = await _context.Areas
                .Where(a => a.estatus == 1)  // Asegúrate de que la propiedad 'Estatus' existe y es correcta
                .ToListAsync();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetAreaPorId(int id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        [HttpPost("CreateArea")]
        public async Task<ActionResult<Area>> CreateAlumno(string descripcion, byte estatus)
        {
            int estadostatus = estatus;

            var nuevoCliente = new Area
            {
                descripcion = descripcion,
                estatus = (byte)estadostatus
            };

            _context.Areas.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAreaPorId), new { id = nuevoCliente.idarea }, nuevoCliente);
        }

        [HttpPut("UpdateArea/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, string descripcion, byte estatus)
        {
            var cliente = await _context.Areas.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.descripcion = descripcion;
         

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(id))
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
        [HttpDelete("DeleteArea/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Areas.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(id))
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
        private bool ClienteExiste(int id)
        {
            return _context.Areas.Any(e => e.idarea == id);
        }

    }
}
