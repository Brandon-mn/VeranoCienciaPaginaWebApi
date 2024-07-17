using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1.Controllers
{
    public class investigador
    {
        [Key]
        public int idInvestigador { get; set; }
        public string titulo { get; set; }
        public string departamento { get; set; }
        public byte nivelSNI { get; set; }
        public byte PRODEP { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModifica { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
