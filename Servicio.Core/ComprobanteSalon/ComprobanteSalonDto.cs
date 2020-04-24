using DAL;
using Servicio.Core.Empleado.DTO;
using System.Collections.Generic;

namespace Servicio.Core.ComprobanteSalon
{
    public class ComprobanteSalonDto
    {
        public ComprobanteSalonDto()
        {
            ComprobanteSalonDetalleDtos = new List<ComprobanteSalonDetalleDto>();
        }

        public long Id { get; set; }

        public long MesaId { get; set; }

        public long ClienteId { get; set; }

        public string Cliente { get; set; }

        public long MozoId { get; set; }

        public string MozoStr { get; set; }

        public string MozoLegajo { get; set; }

        public decimal Total { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Descuento { get; set; }

        public EstadoSalon Estado { get; set; }

        public string Fecha { get; set; }

        public List<ComprobanteSalonDetalleDto> ComprobanteSalonDetalleDtos { get; set; }
    }
}
