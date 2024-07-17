using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly VeranoContext _context;

        public ProyectoController(VeranoContext context)
        {
            _context = context;
        }

        private bool ProyectoExiste(int id)
        {
            return _context.Proyecto.Any(e => e.idProyecto == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosProyectos()
        {
            var usuarios = await _context.Proyecto
                     .Where(a => a.estatus == 1)
                     .ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyectoPorId(int id)
        {
            var proyecto = await _context.Proyecto.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        [HttpPost("CreateProyecto")]
        public async Task<ActionResult<Proyecto>> CreateProyecto(
            string titulo,
            string perfil,
            byte? porcentajeAvanceCarrera,
            string carrera,
            string actividad,
            string habilidad,
            string modalidad,
            string observaciones,
            byte? cantidadAlumnos,
            int validado,
            int idUsuarioInvestigador,
            int idCampus,
            int idInstitucion,
            int idVerano,
            int idarea,
            DateTime fechaCreacion,
            DateTime fechaModifica,
            byte estatus)
        {
            var nuevoProyecto = new Proyecto
            {
                titulo = titulo,
                perfil = perfil,
                porcentajeAvanceCarrera = porcentajeAvanceCarrera ?? 1,
                carrera = carrera,
                actividad = actividad,
                habilidad = habilidad,
                modalidad = modalidad,
                observaciones = observaciones,
                cantidadAlumnos = cantidadAlumnos ?? 1,
                validado = validado,
                idUsuarioInvestigador = idUsuarioInvestigador,
                idCampus = idCampus,
                idInstitucion = idInstitucion,
                idVerano = idVerano,
                idarea = idarea,
                fechaCreacion = fechaCreacion,
                fechaModifica = fechaModifica,
                estatus = estatus
            };

            _context.Proyecto.Add(nuevoProyecto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProyectoPorId), new { id = nuevoProyecto.idProyecto }, nuevoProyecto);
        }

        [HttpPut("UpdateProyecto/{id}")]
        public async Task<IActionResult> UpdateProyecto(int id,
            string titulo,
            string perfil,
            byte? porcentajeAvanceCarrera,
            string carrera,
            string actividad,
            string habilidad,
            string modalidad,
            string observaciones,
            byte? cantidadAlumnos,
            int validado,
            int idUsuarioInvestigador,
            int idCampus,
            int idInstitucion,
            int idVerano,
            int idarea,
            DateTime? fechaCreacion,
            DateTime? fechaModifica,
            byte estatus)
        {
            var proyecto = await _context.Proyecto.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.titulo = titulo;
            proyecto.perfil = perfil;
            proyecto.porcentajeAvanceCarrera = porcentajeAvanceCarrera ?? 1;
            proyecto.carrera = carrera;
            proyecto.actividad = actividad;
            proyecto.habilidad = habilidad;
            proyecto.modalidad = modalidad;
            proyecto.observaciones = observaciones;
            proyecto.cantidadAlumnos = cantidadAlumnos ?? 1;
            proyecto.validado = validado;
            proyecto.idUsuarioInvestigador = idUsuarioInvestigador;
            proyecto.idCampus = idCampus;
            proyecto.idInstitucion = idInstitucion;
            proyecto.idVerano = idVerano;
            proyecto.idarea = idarea;
            proyecto.fechaCreacion = fechaCreacion ?? proyecto.fechaCreacion;
            proyecto.fechaModifica = fechaModifica ?? DateTime.Now;
            proyecto.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExiste(id))
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

        [HttpDelete("DeleteProyecto/{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var proyecto = await _context.Proyecto.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.estatus = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExiste(id))
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
