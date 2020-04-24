using System.Collections.Generic;
using Servicio.Core.Rubro.DTO;

namespace Servicio.Core.Rubro
{
    public interface IRubroServicio
    {
        void Insertar(RubroDto dto);

        void Eliminar(long RubroId);

        void Modificar(RubroDto dto);

        IEnumerable<RubroDto> ObtenerPorFiltro(string CadenaBuscar);

        RubroDto ObtenerPorId(long RubroId);

        bool VerificarSiExiste(long? RubroId, int codigo, string descripcion);

        long ObtenerSiguienteId();

        int ObtenerSiguienteCodigo();

        IEnumerable<RubroDto> ObtenerTodo();
    }
}