using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.ListaPprecio;
using Servicio.Core.ListaPrecioProducto;
using Servicio.Core.ListaPrecioProducto.DTO;
using Servicio.Core.PedidoProducto;
using Servicio.Core.Producto;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Lista_Precio_Producto
{
    public partial class _90002_ABM_ListaPrecioProducto : FormularioABM
    {
        private readonly IListaPrecioProductoServicio _listaPrecioProductoServicio;
        private readonly IProductoServicio _productoServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IPedidoProductoServicio _pedidoProductoServicio;
        private ProductoDto _productoSeleccionado;
        public _90002_ABM_ListaPrecioProducto(string _tipoOperacion, long? _entidadId)
            : base(_tipoOperacion, _entidadId)
        {
            InitializeComponent();

            _productoServicio = new ProductoServicio();
            _listaPrecioServicio = new ListaPrecioServicio();
            _listaPrecioProductoServicio = new ListaPrecioProductoServicio();
            _pedidoProductoServicio = new PedidoProductoServicio();

            PoblarComboBox(cmbListaPrecio, _listaPrecioServicio.ObtenerPorFiltro(string.Empty), "Descripcion", "Id");

            PoblarComboBox(cmbProducto, _productoServicio.ObtenerPorFiltro(string.Empty), "Descripcion", "Id");

            Init(_tipoOperacion, _entidadId);

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _productoSeleccionado = (ProductoDto)cmbProducto.SelectedItem;

                if (_productoSeleccionado != null)
                {
                    if (_pedidoProductoServicio.verificarPedidoPorProducto(_productoSeleccionado.Id))
                    {
                        lblPrecioCosto.Text = _pedidoProductoServicio.obtenerPedidoPorProducto(_productoSeleccionado.Id).PrecioCosto.ToString();
                    }
                }
                else
                {
                    lblPrecioCosto.Text = 0m.ToString();
                }
            }

            

            if(_tipoOperacion == Constante.TipoOperacion.Modificar)
            {

                cmbListaPrecio.Enabled = false;
                cmbProducto.Enabled = false;
                DtpFechaActualizacion.Value = DateTime.Now;
                
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                cmbListaPrecio.Enabled = false;
                cmbProducto.Enabled = false;
                txtPrecioPublico.Enabled = false;
            }


            txtPrecioPublico.Text.Trim();
            txtPrecioPublico.KeyPress += Validacion.NoLetras;
            txtPrecioPublico.KeyPress += Validacion.NoSimbolos;
            txtPrecioPublico.KeyPress += Validacion.NoInyeccion;

            txtPrecioPublico.Enter += txt_Enter;
            txtPrecioPublico.Leave += txt_Leave;

        }

        public override void CargarDatos(long? _entidadId)
        {
            var ListaPrecioProducto = _listaPrecioProductoServicio.ObtenerPorId((int)_entidadId);

            {
                cmbListaPrecio.SelectedIndex = cmbListaPrecio.FindString(ListaPrecioProducto.ListaPrecioStr);
                cmbProducto.SelectedIndex = cmbProducto.FindString(ListaPrecioProducto.ProductoStr);
                lblPrecioCosto.Text = ListaPrecioProducto.PrecioCosto.ToString();
                txtPrecioPublico.Text = ListaPrecioProducto.PrecioPublico.ToString();
                DtpFechaActualizacion.Value = ListaPrecioProducto.FechaActualizacion.ToUniversalTime();
                
            }
        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            cmbListaPrecio.Text = null;
            cmbProducto.Text = null;
            lblPrecioCosto.Text = null;
            txtPrecioPublico.Clear();
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(cmbListaPrecio.Text.Trim())) return false;
            if (string.IsNullOrEmpty(cmbProducto.Text)) return false;
            if (string.IsNullOrEmpty(txtPrecioPublico.Text.Trim())) return false;
            if (lblPrecioCosto.Text == 0m.ToString())
            {
                Mensaje.Mostrar("El precio de costo no puede ser 0, realize un pedido del producto para que tenga un precio de costo valido.",Mensaje.Tipo.Error);

                return false;
            }
            if (txtPrecioPublico.Text == 0m.ToString() || txtPrecioPublico.Text == "00" || txtPrecioPublico.Text == "000" || txtPrecioPublico.Text == "0000")
            {
                Mensaje.Mostrar("El precio publico no puede ser 0.", Mensaje.Tipo.Error);

                return false;
            }
            return true;

        }

        public override bool VerificarSiExiste()
        {

            return _listaPrecioProductoServicio.VerificarSiExiste(entidadId, cmbListaPrecio.Text, cmbProducto.Text);
        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                    _listaPrecioProductoServicio.Insertar(new ListaPrecioProductoDto
                    {

                        ListaPrecioId = Convert.ToInt64(cmbListaPrecio.SelectedValue),
                        ProductoId = Convert.ToInt64(cmbProducto.SelectedValue),
                        PrecioCosto = Convert.ToDecimal(lblPrecioCosto.Text),
                        PrecioPublico = Convert.ToDecimal(txtPrecioPublico.Text),
                        FechaActualizacion = DtpFechaActualizacion.Value,

                    }
                    );

                    Mensaje.Mostrar("Los datos se grabaron Correctamente.", Mensaje.Tipo.Informacion);

                    return true;
                
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }

        }

        public override bool EjecutarComandoModificar()
        {

            try
            {

              _listaPrecioProductoServicio.Modificar(new ListaPrecioProductoDto()
              {
                  Id = entidadId.Value,
                  PrecioCosto = Convert.ToDecimal(lblPrecioCosto.Text),
                  PrecioPublico = Convert.ToDecimal(txtPrecioPublico.Text),
                  FechaActualizacion = DtpFechaActualizacion.Value
                  
              });

              Mensaje.Mostrar("Los datos se grabaron Correctamente.", Mensaje.Tipo.Informacion);
                
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Mensaje.Tipo.Error);

                return false;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            try
            {
                _listaPrecioProductoServicio.Eliminar((int)entidadId.Value);

                Mensaje.Mostrar(@"Los datos se eliminaron correctamente", Mensaje.Tipo.Informacion);

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }

        private void cmbProducto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _productoSeleccionado = (ProductoDto)cmbProducto.SelectedItem;

            if (_pedidoProductoServicio.verificarPedidoPorProducto(_productoSeleccionado.Id))
            {
                lblPrecioCosto.Text = _pedidoProductoServicio.obtenerPedidoPorProducto(_productoSeleccionado.Id).PrecioCosto.ToString();
            }

            else
            {
                lblPrecioCosto.Text = 0m.ToString();
            }
        }
    }
}
