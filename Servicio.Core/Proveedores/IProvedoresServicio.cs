using System.Collections.Generic;

namespace Servicio.Core.Proveedores
{
    public interface IProveedorServicio
    {
        void Insertar(ProveedorDto dto);

        void Modificar(ProveedorDto dto);

        void Eliminar(long id);

        ProveedorDto ObtenerPorId(long id);

        IEnumerable<ProveedorDto> ObtenerPorFiltro(string cadenaBuscar);

        IEnumerable<ProveedorDto> ObtenerTodo();

        int ObtenerSiguienteCodigo();  //por el momento no se utiliza

        bool VerificarSiExiste(long? id, string nombre, string cuit);

        ProveedorDto ObtenerPorProducto(int idProducto);

        bool obtenerPorCondicionIva(long condicionIvaId);

        bool obtenerPorRubro(long rubroId);

        bool VerificarSiperteneceAlProducto(long idProducto);

    }
}
