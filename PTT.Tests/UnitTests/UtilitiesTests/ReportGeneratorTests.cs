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
            // Arrange
            var estudiante = new Estudiante
            {
                CURP = "ABCD123456HDFMNN01",
                Nombre = "Juan",
                ApellidoPaterno = "García",
                ApellidoMaterno = "López",
                Edad = 18,
                Sexo = 'H',
                TelefonoContacto = "6141234567",
                Grado = 3
            };

            var municipio = new Municipio { IdMunicipio = 1, Nombre = "Saltillo" };

            var solicitud = new SolicitudTurno
            {
                NumeroTurno = 1,
                CURP = "ABCD123456HDFMNN01",
                Asunto = "Trámite administrativo",
                PersonaTramitera = "María García",
                Parentesco = "Madre",
                Estatus = "Pendiente"
            };

            // Act
            var resultado = ReportGenerator.GenerarComprobanteTexto(solicitud, estudiante, municipio);

            // Assert
            Assert.NotNull(resultado);
            Assert.Contains("COMPROBANTE DE TURNO", resultado);
            Assert.Contains("Juan García López", resultado);
        }

        [Fact]
        public void GenerarComprobanteTexto_ConNullEstudiante_DebeRetornarError()
        {
            // Arrange
            var municipio = new Municipio { IdMunicipio = 1, Nombre = "Saltillo" };
            var solicitud = new SolicitudTurno { NumeroTurno = 1 };

            // Act
            var resultado = ReportGenerator.GenerarComprobanteTexto(solicitud, null, municipio);

            // Assert
            Assert.Contains("Error", resultado);
        }

        [Fact]
        public void GenerarEstadisticasTexto_ConDatosValidos_DebeRetornarEstadisticas()
        {
            // Arrange
            int pendientes = 10;
            int resueltos = 5;

            // Act
            var resultado = ReportGenerator.GenerarEstadisticasTexto(pendientes, resueltos);

            // Assert
            Assert.NotNull(resultado);
            Assert.Contains("ESTADÍSTICAS", resultado);
            Assert.Contains("Pendientes:     10", resultado);
            Assert.Contains("Resueltos:      5", resultado);
        }
    }
}
