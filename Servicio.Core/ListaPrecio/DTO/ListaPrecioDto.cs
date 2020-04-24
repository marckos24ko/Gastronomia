namespace Servicio.Core.ListaPprecio
{
    public class ListaPrecioDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool EstaEliminado { get; set; }
    }
}