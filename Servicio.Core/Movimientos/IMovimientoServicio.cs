using DAL;
using Servicio.Core.Movimientos.Dto;
using System.Collections.Generic;

namespace Servicio.Core.Movimientos
{
    public interface IMovimientoServicio
    {
        void EmitirMovimiento(long? clienteId, decimal monto, TipoMovimiento TipoMovimiento, long? facturaId, long? proveedorId);

        int ObtenerSiguienteNumero();

        IEnumerable<MovimientoDto> ObtenerMovimientoPorFacturaId(long? facturaId, string cadenaBuscar);

        IEnumerable<MovimientoDto> ObtenerMovimientoPorProveedorId(long? proveedorId, string cadenaBuscar);



    }
}