using Servicio.Core.Reserva.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Reserva
{
    public interface IComprobanteReservaServicio
    {
        void Crear(ReservaDto dto);

        void Modificar(ReservaDto dto);

        void Cerrar(long ReservaId);

        IEnumerable<ReservaDto> ObtenerPorFiltro(string cadenaBuscar);

        ReservaDto obtenerPorId(long reservaId);

        ReservaDto obtenerPorMesa(long mesaId);

        int ObtenerSiguienteCodigo();

        bool VerificarSiExiste(long ReservaId, int codigo);

        void Cancelar(long MesaId);
    }
}
