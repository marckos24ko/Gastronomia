using System.Collections.Generic;
using Servicio.Core.Cliente.DTO;

namespace Servicio.Core.Cliente
{
    public interface IClienteServicio
    {
        void Insertar(ClienteDto dto);

        void Modificar(ClienteDto dto);

        void ClienteOcupado(ClienteDto dto);

        void ClienteDesocupado(long ClienteId);

        IEnumerable<ClienteDto> ObtenerPorFiltro(string cadenaBuscar);

        IEnumerable<ClienteDto> ObtenerClientesDesocupados(string cadenaBuscar);

        IEnumerable<ClienteDto> ObtenerClientesDesocupadosSinFiltro();

        ClienteDto obtenerPorId(long ClienteId);

        ClienteDto obtenerPorCodigo(int codigo);

        int ObtenerSiguienteCodigo();

        bool VerificarSiExiste(long? id, int codigo, string cuil);

        long obtenerSiguienteId();

        void Eliminar(long ClienteId);

        bool VerificarSiTieneDeuda(long ClienteId);

        bool VerificarSiEstaOcupado(long ClienteId);

        void ModificarMontoCtaCte(decimal montoGastado, long clienteId);

        void AgregarGastoActual(long ClienteId, decimal Gasto);

        void RestarPagoActual(long ClienteId, decimal pago);

        void DesactivarParaCompras(long ClienteId);

        void ActivarParaCompras(long ClienteId);

        void ActivarEstadoTieneCtaCte(long ClienteId);

        void DesactivarEstadoTieneCtaCte(long ClienteId);
    }
}