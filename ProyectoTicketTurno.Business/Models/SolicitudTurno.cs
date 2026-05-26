using System;

namespace ProyectoTicketTurno.Business.Models
{
    public class SolicitudTurno
    {
        public int NumeroTurno { get; set; }
        public string CURP { get; set; }
        public int IdMunicipio { get; set; }
        public int IdAsunto { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string PersonaTramitera { get; set; }
        public string Parentesco { get; set; }
        public string Estatus { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public string Observaciones { get; set; }

        // Navegacion
        public virtual Estudiante Estudiante { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Asunto Asunto { get; set; }

        public SolicitudTurno()
        {
            FechaSolicitud = DateTime.Now;
            Estatus = "Pendiente";
        }
    }
}