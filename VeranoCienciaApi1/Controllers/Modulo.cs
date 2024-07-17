using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1.Controllers
{
    public class Modulo
    {
        [Key]
        public int idmodulo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string ruta { get; set; }

        [DefaultValue(1)]
        public int estatus { get; set; }
    }
}
