using System.Collections.Generic;
using Servicio.Core.Mesa;

namespace Servicio.Core.SalonMesa
{
    public interface ISaloMesaServicio
    {
        IEnumerable<MesaDto> ObtenerMesasParaSalon();

        //decimal ObtenerTotalVenta(long mesaId);
    }
}
