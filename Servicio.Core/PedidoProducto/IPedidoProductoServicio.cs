using Servicio.Core.PedidoProducto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.PedidoProducto
{
   public interface IPedidoProductoServicio
    {
        void EmitirPedido(decimal Precio, int Cantidad, decimal Total, int ProductoId, long ProveedorId);

        int ObtenerSiguienteNumero();

        PedidoProductoDto obtenerUltimoPedidoEmitido();

        PedidoProductoDto obtenerPedidoPorProducto(long productoId);

        PedidoProductoDto obtenerPdidoPorId(int id);

        bool verificarPedidoPorProducto(long productoId);


    }
}
