using System.Collections.Generic;
using DAL;

namespace Servicio.Core.Mesa
{
    public interface IMesaServicio
    {
        void Insertar(int numero, string descripcion);
        void Insertar(MesaDto dto);

        void Modificar(long id, int numero, string descripcion);
        void Modificar(MesaDto dto);

        void Eliminar(long id);

        MesaDto ObtenerPorId(long id);
        IEnumerable<MesaDto> ObtenerPorFiltro(string cadenaBuscar);
        IEnumerable<MesaDto> ObtenerTodo();
        IEnumerable<MesaDto> ObtenerMesasLibres(string cadenaBuscar);

        bool VerificarSiExiste(long? id, int numero, string descripcion);

        int ObtenerSiguienteNumero();

        void CambiarEstado(long id, EstadoMesa estado);

        bool VerificarSiEstaUsandose(long id, EstadoMesa estado);
    }
}
