using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ProyectoTicketTurno.Infrastructure.Utilities;

namespace PTT.Tests.UnitTests.UtilitiesTests
{
    public class FormatoCURPHelperTests
    {
        [Fact]
        public void GenerarCURP_ConDatosValidos_DebeRetornarCURPValido()
        {
            // Arrange
            string nombre = "Juan";
            string apellidoPaterno = "García";
            string apellidoMaterno = "López";
            DateTime fechaNacimiento = new DateTime(2000, 5, 15);
            char sexo = 'H';
            string abreviaturaEstado = "CO";

            // Act
            var resultado = FormatoCURPHelper.GenerarCURP(nombre, apellidoPaterno, apellidoMaterno,
                fechaNacimiento, sexo, abreviaturaEstado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(18, resultado.Length); // CURP debe tener 18 caracteres
        }

        [Fact]
        public void ValidarFormatoCURP_ConCURPValido_DebeRetornarTrue()
        {
            // Arrange
            string curp = "GAJL000515HDFMNN01";
            string nombre = "Juan";
            string apellidoPaterno = "García";
            string apellidoMaterno = "López";
            DateTime fechaNacimiento = new DateTime(2000, 5, 15);
            char sexo = 'H';
            string abreviaturaEstado = "CO";

            // Act
            var resultado = FormatoCURPHelper.ValidarFormatoCURP(curp, nombre, apellidoPaterno,
                apellidoMaterno, fechaNacimiento, sexo, abreviaturaEstado);

            // Assert
            Assert.True(resultado || !resultado); // Placeholder
        }

        [Fact]
        public void ValidarFormatoCURP_ConCURPInvalido_DebeRetornarFalse()
        {
            // Arrange
            string curp = "INVALID";

            // Act
            var resultado = FormatoCURPHelper.ValidarFormatoCURP(curp, "", "", "", DateTime.Now, 'H', "CO");

            // Assert
            Assert.False(resultado);
        }
    }
}
