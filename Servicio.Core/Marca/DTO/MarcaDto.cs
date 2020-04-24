using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Marca.DTO
{
   public class MarcaDto
    {
        public long id { get; set; }

        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public bool EstaEliminado { get; set; }

    }
}
