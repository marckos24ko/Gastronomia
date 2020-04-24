using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Servicio.Core.ListaPprecio
{
    public interface IListaPrecioServicio
    {
        IEnumerable<ListaPrecioDto> Obtener();

        IEnumerable<ListaPrecioDto> ObtenerPorFiltro(string cadenaBuscar);

        ListaPrecioDto ObtenerPorId(long? ListaPrecioId);

        void Insertar(ListaPrecioDto dto);

        void Modificar(ListaPrecioDto dto);

        void Eliminar(long ListaPrecioId);

        int ObtenerSiguienteCodigo();

        bool VerificarSiExiste(long? ListaPrecioId, int? codigo, string descripcion);

       


    }
}
