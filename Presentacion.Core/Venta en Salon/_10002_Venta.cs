using Presentacion.Base;
using Presentacion.Base.Varios;
using Presentacion.Core.AbonarConsumicion;
using Presentacion.Core.Producto;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.Empleado;
using Servicio.Core.ListaPprecio;
using Servicio.Core.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Venta_en_Salon
{
    public partial class _10002_Venta : FormularioBase
    {
        private long _mesaId;
        private ComprobanteSalonDto _comprobante;
        private readonly IComprobanteSalon _comprobanteSalon;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IProductoServicio _productoServicio;
        private long _empleadoSeleccionadoId;
        private ClienteDto _clienteSeleccionado;
        private ComprobanteSalonDetalleDto _productoSeleccionado;
        public bool _realizoAlgunaOperacion;


        public bool RealizoAlgunaOperacion { get { return _realizoAlgunaOperacion; } }

        public _10002_Venta(long mesaId, ClienteDto Cliente, long EmpleadoId)
        {
            InitializeComponent();

            _mesaId = mesaId;
            _clienteSeleccionado = Cliente;
            _empleadoSeleccionadoId = EmpleadoId;
            _comprobante = new ComprobanteSalonDto();
            _productoServicio = new ProductoServicio();
           _productoSeleccionado = new ComprobanteSalonDetalleDto();
            _listaPrecioServicio = new ListaPrecioServicio();
            _empleadoServicio = new EmpleadoServicio();
            _comprobanteSalon = new ComprobanteSalon();
            _realizoAlgunaOperacion = false;

            txtDescripcion.KeyPress += Validacion.NoInyeccion;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;

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

        private void _10002_Venta_Load(object sender, System.EventArgs e)
        {
            CargarDatos(); // 1ro cargo los datos
            FormatearGrilla(dgvGrilla); // despues formateo grilla

        }

        private void CargarDatos()
        {
            _comprobante = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_mesaId);

            var Subtotal = _comprobante.ComprobanteSalonDetalleDtos.Sum(x => x.SubTotal);

            var descuento = _comprobante.Descuento;

            PoblarComboBox(cmbListaPrecio, _listaPrecioServicio.Obtener(), "Descripcion", "Id");

            dgvGrilla.DataSource = _comprobante.ComprobanteSalonDetalleDtos.ToList();

            if (_comprobante.MozoStr != null && dgvGrilla.RowCount > 0)
            {
               
                lblEmpleado.Text = _comprobante.MozoStr;
                lblCliente.Text = _clienteSeleccionado.ApyNom;


            }
            else
            {
                
               
                lblEmpleado.Text = _comprobante.MozoStr;
                lblCliente.Text = _clienteSeleccionado.ApyNom;
            }

            txtTotal.Text = (Subtotal - (Subtotal * descuento / 100m)).ToString("C2");
            txtFecha.Text = Convert.ToString(DateTime.Now);
            txtSubTotal.Text = Subtotal.ToString("C2");
            nudDescuento.Value = descuento;


        }

        private void ActualizarTotalizadores()
        {

           _comprobante = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_mesaId);

            var Subtotal = _comprobante.ComprobanteSalonDetalleDtos.Sum(x => x.SubTotal);

            var descuento = nudDescuento.Value;

            txtTotal.Text = (Subtotal - (Subtotal * descuento / 100m)).ToString("C2");

            txtSubTotal.Text = Subtotal.ToString("C2");

            dgvGrilla.DataSource = _comprobante.ComprobanteSalonDetalleDtos.ToList();

            
        }

        public void ActualizarGrilla()
        {
            dgvGrilla.DataSource = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_mesaId).ComprobanteSalonDetalleDtos.ToList();

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
            return _productoServicio.ObtenerPorListaDePrecio( text, listaId);
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
                            _comprobanteSalon.AgregarItem(_comprobante.Id, (int)cantidad, producto, _empleadoSeleccionadoId);
                            _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);
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
                                _comprobanteSalon.AgregarItem(_comprobante.Id, (int)cantidad, productoSeleccionado, _empleadoSeleccionadoId);
                                _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);
                                ActualizarGrilla();

                                ActualizarTotalizadores();
                            }

                            else
                            {
                                Mensaje.Mostrar("No hay stock suficiente",Mensaje.Tipo.Informacion);
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
                Mensaje.Mostrar("Ingrese un código del producto.",Mensaje.Tipo.Informacion);
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

                        if (_productoServicio.Stock(producto, (int)cantidad))
                        {
                            nudPrecio.Value = producto.PrecioPublico;
                            _comprobanteSalon.AgregarItem(_comprobante.Id, (int)cantidad, producto, _empleadoSeleccionadoId);
                            _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);
                            ActualizarGrilla();

                            ActualizarTotalizadores();
                        }

                        else
                        {
                            Mensaje.Mostrar("No hay stock suficiente",Mensaje.Tipo.Informacion);
                            
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
                                _comprobanteSalon.AgregarItem(_comprobante.Id, (int)cantidad, productoSeleccionado, _empleadoSeleccionadoId);
                                _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);
                                ActualizarGrilla();

                                ActualizarTotalizadores();
                            }

                            else
                            {
                                Mensaje.Mostrar("No hay stock suficiente",Mensaje.Tipo.Informacion);
                            }
                        }
                    }

                    ActualizarTotalizadores(); // aqui llamo este metodo para que en tiempo real el txt total vaya cambiando

                }

                else
                {
                    Mensaje.Mostrar("No hay una lista de precio seleecionada",Mensaje.Tipo.Informacion);
                }

            }

        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                _productoSeleccionado = (ComprobanteSalonDetalleDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;

            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {

            _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);

            if (dgvGrilla.RowCount > 0)
            {

                var form = new _101_Elegir_FormaDePago(_clienteSeleccionado, _mesaId);
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
            _comprobante = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_mesaId);

            var Subtotal = _comprobante.ComprobanteSalonDetalleDtos.Sum(x => x.SubTotal);

            var descuento = nudDescuento.Value;

            _comprobanteSalon.obtenerDescuento(descuento, _mesaId);

            txtTotal.Text = (Subtotal - (Subtotal * descuento / 100m)).ToString("C2");

            txtSubTotal.Text = Subtotal.ToString("C2");
        }

        private void btnCambiarCantidadItem_Click(object sender, EventArgs e)
        {

            if (_productoSeleccionado != null)
            {
                if (_productoSeleccionado.Cantidad > 0)
                {
                    _comprobanteSalon.DisminuirItem(_comprobante.Id, 1, _productoSeleccionado);

                    var prod = _productoServicio.ObenerPorId(_productoSeleccionado.ProductoId);

                    _productoServicio.AumentarStock(prod);

                    _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);

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

        private void _10002_Venta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_comprobanteSalon.ObtenerComprobantePorId(_comprobante.Id).Estado == DAL.EstadoSalon.Pendiente)
            {
                _comprobanteSalon.obtenerDescuento(nudDescuento.Value, _mesaId);
            }
           
        }
    }
}


