using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly VeranoContext _context;

        public SolicitudController(VeranoContext context)
        {
            _context = context;
        }

        private bool SolicitudExiste(int id)
        {
            return _context.Solicitud.Any(e => e.idSolicitud == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasSolicitudes()
        {
            var solicitudes = await _context.Solicitud
                .Where(s => s.estatus == 1)
                .ToListAsync();
            return Ok(solicitudes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitudPorId(int id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return solicitud;
        }

        [HttpPost("CreateSolicitud")]
        public async Task<ActionResult<Solicitud>> CreateSolicitud(
            string comentario,
            byte numeroVuelta,
            int idProyecto,
            int orden,
            byte aceptada,
            int idVerano,
            int idUsuarioAlumno,
            DateTime fechaCreacion,
            DateTime? fechaModifica,
            byte estatus)
        {
            var nuevaSolicitud = new Solicitud
            {
                comentario = comentario,
                numeroVuelta = numeroVuelta,
                idProyecto = idProyecto,
                orden = orden,
                aceptada = aceptada,
                idVerano = idVerano,
                idUsuarioAlumno = idUsuarioAlumno,
                fechaCreacion = fechaCreacion,
                fechaModifica = fechaModifica,
                estatus = estatus
            };

            _context.Solicitud.Add(nuevaSolicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSolicitudPorId), new { id = nuevaSolicitud.idSolicitud }, nuevaSolicitud);
        }

        [HttpPut("UpdateSolicitud/{id}")]
        public async Task<IActionResult> UpdateSolicitud(
            int id,
            string comentario,
            byte numeroVuelta,
            int idProyecto,
            int orden,
            byte aceptada,
            int idVerano,
            int idUsuarioAlumno,
            DateTime fechaCreacion,
            DateTime? fechaModifica,
            byte estatus)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            solicitud.comentario = comentario;
            solicitud.numeroVuelta = numeroVuelta;
            solicitud.idProyecto = idProyecto;
            solicitud.orden = orden;
            solicitud.aceptada = aceptada;
            solicitud.idVerano = idVerano;
            solicitud.idUsuarioAlumno = idUsuarioAlumno;
            solicitud.fechaCreacion = fechaCreacion;
            solicitud.fechaModifica = fechaModifica;
            solicitud.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExiste(id))
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

        [HttpDelete("DeleteSolicitud/{id}")]
        public async Task<IActionResult> DeleteSolicitud(int id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            solicitud.estatus = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExiste(id))
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
