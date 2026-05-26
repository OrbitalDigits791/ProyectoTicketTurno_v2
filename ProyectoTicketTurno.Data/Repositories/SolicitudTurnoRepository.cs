using System.Collections.Generic;
using System.Linq;
using ProyectoTicketTurno.Business.Models;
using ProyectoTicketTurno.Data.Context;

namespace ProyectoTicketTurno.Data.Repositories
{
    public class SolicitudTurnoRepository : Repository<SolicitudTurno>, ISolicitudTurnoRepository
    {
        public SolicitudTurnoRepository(AplicacionDbContext context) : base(context)
        {
        }

        public SolicitudTurno ObtenerPorNumeroTurno(int numeroTurno)
        {
            return ObtenerPorId(numeroTurno);
        }

        public IEnumerable<SolicitudTurno> ObtenerPorMunicipio(int idMunicipio)
        {
            return ObtenerPor(s => s.IdMunicipio == idMunicipio);
        }

        public IEnumerable<SolicitudTurno> ObtenerPorCURP(string curp)
        {
            return ObtenerPor(s => s.CURP == curp);
        }

        public IEnumerable<SolicitudTurno> ObtenerPorEstatus(string estatus)
        {
            return ObtenerPor(s => s.Estatus == estatus);
        }

        public IEnumerable<SolicitudTurno> ObtenerPorAsunto(int idAsunto)
        {
            return ObtenerPor(s => s.IdAsunto == idAsunto);
        }
    }
}