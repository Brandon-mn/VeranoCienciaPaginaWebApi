using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace VeranoCienciaApi1
{
    public class Reportestatus
    {
        [Key]
        public int idreportestatus { get; set; }
        public string descripcion { get; set; }
  
        [DefaultValue(1)]
        public int status { get; set; }
    }
}
