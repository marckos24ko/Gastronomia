using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.FacturaEfectivo.Dto;
using Servicio.Core.Movimientos;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Movimientos
{
    public partial class Movimientos_Clientes : Form
    {
        private IClienteServicio _ClienteServicio;
        private IFacturaServicio _facturaServicio;
        private IMovimientoServicio _movimientoServicio;
        private FacturaDto _facturaSeleccionada;
        private ClienteDto _clienteSeleccionado;


        public Movimientos_Clientes()
        {
            InitializeComponent();

            _ClienteServicio = new ClienteServicio();
            _facturaServicio = new FacturaServicio();
            _movimientoServicio = new MovimientoServicio();
            
            ActualizarDatosCliente(txtBuscarClientes.Text.Trim());

            txtBuscarClientes.KeyPress += Validacion.NoInyeccion;
            txtBuscarClientes.KeyPress += Validacion.NoSimbolos;

            txtBuscarFacturas.KeyPress += Validacion.NoInyeccion;
            txtBuscarFacturas.KeyPress += Validacion.NoSimbolos;

            txtBuscarMovimientos.KeyPress += Validacion.NoInyeccion;
            txtBuscarMovimientos.KeyPress += Validacion.NoSimbolos;

            txtBuscarFacturas.Enter += txt_Enter;
            txtBuscarFacturas.Leave += txt_Leave;

            txtBuscarClientes.Enter += txt_Enter;
            txtBuscarClientes.Leave += txt_Leave;

            txtBuscarMovimientos.Enter += txt_Enter;
            txtBuscarMovimientos.Leave += txt_Leave;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;

        }

        private void ActualizarDatosCliente(string cadenaBuscar)
        {
            dgvGrillaClientes.DataSource = _ClienteServicio.ObtenerPorFiltro(cadenaBuscar).ToList();

            FormatearGrillaClientes(dgvGrillaClientes);

            if (dgvGrillaClientes.RowCount == 0)
            {
               _clienteSeleccionado = null;

               _facturaSeleccionada = null;

            }

        }

        private void ActualizarDatosFactura( long? clienteId, string cadenaBuscar)
        {
            if (_clienteSeleccionado != null)
            {
                dgvGrillaFactura.DataSource = _facturaServicio.ObtenerFacturasPorCliente(clienteId, cadenaBuscar).ToList();

                FormatearGrillaFacturas(dgvGrillaFactura);

                if (dgvGrillaFactura.RowCount == 0)
                {
                    _facturaSeleccionada = null;
                }
            }

            else
            {
                dgvGrillaFactura.DataSource = _facturaServicio.ObtenerFacturasPorCliente(null, cadenaBuscar).ToList();

                FormatearGrillaFacturas(dgvGrillaFactura);
            }

            
        }

        private void ActualizarDatosMovimiento(string cadenaBuscar, long? facturaId)
        {
            if (_facturaSeleccionada != null)
            {
                dgvGrillaMovimientos.DataSource = _movimientoServicio.ObtenerMovimientoPorFacturaId(facturaId, cadenaBuscar).ToList();

                FormatearGrillaMovimientos(dgvGrillaMovimientos);
            }

            else
            {
                dgvGrillaMovimientos.DataSource = _movimientoServicio.ObtenerMovimientoPorFacturaId(null, cadenaBuscar).ToList();

                FormatearGrillaMovimientos(dgvGrillaMovimientos);
            }

           
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvgrillaFactura_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaFactura.RowCount > 0)
            {
                _facturaSeleccionada = (FacturaDto)dgvGrillaFactura.Rows[e.RowIndex].DataBoundItem;

            }
        }

        private void dgvGrillaClientes_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaClientes.RowCount > 0)
            {
             _clienteSeleccionado = (ClienteDto)dgvGrillaClientes.Rows[e.RowIndex].DataBoundItem;

            }
        }

        private void dgvGrillaClientes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_clienteSeleccionado != null)
            {
                ActualizarDatosFactura(_clienteSeleccionado.Id, string.Empty);

                FormatearGrillaFacturas(dgvGrillaFactura);
            }

            else
            {
                Mensaje.Mostrar("Seleccione un elemento de la lista para poder continuar.", Mensaje.Tipo.Informacion);
            }

        }

        private void dgvgrillaFactura_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_facturaSeleccionada != null)
            {
                ActualizarDatosMovimiento(string.Empty, _facturaSeleccionada.Id);

                FormatearGrillaMovimientos(dgvGrillaMovimientos);
            }

            else
            {
                Mensaje.Mostrar("Seleccione un elemento de la lista para poder continuar.", Mensaje.Tipo.Informacion);
            }
        }

        private void FormatearGrillaClientes(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["Nombre"].Visible = false;
            dgvGrilla.Columns["Apellido"].Visible = false;
            dgvGrilla.Columns["DeudaTotal"].Visible = false;
            dgvGrilla.Columns["EstaEliminado"].Visible = false;
            dgvGrilla.Columns["Direccion"].Visible = false;
            dgvGrilla.Columns["Dni"].Visible = false;
            dgvGrilla.Columns["Celular"].Visible = false;
            dgvGrilla.Columns["Telefono"].Visible = false;
            dgvGrilla.Columns["Cuil"].Visible = false;
            dgvGrilla.Columns["EstaOcupado"].Visible = false;

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].Width = 50;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Código";
            dgvGrilla.Columns["Codigo"].DisplayIndex = 0;

            dgvGrilla.Columns["ApyNom"].Visible = true;
            dgvGrilla.Columns["ApyNom"].Width = 100;
            dgvGrilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            dgvGrilla.Columns["ApyNom"].DisplayIndex = 1;

            dgvGrilla.Columns["TieneCuentaCorriente"].Visible = true;
            dgvGrilla.Columns["TieneCuentaCorriente"].Width = 50;
            dgvGrilla.Columns["TieneCuentaCorriente"].HeaderText = @"Cuenta Corriente";
            dgvGrilla.Columns["TieneCuentaCorriente"].DisplayIndex = 2;

            dgvGrilla.Columns["MontoMaximoCtaCte"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["MontoMaximoCtaCte"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Maximo Cta Cte";
            dgvGrilla.Columns["MontoMaximoCtaCte"].DisplayIndex = 3;

            dgvGrilla.Columns["ActivoParaCompras"].Visible = true;
            dgvGrilla.Columns["ActivoParaCompras"].Width = 50;
            dgvGrilla.Columns["ActivoParaCompras"].HeaderText = @"Activo Para Compras";
            dgvGrilla.Columns["ActivoParaCompras"].DisplayIndex = 4;
        }

        private void FormatearGrillaFacturas(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["ClienteId"].Visible = false;
            dgvGrilla.Columns["EmpleadoId"].Visible = false;
            dgvGrilla.Columns["ComprobanteId"].Visible = false;
            dgvGrilla.Columns["CuentaCorrienteId"].Visible = false;

            dgvGrilla.Columns["Numero"].Visible = true;
            dgvGrilla.Columns["Numero"].HeaderText = @"Numero";
            dgvGrilla.Columns["Numero"].DisplayIndex = 0;

            dgvGrilla.Columns["CLienteApynom"].Visible = true;
            dgvGrilla.Columns["CLienteApynom"].HeaderText = @"CLiente";
            dgvGrilla.Columns["CLienteApynom"].DisplayIndex = 1;

            dgvGrilla.Columns["EmpleadoApynom"].Visible = true;
            dgvGrilla.Columns["EmpleadoApynom"].HeaderText = @"Empleado";
            dgvGrilla.Columns["EmpleadoApynom"].DisplayIndex = 2;

            dgvGrilla.Columns["Total"].Visible = true;
            dgvGrilla.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["Total"].HeaderText = @"Total";
            dgvGrilla.Columns["Total"].DisplayIndex = 3;

            dgvGrilla.Columns["TotalAbonado"].Visible = true;
            dgvGrilla.Columns["TotalAbonado"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["TotalAbonado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["TotalAbonado"].HeaderText = @"Total Abonado";
            dgvGrilla.Columns["TotalAbonado"].DisplayIndex = 4;

            dgvGrilla.Columns["Fecha"].Visible = true;
            dgvGrilla.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvGrilla.Columns["Fecha"].HeaderText = @"Fecha";
            dgvGrilla.Columns["Fecha"].DisplayIndex = 5;
           
        }

        private void FormatearGrillaMovimientos(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["ClienteId"].Visible = false;
            dgvGrilla.Columns["ProveedorNombre"].Visible = false;
            dgvGrilla.Columns["FacturaId"].Visible = false;

            dgvGrilla.Columns["Numero"].Visible = true;
            dgvGrilla.Columns["Numero"].HeaderText = @"Numero";
            dgvGrilla.Columns["Numero"].DisplayIndex = 0;

            dgvGrilla.Columns["ClienteApyNom"].Visible = true;
            dgvGrilla.Columns["ClienteApyNom"].HeaderText = @"Cliente";
            dgvGrilla.Columns["ClienteApyNom"].DisplayIndex = 1;

            dgvGrilla.Columns["Monto"].Visible = true;
            dgvGrilla.Columns["Monto"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["Monto"].HeaderText = @"Monto";
            dgvGrilla.Columns["Monto"].DisplayIndex = 2;

            dgvGrilla.Columns["TipoMovimiento"].Visible = true;
            dgvGrilla.Columns["TipoMovimiento"].HeaderText = @"Tipo de Movimiento";
            dgvGrilla.Columns["TipoMovimiento"].DisplayIndex = 3;

            dgvGrilla.Columns["Fecha"].Visible = true;
            dgvGrilla.Columns["Fecha"].HeaderText = @"Fecha";
            dgvGrilla.Columns["Fecha"].DisplayIndex = 4;

        }

        private void btnBuscarClientes_Click(object sender, EventArgs e)
        {
            ActualizarDatosCliente(txtBuscarClientes.Text.Trim());

            if (_clienteSeleccionado == null)
            {
                ActualizarDatosFactura(null, string.Empty);
                ActualizarDatosMovimiento(string.Empty, null);
            }

            
        }

        private void btnBuscarFacturas_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado != null)
            {
                ActualizarDatosFactura(_clienteSeleccionado.Id, txtBuscarFacturas.Text.Trim());

                if (_facturaSeleccionada == null)
                {
                    ActualizarDatosMovimiento(string.Empty, null);
                }
            }

            else
            {
                ActualizarDatosFactura(null, txtBuscarFacturas.Text.Trim());

                ActualizarDatosMovimiento(string.Empty, null);
            }

           
        }

        private void btnBuscarMovimientos_Click(object sender, EventArgs e) 
        {
            if (_facturaSeleccionada != null)
            {
                ActualizarDatosMovimiento(txtBuscarMovimientos.Text.Trim(), _facturaSeleccionada.Id);
            }

            else
            {
                ActualizarDatosMovimiento(txtBuscarMovimientos.Text.Trim(), null);
            }

           
        }

        public void txt_Leave(object sender, EventArgs e)
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

        public void txt_Enter(object sender, EventArgs e)
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

    }
}



