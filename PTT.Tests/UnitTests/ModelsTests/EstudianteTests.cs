using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ProyectoTicketTurno.Business.Models;

namespace PTT.Tests.UnitTests.ModelsTests
{
    public class EstudianteTests
    {
        [Fact]
        public void Estudiante_Constructor_DebeSetearFechaRegistro()
        {
            // Act
            var estudiante = new Estudiante();

            // Assert
            Assert.NotEqual(DateTime.MinValue, estudiante.FechaRegistro);
            Assert.True(estudiante.Activo);
        }

        [Fact]
        public void Estudiante_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange
            var estudiante = new Estudiante
            {
                CURP = "ABCD123456HDFMNN01",
                Nombre = "Juan",
                ApellidoPaterno = "García",
                ApellidoMaterno = "López",
                FechaNacimiento = new DateTime(2000, 5, 15),
                Sexo = 'H',
                Edad = 23,
                TelefonoContacto = "6141234567",
                Grado = 3
            };

            // Act & Assert
            Assert.Equal("ABCD123456HDFMNN01", estudiante.CURP);
            Assert.Equal("Juan", estudiante.Nombre);
            Assert.Equal("García", estudiante.ApellidoPaterno);
            Assert.Equal("López", estudiante.ApellidoMaterno);
        }

        [Fact]
        public void Estudiante_SexoInvalido_DebeAceptarH_o_M()
        {
            // Arrange
            var estudiante = new Estudiante { Sexo = 'H' };
            var estudiante2 = new Estudiante { Sexo = 'M' };

            // Act & Assert
            Assert.Equal('H', estudiante.Sexo);
            Assert.Equal('M', estudiante2.Sexo);
        }
    }
}
