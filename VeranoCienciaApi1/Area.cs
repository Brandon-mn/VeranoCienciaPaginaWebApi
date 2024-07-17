using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Area
    {
        [Key]
        public int idarea { get; set; }

        [Required]
        [MaxLength(300)]
        public string descripcion { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
