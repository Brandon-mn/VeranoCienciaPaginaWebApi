using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Carrera
    {
        [Key]
        public int idCarrera { get; set; }
        public string nombre { get; set; }
        public int idCampus { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
