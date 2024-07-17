using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly VeranoContext _context;

        public DocumentoController(VeranoContext context)
        {
            _context = context;
        }

        private bool DocumentoExiste(int id)
        {
            return _context.Documento.Any(e => e.idDocumento == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosDocumentos()
        {
            var documentos = await _context.Documento
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(documentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> GetDocumentoPorId(int id)
        {
            var documento = await _context.Documento.FindAsync(id);

            if (documento == null)
            {
                return NotFound();
            }

            return documento;
        }

        [HttpPost("CreateDocumento")]
        public async Task<ActionResult<Documento>> CreateDocumento(string descripcion, string prefijo, byte estatus)
        {
            var nuevoDocumento = new Documento
            {
                descripcion = descripcion,
                prefijo = prefijo,
                estatus = estatus
            };

            _context.Documento.Add(nuevoDocumento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumentoPorId), new { id = nuevoDocumento.idDocumento }, nuevoDocumento);
        }

        [HttpPut("UpdateDocumento/{id}")]
        public async Task<IActionResult> UpdateDocumento(int id, string descripcion, string prefijo, byte estatus)
        {
            var documento = await _context.Documento.FindAsync(id);

            if (documento == null)
            {
                return NotFound();
            }

            documento.descripcion = descripcion;
            documento.prefijo = prefijo;
            documento.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExiste(id))
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

        [HttpDelete("DeleteDocumento/{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var documento = await _context.Documento.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }

            documento.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExiste(id))
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
