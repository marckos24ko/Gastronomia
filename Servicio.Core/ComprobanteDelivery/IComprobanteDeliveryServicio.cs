using Servicio.Core.Delivery.Dto;
using Servicio.Core.Producto;
using System.Collections.Generic;

namespace Servicio.Core.ComprobanteDelivery
{
    public interface IComprobanteDeliveryServicio
    {
        void Crear(long clienteId, long empleadoId);

        void Eliminar(long clienteId);

        void AgregarItem(long comprobanteId, int cantidad, ProductoDto producto, long empleadoId);

        void DisminuirItem(long comprobanteId, int cantidad, ComprobanteDeliveryDetalleDto producto);

        void Cerrar(long Id);

        ComprobanteDeliveryDto ObtenerComprobantePorCLienteSinFacturar(long ClienteId);

        ComprobanteDeliveryDto ObtenerComprobantePorId(long id);

        void obtenerDescuento(decimal Descuento, long cliemteId);

        void ObtenerObservacion(string Observacion, long clienteId);

        IEnumerable<ComprobanteDeliveryDto> ObtenerPorFiltro(string cadenaBuscar);

        bool ObtenerComprobantesSinFacturar();

        bool ObtenerComprobantesSinFacturarPorDelivery(long cadeteId);

        bool ObtenerComprobantesSinFacturarPorProdcuto(long productoId);
    }
}
