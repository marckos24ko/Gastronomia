using System.Collections.Generic;
using Servicio.Core.SubRubro.DTO;

namespace Servicio.Core.SubRubro
{
    public interface ISubRuroServicio
    {
        void Insertar(SubRubroDto dto);

        void Eliminar(long SubRubroId);

        void Modificar(SubRubroDto dto);

        IEnumerable<SubRubroDto> ObtenerPorFiltro(string CadenaBuscar);

        SubRubroDto ObtenerPorId(long SubRubroId);

        bool VerificarSiExiste(long? SubRubroId, int codigo, string descripcion);

        long ObtenerSiguienteId();

        int ObtenerSiguienteCodigo();

        IEnumerable<SubRubroDto> ObtenerTodo();

        IEnumerable<SubRubroDto> ObtenerPorRubro(long rubroId);

        bool ObtenerPorRubroId(long RubroId);
    }
}