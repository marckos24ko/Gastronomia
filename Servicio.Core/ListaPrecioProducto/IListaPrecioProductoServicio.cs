using Servicio.Core.ListaPrecioProducto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.ListaPrecioProducto
{
    public interface IListaPrecioProductoServicio
    {
        void Insertar(ListaPrecioProductoDto dto);

        void Modificar(ListaPrecioProductoDto dto);

        void Eliminar(long id);

        IEnumerable<ListaPrecioProductoDto> Obtener();

        ListaPrecioProductoDto ObtenerPorId(long? ListaPrecioProductoId);

        bool VerificarSiExiste(long? id, string listaPrecio, string producto);

        IEnumerable<ListaPrecioProductoDto> ObtenerPorFiltro(string cadenaBuscar);

        bool VerificarSiListaPrecioEstaUsandose(long? ListaPrecioId);





    }
}
