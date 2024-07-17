using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioModuloController : ControllerBase
    {
        private readonly VeranoContext _context;

        public TipoUsuarioModuloController(VeranoContext context)
        {
            _context = context;
        }

        private bool TipoUsuarioModuloExiste(int id)
        {
            return _context.Tipousuariomodulo.Any(e => e.idtipousuariomodulo == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosTipoUsuarioModulos()
        {
            var tipoUsuarioModulos = await _context.Tipousuariomodulo
                .Where(t => t.estatus == 1)
                .ToListAsync();
            return Ok(tipoUsuarioModulos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tipousuariomodulo>> GetTipoUsuarioModuloPorId(int id)
        {
            var tipoUsuarioModulo = await _context.Tipousuariomodulo.FindAsync(id);

            if (tipoUsuarioModulo == null)
            {
                return NotFound();
            }

            return tipoUsuarioModulo;
        }

        [HttpPost("CreateTipoUsuarioModulo")]
        public async Task<ActionResult<Tipousuariomodulo>> CreateTipoUsuarioModulo(int idtipousuario, int idmodulo, int estatus)
        {
            var nuevoTipoUsuarioModulo = new Tipousuariomodulo
            {
                idtipousuario = idtipousuario,
                idmodulo = idmodulo,
                estatus = estatus
            };

            _context.Tipousuariomodulo.Add(nuevoTipoUsuarioModulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoUsuarioModuloPorId), new { id = nuevoTipoUsuarioModulo.idtipousuariomodulo }, nuevoTipoUsuarioModulo);
        }

        [HttpPut("UpdateTipoUsuarioModulo/{id}")]
        public async Task<IActionResult> UpdateTipoUsuarioModulo(int id, int idtipousuario, int idmodulo, int estatus)
        {
            var tipoUsuarioModulo = await _context.Tipousuariomodulo.FindAsync(id);

            if (tipoUsuarioModulo == null)
            {
                return NotFound();
            }

            tipoUsuarioModulo.idtipousuario = idtipousuario;
            tipoUsuarioModulo.idmodulo = idmodulo;
            tipoUsuarioModulo.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUsuarioModuloExiste(id))
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

        [HttpDelete("DeleteTipoUsuarioModulo/{id}")]
        public async Task<IActionResult> DeleteTipoUsuarioModulo(int id)
        {
            var tipoUsuarioModulo = await _context.Tipousuariomodulo.FindAsync(id);
            if (tipoUsuarioModulo == null)
            {
                return NotFound();
            }

            tipoUsuarioModulo.estatus = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUsuarioModuloExiste(id))
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
