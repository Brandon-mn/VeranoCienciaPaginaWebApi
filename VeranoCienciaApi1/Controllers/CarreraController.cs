using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly VeranoContext _context;
        public CarreraController(VeranoContext context)
        {
            _context = context;
        }

        private bool CarreraExiste(int id)
        {
            return _context.Carrera.Any(e => e.idCarrera == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasCarreras()
        {
            var carreras = await _context.Carrera
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(carreras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarreraPorId(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            return carrera;
        }

        [HttpPost("CreateCarrera")]
        public async Task<ActionResult<Carrera>> CreateCarrera(string nombre, int idCampus, byte estatus)
        {
            int estadostatus = estatus;

            var nuevaCarrera = new Carrera
            {
                nombre = nombre,
                idCampus = idCampus,
                estatus = (byte)estadostatus
            };

            _context.Carrera.Add(nuevaCarrera);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarreraPorId), new { id = nuevaCarrera.idCarrera }, nuevaCarrera);
        }

        [HttpPut("UpdateCarrera/{id}")]
        public async Task<IActionResult> UpdateCarrera(int id, string nombre, int idCampus, byte estatus)
        {
            var carrera = await _context.Carrera.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            carrera.nombre = nombre;
            carrera.idCampus = idCampus;
            carrera.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExiste(id))
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

        [HttpDelete("DeleteCarrera/{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            carrera.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExiste(id))
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
