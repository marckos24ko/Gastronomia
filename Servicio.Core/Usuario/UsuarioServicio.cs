using System;
using System.Linq;
using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Usuario.DTO;

namespace Servicio.Core.Usuario
{
    public class UsuarioServicio : IUsuarioServicio
    {
         public bool AutenticarPassword(string nombre, string password)
        {
            try
            {
                using (var context = new ModeloGastronomiaContainer())
                {
                    var usuario = context.Usuarios.Any(x => x.Nombre == nombre
                                                       && !x.EstaBloqueado)
                                                       || (Constante.Seguridad.UsuarioAdmin == nombre);

                    if (usuario == true)
                    {
                        var clave = context.Usuarios.Any(x => x.Nombre == nombre
                                                    && x.Password == password
                                                    && !x.EstaBloqueado)
                                                    || (Constante.Seguridad.UsuarioAdmin == nombre
                                                        && Constante.Seguridad.PasswordAdmin == password);

                        if (clave == true)
                        {
                            return true;
                        }
                    }

                    return false;
                                                           
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AutenticarUsuario(string nombre)
        {
            try
            {
                using (var context = new ModeloGastronomiaContainer())
                {
                    return context.Usuarios.Any(x => x.Nombre == nombre
                                                       && !x.EstaBloqueado)
                                                       || (Constante.Seguridad.UsuarioAdmin == nombre);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AutenticarUsuarioSinAdmin(string nombre)
        {
            try
            {
                using (var context = new ModeloGastronomiaContainer())
                {
                    return context.Usuarios.Any(x => x.Nombre == nombre
                                                       && !x.EstaBloqueado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarPassword(string usuario, string password)
        {
            try
            {
                using (var context = new ModeloGastronomiaContainer())
                {
                    var _usuario = context.Usuarios.FirstOrDefault(x => x.Nombre == usuario
                                                       && !x.EstaBloqueado || (Constante.Seguridad.UsuarioAdmin == usuario));

                    if (_usuario != null)
                    {
                        _usuario.Password = password;

                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuarioDto ObtenerPorId(long id)
        {
            using(var context = new ModeloGastronomiaContainer())
            {
                var cuenta = context.Usuarios.FirstOrDefault(x => x.Id == id);

                return new UsuarioDto()
                {
                    Id = cuenta.Id,
                    Usuario = cuenta.Nombre,
                    Password = cuenta.Password,
                    EstaBloqueado = cuenta.EstaBloqueado
                };
            }    
        }
    }
}
