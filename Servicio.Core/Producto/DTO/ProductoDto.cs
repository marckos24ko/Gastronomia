using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Producto
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public int Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public decimal PrecioPublico { get; set; }

        public bool EstaEliminado { get; set; }

        public long MarcaId { get; set; }

        public long SubRubroId { get; set; }

        public long RubroId { get; set; }

        public string MarcaStr { get; set; }

        public string SubRubroStr { get; set; }

        public string RubroStr { get; set; }

        public long ListaPrecioId { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public virtual ICollection<DAL.Proveedor> Proveedores { get; set; }
    }
}
