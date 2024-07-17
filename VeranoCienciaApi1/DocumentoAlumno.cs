using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace VeranoCienciaApi1
{
    public class DocumentoAlumno
    {
        [Key]
        public int idDocumentoAlumno { get; set; }
        public int idDocumento { get; set; }
        public int idAlumno { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaModifica { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
