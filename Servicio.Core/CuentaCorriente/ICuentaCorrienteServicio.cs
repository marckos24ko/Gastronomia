using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.CuentaCorriente.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.CuentaCorriente
{
    public interface ICuentaCorrienteServicio
    {
        void CrearCuentaCorriente(long ClienteId);
        
        void SuspenderCuentaCorriente(long ctaCteId, long ClienteId);

        void PagarFactura(long facturaId, decimal MontoDinero, long empleadoId);

        int ObtenerSiguienteNumero();

        CuentaCorrienteDto ObtenerCuentaCorrientePorClienteId(long ClienteId);

        void ReactivarCuentaCorriente(long ctaCteId, long ClienteId);

        CuentaCorrienteDto ObtenerCuentaCorrientePorClienteIdSinFiltro(long ClienteId);

        bool verificarSiTieneCtaCte(long ClienteId);




    }
}
