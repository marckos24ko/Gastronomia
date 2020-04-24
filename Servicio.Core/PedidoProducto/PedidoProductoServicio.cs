using DAL;
using Servicio.Core.PedidoProducto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.PedidoProducto
{
    public class PedidoProductoServicio : IPedidoProductoServicio
    {
        public void EmitirPedido(decimal Precio, int Cantidad, decimal Total, int ProductoId, long ProveedorId)
        {

            using (var context = new ModeloGastronomiaContainer())
            {
                var PedidoNuevo = new DAL.PedidoProducto
                {

                    Numero = ObtenerSiguienteNumero(),
                    Fecha = DateTime.Now,
                    PrecioCosto = Precio,
                    Cantidad = Cantidad,
                    Total = Total,
                    ProductoId = ProductoId,
                    ProveedorId = ProveedorId,


                };

                context.PedidosProducto.Add(PedidoNuevo);

                context.SaveChanges();

            }
        }

        public int ObtenerSiguienteNumero()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.PedidosProducto.Any() ? context.PedidosProducto.Max(x => x.Numero) + 1 : 1;
            }
        }

        public PedidoProductoDto obtenerUltimoPedidoEmitido()
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var numero = context.PedidosProducto.Max(x => x.Numero);

                var pedido = context.PedidosProducto
                            .FirstOrDefault(x => x.Numero == numero);

                var pedidoDto = new PedidoProductoDto()
                {

                Id = pedido.Id,
                Cantidad = pedido.Cantidad,
                Fecha = pedido.Fecha,
                Numero = pedido.Numero,
                PrecioCosto = pedido.PrecioCosto,
                ProductoId = pedido.ProductoId,
                ProductoStr = pedido.Producto.Descripcion,
                ProveedorApyNom = pedido.Proveedor.ApyNomContacto,
                ProveedorId = pedido.ProveedorId,
                Total = pedido.Total

                };

                return pedidoDto;

             
            }
        }

        public PedidoProductoDto obtenerPdidoPorId(int id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var pedido = context.PedidosProducto
                            .FirstOrDefault(x => x.Id == id);

                var pedidoDto = new PedidoProductoDto()
                {
                    Id = id,
                    Cantidad = pedido.Cantidad,
                    Fecha = pedido.Fecha,
                    Numero = pedido.Numero,
                    PrecioCosto = pedido.PrecioCosto,
                    ProductoId = pedido.ProductoId,
                    ProductoStr = pedido.Producto.Descripcion,
                    ProveedorApyNom = pedido.Proveedor.ApyNomContacto,
                    ProveedorId = pedido.ProveedorId,
                    Total = pedido.Total

                };

                return pedidoDto;


            }
        }

        public PedidoProductoDto obtenerPedidoPorProducto(long productoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var pedido = context.PedidosProducto
                            .FirstOrDefault(x => x.ProductoId == productoId);

                var pedidoDto = new PedidoProductoDto()
                {

                    Id = pedido.Id,
                    Cantidad = pedido.Cantidad,
                    Fecha = pedido.Fecha,
                    Numero = pedido.Numero,
                    PrecioCosto = pedido.PrecioCosto,
                    ProductoId = pedido.ProductoId,
                    ProductoStr = pedido.Producto.Descripcion,
                    ProveedorApyNom = pedido.Proveedor.ApyNomContacto,
                    ProveedorId = pedido.ProveedorId,
                    Total = pedido.Total

                };

                return pedidoDto;


            }
        }

        public bool verificarPedidoPorProducto(long productoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var pedido = context.PedidosProducto
                            .Any(x => x.ProductoId == productoId);

                return pedido;

            }
        }
        
    }
}
