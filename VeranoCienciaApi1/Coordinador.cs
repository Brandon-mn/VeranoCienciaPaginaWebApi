using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace VeranoCienciaApi1
{
    public class Coordinador
    {
        [Key]
        public int idCoordinador { get; set; }
        public string puesto { get; set; }
        public int esCoordinadorInstitucional { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaModifica { get; set; }
        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
