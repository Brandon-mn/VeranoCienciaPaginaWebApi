using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public int idTipoUsuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string genero { get; set; }
        public string telefono { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string numero { get; set; }
        public string codigoPostal { get; set; }
        public int idCampus { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModifica { get; set; }
        public byte validado { get; set; }
        public int idUsuarioValida { get; set; }
        public DateTime fechaValida { get; set; }
       [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
