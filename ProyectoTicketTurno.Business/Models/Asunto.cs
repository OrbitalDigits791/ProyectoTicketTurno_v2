using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTicketTurno.Business.Models
{
    public class Asunto
    {
        // PK: Asuntos.IdAsunto
        public int IdAsunto { get; set; }

        public string Descripcion { get; set; }

        public string Categoria { get; set; }

        public bool Activo { get; set; }

        public Asunto()
        {
            Activo = true;
        }
    }
}
