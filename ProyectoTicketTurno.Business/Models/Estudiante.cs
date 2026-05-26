using System;

namespace ProyectoTicketTurno.Business.Models
{
    public class Estudiante
    {
        public string CURP { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }
        public int Edad { get; set; }

        // FK a Estados.Clave (columna: EstadoNacimiento)
        public string EstadoNacimiento { get; set; }

        // FK a Municipios.IdMunicipio (columna: MunicipioEstudio)
        public int MunicipioEstudio { get; set; }

        public int IdNivelEducativo { get; set; }
        public byte Grado { get; set; }
        public string TelefonoContacto { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Navegacion
        public virtual Estado EstadoNacimientoEntidad { get; set; }
        public virtual Municipio MunicipioEstudioEntidad { get; set; }
        public virtual NivelEducativo NivelEducativo { get; set; }

        public Estudiante()
        {
            FechaRegistro = DateTime.Now;
        }
    }
}
