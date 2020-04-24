using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Condicion_Iva
{
    public interface ICondicionIvaServicio
    {
        void Insertar(CondicionIvaDto dto);
        void Modificar(CondicionIvaDto dto);
        void Eliminar(long id);

        CondicionIvaDto ObtenerPorId(long id);

        IEnumerable<CondicionIvaDto> ObtenerPorFiltro(string cadenaBuscar);
        IEnumerable<CondicionIvaDto> ObtenerTodo();

        int ObtenerSiguienteCodigo();
        bool VerificarSiExiste(long? id, int codigo, string descripcion);
    }
}
