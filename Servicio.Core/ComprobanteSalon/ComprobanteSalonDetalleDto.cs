namespace Servicio.Core.ComprobanteSalon
{
    public class ComprobanteSalonDetalleDto
    {
        public long Id { get; set; }

        public long ComprobanteSalonId { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal { get; set; }

        public string Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int ProductoId { get; set; }
    }
}
