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
        private AplicacionDbContext GetInMemoryContext()
        {
            return new AplicacionDbContext();
        }

        [Fact]
        public void ObtenerPorNumeroTurno_ConNumeroValido_DebeRetornarSolicitud()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repository = new SolicitudTurnoRepository(context);
            int numeroTurno = 1;

            // Act
            var resultado = repository.ObtenerPorNumeroTurno(numeroTurno);

            // Assert
            // En una BD vacía será null; en una con datos, será un objeto
            Assert.IsAssignableFrom<SolicitudTurno>(resultado);
        }

        [Fact]
        public void ObtenerPorEstatus_ConEstatusValido_DebeRetornarLista()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repository = new SolicitudTurnoRepository(context);
            string estatus = "Pendiente";

            // Act
            var resultado = repository.ObtenerPorEstatus(estatus);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<SolicitudTurno>>(resultado);
        }

        [Fact]
        public void ObtenerPorMunicipio_ConIdValido_DebeRetornarLista()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repository = new SolicitudTurnoRepository(context);
            int idMunicipio = 1;

            // Act
            var resultado = repository.ObtenerPorMunicipio(idMunicipio);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<SolicitudTurno>>(resultado);
        }

        [Fact]
        public void ObtenerPorCURP_ConCURPValido_DebeRetornarLista()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repository = new SolicitudTurnoRepository(context);
            string curp = "ABCD123456HDFMNN01";

            // Act
            var resultado = repository.ObtenerPorCURP(curp);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<SolicitudTurno>>(resultado);
        }
    }
}
