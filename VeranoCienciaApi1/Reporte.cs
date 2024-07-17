using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Reporte
    {

        [Key]
        public int idreporte { get; set; }
        public int idusuario { get; set; }
        public int idstatusreporte { get; set; }
        public int idproyecto { get; set; }
        public DateTime fechaactualizacion { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
