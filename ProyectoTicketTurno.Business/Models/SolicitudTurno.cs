using System;

namespace ProyectoTicketTurno.Business.Models
{
    public class SolicitudTurno
    {
        public int NumeroTurno { get; set; }     // PK
        public string CURP { get; set; }         // FK
        public int IdMunicipio { get; set; }     // FK (ID del municipio, no string)
        public DateTime FechaSolicitud { get; set; }
        public string Asunto { get; set; }
        public string PersonaTramitera { get; set; }  // Nombre de quien tramita
        public string Parentesco { get; set; }
        public string Estatus { get; set; }      // "Pendiente" o "Resuelto"

        // Propiedades de Navegación
        public virtual Estudiante Estudiante { get; set; }
        public virtual Municipio Municipio { get; set; }

        public SolicitudTurno()
        {
            FechaSolicitud = DateTime.Now;
            Estatus = "Pendiente";
        }
    }
}