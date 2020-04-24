using DAL;

namespace Servicio.Core.Mesa
{
    public class MesaDto
    {
        public long Id { get; set; }

        public int Numero { get; set; }

        public string Descripcion { get; set; }

        public EstadoMesa EstadoMesa { get; set; }

        public decimal Total => EstadoMesa == EstadoMesa.Libre || EstadoMesa == EstadoMesa.Reparacion || EstadoMesa == EstadoMesa.Reservada ? 0m : SalonMesa.SaloMesaServicio.ObtenerTotalVenta(Id);

        public long ComprobanteId { get; set; }
    }
}