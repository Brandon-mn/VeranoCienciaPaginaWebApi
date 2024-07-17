using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportestatusController : ControllerBase
    {
        private readonly VeranoContext _context;

        public ReportestatusController(VeranoContext context)
        {
            _context = context;
        }

        private bool ReportestatusExiste(int id)
        {
            return _context.Reportestatus.Any(e => e.idreportestatus == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosReportestatus()
        {
            var reportestatuses = await _context.Reportestatus
                .Where(r => r.status == 1)
                .ToListAsync();
            return Ok(reportestatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reportestatus>> GetReportestatusPorId(int id)
        {
            var reportestatus = await _context.Reportestatus.FindAsync(id);

            if (reportestatus == null)
            {
                return NotFound();
            }

            return reportestatus;
        }

        [HttpPost("CreateReportestatus")]
        public async Task<ActionResult<Reportestatus>> CreateReportestatus(
            string descripcion,
            int status)
        {
            var nuevoReportestatus = new Reportestatus
            {
                descripcion = descripcion,
                status = status
            };

            _context.Reportestatus.Add(nuevoReportestatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReportestatusPorId), new { id = nuevoReportestatus.idreportestatus }, nuevoReportestatus);
        }

        [HttpPut("UpdateReportestatus/{id}")]
        public async Task<IActionResult> UpdateReportestatus(
            int id,
            string descripcion,
            int status)
        {
            var reportestatus = await _context.Reportestatus.FindAsync(id);

            if (reportestatus == null)
            {
                return NotFound();
            }

            reportestatus.descripcion = descripcion;
            reportestatus.status = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportestatusExiste(id))
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

        [HttpDelete("DeleteReportestatus/{id}")]
        public async Task<IActionResult> DeleteReportestatus(int id)
        {
            var reportestatus = await _context.Reportestatus.FindAsync(id);
            if (reportestatus == null)
            {
                return NotFound();
            }

            reportestatus.status = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportestatusExiste(id))
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
