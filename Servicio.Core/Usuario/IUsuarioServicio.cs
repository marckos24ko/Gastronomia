using Servicio.Core.Usuario.DTO;

namespace Servicio.Core.Usuario
{
    public interface IUsuarioServicio
    {

        bool AutenticarUsuario(string nombre);

        bool AutenticarUsuarioSinAdmin(string nombre);

        bool AutenticarPassword(string nombre, string password);

        void CambiarPassword(string usuario, string password);

        UsuarioDto ObtenerPorId(long id);
    }
}
