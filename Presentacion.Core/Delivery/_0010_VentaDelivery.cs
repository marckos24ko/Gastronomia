using Presentacion.Base;
using Presentacion.Base.Varios;
using Presentacion.Core.Producto;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.Delivery.Dto;
using Servicio.Core.Empleado;
using Servicio.Core.ListaPprecio;
using Servicio.Core.Producto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Delivery
{
    public partial class _0010_VentaDelivery : FormularioBase
    {
        private ComprobanteDeliveryDto _comprobante;
        private readonly IComprobanteDeliveryServicio _comprobanteDelivery;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IClienteServicio _clienteServicio;
        private readonly IProductoServicio _productoServicio;
        private long _empleadoSeleccionadoId;
        private ClienteDto _clienteSeleccionado; 
        private ComprobanteDeliveryDetalleDto _productoSeleccionado;
        private bool _realizoAlgunaOperacion;

        public bool RealizoAlgunaOperacion { get { return _realizoAlgunaOperacion; } }

        public _0010_VentaDelivery(ClienteDto Cliente, long EmpleadoId)
        {
            InitializeComponent();

            _clienteSeleccionado = Cliente;
            _empleadoSeleccionadoId = EmpleadoId;
            _comprobante = new ComprobanteDeliveryDto();
            _productoServicio = new ProductoServicio();
            _productoSeleccionado = new ComprobanteDeliveryDetalleDto();
            _empleadoServicio = new EmpleadoServicio();
            _clienteServicio = new ClienteServicio();
            _comprobanteDelivery = new ComprobanteDeliveryServicio();
            _listaPrecioServicio = new ListaPrecioServicio();
            _realizoAlgunaOperacion = false;

            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoInyeccion;

            txtObservacion.KeyPress += Validacion.NoInyeccion;


            txtObservacion.Enter += txt_Enter;
            txtObservacion.Leave += txt_Leave;

            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Leave;

            nudCantidad.Enter += txt_Enter;
            nudCantidad.Leave += txt_Leave;

            nudDescuento.Enter += txt_Enter;
            nudDescuento.Leave += txt_Leave;

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void CargarDatos()
        {
            _comprobante = _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_clienteSeleccionado.Id);

            var Subtotal = _comprobante.ComprobanteDeliveryDetalletos.Sum(x => x.SubTotal);

            var descuento = _comprobante.Descuento;

            PoblarComboBox(cmbListaPrecio, _listaPrecioServicio.Obtener(), "Descripcion", "Id");
            
            dgvGrilla.DataSource = _comprobante.ComprobanteDeliveryDetalletos.ToList();

            txtObservacion.Text = _comprobante.Observacion.ToString();

            if (_comprobante.Deliverie != null && dgvGrilla.RowCount > 0)
            {
                lblEmpleado.Text = _comprobante.Deliverie;
                lblCliente.Text = _clienteSeleccionado.ApyNom;

            }
            else
            {

                lblEmpleado.Text = _comprobante.Deliverie;
                lblCliente.Text = _clienteSeleccionado.ApyNom;
            }

            txtTotal.Text = (Subtotal - (Subtotal * descuento / 100m)).ToString("C2");
            txtFecha.Text = Convert.ToString(DateTime.Now);
            txtSubTotal.Text = Subtotal.ToString("C2");
            nudDescuento.Value = descuento;


        }

        private void ActualizarTotalizadores()
        {
            _comprobante = _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_clienteSeleccionado.Id);

            var Subtotal = _comprobante.ComprobanteDeliveryDetalletos.Sum(x => x.SubTotal);

            var descuento = _comprobante.Descuento;

            txtTotal.Text = (Subtotal - (Subtotal * descuento / 100m)).ToString("C2");

            txtSubTotal.Text = Subtotal.ToString("C2");

            dgvGrilla.DataSource = _comprobante.ComprobanteDeliveryDetalletos.ToList();

        }

        public void ActualizarGrilla()
        {
            dgvGrilla.DataSource = _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_clienteSeleccionado.Id).ComprobanteDeliveryDetalletos.ToList();

            if (dgvGrilla.Rows.Count == 0)
            {
                _productoSeleccionado.Cantidad = 0;

                nudPrecio.Value = 0m;
            }
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Código";
            dgvGrilla.Columns["Codigo"].Width = 50;
            dgvGrilla.Columns["Codigo"].DisplayIndex = 0;

            dgvGrilla.Columns["CodigoBarra"].Visible = true;
            dgvGrilla.Columns["CodigoBarra"].HeaderText = @"Código de barra";
            dgvGrilla.Columns["CodigoBarra"].Width = 150;
            dgvGrilla.Columns["CodigoBarra"].DisplayIndex = 1;

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";
            dgvGrilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["Descripcion"].DisplayIndex = 2;

            dgvGrilla.Columns["Precio"].Visible = true;
            dgvGrilla.Columns["Precio"].HeaderText = @"Precio";
            dgvGrilla.Columns["Precio"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["Precio"].Width = 50;
            dgvGrilla.Columns["Precio"].DisplayIndex = 4;

            dgvGrilla.Columns["Cantidad"].Visible = true;
            dgvGrilla.Columns["Cantidad"].HeaderText = @"Cantidad";
            dgvGrilla.Columns["Cantidad"].Width = 50;
            dgvGrilla.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["Cantidad"].DisplayIndex = 5;


            dgvGrilla.Columns["SubTotal"].Visible = true;
            dgvGrilla.Columns["SubTotal"].HeaderText = @"SubTotal";
            dgvGrilla.Columns["SubTotal"].Width = 100;
            dgvGrilla.Columns["SubTotal"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["SubTotal"].DisplayIndex = 6;
        }

        private IEnumerable<ProductoDto> BuscarProducto(string text, long listaId)
        {
            return _productoServicio.ObtenerPorListaDePrecio(text, listaId);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Enter != e.KeyChar) return;

            if (cmbListaPrecio.SelectedValue != null)
            {

                var listaId = (long)cmbListaPrecio.SelectedValue;

                var listaProducto = BuscarProducto(txtDescripcion.Text, listaId);

                {
                    if (listaProducto.Count() == 1)
                    {
                        var producto = new ProductoDto();

                        foreach (var item in listaProducto)
                        {
                            producto.Id = item.Id;
                            producto.Codigo = item.Codigo;
                            producto.CodigoBarra = item.CodigoBarra;
                            producto.Descripcion = item.Descripcion;
                            producto.Stock = item.Stock;
                            producto.ListaPrecioId = listaId;
                            producto.PrecioPublico = item.PrecioPublico;
                            producto.FechaActualizacion = item.FechaActualizacion;
                        };

                        // Cargar Datos

                        var cantidad = nudCantidad.Value;

                        if (_productoServicio.Stock(producto, (int)cantidad))
                        {
                            nudPrecio.Value = producto.PrecioPublico;
                            _comprobanteDelivery.AgregarItem(_comprobante.Id, (int)cantidad, producto, _empleadoSeleccionadoId);
                            _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);
                            ActualizarGrilla();
                            txtDescripcion.Focus();

                            ActualizarTotalizadores();
                        }

                        else
                        {
                            Mensaje.Mostrar("No hay stock suficiente.", Mensaje.Tipo.Informacion);
                        }

                    }

                    else
                    {
                        var lookUpProducto = new Producto_LookUp(listaId);
                        lookUpProducto.ShowDialog();

                        var prod = (ProductoDto)lookUpProducto.elementoSeleccionado;

                        var cantidad = nudCantidad.Value;

                        if (lookUpProducto.Entidad != null)
                        {

                            if (_productoServicio.Stock(prod, (int)cantidad))
                            {

                                var productoSeleccionado = (ProductoDto)lookUpProducto.Entidad;
                                txtDescripcion.Text = productoSeleccionado.Descripcion;
                                nudPrecio.Value = productoSeleccionado.PrecioPublico;
                                _comprobanteDelivery.AgregarItem(_comprobante.Id, (int)cantidad, productoSeleccionado, _empleadoSeleccionadoId);
                                _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);
                                ActualizarGrilla();

                                ActualizarTotalizadores();
                            }

                            else
                            {
                                Mensaje.Mostrar("No hay stock suficiente", Mensaje.Tipo.Informacion);
                            }
                        }

                        ActualizarTotalizadores(); // aqui llamo este metodo para que en tiempo real actualize el nud total
                    }
                }
            }

            else
            {
                Mensaje.Mostrar("No hay una lista de precio seleecionada.", Mensaje.Tipo.Informacion);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                Mensaje.Mostrar("Ingrese un código del producto por favor", Mensaje.Tipo.Informacion);
                txtDescripcion.Focus();
            }

            else
            {
                if (cmbListaPrecio.SelectedValue != null)
                {

                    var listaId = (long)cmbListaPrecio.SelectedValue;

                    var listaProducto = BuscarProducto(txtDescripcion.Text, listaId);

                    var producto = new ProductoDto();

                    foreach (var item in listaProducto)
                    {
                        producto.Id = item.Id;
                        producto.Codigo = item.Codigo;
                        producto.CodigoBarra = item.CodigoBarra;
                        producto.Descripcion = item.Descripcion;
                        producto.Stock = item.Stock;
                        producto.ListaPrecioId = listaId;
                        producto.PrecioPublico = item.PrecioPublico;
                        producto.FechaActualizacion = item.FechaActualizacion;
                    };


                    if (listaProducto.Count() == 1)
                    {
                        // Cargar Datos

                        var cantidad = nudCantidad.Value;

                        if (_productoServicio.Stock((ProductoDto)producto, (int)cantidad))
                        {

                            _comprobanteDelivery.AgregarItem(_comprobante.Id, (int)cantidad, (ProductoDto)producto, _empleadoSeleccionadoId);
                            _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);
                            ActualizarGrilla();

                            ActualizarTotalizadores();
                        }

                        else
                        {
                            Mensaje.Mostrar("No hay stock suficiente", Mensaje.Tipo.Informacion);
                        }


                    }

                    else
                    {
                        var lookUpProducto = new Producto_LookUp(listaId);
                        lookUpProducto.ShowDialog();

                        var prod = (ProductoDto)lookUpProducto.elementoSeleccionado;

                        var cantidad = nudCantidad.Value;

                        if (lookUpProducto.Entidad != null)
                        {

                            if (_productoServicio.Stock(prod, (int)cantidad))
                            {

                                var productoSeleccionado = (ProductoDto)lookUpProducto.Entidad;
                                txtDescripcion.Text = productoSeleccionado.Descripcion;
                                nudPrecio.Value = productoSeleccionado.PrecioPublico;
                                _comprobanteDelivery.AgregarItem(_comprobante.Id, (int)cantidad, productoSeleccionado, _empleadoSeleccionadoId);
                                _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);
                                ActualizarGrilla();

                                ActualizarTotalizadores();
                            }

                            else
                            {
                                Mensaje.Mostrar("No hay stock suficiente", Mensaje.Tipo.Informacion);
                            }
                        }
                    }

                    ActualizarTotalizadores(); // aqui llamo este metodo para que en tiempo real el nud total vaya cambiando

                }

                else
                {
                    MessageBox.Show("No hay una lista de precio seleecionada", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                _productoSeleccionado = (ComprobanteDeliveryDetalleDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;

            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);

            _comprobanteDelivery.ObtenerObservacion(txtObservacion.Text.Trim(), _clienteSeleccionado.Id);

            if (dgvGrilla.RowCount > 0)
            {

                var form = new _0030_Elegir_FormaDePago_Delivery(_clienteSeleccionado);
                form.ShowDialog();

                if (form.RealizoAlgunaOperacion)
                {
                    _realizoAlgunaOperacion = true;

                    Close();
                }
            }

            else
            {
                Mensaje.Mostrar("No se detectaron transacciones en la compra", Mensaje.Tipo.Informacion);
            }
        }

        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            _comprobante = _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_clienteSeleccionado.Id);

            var Subtotal = _comprobante.ComprobanteDeliveryDetalletos.Sum(x => x.SubTotal);

            var descuento = nudDescuento.Value;

            txtTotal.Text = (Subtotal - (Subtotal * descuento / 100m)).ToString("C2");

            txtSubTotal.Text = Subtotal.ToString("C2");

            _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);
        }

        private void btnCambiarCantidadItem_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionado != null)
            {
                if (_productoSeleccionado.Cantidad > 0)
                {
                    _comprobanteDelivery.DisminuirItem(_comprobante.Id, 1, _productoSeleccionado);

                    _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);

                    var prod = _productoServicio.ObenerPorId(_productoSeleccionado.ProductoId);

                    _productoServicio.AumentarStock(prod);

                    ActualizarGrilla();

                    ActualizarTotalizadores(); // aqui llamo este metodo para que en tiempo real el nud total vaya cambiando
                }
                else
                {
                    Mensaje.Mostrar("No existen productos en la grilla.", Mensaje.Tipo.Informacion);
                    txtDescripcion.Focus();
                }
            }
             else
             {
                Mensaje.Mostrar("No existen productos en la grilla.", Mensaje.Tipo.Informacion);
                txtDescripcion.Focus();
             }
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            _comprobanteDelivery.ObtenerObservacion(txtObservacion.Text.Trim(), _clienteSeleccionado.Id);
        }

        private void _0010_VentaDelivery_Load(object sender, EventArgs e)
        {
            CargarDatos(); // 1ro cargo los datos
            FormatearGrilla(dgvGrilla); // despues formateo grilla
        }

        private void _0010_VentaDelivery_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_comprobanteDelivery.ObtenerComprobantePorId(_comprobante.Id).EstadoDelivery == DAL.EstadoDelivery.EnProceso)
            {
                _comprobanteDelivery.obtenerDescuento(nudDescuento.Value, _clienteSeleccionado.Id);
            }
        }
    }
}
