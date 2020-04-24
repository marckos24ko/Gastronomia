using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Delivery.Dto
{
    public class ComprobanteDeliveryDetalleDto
    {
        public long Id { get; set; }

        public long ComprobanteDeliveryId { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal { get; set; }

        public string Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int ProductoId { get; set; }
    }
}
