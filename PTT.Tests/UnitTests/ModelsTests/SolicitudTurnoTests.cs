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
            var solicitud = new SolicitudTurno();

            Assert.NotEqual(DateTime.MinValue, solicitud.FechaSolicitud);
            Assert.Equal("Pendiente", solicitud.Estatus);
        }

        [Fact]
        public void SolicitudTurno_ConDatosValidos_DebeCrearseCorrectamente()
        {
            var solicitud = new SolicitudTurno
            {
                NumeroTurno = 1,
                CURP = "GAJL000515HCLRPN01",
                IdMunicipio = 1,
                IdAsunto = 2,
                PersonaTramitera = "Maria Garcia",
                Parentesco = "Madre",
                Estatus = "Pendiente"
            };

            Assert.Equal(1, solicitud.NumeroTurno);
            Assert.Equal(2, solicitud.IdAsunto);
            Assert.Equal("GAJL000515HCLRPN01", solicitud.CURP);
        }
    }
}
