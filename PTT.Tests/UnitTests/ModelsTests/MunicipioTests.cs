using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ProyectoTicketTurno.Business.Models;

namespace PTT.Tests.UnitTests.ModelsTests
{
    public class MunicipioTests
    {
        [Fact]
        public void Municipio_ObtenerProximoTurno_DebeIncrementarContador()
        {
            // Arrange
            var municipio = new Municipio { Nombre = "Saltillo", ContadorTurnos = 0 };

            // Act
            int turno1 = municipio.ObtenerProximoTurno();
            int turno2 = municipio.ObtenerProximoTurno();

            // Assert
            Assert.Equal(1, turno1);
            Assert.Equal(2, turno2);
            Assert.Equal(2, municipio.ContadorTurnos);
        }

        [Fact]
        public void Municipio_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange
            var municipio = new Municipio { IdMunicipio = 1, Nombre = "Saltillo", ContadorTurnos = 5 };

            // Act & Assert
            Assert.Equal(1, municipio.IdMunicipio);
            Assert.Equal("Saltillo", municipio.Nombre);
            Assert.Equal(5, municipio.ContadorTurnos);
        }
    }
}
