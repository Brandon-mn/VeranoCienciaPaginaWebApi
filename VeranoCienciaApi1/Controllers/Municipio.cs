using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1.Controllers
{
    public class Municipio
    {
        [Key]
        public int idMunicipio { get; set; }
        public string nombre { get; set; }
        public int idEstado { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
