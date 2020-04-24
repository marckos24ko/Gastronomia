using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.PedidoProducto.Dto
{
    public class PedidoProductoDto
    {

        public long Id { get; set; }

        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public decimal PrecioCosto { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }

        public long ProveedorId { get; set; }

        public long ProductoId { get; set; }

        public string ProveedorApyNom { get; set; }

        public string ProductoStr { get; set; }



    }
}
