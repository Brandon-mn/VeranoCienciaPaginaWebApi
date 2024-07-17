namespace VeranoCienciaApi1.Controllers
{
    public class Solicitud
    {
        public int idSolicitud { get; set; }
        public string comentario { get; set; }
        public byte numeroVuelta { get; set; }
        public int idProyecto { get; set; }
        public int orden { get; set; }
        public byte aceptada { get; set; }
        public int idVerano { get; set; }
        public int idUsuarioAlumno { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaModifica { get; set; }
        public byte estatus { get; set; }
    }
}
