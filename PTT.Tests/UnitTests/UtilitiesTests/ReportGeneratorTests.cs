using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ProyectoTicketTurno.Business.Models;
using ProyectoTicketTurno.Infrastructure.Utilities;

namespace PTT.Tests.UnitTests.UtilitiesTests
{
    public class ReportGeneratorTests
    {
        [Fact]
        public void GenerarComprobanteTexto_ConDatosValidos_DebeRetornarComprobante()
        {
            var estudiante = new Estudiante
            {
                CURP = "GAJL000515HCLRPN01",
                Nombre = "Juan",
                ApellidoPaterno = "Garcia",
                ApellidoMaterno = "Lopez",
                Edad = 18,
                Sexo = 'H',
                Grado = 3
            };

            var municipio = new Municipio { IdMunicipio = 1, Nombre = "Saltillo" };

            var solicitud = new SolicitudTurno
            {
                NumeroTurno = 1,
                CURP = "GAJL000515HCLRPN01",
                IdAsunto = 4,
                Asunto = new Asunto { IdAsunto = 4, Descripcion = "Reinscripción" },
                PersonaTramitera = "Maria Garcia",
                Parentesco = "Madre",
                Estatus = "Pendiente"
            };

            var resultado = ReportGenerator.GenerarComprobanteTexto(solicitud, estudiante, municipio);

            Assert.NotNull(resultado);
            Assert.Contains("COMPROBANTE DE TURNO", resultado);
            Assert.Contains("Reinscripción", resultado);
        }
    }
}
