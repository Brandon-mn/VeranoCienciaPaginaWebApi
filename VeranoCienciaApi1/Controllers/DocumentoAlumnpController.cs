using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoAlumnoController : ControllerBase
    {
        private readonly VeranoContext _context;

        public DocumentoAlumnoController(VeranoContext context)
        {
            _context = context;
        }

        private bool DocumentoAlumnoExiste(int id)
        {
            return _context.DocumentoAlumno.Any(e => e.idDocumentoAlumno == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosDocumentoAlumnos()
        {
            var documentoAlumnos = await _context.DocumentoAlumno
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(documentoAlumnos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentoAlumno>> GetDocumentoAlumnoPorId(int id)
        {
            var documentoAlumno = await _context.DocumentoAlumno.FindAsync(id);

            if (documentoAlumno == null)
            {
                return NotFound();
            }

            return documentoAlumno;
        }

        [HttpPost("CreateDocumentoAlumno")]
        public async Task<ActionResult<DocumentoAlumno>> CreateDocumentoAlumno(int idDocumento, int idAlumno, DateTime fechaCreacion, DateTime? fechaModifica, byte estatus)
        {
            var nuevoDocumentoAlumno = new DocumentoAlumno
            {
                idDocumento = idDocumento,
                idAlumno = idAlumno,
                fechaCreacion = fechaCreacion,
                fechaModifica = fechaModifica,

                estatus = estatus
            };

            _context.DocumentoAlumno.Add(nuevoDocumentoAlumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumentoAlumnoPorId), new { id = nuevoDocumentoAlumno.idDocumentoAlumno }, nuevoDocumentoAlumno);
        }

        [HttpPut("UpdateDocumentoAlumno/{id}")]
        public async Task<IActionResult> UpdateDocumentoAlumno(int id, int idDocumento, int idAlumno, DateTime fechaCreacion, DateTime? fechaModifica, byte estatus)
        {
            var documentoAlumno = await _context.DocumentoAlumno.FindAsync(id);

            if (documentoAlumno == null)
            {
                return NotFound();
            }

            documentoAlumno.idDocumento = idDocumento;
            documentoAlumno.idAlumno = idAlumno;
            documentoAlumno.estatus = estatus;
            documentoAlumno.fechaCreacion = fechaCreacion;
            documentoAlumno.fechaModifica = fechaModifica;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoAlumnoExiste(id))
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

        [HttpDelete("DeleteDocumentoAlumno/{id}")]
        public async Task<IActionResult> DeleteDocumentoAlumno(int id)
        {
            var documentoAlumno = await _context.DocumentoAlumno.FindAsync(id);
            if (documentoAlumno == null)
            {
                return NotFound();
            }

            documentoAlumno.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoAlumnoExiste(id))
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
