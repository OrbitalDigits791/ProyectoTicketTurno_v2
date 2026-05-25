namespace ProyectoTicketTurno.Business.Models
{
    public class Municipio
    {
        public int IdMunicipio { get; set; } // PK
        public string Nombre { get; set; }
        public int ContadorTurnos { get; set; } // Para auto-incremento de turnos

        public Municipio()
        {
            ContadorTurnos = 0;
        }

        /// Obtiene el próximo número de turno e incrementa el contador
        public int ObtenerProximoTurno()
        {
            ContadorTurnos++;
            return ContadorTurnos;
        }

    }
}