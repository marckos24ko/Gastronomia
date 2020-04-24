using Presentacion.Base.Varios;
using Presentacion.Core.Clientes;
using Servicio.Core.Cliente;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.Delivery.Dto;
using Servicio.Core.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Delivery
{
    public partial class _0020_PreventaDelivery : Form
    {
        private IComprobanteDeliveryServicio _comprobanteDelivery;
        private IClienteServicio _clienteServicio;
        private IProductoServicio _productoServicio;
        private ComprobanteDeliveryDto _ventaSeleccionada;

        public ComprobanteDeliveryDto ClienteDelivery { get { return _ventaSeleccionada; } }

        public _0020_PreventaDelivery()
        {
            InitializeComponent();

            _comprobanteDelivery = new ComprobanteDeliveryServicio();
            _clienteServicio = new ClienteServicio();
            _productoServicio = new ProductoServicio();

            txtBuscar.KeyPress += Validacion.NoSimbolos;

            txtBuscar.Enter += txt_Enter;

            txtBuscar.Leave += txt_Leave;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;
        }

        private void ActualizarDatos(string cadena)
        {
            dgvgrilla.DataSource = _comprobanteDelivery.ObtenerPorFiltro(cadena).ToList();

            if (dgvgrilla.RowCount == 0)
            {
                _ventaSeleccionada = null;
            }
        }

        private void dgvgrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _ventaSeleccionada = (ComprobanteDeliveryDto)dgvgrilla.Rows[e.RowIndex].DataBoundItem;

        }

        private void txt_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Constante.ColorControl.ColorSinFoco;
            }
            else if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).BackColor = Constante.ColorControl.ColorSinFoco;
            }
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Constante.ColorControl.ColorConFoco;
            }
            else if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).BackColor = Constante.ColorControl.ColorConFoco;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(txtBuscar.Text.Trim());

            FormatearGrilla(dgvgrilla);
        }

        private void FormatearGrilla(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["CadeteId"].Visible = false;
            dgvGrilla.Columns["ClienteId"].Visible = false;
            dgvGrilla.Columns["DelievirieLegajo"].Visible = false;
            dgvGrilla.Columns["SubTotal"].Visible = false;
            dgvGrilla.Columns["Descuento"].Visible = false;


            dgvGrilla.Columns["ClienteStr"].Visible = true;
            dgvGrilla.Columns["ClienteStr"].Width = 100;
            dgvGrilla.Columns["ClienteStr"].HeaderText = @"Cliente";
            dgvGrilla.Columns["ClienteStr"].DisplayIndex = 0;

            dgvGrilla.Columns["Deliverie"].Visible = true;
            dgvGrilla.Columns["Deliverie"].Width = 100;
            dgvGrilla.Columns["Deliverie"].HeaderText = @"Delivery";
            dgvGrilla.Columns["Deliverie"].DisplayIndex = 1;

            dgvGrilla.Columns["DireccionEnvio"].Visible = true;
            dgvGrilla.Columns["DireccionEnvio"].Width = 100;
            dgvGrilla.Columns["DireccionEnvio"].HeaderText = @"Destino";
            dgvGrilla.Columns["DireccionEnvio"].DisplayIndex = 2;

            dgvGrilla.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["Total"].DisplayIndex = 3;

            dgvGrilla.Columns["Observacion"].Visible = true;
            dgvGrilla.Columns["Observacion"].Width = 100;
            dgvGrilla.Columns["Observacion"].HeaderText = @"Observacion";
            dgvGrilla.Columns["Observacion"].DisplayIndex = 5;

            dgvGrilla.Columns["EstadoDelivery"].Visible = true;
            dgvGrilla.Columns["EstadoDelivery"].Width = 50;
            dgvGrilla.Columns["EstadoDelivery"].HeaderText = @"Estado Delivery";
            dgvGrilla.Columns["EstadoDelivery"].DisplayIndex = 6;

            dgvGrilla.Columns["Fecha"].Visible = true;
            dgvGrilla.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvGrilla.Columns["Fecha"].Width = 100;
            dgvGrilla.Columns["Fecha"].HeaderText = @"Fecha";
            dgvGrilla.Columns["Fecha"].DisplayIndex = 7;

        }

        private void dgvgrilla_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            _ventaSeleccionada = (ComprobanteDeliveryDto)dgvgrilla.Rows[e.RowIndex].DataBoundItem;

        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            var Nuevaenta = new Clientes_Cadete_LookUp();
            Nuevaenta.ShowDialog();

            if (Nuevaenta.RealizoAlgunaOperacion == true)
            {
                ActualizarDatos(string.Empty);
            }
        }

        private void btnVentasAbiertas_Click(object sender, EventArgs e)
        {
            pnlGriila.Visible = true;
            pnlGriila.Enabled = true;
            btnContinuar.Visible = true;
            btnCancelar.Visible = true;

            ActualizarDatos(string.Empty);

            FormatearGrilla(dgvgrilla);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (_ventaSeleccionada == null)
            {
                Mensaje.Mostrar("Se debe seleccionar una venta de la grilla para poder continuar", Mensaje.Tipo.Informacion);
            }

            else
            {
                var comprobante = _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_ventaSeleccionada.ClienteId);

                var cliente = _clienteServicio.obtenerPorId(comprobante.ClienteId);

                var ventaDelivery = new _0010_VentaDelivery(cliente, comprobante.CadeteId);
                ventaDelivery.ShowDialog();

                if (ventaDelivery.RealizoAlgunaOperacion == false)
                {
                    ActualizarDatos(string.Empty);
                }

                else
                {
                    ActualizarDatos(string.Empty);
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_ventaSeleccionada == null)
            {
                Mensaje.Mostrar("Se debe seleccionar una venta de la grilla para poder continuar", Mensaje.Tipo.Informacion);
            }

            else
            {
                if (Mensaje.Mostrar(@"Seguro desea cancelar la venta seleccionada?", Mensaje.Tipo.Pregunta) == System.Windows.Forms.DialogResult.Yes)
                {
                    var listaProductos = _productoServicio.ObtenerPorFiltro(string.Empty);
                    var listaProductosEnLaVenta = new List<ProductoDto>();

                    foreach (var item in _ventaSeleccionada.ComprobanteDeliveryDetalletos)
                    {
                        var producto = new ProductoDto()
                        {
                            Id = item.ProductoId,
                            Descripcion = item.Descripcion,
                            Stock = item.Cantidad
                        };

                        listaProductosEnLaVenta.Add(producto);
                    }

                    foreach (var item in listaProductosEnLaVenta)
                    {
                        foreach (var item2 in listaProductos)
                        {
                            if (item.Id == item2.Id)
                            {
                                _productoServicio.AumentarStockPorCancelarVenta(item2, item.Stock);
                            }
                        }
                    }

                    _clienteServicio.ClienteDesocupado(_ventaSeleccionada.ClienteId);

                    _comprobanteDelivery.Eliminar(_ventaSeleccionada.ClienteId);

                    ActualizarDatos(string.Empty);
                }

            }
        }
    }
}
