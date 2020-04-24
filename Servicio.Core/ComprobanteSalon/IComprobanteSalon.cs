using Servicio.Core.Producto;

namespace Servicio.Core.ComprobanteSalon
{
    public interface IComprobanteSalon
    {
        void Crear(long mesaId, long empleadoId, long clienteId);

        void Eliminar(long id, long mesaId);

        void AgregarItem(long comprobanteId, int cantidad, ProductoDto producto, long empleadoId);

        void DisminuirItem(long comprobanteId, int cantidad, ComprobanteSalonDetalleDto producto);

        void Cerrar(long Id);

        ComprobanteSalonDto ObtenerComprobantePorMesaSinFacturar(long mesaId);

        ComprobanteSalonDto ObtenerComprobantePorId(long Id);

        bool ObtenerComprobantesSinFacturar();

        void modificar(long mesaOcupadaId, long mesaLibreId);

        void obtenerDescuento(decimal Descuento, long mesaId);

        bool ObtenerComprobantesSinFacturarPorMozo(long mozoId);

        bool ObtenerComprobantesSinFacturarPorProdcuto(long productoId);
    }
}
