using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Campus
    {
        [Key]
        public int idCampus { get; set; }
        public string nombre { get; set; }
        public string abreviaturaCampus { get; set; }
        public int idInstitucion { get; set; }
        public int idMunicipio { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
