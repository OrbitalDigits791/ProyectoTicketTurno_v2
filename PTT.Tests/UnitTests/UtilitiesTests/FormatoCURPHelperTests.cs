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
        public void GenerarCURP_DebeRetornar18Caracteres()
        {
            var curp = FormatoCURPHelper.GenerarCURP(
                "Juan", "Garcia", "Lopez",
                new DateTime(2000, 5, 15), 'H', "CL");

            Assert.False(string.IsNullOrWhiteSpace(curp));
            Assert.Equal(18, curp.Length);
        }

        [Fact]
        public void ValidarFormatoCURP_ConFormatoInvalido_DebeRetornarFalse()
        {
            Assert.False(FormatoCURPHelper.ValidarFormatoCURP("INVALID"));
        }

        [Fact]
        public void ValidarFormatoCURP_CoherenteConDatos_DebeRetornarTrue()
        {
            var curp = FormatoCURPHelper.GenerarCURP(
                "Juan", "Garcia", "Lopez",
                new DateTime(2000, 5, 15), 'H', "CL");

            var valido = FormatoCURPHelper.ValidarFormatoCURP(
                curp, "Juan", "Garcia", "Lopez",
                new DateTime(2000, 5, 15), 'H', "CL");

            Assert.True(valido);
        }
    }
}
