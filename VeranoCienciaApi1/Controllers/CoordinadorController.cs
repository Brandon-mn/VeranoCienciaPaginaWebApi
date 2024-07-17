using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinadorController : ControllerBase
    {
        private readonly VeranoContext _context;

        public CoordinadorController(VeranoContext context)
        {
            _context = context;
        }

        private bool CoordinadorExiste(int id)
        {
            return _context.Coordinador.Any(e => e.idCoordinador == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosCoordinadores()
        {
            var coordinadores = await _context.Coordinador
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(coordinadores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coordinador>> GetCoordinadorPorId(int id)
        {
            var coordinador = await _context.Coordinador.FindAsync(id);

            if (coordinador == null)
            {
                return NotFound();
            }

            return coordinador;
        }

        [HttpPost("CreateCoordinador")]
        public async Task<ActionResult<Coordinador>> CreateCoordinador(string puesto, int esCoordinadorInstitucional, int idUsuario, DateTime fechaCreacion, DateTime fechaModifica, byte estatus)
        {
            var nuevoCoordinador = new Coordinador
            {
                puesto = puesto,
                esCoordinadorInstitucional = esCoordinadorInstitucional,
                idUsuario = idUsuario,
                fechaCreacion = fechaCreacion,
                fechaModifica = fechaModifica,
                estatus = estatus
            };

            _context.Coordinador.Add(nuevoCoordinador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCoordinadorPorId), new { id = nuevoCoordinador.idCoordinador }, nuevoCoordinador);
        }

        [HttpPut("UpdateCoordinador/{id}")]
        public async Task<IActionResult> UpdateCoordinador(int id, string puesto, int esCoordinadorInstitucional, int idUsuario, DateTime? fechaCreacion, DateTime? fechaModifica, byte estatus)
        {
            var coordinador = await _context.Coordinador.FindAsync(id);

            if (coordinador == null)
            {
                return NotFound();
            }

            coordinador.puesto = puesto;
            coordinador.esCoordinadorInstitucional = esCoordinadorInstitucional;
            idUsuario = idUsuario;
                fechaCreacion = fechaCreacion;
                fechaModifica = fechaModifica;
            coordinador.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoordinadorExiste(id))
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

        [HttpDelete("DeleteCoordinador/{id}")]
        public async Task<IActionResult> DeleteCoordinador(int id)
        {
            var coordinador = await _context.Coordinador.FindAsync(id);
            if (coordinador == null)
            {
                return NotFound();
            }

            coordinador.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoordinadorExiste(id))
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

