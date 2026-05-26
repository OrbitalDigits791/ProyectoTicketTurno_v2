using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTicketTurno.Data.Repositories
{
    public interface IAsuntoRepository : IRepository<Asunto>
    {
        Asunto ObtenerPorIdAsunto(int idAsunto);
        IEnumerable<Asunto> ObtenerActivos();
        IEnumerable<Asunto> ObtenerPorCategoria(string categoria);
        Asunto ObtenerPorDescripcion(string descripcion);
    }
}
