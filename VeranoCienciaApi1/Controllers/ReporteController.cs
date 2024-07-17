using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly VeranoContext _context;

        public ReporteController(VeranoContext context)
        {
            _context = context;
        }

        private bool ReporteExiste(int id)
        {
            return _context.Reporte.Any(e => e.idreporte == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosReportes()
        {
            var reportes = await _context.Reporte
                .Where(r => r.status == 1)
                .ToListAsync();
            return Ok(reportes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reporte>> GetReportePorId(int id)
        {
            var reporte = await _context.Reporte.FindAsync(id);

            if (reporte == null)
            {
                return NotFound();
            }

            return reporte;
        }

        [HttpPost("CreateReporte")]
        public async Task<ActionResult<Reporte>> CreateReporte(
            int idusuario,
            int idstatusreporte,
            int idproyecto,
            DateTime fechaactualizacion,
            int status)
        {
            var nuevoReporte = new Reporte
            {
                idusuario = idusuario,
                idstatusreporte = idstatusreporte,
                idproyecto = idproyecto,
                fechaactualizacion = fechaactualizacion,
                status = status
            };

            _context.Reporte.Add(nuevoReporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReportePorId), new { id = nuevoReporte.idreporte }, nuevoReporte);
        }

        [HttpPut("UpdateReporte/{id}")]
        public async Task<IActionResult> UpdateReporte(
            int id,
            int idusuario,
            int idstatusreporte,
            int idproyecto,
            DateTime fechaactualizacion,
            int status)
        {
            var reporte = await _context.Reporte.FindAsync(id);

            if (reporte == null)
            {
                return NotFound();
            }

            reporte.idusuario = idusuario;
            reporte.idstatusreporte = idstatusreporte;
            reporte.idproyecto = idproyecto;
            reporte.fechaactualizacion = fechaactualizacion;
            reporte.status = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReporteExiste(id))
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

        [HttpDelete("DeleteReporte/{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
            var reporte = await _context.Reporte.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }

            reporte.status = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReporteExiste(id))
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
