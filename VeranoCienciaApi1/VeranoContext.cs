using Microsoft.EntityFrameworkCore;
using VeranoCienciaApi1;
using VeranoCienciaApi1.Controllers;
namespace VeranoCienciaApi1

{
    namespace VeranoCienciaApi1
    {
        public class VeranoContext : DbContext
        {
            public VeranoContext(DbContextOptions<VeranoContext> options) : base(options)
            {
            }

            public DbSet<Alumno> alumno { get; set; }
            public DbSet<Area> Areas { get; set; }
            public DbSet<Campus> Campus { get; set; }
            public DbSet<Carrera> Carrera { get; set; }
            public DbSet<Coordinador> Coordinador { get; set; }
            public DbSet<Documento> Documento { get; set; }
            public DbSet<DocumentoAlumno> DocumentoAlumno { get; set; }
            public DbSet<Estado> Estado { get; set; }
            public DbSet<Institucion> Institucion { get; set; }
            public DbSet<Usuario> Usuario { get; set; }
            public DbSet<Verano> Verano { get; set; }
            public DbSet<Proyecto> Proyecto { get; set; }
            public DbSet<investigador> investigador { get; set; }
            public DbSet<Modulo> Modulo { get; set; }
            public DbSet<Municipio> Municipio { get; set; }
            public DbSet<Reporte> Reporte { get; set; }
            public DbSet<Reportestatus> Reportestatus { get; set; }
            public DbSet<Solicitud> Solicitud { get; set; }
            public DbSet<tipousuario> tipousuario { get; set; }
            public DbSet<Tipousuariomodulo> Tipousuariomodulo { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Alumno>(entity =>
                {
                    entity.HasKey(e => e.IdAlumno);
                    entity.Property(e => e.Matricula).IsRequired().HasMaxLength(20);
                    entity.Property(e => e.CURP).IsRequired().HasMaxLength(18);
                    entity.Property(e => e.Semestre).IsRequired();
                    entity.Property(e => e.Promedio).HasColumnType("decimal(4,2)");
                    entity.Property(e => e.PorcentajeAvanceCarrera).HasDefaultValue((byte)1);
                    entity.Property(e => e.IdCarrera).IsRequired();
                    entity.Property(e => e.NombreInvestigadorRecomienda).HasMaxLength(250);
                    entity.Property(e => e.TelefonoInvestigadorRecomienda).HasMaxLength(20);
                    entity.Property(e => e.CorreoInvestigadorRecomienda).HasMaxLength(200);
                    entity.Property(e => e.IdUsuario).IsRequired();
                    entity.Property(e => e.IdVerano).IsRequired();
                    entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.FechaModifica);
                    entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
                });
                modelBuilder.Entity<Area>(entity =>
                {
                    entity.ToTable("area"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idarea);
                    entity.Property(e => e.descripcion).IsRequired().HasMaxLength(300);
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Campus>(entity =>
                {
                    entity.ToTable("campus"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idCampus);
                    entity.Property(e => e.nombre).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.abreviaturaCampus).IsRequired().HasMaxLength(20);
                    entity.Property(e => e.idInstitucion).IsRequired();
                    entity.Property(e => e.idMunicipio).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Carrera>(entity =>
                {
                    entity.ToTable("carrera"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idCarrera);
                    entity.Property(e => e.nombre).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.idCampus).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Coordinador>(entity =>
                { 
                    entity.ToTable("coordinador"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idCoordinador);
                    entity.Property(e => e.puesto).IsRequired().HasMaxLength(50);
                    entity.Property(e => e.esCoordinadorInstitucional).IsRequired();
                    entity.Property(e => e.idUsuario).IsRequired();
                    entity.Property(e => e.fechaCreacion).HasDefaultValueSql("GETDATE()"); 
                    entity.Property(e => e.fechaModifica);
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Documento>(entity =>
                {
                    entity.ToTable("documento"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idDocumento);
                    entity.Property(e => e.descripcion).IsRequired().HasMaxLength(250);
                    entity.Property(e => e.prefijo).IsRequired().HasMaxLength(20);
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<DocumentoAlumno>(entity =>
                {
                    entity.ToTable("DocumentoAlumno"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idDocumentoAlumno);
                    entity.Property(e => e.idDocumento).IsRequired();
                    entity.Property(e => e.idAlumno).IsRequired();
                    entity.Property(e => e.fechaCreacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaModifica);
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Estado>(entity =>
                {
                    entity.ToTable("estado"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idEstado);
                    entity.Property(e => e.nombre).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Institucion>(entity =>
                {
                    entity.ToTable("institucion"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idInstitucion);
                    entity.Property(e => e.nombre).IsRequired().HasMaxLength(250);
                    entity.Property(e => e.abreviatura).IsRequired().HasMaxLength(20);
                    entity.Property(e => e.idEstado).IsRequired(); 
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Usuario>(entity =>
                {
                    entity.ToTable("usuario"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idUsuario);
                    entity.Property(e => e.idTipoUsuario).IsRequired();
                    entity.Property(e => e.correo).IsRequired();
                    entity.Property(e => e.clave).IsRequired();
                    entity.Property(e => e.nombre);
                    entity.Property(e => e.apellidoPaterno).IsRequired();
                    entity.Property(e => e.apellidoMaterno).IsRequired();
                    entity.Property(e => e.fechaNacimiento).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.genero).IsRequired();
                    entity.Property(e => e.telefono).IsRequired();
                    entity.Property(e => e.calle).IsRequired();
                    entity.Property(e => e.colonia).IsRequired();
                    entity.Property(e => e.numero).IsRequired();
                    entity.Property(e => e.codigoPostal).IsRequired();
                    entity.Property(e => e.idCampus).IsRequired(); 
                    entity.Property(e => e.fechaCreacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaModifica).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.validado).IsRequired();
                    entity.Property(e => e.idUsuarioValida).IsRequired();
                    entity.Property(e => e.fechaValida).HasDefaultValueSql("GETDATE()"); 
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Verano>(entity =>
                {   
                    entity.ToTable("verano"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idVerano);
                    entity.Property(e => e.descripcion).IsRequired();
                    entity.Property(e => e.fechaVeranoInicio).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaVeranoFin).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.proyectosPorInvestigador);
                    entity.Property(e => e.solicitudesPorAlumno).IsRequired();
                    entity.Property(e => e.alumnosPorProyecto).IsRequired();
                    entity.Property(e => e.porcentajeMinimoAvanceAlumno).IsRequired();
                    entity.Property(e => e.fechaCrearProyectoInicio).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaCrearProyectoFin).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaValidarInvestigadorInicio).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaValidarInvestigadorFin).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaCrearSolicitudInicio).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaCrearSolicitudFin).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaValidarSolicitudInicio).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.estado).HasDefaultValue(1);
                });
                modelBuilder.Entity<Proyecto>(entity =>
                {
                    entity.ToTable("proyecto"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idProyecto);
                    entity.Property(e => e.titulo).IsRequired();
                    entity.Property(e => e.perfil).IsRequired();
                    entity.Property(e => e.porcentajeAvanceCarrera).IsRequired();
                    entity.Property(e => e.carrera).IsRequired();
                    entity.Property(e => e.actividad).IsRequired();
                    entity.Property(e => e.habilidad).IsRequired();
                    entity.Property(e => e.modalidad).IsRequired();
                    entity.Property(e => e.observaciones).IsRequired();
                    entity.Property(e => e.cantidadAlumnos).IsRequired();
                    entity.Property(e => e.validado).IsRequired();
                    entity.Property(e => e.idUsuarioInvestigador).IsRequired();
                    entity.Property(e => e.idCampus).IsRequired(); 
                    entity.Property(e => e.idInstitucion).IsRequired(); 
                    entity.Property(e => e.idVerano).IsRequired();
                    entity.Property(e => e.fechaCreacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaModifica).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<investigador>(entity =>
                {
                    entity.ToTable("investigador"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idInvestigador);
                    entity.Property(e => e.titulo).IsRequired();
                    entity.Property(e => e.departamento).IsRequired();
                    entity.Property(e => e.nivelSNI).IsRequired();
                    entity.Property(e => e.PRODEP).IsRequired(); 
                    entity.Property(e => e.idUsuario).IsRequired();
                    entity.Property(e => e.fechaCreacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaModifica).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Modulo>(entity =>
                {
                    entity.ToTable("modulo"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idmodulo);
                    entity.Property(e => e.nombre).IsRequired();
                    entity.Property(e => e.descripcion).IsRequired();
                    entity.Property(e => e.ruta).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Municipio>(entity =>
                {
                    entity.ToTable("municipio"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idMunicipio);
                    entity.Property(e => e.nombre).IsRequired();
                    entity.Property(e => e.idEstado).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Reporte>(entity =>
                {
                    entity.ToTable("reporte"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idreporte);
                    entity.Property(e => e.idusuario).IsRequired();
                    entity.Property(e => e.idstatusreporte).IsRequired();
                    entity.Property(e => e.idproyecto).IsRequired();
                    entity.Property(e => e.fechaactualizacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.status).HasDefaultValue(1);
                });
                modelBuilder.Entity<Reportestatus>(entity =>
                {
                    entity.ToTable("reportestatus"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idreportestatus);
                    entity.Property(e => e.descripcion).IsRequired();
                    entity.Property(e => e.status).HasDefaultValue(1);
                });
                modelBuilder.Entity<Solicitud>(entity =>
                {
                    entity.ToTable("solicitud"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idSolicitud);
                    entity.Property(e => e.comentario).IsRequired();
                    entity.Property(e => e.numeroVuelta).IsRequired();
                    entity.Property(e => e.idProyecto).IsRequired();
                    entity.Property(e => e.orden).IsRequired();
                    entity.Property(e => e.aceptada).IsRequired();
                    entity.Property(e => e.idVerano).IsRequired();
                    entity.Property(e => e.idUsuarioAlumno).IsRequired();
                    entity.Property(e => e.fechaCreacion).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.fechaModifica).HasDefaultValueSql("GETDATE()");
                    entity.Property(e => e.estatus).HasDefaultValue(1);

                    
                });
                modelBuilder.Entity<tipousuario>(entity =>
                {
                    entity.ToTable("tipousuario"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idTipoUsuario);
                    entity.Property(e => e.descripcion).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
                modelBuilder.Entity<Tipousuariomodulo>(entity =>
                {
                    entity.ToTable("tipousuariomodulo"); // Aquí especificas el nombre de la tabla
                    entity.HasKey(e => e.idtipousuariomodulo);
                    entity.Property(e => e.idtipousuariomodulo).IsRequired();
                    entity.Property(e => e.idmodulo).IsRequired();
                    entity.Property(e => e.estatus).HasDefaultValue(1);
                });
            }

        }
    }
}

