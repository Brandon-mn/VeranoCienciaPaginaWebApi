using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : ControllerBase
    {
        private readonly VeranoContext _context;
        public CampusController(VeranoContext context)
        {
            _context = context;
        }
        private bool ClienteExiste(int id)
        {
            return _context.Campus.Any(e => e.idCampus == id);
        }
        [HttpGet]
        public async Task<IActionResult> GetTodosCampus()
        {
            var alumnos = await _context.Campus
                .Where(a => a.estatus == 1)  // Asegúrate de que la propiedad 'Estatus' existe y es correcta
                .ToListAsync();
            return Ok(alumnos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Campus>> GetAreaPorId(int id)
        {
            var area = await _context.Campus.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }
        [HttpPost("CreateCampus")]
        public async Task<ActionResult<Campus>> CreateAlumno(string nombre, string abreviaturaCampus, int idInstitucion, int idMunicipio, byte estatus)
        {
            int estadostatus = estatus;

            var nuevoCliente = new Campus
            {
                nombre = nombre,
                abreviaturaCampus = abreviaturaCampus,
                idInstitucion = idInstitucion,
                idMunicipio = idMunicipio,
                estatus = (byte)estadostatus
            };

            _context.Campus.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAreaPorId), new { id = nuevoCliente.idCampus }, nuevoCliente);
        }
        [HttpPut("UpdateCampus/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, string nombre, string abreviaturaCampus, int idInstitucion, int idMunicipio, byte estatus)
        {
            var cliente = await _context.Campus.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.nombre = nombre;
            cliente.abreviaturaCampus = abreviaturaCampus;
            cliente.idInstitucion = idInstitucion;
            cliente.idMunicipio = idMunicipio;


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
        [HttpDelete("DeleteCampus/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Campus.FindAsync(id);
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
    }
}
