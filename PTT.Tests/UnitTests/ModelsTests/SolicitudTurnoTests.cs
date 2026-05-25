using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ProyectoTicketTurno.Business.Models;

namespace PTT.Tests.UnitTests.ModelsTests
{
    public class SolicitudTurnoTests
    {
        [Fact]
        public void SolicitudTurno_Constructor_DebeSetearDefaults()
        {
            // Act
            var solicitud = new SolicitudTurno();

            // Assert
            Assert.NotEqual(DateTime.MinValue, solicitud.FechaSolicitud);
            Assert.Equal("Pendiente", solicitud.Estatus);
        }

        [Fact]
        public void SolicitudTurno_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange
            var solicitud = new SolicitudTurno
            {
                NumeroTurno = 1,
                CURP = "ABCD123456HDFMNN01",
                IdMunicipio = 1,
                Asunto = "Trámite administrativo",
                PersonaTramitera = "María García",
                Parentesco = "Madre"
            };

            // Act & Assert
            Assert.Equal(1, solicitud.NumeroTurno);
            Assert.Equal("ABCD123456HDFMNN01", solicitud.CURP);
            Assert.Equal(1, solicitud.IdMunicipio);
            Assert.Equal("Trámite administrativo", solicitud.Asunto);
        }

        [Fact]
        public void SolicitudTurno_Estatus_DebeSerPendiente_o_Resuelto()
        {
            // Arrange
            var solicitud1 = new SolicitudTurno { Estatus = "Pendiente" };
            var solicitud2 = new SolicitudTurno { Estatus = "Resuelto" };

            // Act & Assert
            Assert.Equal("Pendiente", solicitud1.Estatus);
            Assert.Equal("Resuelto", solicitud2.Estatus);
        }
    }
}
