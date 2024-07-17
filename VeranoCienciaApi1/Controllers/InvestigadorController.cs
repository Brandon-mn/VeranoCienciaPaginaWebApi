using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigadorController : ControllerBase
    {
        private readonly VeranoContext _context;

        public InvestigadorController(VeranoContext context)
        {
            _context = context;
        }

        private bool InvestigadorExiste(int id)
        {
            return _context.investigador.Any(e => e.idInvestigador == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosInvestigadores()
        {
            var alumnos = await _context.investigador
                .Where(a => a.estatus == 1)  // Asegúrate de que la propiedad 'Estatus' existe y es correcta
                .ToListAsync();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<investigador>> GetInvestigadorPorId(int id)
        {
            var investigador = await _context.investigador.FindAsync(id);

            if (investigador == null)
            {
                return NotFound();
            }

            return investigador;
        }

        [HttpPost("CreateInvestigador")]
        public async Task<ActionResult<investigador>> CreateInvestigador(
            string titulo,
            string departamento,
            byte? nivelSNI,
            byte? PRODEP,
            int idUsuario,
            DateTime? fechaCreacion,
            DateTime? fechaModifica,
            byte estatus)
        {
            var nuevoInvestigador = new investigador
            {
                titulo = titulo,
                departamento = departamento,
                nivelSNI = nivelSNI ?? 0,
                PRODEP = PRODEP ?? 0,
                idUsuario = idUsuario,
                fechaCreacion = fechaCreacion ?? DateTime.Now,
                fechaModifica = fechaModifica ?? DateTime.Now,
                estatus = estatus
            };

            _context.investigador.Add(nuevoInvestigador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvestigadorPorId), new { id = nuevoInvestigador.idInvestigador }, nuevoInvestigador);
        }

        [HttpPut("UpdateInvestigador/{id}")]
        public async Task<IActionResult> UpdateInvestigador(int id,
            string titulo,
            string departamento,
            byte? nivelSNI,
            byte? PRODEP,
            int idUsuario,
            DateTime? fechaCreacion,
            DateTime? fechaModifica,
            byte estatus)
        {
            var investigador = await _context.investigador.FindAsync(id);

            if (investigador == null)
            {
                return NotFound();
            }

            investigador.titulo = titulo;
            investigador.departamento = departamento;
            investigador.nivelSNI = nivelSNI ?? 0;
            investigador.PRODEP = PRODEP ?? 0;
            investigador.idUsuario = idUsuario;
            investigador.fechaCreacion = fechaCreacion ?? investigador.fechaCreacion;
            investigador.fechaModifica = fechaModifica ?? DateTime.Now;
            investigador.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigadorExiste(id))
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

        [HttpDelete("DeleteInvestigador/{id}")]
        public async Task<IActionResult> DeleteInvestigador(int id)
        {
            var investigador = await _context.investigador.FindAsync(id);
            if (investigador == null)
            {
                return NotFound();
            }

            investigador.estatus = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigadorExiste(id))
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
