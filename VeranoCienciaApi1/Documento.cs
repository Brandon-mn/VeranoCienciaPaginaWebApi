using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace VeranoCienciaApi1
{
    public class Documento
    {
        [Key]
        public int idDocumento { get; set; }
        public string descripcion { get; set; }
        public string prefijo { get; set; }
        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
