using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.ListaPprecio;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.ListaPrecio
{
    public partial class _50002_ABM_ListaPrecios : FormularioABM
    {
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private long? _listaPrecioId;

        public _50002_ABM_ListaPrecios(string _tipoOperacion, long? _entidadId)
            : base(_tipoOperacion, _entidadId)
        {
            InitializeComponent();

            _listaPrecioServicio = new ListaPrecioServicio();
            
            Init(_tipoOperacion, _entidadId);

            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _listaPrecioId = entidadId;
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _listaPrecioId = null;
                nudCodigo.Value = _listaPrecioServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _listaPrecioId = entidadId;
            }

            txtDescripcion.KeyPress += Validacion.NoNumeros;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoInyeccion;

            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Leave;

            txtDescripcion.Text.Trim();

        }

        public override void CargarDatos(long? _entidadId)
        {
            var ListaPrecio = _listaPrecioServicio.ObtenerPorId(_entidadId);


            nudCodigo.Value = ListaPrecio.Codigo;
            txtDescripcion.Text = ListaPrecio.Descripcion;
        }

        public override void ObtenerSiguienteCodigo()
        {

            nudCodigo.Value = _listaPrecioServicio.ObtenerSiguienteCodigo();
        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            txtDescripcion.Clear();
        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                _listaPrecioServicio.Insertar(new ListaPrecioDto
                {

                 Codigo = (int)nudCodigo.Value,
                 Descripcion = txtDescripcion.Text
                });

                Mensaje.Mostrar("Los datos se grabaron Correctamente.", Mensaje.Tipo.Informacion);
                return true;

            }
            catch (Exception ex)
            {
                Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);

                return false;
                
            }
        }

        public override bool EjecutarComandoModificar()
        {
            try
            {           
                   _listaPrecioServicio.Modificar(new ListaPrecioDto()
                    {
                          Id = entidadId.Value,
                          Descripcion = txtDescripcion.Text

                    });

                Mensaje.Mostrar("Los datos se grabaron Correctamente.", Mensaje.Tipo.Informacion);


                return true;
            }

            catch (Exception ex)
            {
                Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            try
            {
             
                _listaPrecioServicio.Eliminar(entidadId.Value);

                Mensaje.Mostrar("Los datos se Eliminaron Correctamente.", Mensaje.Tipo.Informacion);

                return true;
            }
            catch
                (Exception ex)
            {
                Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim())) return false;

            return true;
        }

        public override bool VerificarSiExiste()
        {
            return _listaPrecioServicio.VerificarSiExiste(_listaPrecioId, (int)nudCodigo.Value, txtDescripcion.Text);
        }

    }
}

