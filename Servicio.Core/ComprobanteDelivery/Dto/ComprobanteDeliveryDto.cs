using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Delivery.Dto
{
   public class ComprobanteDeliveryDto
    {

        public ComprobanteDeliveryDto()
        {
            ComprobanteDeliveryDetalletos = new List<ComprobanteDeliveryDetalleDto>();
        }

        public long Id { get; set; }

        public long ClienteId { get; set; }

        public String ClienteStr { get; set; }

        public decimal Total { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Descuento { get; set; }

        public string  DireccionEnvio { get; set; }

        public DateTime Fecha { get; set; }

        public string Observacion { get; set; }

        public EstadoDelivery EstadoDelivery { get; set; }

        public long CadeteId { get; set; }

        public string Deliverie { get; set; }

        public string DelievirieLegajo { get; set; }

        public List<ComprobanteDeliveryDetalleDto> ComprobanteDeliveryDetalletos { get; set; }


    }
}
