using DAL;
using Servicio.Core.FacturaEfectivo.Dto;
using System.Collections.Generic;

namespace Servicio.Core.FacturaEfectivo
{
    public interface IFacturaServicio
    {
        void EmitirFactura(long clienteId, decimal Total, long empleadoId, long? CtaCteId, long ComprobanteId, decimal totalAbonado);

        int ObtenerSiguienteNumero();

        FacturaDto ObtenerUltimaFacturaEmitida();

        IEnumerable<FacturaDto> ObtenerFacturasPorCtaCte(string cadenaBuscar, long CtaCteId, long clienteId);

        IEnumerable<FacturaDto> ObtenerFacturasPagadasCtaCte(string cadenaBuscar, long CtaCteId, EstadoFactura Estado);

        IEnumerable<FacturaDto> ObtenerFacturasImpagadasCtaCte(string cadenaBuscar, long CtaCteId, EstadoFactura Estado, EstadoFactura Estado2);

        IEnumerable<FacturaDto> ObtenerFacturasPorCliente(long? clienteId, string cadenabuscar);

        void ModificarEstado(long facturaId, decimal montoAbonado);




    }
}