using System.Windows.Forms;

namespace Servicio.Core.Permiso
{
    public interface IPermisoServicio
    {
        bool VerificarAcceso(string usuarioLogin, Form formulario);
    }
}
