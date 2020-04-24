namespace Servicio.Core.Cliente.DTO
{
    public class ClienteDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string ApyNom
        {
            get { return $"{Apellido} {Nombre}"; }
        }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Celular { get; set; }

        public string Cuil { get; set; }

        public bool EstaEliminado { get; set; }

        public bool TieneCuentaCorriente { get; set; }

        public decimal? MontoMaximoCtaCte { get; set; }

        public decimal? DeudaTotal { get; set; }

        public bool ActivoParaCompras { get; set; }

        public bool EstaOcupado { get; set; }


    }
}
