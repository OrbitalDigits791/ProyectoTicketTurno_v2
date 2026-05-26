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
            var estudiante = new Estudiante();
            Assert.NotEqual(DateTime.MinValue, estudiante.FechaRegistro);
        }

        [Fact]
        public void Estudiante_ConDatosValidos_DebeCrearseCorrectamente()
        {
            var estudiante = new Estudiante
            {
                CURP = "GAJL000515HCLRPN01",
                Nombre = "Juan",
                ApellidoPaterno = "Garcia",
                ApellidoMaterno = "Lopez",
                FechaNacimiento = new DateTime(2000, 5, 15),
                Sexo = 'H',
                Edad = 23,
                EstadoNacimiento = "CL",
                MunicipioEstudio = 1,
                IdNivelEducativo = 2,
                Grado = 3,
                TelefonoContacto = "8441234567"
            };

            Assert.Equal("GAJL000515HCLRPN01", estudiante.CURP);
            Assert.Equal("CL", estudiante.EstadoNacimiento);
            Assert.Equal(1, estudiante.MunicipioEstudio);
            Assert.Equal(2, estudiante.IdNivelEducativo);
        }
    }
}
