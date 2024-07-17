using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Estado
    {

        [Key]
        public int idEstado { get; set; }
        public string nombre { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
