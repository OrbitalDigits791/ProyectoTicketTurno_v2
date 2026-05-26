using System.Data.Entity;
using ProyectoTicketTurno.Business.Models;

using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTicketTurno.Data.Context
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext() : base("name=ProyectoTicketTurnoConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<NivelEducativo> NivelesEducativos { get; set; }
        public DbSet<Asunto> Asuntos { get; set; }
        public DbSet<SolicitudTurno> SolicitudesTurno { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ESTADOS
            modelBuilder.Entity<Estado>()
                .ToTable("Estados")
                .HasKey(e => e.Clave);

            modelBuilder.Entity<Estado>()
                .Property(e => e.Clave)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estado>()
                .Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            // MUNICIPIOS
            modelBuilder.Entity<Municipio>()
                .ToTable("Municipios")
                .HasKey(m => m.IdMunicipio);

            modelBuilder.Entity<Municipio>()
                .Property(m => m.IdMunicipio)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Municipio>()
                .Property(m => m.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            // NIVELES EDUCATIVOS
            modelBuilder.Entity<NivelEducativo>()
                .ToTable("NivelesEducativos")
                .HasKey(n => n.IdNivelEducativo);

            modelBuilder.Entity<NivelEducativo>()
                .Property(n => n.IdNivelEducativo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<NivelEducativo>()
                .Property(n => n.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<NivelEducativo>()
                .Property(n => n.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsOptional();

            // ASUNTOS
            modelBuilder.Entity<Asunto>()
                .ToTable("Asuntos")
                .HasKey(a => a.IdAsunto);

            modelBuilder.Entity<Asunto>()
                .Property(a => a.IdAsunto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Asunto>()
                .Property(a => a.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Asunto>()
                .Property(a => a.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsOptional();

            modelBuilder.Entity<Asunto>()
                .Property(a => a.Activo)
                .IsRequired();

            // ESTUDIANTES
            modelBuilder.Entity<Estudiante>()
                .ToTable("Estudiantes")
                .HasKey(e => e.CURP);

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.CURP)
                .HasMaxLength(18)
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.Sexo)
                .HasColumnType("char")
                .HasMaxLength(1)
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.EstadoNacimiento)
                .HasColumnType("char")
                .HasMaxLength(2)
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.TelefonoContacto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsOptional();

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.FechaRegistro)
                .IsRequired();

            modelBuilder.Entity<Estudiante>()
                .HasRequired(e => e.EstadoNacimientoEntidad)
                .WithMany()
                .HasForeignKey(e => e.EstadoNacimiento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estudiante>()
                .HasRequired(e => e.MunicipioEstudioEntidad)
                .WithMany()
                .HasForeignKey(e => e.MunicipioEstudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estudiante>()
                .HasRequired(e => e.NivelEducativo)
                .WithMany()
                .HasForeignKey(e => e.IdNivelEducativo)
                .WillCascadeOnDelete(false);

            // SOLICITUDES TURNO
            modelBuilder.Entity<SolicitudTurno>()
                .ToTable("SolicitudesTurno")
                .HasKey(s => s.NumeroTurno);

            modelBuilder.Entity<SolicitudTurno>()
                .Property(s => s.NumeroTurno)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<SolicitudTurno>()
                .Property(s => s.CURP)
                .HasMaxLength(18)
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<SolicitudTurno>()
                .Property(s => s.PersonaTramitera)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<SolicitudTurno>()
                .Property(s => s.Parentesco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<SolicitudTurno>()
                .Property(s => s.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<SolicitudTurno>()
                .Property(s => s.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false)
                .IsOptional();

            modelBuilder.Entity<SolicitudTurno>()
                .HasRequired(s => s.Estudiante)
                .WithMany()
                .HasForeignKey(s => s.CURP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SolicitudTurno>()
                .HasRequired(s => s.Municipio)
                .WithMany()
                .HasForeignKey(s => s.IdMunicipio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SolicitudTurno>()
                .HasRequired(s => s.Asunto)
                .WithMany()
                .HasForeignKey(s => s.IdAsunto)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}