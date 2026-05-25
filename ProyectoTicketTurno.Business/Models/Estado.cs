namespace ProyectoTicketTurno.Business.Models
{
    public class Estado
    {
        public int IdEstado { get; set; }        // PK (identidad)
        public string Clave { get; set; }        // Abreviatura RENAPO (CO, CH, DG, etc.)
        public string Nombre { get; set; }       // Nombre del estado
    }
}