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
    public class EstudianteRepositoryTests
    {
        private AplicacionDbContext GetContext()
        {
            return new AplicacionDbContext();
        }

        [Fact]
        public void ObtenerPorCURP_NoDebeLanzarExcepcion()
        {
            using (var context = GetContext())
            {
                var repository = new EstudianteRepository(context);
                var resultado = repository.ObtenerPorCURP("GAJL000515HCLRPN01");
                Assert.True(resultado == null || resultado.CURP != null);
            }
        }
    }
}
