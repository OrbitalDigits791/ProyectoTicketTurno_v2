using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTicketTurno.Data.Repositories
{
    public class AsuntoRepository : Repository<Asunto>, IAsuntoRepository
    {
        public AsuntoRepository(AplicacionDbContext context) : base(context)
        {
        }

        public Asunto ObtenerPorIdAsunto(int idAsunto)
        {
            return ObtenerPorId(idAsunto);
        }

        public IEnumerable<Asunto> ObtenerActivos()
        {
            return ObtenerPor(a => a.Activo);
        }

        public IEnumerable<Asunto> ObtenerPorCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                return Enumerable.Empty<Asunto>();

            return ObtenerPor(a => a.Categoria == categoria);
        }

        public Asunto ObtenerPorDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return null;

            return ObtenerPor(a => a.Descripcion == descripcion).FirstOrDefault();
        }
    }
}
