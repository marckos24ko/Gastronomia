using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.SubRubro.DTO
{
    public class SubRubroDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }
        
        public long RubroId { get; set; }

        public String RubroStr { get; set; }
                           
        public bool EstaEliminado { get; set; }
    }
}
