using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VeranoCienciaApi1
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Matricula { get; set; }
        public string CURP { get; set; }
        public int Semestre { get; set; } 
        public decimal? Promedio { get; set; } 
        public byte PorcentajeAvanceCarrera { get; set; } 
        public int IdCarrera { get; set; }
        public string NombreInvestigadorRecomienda { get; set; }
        public string TelefonoInvestigadorRecomienda { get; set; }
        public string CorreoInvestigadorRecomienda { get; set; }
        public int IdUsuario { get; set; }
        public int IdVerano { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModifica { get; set; }
        public byte Estatus { get; set; } 
    }

}
