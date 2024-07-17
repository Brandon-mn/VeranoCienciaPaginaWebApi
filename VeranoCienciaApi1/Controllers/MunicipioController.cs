using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using VeranoCienciaApi1.Controllers;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly VeranoContext _context;

        public MunicipioController(VeranoContext context)
        {
            _context = context;
        }

        private bool MunicipioExiste(int id)
        {
            return _context.Municipio.Any(e => e.idMunicipio == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosMunicipios()
        {
            var municipios = await _context.Municipio
                .Where(m => m.estatus == 1)
                .ToListAsync();
            return Ok(municipios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipio>> GetMunicipioPorId(int id)
        {
            var municipio = await _context.Municipio.FindAsync(id);

            if (municipio == null)
            {
                return NotFound();
            }

            return municipio;
        }

        [HttpPost("CreateMunicipio")]
        public async Task<ActionResult<Municipio>> CreateMunicipio(
            string nombre,
            int idEstado,
            byte estatus)
        {
            var nuevoMunicipio = new Municipio
            {
                nombre = nombre,
                idEstado = idEstado,
                estatus = estatus
            };

            _context.Municipio.Add(nuevoMunicipio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMunicipioPorId), new { id = nuevoMunicipio.idMunicipio }, nuevoMunicipio);
        }

        [HttpPut("UpdateMunicipio/{id}")]
        public async Task<IActionResult> UpdateMunicipio(
            int id,
            string nombre,
            int idEstado,
            byte estatus)
        {
            var municipio = await _context.Municipio.FindAsync(id);

            if (municipio == null)
            {
                return NotFound();
            }

            municipio.nombre = nombre;
            municipio.idEstado = idEstado;
            municipio.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipioExiste(id))
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

        [HttpDelete("DeleteMunicipio/{id}")]
        public async Task<IActionResult> DeleteMunicipio(int id)
        {
            var municipio = await _context.Municipio.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            municipio.estatus = 0; // Cambiando el estatus a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipioExiste(id))
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

