using System.Collections.Generic;
using Servicio.Core.Marca.DTO;

namespace Servicio.Core.Marca
{
    public interface IMarcaServicio
    {
        void Insertar(MarcaDto dto);

        void Modificar(MarcaDto dto);

        void Eliminar(long Id);

        MarcaDto ObtenerPorid(long MarcaId);

        int ObtenerSiguienteCodigo();

        IEnumerable<MarcaDto> ObtenerPorFiltro(string cadenaBuscar);

        IEnumerable<MarcaDto> ObtenerTodo();

        bool VerificarSiExiste(long? MarcaId, int codigo, string descripcion);

    }
}