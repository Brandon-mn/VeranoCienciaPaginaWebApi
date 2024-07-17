using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeranoController : ControllerBase
    {
        private readonly VeranoContext _context;

        public VeranoController(VeranoContext context)
        {
            _context = context;
        }

        private bool VeranoExiste(int id)
        {
            return _context.Verano.Any(e => e.idVerano == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosVeranos()
        {
            var usuarios = await _context.Verano
              .Where(a => a.estado == 1)
              .ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Verano>> GetVeranoPorId(int id)
        {
            var verano = await _context.Verano.FindAsync(id);

            if (verano == null)
            {
                return NotFound();
            }

            return verano;
        }

        [HttpPost("CreateVerano")]
        public async Task<ActionResult<Verano>> CreateVerano(
            string descripcion,
            DateTime fechaVeranoInicio,
            DateTime fechaVeranoFin,
            int proyectosPorInvestigador,
            int solicitudesPorAlumno,
            int alumnosPorProyecto,
            int porcentajeMinimoAvanceAlumno,
            DateTime fechaCrearProyectoInicio,
            DateTime fechaCrearProyectoFin,
            DateTime fechaValidarInvestigadorInicio,
            DateTime fechaValidarInvestigadorFin,
            DateTime fechaCrearSolicitudInicio,
            DateTime fechaCrearSolicitudFin,
            DateTime fechaValidarSolicitudInicio,
            byte estatus)
        {
            var nuevoVerano = new Verano
            {
                descripcion = descripcion,
                fechaVeranoInicio = fechaVeranoInicio,
                fechaVeranoFin = fechaVeranoFin,
                proyectosPorInvestigador = proyectosPorInvestigador,
                solicitudesPorAlumno = solicitudesPorAlumno,
                alumnosPorProyecto = alumnosPorProyecto,
                porcentajeMinimoAvanceAlumno = porcentajeMinimoAvanceAlumno,
                fechaCrearProyectoInicio = fechaCrearProyectoInicio,
                fechaCrearProyectoFin = fechaCrearProyectoFin,
                fechaValidarInvestigadorInicio = fechaValidarInvestigadorInicio,
                fechaValidarInvestigadorFin = fechaValidarInvestigadorFin,
                fechaCrearSolicitudInicio = fechaCrearSolicitudInicio,
                fechaCrearSolicitudFin = fechaCrearSolicitudFin,
                fechaValidarSolicitudInicio = fechaValidarSolicitudInicio,
                estado = estatus
            };

            _context.Verano.Add(nuevoVerano);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVeranoPorId), new { id = nuevoVerano.idVerano }, nuevoVerano);
        }

        [HttpPut("UpdateVerano/{id}")]
        public async Task<IActionResult> UpdateVerano(int id,
            string descripcion,
            DateTime fechaVeranoInicio,
            DateTime fechaVeranoFin,
            int proyectosPorInvestigador,
            int solicitudesPorAlumno,
            int alumnosPorProyecto,
            int porcentajeMinimoAvanceAlumno,
            DateTime fechaCrearProyectoInicio,
            DateTime fechaCrearProyectoFin,
            DateTime fechaValidarInvestigadorInicio,
            DateTime fechaValidarInvestigadorFin,
            DateTime fechaCrearSolicitudInicio,
            DateTime fechaCrearSolicitudFin,
            DateTime fechaValidarSolicitudInicio,
            byte estatus)
        {
            var verano = await _context.Verano.FindAsync(id);

            if (verano == null)
            {
                return NotFound();
            }

            verano.descripcion = descripcion;
            verano.fechaVeranoInicio = fechaVeranoInicio;
            verano.fechaVeranoFin = fechaVeranoFin;
            verano.proyectosPorInvestigador = proyectosPorInvestigador;
            verano.solicitudesPorAlumno = solicitudesPorAlumno;
            verano.alumnosPorProyecto = alumnosPorProyecto;
            verano.porcentajeMinimoAvanceAlumno = porcentajeMinimoAvanceAlumno;
            verano.fechaCrearProyectoInicio = fechaCrearProyectoInicio;
            verano.fechaCrearProyectoFin = fechaCrearProyectoFin;
            verano.fechaValidarInvestigadorInicio = fechaValidarInvestigadorInicio;
            verano.fechaValidarInvestigadorFin = fechaValidarInvestigadorFin;
            verano.fechaCrearSolicitudInicio = fechaCrearSolicitudInicio;
            verano.fechaCrearSolicitudFin = fechaCrearSolicitudFin;
            verano.fechaValidarSolicitudInicio = fechaValidarSolicitudInicio;
            verano.estado = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeranoExiste(id))
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

        [HttpDelete("DeleteVerano/{id}")]
        public async Task<IActionResult> DeleteVerano(int id)
        {
            var verano = await _context.Verano.FindAsync(id);
            if (verano == null)
            {
                return NotFound();
            }

            verano.estado = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeranoExiste(id))
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
