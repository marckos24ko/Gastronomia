using System.Drawing;

namespace Presentacion.Base.Varios
{
    public static class Constante
    {
        public static class ImagenControl
        {
            public static Image ImagenDeFondo =  Recursos.ImagenFondo;
            public static Image BotonSalir = Recursos.Salir;
            public static Image BotonLimpiar = Recursos.Actualizar;
            public static Image BotonGuardar = Recursos.Guardar;
            public static Image BotonEliminar = Recursos.Borrar;
            public static Image Usuario = Recursos.Usuario;
            public static Image BotonNuevo = Recursos.Nuevo;
            public static Image BotonActualizar = Recursos.Actualizar5;
            public static Image BotonImprimir = Recursos.Impresora;
            public static Image BotonModificar = Recursos.Editar;
            public static Image Buscar = Recursos.Buscar;
            public static Image BuscarChico = Recursos.search;
            public static Image CarritoCompra = Recursos.CarritoCompra;
            public static Image Casa = Recursos.Casa2;
            public static Image Camara = Recursos.Camara;
            public static Image Login = Recursos.Login; 
            public static Image Ojo = Recursos.Ojo;
            public static Image Password= Recursos.Password;
            public static Image Bloquear= Recursos.Candado;
            public static Image Formularios = Recursos.Formularios;
            public static Image CajaRegistradora = Recursos.CajaRegistradora;
            public static Image Moneda = Recursos.Moneda;
            public static Image CtaCte = Recursos.Cta_Corriente;


        }

        public static class ColorControl
        {
            public static Color ColorConFoco = Color.Beige;
            public static Color ColorSinFoco = Color.White;
        }

        public static class TipoOperacion
        {
            public const string Nuevo = "Nuevo";
            public const string Eliminar = "Eliminar";
            public const string Modificar = "Modificar";
        }

        public static class Mensaje
        {
            public const string NoHayDatosCargados = "Faltan datos por cargar";
        }
        public static class Seguridad
        {
            public const string PasswordPorDefecto = "123";
            public static string PasswordAdmin = "123";
            public static string UsuarioAdmin = "admin";
        }
    }
}
