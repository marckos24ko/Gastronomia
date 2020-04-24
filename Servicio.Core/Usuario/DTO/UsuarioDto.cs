namespace Servicio.Core.Usuario.DTO
{
    public class UsuarioDto 
    {
        public long Id { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public bool EstaBloqueado { get; set; }
    }
}
