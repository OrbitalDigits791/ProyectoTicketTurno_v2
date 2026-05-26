using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ProyectoTicketTurno.Business.Models;
using ProyectoTicketTurno.Data.Context;
using ProyectoTicketTurno.Data.Repositories;

namespace PTT.Tests.UnitTests.RepositoriesTests
{
    public class SolicitudTurnoRepositoryTests
    {
        private AplicacionDbContext GetContext()
        {
            return new AplicacionDbContext();
        }

        [Fact]
        public void ObtenerPorEstatus_DebeRetornarColeccion()
        {
            using (var context = GetContext())
            {
                var repository = new SolicitudTurnoRepository(context);
                var resultado = repository.ObtenerPorEstatus("Pendiente");
                Assert.NotNull(resultado);
                Assert.IsAssignableFrom<IEnumerable<SolicitudTurno>>(resultado);
            }
        }

        [Fact]
        public void ObtenerPorAsunto_DebeRetornarColeccion()
        {
            using (var context = GetContext())
            {
                var repository = new SolicitudTurnoRepository(context);
                var resultado = repository.ObtenerPorAsunto(1);
                Assert.NotNull(resultado);
                Assert.IsAssignableFrom<IEnumerable<SolicitudTurno>>(resultado);
            }
        }
    }
}
