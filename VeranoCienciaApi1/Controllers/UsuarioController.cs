using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1.VeranoCienciaApi1;

namespace VeranoCienciaApi1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly VeranoContext _context;

        public UsuarioController(VeranoContext context)
        {
            _context = context;
        }

        private bool UsuarioExiste(int id)
        {
            return _context.Usuario.Any(e => e.idUsuario == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosUsuarios()
        {
            var usuarios = await _context.Usuario
                .Where(a => a.estatus == 1)
                .ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioPorId(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("CreateUsuario")]
        public async Task<ActionResult<Usuario>> CreateUsuario(int idTipoUsuario, string correo, string clave, string nombre, string apellidoPaterno, string apellidoMaterno, DateTime fechaNacimiento, string genero, string telefono, string calle, string colonia, string numero, string codigoPostal, int idCampus, DateTime fechaCreacion, DateTime fechaModifica, byte validado, int idUsuarioValida, DateTime fechaValida, byte estatus)
        {
            var nuevoUsuario = new Usuario
            {
                idTipoUsuario = idTipoUsuario,
                correo = correo,
                clave = clave,
                nombre = nombre,
                apellidoPaterno = apellidoPaterno,
                apellidoMaterno = apellidoMaterno,
                fechaNacimiento = fechaNacimiento,
                genero = genero,
                telefono = telefono,
                calle = calle,
                colonia = colonia,
                numero = numero,
                codigoPostal = codigoPostal,
                idCampus = idCampus,
                fechaCreacion= fechaCreacion,
                fechaModifica = fechaModifica,
                validado = validado,
                idUsuarioValida = idUsuarioValida,
                fechaValida = fechaValida,
                estatus = estatus
            };

            _context.Usuario.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioPorId), new { id = nuevoUsuario.idUsuario }, nuevoUsuario);
        }

        [HttpPut("UpdateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, int idTipoUsuario, string correo, string clave, string nombre, string apellidoPaterno, string apellidoMaterno, DateTime fechaNacimiento, string genero, string telefono, string calle, string colonia, string numero, string codigoPostal, int idCampus, DateTime fechaCreacion, DateTime fechaModifica, byte validado, int idUsuarioValida, DateTime fechaValida, byte estatus)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.idTipoUsuario = idTipoUsuario;
            usuario.correo = correo;
            usuario.clave = clave;
            usuario.nombre = nombre;
            usuario.apellidoPaterno = apellidoPaterno;
            usuario.apellidoMaterno = apellidoMaterno;
            usuario.fechaNacimiento = fechaNacimiento;
            usuario.genero = genero;
            usuario.telefono = telefono;
            usuario.calle = calle;
            usuario.colonia = colonia;
            usuario.numero = numero;
            usuario.codigoPostal = codigoPostal;
            usuario.idCampus = idCampus;
            usuario.fechaCreacion = fechaCreacion;
            usuario.fechaModifica = fechaModifica;
            usuario.validado = validado;
            usuario.idUsuarioValida = idUsuarioValida;
            usuario.fechaValida = fechaValida;
            usuario.estatus = estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExiste(id))
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

        [HttpDelete("DeleteUsuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.estatus = 0; // Cambiando el status a 0 en lugar de eliminar

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExiste(id))
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
