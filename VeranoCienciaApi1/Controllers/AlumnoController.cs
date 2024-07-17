using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly VeranoContext _context;

        public AlumnoController(VeranoContext context)
        {
            _context = context;
        }

        // GET: api/Alumno
        [HttpGet]
        public async Task<IActionResult> GetTodosAlumnos()
        {
            var alumnos = await _context.alumno
                .Where(a => a.Estatus == 1)  // Asegúrate de que la propiedad 'Estatus' existe y es correcta
                .ToListAsync();
            return Ok(alumnos);
        }

        // GET: api/Cliente/GetCliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumnoPorId(int id)
        {
            var alumno = await _context.alumno.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }
        // POST: api/Cliente/CreateCliente
        [HttpPost("CreateAlumno")]
        public async Task<ActionResult<Alumno>> CreateAlumno(string Matricula, string CURP, int Semestre, decimal Promedio, byte PorcentajeAvanceCarrera, int IdCarrera, string NombreInvestigadorRecomienda, string TelefonoInvestigadorRecomienda, string CorreoInvestigadorRecomienda, int IdUsuario, int IdVerano, DateTime FechaCreacion, DateTime? FechaModifica, byte Estatus)
        {
            int estadostatus = Estatus;

            var nuevoCliente = new Alumno
            {
                Matricula = Matricula,
                CURP = CURP,
                Semestre = Semestre,
                Promedio = Promedio,
                PorcentajeAvanceCarrera = PorcentajeAvanceCarrera,
                IdCarrera = IdCarrera,
                NombreInvestigadorRecomienda = NombreInvestigadorRecomienda,
                TelefonoInvestigadorRecomienda = TelefonoInvestigadorRecomienda,
                CorreoInvestigadorRecomienda = CorreoInvestigadorRecomienda,
                IdUsuario = IdUsuario,
                IdVerano = IdVerano,
                FechaCreacion = FechaCreacion,
                FechaModifica = FechaModifica,
                Estatus = (byte)estadostatus
            };

            _context.alumno.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumnoPorId), new { id = nuevoCliente.IdAlumno }, nuevoCliente);
        }
        // PUT: api/Cliente/UpdateCliente/5
        [HttpPut("UpdateAlumno/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, string Matricula, string CURP, int Semestre, decimal Promedio, byte PorcentajeAvanceCarrera, int IdCarrera, string NombreInvestigadorRecomienda, string TelefonoInvestigadorRecomienda, string CorreoInvestigadorRecomienda, int IdUsuario, int IdVerano, DateTime FechaCreacion, DateTime? FechaModifica, byte Estatus)
        {
            var cliente = await _context.alumno.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Matricula = Matricula;
            cliente.CURP = CURP;
            cliente.Semestre = Semestre;
            cliente.Promedio = Promedio;
            cliente.PorcentajeAvanceCarrera = PorcentajeAvanceCarrera;
            cliente.IdCarrera = IdCarrera;
            cliente.NombreInvestigadorRecomienda = NombreInvestigadorRecomienda;
            cliente.NombreInvestigadorRecomienda = TelefonoInvestigadorRecomienda;
            cliente.CorreoInvestigadorRecomienda = CorreoInvestigadorRecomienda;
            cliente.IdUsuario = IdUsuario;
            cliente.IdVerano = IdVerano;
            cliente.FechaCreacion = FechaCreacion;
            cliente.FechaModifica = FechaModifica;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(id))
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
        // DELETE: api/Cliente/DeleteCliente/5
        [HttpDelete("DeleteAlumno/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.alumno.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(id))
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
        private bool ClienteExiste(int id)
        {
            return _context.alumno.Any(e => e.IdAlumno == id);
        }
    }
}
