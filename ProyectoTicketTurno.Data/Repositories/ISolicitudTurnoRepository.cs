using System.Collections.Generic;
using ProyectoTicketTurno.Business.Models;

namespace ProyectoTicketTurno.Data.Repositories
{
    public interface ISolicitudTurnoRepository : IRepository<SolicitudTurno>
    {
        SolicitudTurno ObtenerPorNumeroTurno(int numeroTurno);
        IEnumerable<SolicitudTurno> ObtenerPorMunicipio(int idMunicipio);    //Cambio: int, no string
        IEnumerable<SolicitudTurno> ObtenerPorCURP(string curp);              //Nuevo método
        IEnumerable<SolicitudTurno> ObtenerPorEstatus(string estatus);        //Cambio: string, no EstatusEnum
    }
}