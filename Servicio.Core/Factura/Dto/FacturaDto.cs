using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.FacturaEfectivo.Dto
{
    public class FacturaDto
    {

        public long Id { get; set; }

        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; }

        public decimal Total { get; set; }

        public decimal TotalAbonado { get; set; }

        public long ClienteId { get; set; }

        public string CLienteApynom { get; set; }

        public string EmpleadoApynom { get; set; }

        public long EmpleadoId { get; set; }

        public long ComprobanteId { get; set; }

        public long? CuentaCorrienteId { get; set; }


    }
}
