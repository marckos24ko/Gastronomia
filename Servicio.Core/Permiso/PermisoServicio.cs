using System;
using System.Windows.Forms;

namespace Servicio.Core.Permiso
{
    public class PermisoServicio : IPermisoServicio
    {
        public bool VerificarAcceso(string usuarioLogin, Form formulario)
        {
            // Esto es una mentira programar despues
            formulario.ShowDialog();
            return true;
        }
    }
}
