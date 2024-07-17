using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly VeranoContext _context;

        public TipoUsuarioController(VeranoContext context)
        {
            _context = context;
        }

        private bool TipoUsuarioExiste(int id)
        {
            return _context.tipousuario.Any(e => e.idTipoUsuario == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosTipoUsuarios()
        {
            var tipoUsuarios = await _context.tipousuario
                .Where(t => t.estatus == 1)
                .ToListAsync();
            return Ok(tipoUsuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<tipousuario>> GetTipoUsuarioPorId(int id)
        {
            var tipoUsuario = await _context.tipousuario.FindAsync(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return tipoUsuario;
        }

        [HttpPost("CreateTipoUsuario")]
        public async Task<ActionResult<tipousuario>> CreateTipoUsuario(string descripcion, byte estatus)
        {
            var nuevoTipoUsuario = new tipousuario
            {
                descripcion = descripcion,
                estatus = estatus
            };

            _context.tipousuario.Add(nuevoTipoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoUsuarioPorId), new { id = nuevoTipoUsuario.idTipoUsuario }, nuevoTipoUsuario);
        }

        [HttpPut("UpdateTipoUsuario/{id}")]
        public async Task<IActionResult> UpdateTipoUsuario(int id, string descripcion, byte estatus)
        {
            var tipoUsuario = await _context.tipousuario.FindAsync(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            tipoUsuario.descripcion = descripcion;
            tipoUsuario.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUsuarioExiste(id))
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

        [HttpDelete("DeleteTipoUsuario/{id}")]
        public async Task<IActionResult> DeleteTipoUsuario(int id)
        {
            var tipoUsuario = await _context.tipousuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            tipoUsuario.estatus = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUsuarioExiste(id))
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
