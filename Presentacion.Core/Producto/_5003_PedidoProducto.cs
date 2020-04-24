using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Movimientos;
using Servicio.Core.PedidoProducto;
using Servicio.Core.Producto;
using Servicio.Core.Proveedores;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class _5003_PedidoProducto : Form
    {
        private readonly IProductoServicio _productoServicio;
        private readonly IProveedorServicio _proveedorServicio;
        private readonly IMovimientoServicio _movimientoServicio;
        private readonly IPedidoProductoServicio _pedidoProductoServicio;
        private ProductoDto _productoSeleccionado;
        private ProveedorDto _proveedorSeleccionado;
        public bool RealizoAlgunaOperacion;

        public _5003_PedidoProducto()
           
        {
            InitializeComponent();

            _productoServicio = new ProductoServicio();
            _proveedorServicio = new ProvedoresServicio();
            _movimientoServicio = new MovimientoServicio();
            _pedidoProductoServicio = new PedidoProductoServicio();

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;

            PoblarComboBox(cmbProducto, _productoServicio.ObtenerTodo(), "Descripcion", "Id");

            txtTotal.Text = (NudPrecioCosto.Value * nudCantidad.Value).ToString("C2");

            if (cmbProducto.SelectedItem != null)
            {
                if (_proveedorServicio.VerificarSiperteneceAlProducto((int)cmbProducto.SelectedValue))
                {
                    lblProveedor.Text = _proveedorServicio.ObtenerPorProducto((int)cmbProducto.SelectedValue).NombreFantasia;
                    _proveedorSeleccionado = _proveedorServicio.ObtenerPorProducto((int)cmbProducto.SelectedValue);
                }

                else
                {
                    lblProveedor.Text = "Ninguno";
                    _proveedorSeleccionado = null;
                }
            }

            else
            {
                lblProveedor.Text = "Ninguno";
                _proveedorSeleccionado = null;
            }

            

            txtEfectivo.KeyPress += Validacion.NoLetras;
            txtEfectivo.KeyPress += Validacion.NoInyeccion;
            txtEfectivo.KeyPress += Validacion.NoSimbolos;



            RealizoAlgunaOperacion = false;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;

        }

        private bool VerificarDatosObligatorios()
        {
            if (cmbProducto.SelectedItem == null)
            {
                Mensaje.Mostrar("Seleecione un Producto", Mensaje.Tipo.Informacion);
                return false;
            }

            if(_proveedorSeleccionado == null)
            {
                Mensaje.Mostrar("Producto no asociado al proveedor, cree un proveedor con un mismo rubro que el producto seleccionado para poder asociarlo.", Mensaje.Tipo.Error);
                return false;
            }
            return true;
        }

        private void PoblarComboBox(ComboBox cmb, object obj, string display, string valorDevuelto)
        {
            cmb.DataSource = obj;
            cmb.DisplayMember = display;
            cmb.ValueMember = valorDevuelto;
        }

        private void cmbProducto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null)
            {
                if (_proveedorServicio.VerificarSiperteneceAlProducto((int)cmbProducto.SelectedValue))
                {
                    lblProveedor.Text = _proveedorServicio.ObtenerPorProducto((int)cmbProducto.SelectedValue).NombreFantasia;
                    _proveedorSeleccionado = _proveedorServicio.ObtenerPorProducto((int)cmbProducto.SelectedValue);
                }

                else
                {
                    lblProveedor.Text = "Ninguno";
                    _proveedorSeleccionado = null;
                }
            }

            else
            {
                lblProveedor.Text = "Ninguno";
                _proveedorSeleccionado = null;
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            var total = NudPrecioCosto.Value * nudCantidad.Value;

            if (VerificarDatosObligatorios())
            {

                if (string.IsNullOrEmpty(txtEfectivo.Text.Trim()))
                {
                    Mensaje.Mostrar(@"Ingrese un valor en el campo 'Efectivo'.", Mensaje.Tipo.Informacion);
                }

                else
                {
                    var Efectivo = decimal.Parse(txtEfectivo.Text);

                    if (Efectivo >= total)
                    {
                        _pedidoProductoServicio.EmitirPedido(NudPrecioCosto.Value, (int)nudCantidad.Value, total, _productoSeleccionado.Id, _proveedorSeleccionado.Id);

                        _productoServicio.PedidoProducto((int)cmbProducto.SelectedValue, (int)nudCantidad.Value, _proveedorSeleccionado.Id);

                        _movimientoServicio.EmitirMovimiento(null, total, TipoMovimiento.Egreso, null, _proveedorSeleccionado.Id);

                        txtEfectivo.Clear();

                        var pedido = _pedidoProductoServicio.obtenerUltimoPedidoEmitido();

                        var proveedor = _proveedorSeleccionado.NombreFantasia;

                        var form = new _5004_ComprobantePago(pedido, total, Efectivo, (Efectivo - total), proveedor);

                        form.ShowDialog();

                        if (form.realizoAlgunaOperacion == true)
                        {
                            Close();
                        }

                    }

                    else
                    {
                        Mensaje.Mostrar(@"No se puede realizar el pedido por montos insuficientes", Mensaje.Tipo.Informacion);
                    }
                }
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            _productoSeleccionado = (ProductoDto)cmbProducto.SelectedItem;
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (NudPrecioCosto.Value * nudCantidad.Value).ToString("C2");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NudPrecioCosto_ValueChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (NudPrecioCosto.Value * nudCantidad.Value).ToString("C2");
        }
    }

}