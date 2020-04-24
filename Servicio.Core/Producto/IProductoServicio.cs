using System.Collections.Generic;

namespace Servicio.Core.Producto
{
    public interface IProductoServicio
    {
        void Insertar(ProductoDto Dto);

        void Eliminar(int Id);

        void Modificar(ProductoDto Dto);

        IEnumerable<ProductoDto> ObtenerPorFiltro(string cadenaBuscar);

        ProductoDto ObenerPorId(int Id);

        bool VerificarSiExiste(int? id, int codigo, string codigoBarra, string descripcion);

        ProductoDto Obtener(long listaPrecioId, string cadenaBuscar);

        IEnumerable<ProductoDto> ObtenerPorListaDePrecio(string cadenabuscar, long listaPrecioId);

        IEnumerable<ProductoDto> ObtenerPorListaDePrecioParaLookUp(long listaPrecioId, string cadena);

        bool Stock(ProductoDto Dto, int cantidad);

        void PedidoProducto(int id, int cantidad, long idProveedor);

        IEnumerable<ProductoDto> ObtenerTodo();

        bool ObtenerPorSubRubro(long subRubroId);

        bool ObtenerPorMarca(long marcaId);

        void AumentarStock(ProductoDto Dto);

        void AumentarStockPorCancelarVenta(ProductoDto Dto, int cantidad);

        int ObtenerSiguienteCodigo();


    }
}