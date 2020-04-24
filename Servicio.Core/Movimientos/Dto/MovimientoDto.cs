using DAL;
using System;

namespace Servicio.Core.Movimientos.Dto
{
    public class MovimientoDto
    {
        public long Id { get; set; }

        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Monto { get; set; }

        public TipoMovimiento TipoMovimiento { get; set; }

        public long ClienteId { get; set; }

        public string ClienteApyNom { get; set; }

        public string ProveedorNombre { get; set; }

        public long FacturaId { get; set; }


    }
}
