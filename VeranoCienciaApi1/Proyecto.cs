using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Proyecto
    {
        [Key]
        public int idProyecto { get; set; }
        public string titulo { get; set; }
        public string perfil { get; set; }
        public byte porcentajeAvanceCarrera { get; set; }
        public string carrera { get; set; }
        public string actividad { get; set; }
        public string habilidad { get; set; }
        public string modalidad { get; set; }
        public string observaciones { get; set; }
        public byte cantidadAlumnos { get; set; }
        public int validado { get; set; }
        public int idUsuarioInvestigador { get; set; }
        public int idCampus { get; set; }
        public int idInstitucion { get; set; }
        public int idVerano { get; set; }
        public int idarea { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModifica { get; set; }

        [DefaultValue(1)]
        public byte estatus { get; set; }
    }
}
