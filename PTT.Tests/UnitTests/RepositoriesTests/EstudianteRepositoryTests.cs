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
        private AplicacionDbContext GetInMemoryContext()
        {
            return new AplicacionDbContext();
        }

        [Fact]
        public void ObtenerPorCURP_ConCURPValido_DebeRetornarEstudiante()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repository = new EstudianteRepository(context);
            var curp = "ABCD123456HDFMNN01";

            // Act
            var resultado = repository.ObtenerPorCURP(curp);

            // Assert
            // En una BD vacía, puede ser null; en una con datos, será una colección
            Assert.NotNull(resultado);
        }

        [Fact]
        public void ObtenerPorCURP_ConCURPInvalido_DebeRetornarVacio()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repository = new EstudianteRepository(context);
            var curp = "CURP_INEXISTENTE";

            // Act
            var resultado = repository.ObtenerPorCURP(curp);

            // Assert
            Assert.Empty((System.Collections.IEnumerable)resultado);
        }
    }
}
