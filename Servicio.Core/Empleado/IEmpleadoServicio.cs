using System.Collections.Generic;
using DAL;
using Servicio.Core.Empleado.DTO;

namespace Servicio.Core.Empleado
{
    public interface IEmpleadoServicio
    {
        void Insertar(EmpleadoDto dto);

        void Modificar(EmpleadoDto dto);

        IEnumerable<EmpleadoDto> ObtenerPorFiltro(string cadenaBuscar);

        EmpleadoDto obtenerPorId(long? Empleadoid);

        IEnumerable<EmpleadoDto> obtenerMozos(TipoEmpleado mozo);

        EmpleadoDto obtenerMozosPorFiltro(TipoEmpleado mozo, string cadenaBuscar);

        IEnumerable<EmpleadoDto> obtenerCadetes(TipoEmpleado cadete);

        EmpleadoDto obtenerCadetesPorFiltro(TipoEmpleado cadete, string cadenaBuscar);

        int ObtenerSiguienteLegajo();

        bool VerificarSiExiste(long? empleadoId, int legajo, string cuil);

        void Eliminar(long id);
    }
}