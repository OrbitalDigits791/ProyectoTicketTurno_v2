using System.Data.Entity.Migrations;
using ProyectoTicketTurno.Business.Models;
using ProyectoTicketTurno.Data.Context;
using System.Linq;

namespace ProyectoTicketTurno.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AplicacionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AplicacionDbContext context)
        {
            base.Seed(context);

            // Datos iniciales de Estados
            if (!context.Estados.Any())
            {
                var estados = new[]
                {
                    new Estado { Nombre = "Coahuila de Zaragoza", Clave = "CO" },
                    new Estado { Nombre = "Chihuahua", Clave = "CH" },
                    new Estado { Nombre = "Durango", Clave = "DG" },
                    new Estado { Nombre = "Nuevo León", Clave = "NL" },
                    new Estado { Nombre = "Tamaulipas", Clave = "TM" }
                };

                foreach (var estado in estados)
                {
                    context.Estados.Add(estado);
                }

                context.SaveChanges();
            }

            // Datos iniciales de Municipios
            if (!context.Municipios.Any())
            {
                var municipios = new[]
                {
                    new Municipio { Nombre = "Saltillo", ContadorTurnos = 0 },
                    new Municipio { Nombre = "Torreón", ContadorTurnos = 0 },
                    new Municipio { Nombre = "Monclova", ContadorTurnos = 0 },
                    new Municipio { Nombre = "Parras de la Fuente", ContadorTurnos = 0 },
                    new Municipio { Nombre = "Arteaga", ContadorTurnos = 0 }
                };

                foreach (var municipio in municipios)
                {
                    context.Municipios.Add(municipio);
                }

                context.SaveChanges();
            }

            // Datos iniciales de Niveles Educativos
            if (!context.NivelesEducativos.Any())
            {
                var niveles = new[]
                {
                    new NivelEducativo { Nombre = "Preescolar" },
                    new NivelEducativo { Nombre = "Primaria" },
                    new NivelEducativo { Nombre = "Secundaria" },
                    new NivelEducativo { Nombre = "Preparatoria" }
                };

                foreach (var nivel in niveles)
                {
                    context.NivelesEducativos.Add(nivel);
                }

                context.SaveChanges();
            }
        }
    }
}