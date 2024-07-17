using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Institucion
    {

        [Key]
        public int idInstitucion { get; set; }
        public string nombre { get; set; }
        public string abreviatura { get; set; }
        public int idEstado { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
