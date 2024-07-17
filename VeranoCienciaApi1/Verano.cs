using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VeranoCienciaApi1
{
    public class Verano
    {
        [Key]
        public int idVerano { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaVeranoInicio { get; set; }
        public DateTime fechaVeranoFin { get; set; }
        public int proyectosPorInvestigador { get; set; }
        public int solicitudesPorAlumno { get; set; }
        public int alumnosPorProyecto { get; set; }
        public int porcentajeMinimoAvanceAlumno { get; set; }
        public DateTime fechaCrearProyectoInicio { get; set; }
        public DateTime fechaCrearProyectoFin { get; set; }
        public DateTime fechaValidarInvestigadorInicio { get; set; }
        public DateTime fechaValidarInvestigadorFin { get; set; }
        public DateTime fechaCrearSolicitudInicio { get; set; }
        public DateTime fechaCrearSolicitudFin { get; set; }
        public DateTime fechaValidarSolicitudInicio { get; set; }

        [DefaultValue(1)]
        public byte estado { get; set; }
    }
}
