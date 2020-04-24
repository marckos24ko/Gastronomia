using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.CuentaCorriente.Dto;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.FacturaEfectivo.Dto;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Clientes
{
    public partial class Cliente_Cta_Cte : Form
    {
        private readonly IFacturaServicio _facturaServicio;
        private readonly ICuentaCorrienteServicio _cuentaCorrienteServicio;
        private ClienteDto _cliente;
        private CuentaCorrienteDto _ctaCte;
        private FacturaDto _facturaSeleccionada;
        public bool RealizoAlgunaOperacion { get; set; }


        public Cliente_Cta_Cte(ClienteDto Cliente)
        {
            InitializeComponent();

            _cuentaCorrienteServicio = new CuentaCorrienteServicio();
            _facturaServicio = new FacturaServicio();
            _cliente = Cliente;
            lblNombreCliente.Text = string.Concat(string.Concat(Cliente.Apellido, " ", Cliente.Nombre));

            _ctaCte = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id);

            if (_ctaCte.EstaHabilitada == true)
            {
                lblEstadoCuenta.Text = "ACTIVA";
            }

            else
            {
                lblEstadoCuenta.Text = "INACTIVA";
            }

            ActualizarDatos(txtBuscar.Text);
            PoblarCmbEstadoFactura();
            cmbEstadoFactura.SelectedItem = "Todas";
            txtTotal.Text = 0m.ToString("C2");


            txtBuscar.KeyPress += Validacion.NoInyeccion;
            txtBuscar.KeyPress += Validacion.NoSimbolos;

            txtBuscar.Enter += txt_Enter;
            txtBuscar.Leave += txt_Leave;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;

            RealizoAlgunaOperacion = false;


        }

        private void PoblarCmbEstadoFactura()
        {
            cmbEstadoFactura.Items.Add("Todas");
            cmbEstadoFactura.Items.Add("Pagadas");
            cmbEstadoFactura.Items.Add("Impagadas");
        }

        private void ActualizarDatos(string cadena)
        {
            var ctaCteId = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id).Id;

            dgvGrilla.DataSource = _facturaServicio.ObtenerFacturasPorCtaCte(cadena, ctaCteId, _cliente.Id).ToList();

            FormatearGrilla(dgvGrilla);
        }

        private void FormatearGrilla(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["ClienteId"].Visible = false;
            dgvGrilla.Columns["EmpleadoId"].Visible = false;
            dgvGrilla.Columns["CuentaCorrienteId"].Visible = false;
            dgvGrilla.Columns["ComprobanteId"].Visible = false;

            dgvGrilla.Columns["Numero"].Visible = true;
            dgvGrilla.Columns["Numero"].HeaderText = @"Numero";
            dgvGrilla.Columns["Numero"].DisplayIndex = 0;

            dgvGrilla.Columns["CLienteApynom"].Visible = true;
            dgvGrilla.Columns["CLienteApynom"].HeaderText = @"Cliente";
            dgvGrilla.Columns["CLienteApynom"].DisplayIndex = 1;

            dgvGrilla.Columns["EmpleadoApynom"].Visible = true;
            dgvGrilla.Columns["EmpleadoApynom"].HeaderText = @"Empleado";
            dgvGrilla.Columns["EmpleadoApynom"].DisplayIndex = 2;

            dgvGrilla.Columns["Total"].Visible = true;
            dgvGrilla.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["Total"].HeaderText = @"Total";
            dgvGrilla.Columns["Total"].DisplayIndex = 3;

            dgvGrilla.Columns["TotalAbonado"].Visible = true;
            dgvGrilla.Columns["TotalAbonado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["TotalAbonado"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["TotalAbonado"].HeaderText = @"Total Abonado";
            dgvGrilla.Columns["TotalAbonado"].DisplayIndex = 4;

            dgvGrilla.Columns["Fecha"].Visible = true;
            dgvGrilla.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvGrilla.Columns["Fecha"].HeaderText = @"Fecha";
            dgvGrilla.Columns["Fecha"].DisplayIndex = 5;

            dgvGrilla.Columns["Estado"].Visible = true;
            dgvGrilla.Columns["Estado"].HeaderText = @"Estado";
            dgvGrilla.Columns["Estado"].DisplayIndex = 6;

        }

        private void Cliente_Cta_Cte_Load(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                _facturaSeleccionada = (FacturaDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;

                txtTotal.Text = (_facturaSeleccionada.Total - _facturaSeleccionada.TotalAbonado).ToString("C2");
            }
         
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if(_facturaSeleccionada != null)
            {
                if (_facturaSeleccionada.Estado != "Pagada")
                {
                    var formPagar = new Abonar_CtaCte(_facturaSeleccionada, _cliente.Id);

                    formPagar.ShowDialog();

                    if (formPagar.RealizoAlgunaOperacion)
                    {
                        ActualizarDatos(txtBuscar.Text.Trim());
                        RealizoAlgunaOperacion = true;
                    }
                }

                else
                {
                    MessageBox.Show("La factura seleccionada ya ha sido abonada", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show("Seleecione una factura para poder continuar", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbEstadoFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoFactura.SelectedItem.ToString() == "Pagadas")
            {
                var ctaCteId = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id).Id;

                dgvGrilla.DataSource = _facturaServicio.ObtenerFacturasPagadasCtaCte(string.Empty, ctaCteId, EstadoFactura.Pagada).ToList();

                FormatearGrilla(dgvGrilla);

                txtBuscar.Clear();
            }

            if (cmbEstadoFactura.SelectedItem.ToString() == "Impagadas")
            {
                var ctaCteId = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id).Id;

                dgvGrilla.DataSource = _facturaServicio.ObtenerFacturasImpagadasCtaCte(string.Empty, ctaCteId, EstadoFactura.Impagada, EstadoFactura.PagadaParcial).ToList();

                FormatearGrilla(dgvGrilla);

                txtBuscar.Clear();
            }

            if (cmbEstadoFactura.SelectedItem.ToString() == "Todas")
            {
                ActualizarDatos(string.Empty);

                txtBuscar.Clear();

            }

            if (dgvGrilla.RowCount == 0)
            {
                _facturaSeleccionada = null;

                txtTotal.Text = 0m.ToString("C2");
            }


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbEstadoFactura.SelectedItem != null)
            {
                if (cmbEstadoFactura.SelectedItem.ToString() == "Pagadas")
                {
                    var ctaCteId = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id).Id;

                    dgvGrilla.DataSource = _facturaServicio.ObtenerFacturasPagadasCtaCte(txtBuscar.Text.Trim(), ctaCteId, EstadoFactura.Pagada).ToList();

                    FormatearGrilla(dgvGrilla);
                }

                if (cmbEstadoFactura.SelectedItem.ToString() == "Impagadas")
                {
                    var ctaCteId = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id).Id;

                    dgvGrilla.DataSource = _facturaServicio.ObtenerFacturasImpagadasCtaCte(txtBuscar.Text.Trim(), ctaCteId, EstadoFactura.Impagada, EstadoFactura.PagadaParcial).ToList();

                    FormatearGrilla(dgvGrilla);
                }

                if (cmbEstadoFactura.SelectedItem.ToString() == "Todas")
                {
                    ActualizarDatos(txtBuscar.Text.Trim());

                    FormatearGrilla(dgvGrilla);
                }
            }

            if (dgvGrilla.RowCount == 0)
            {
                _facturaSeleccionada = null;

                txtTotal.Text = 0m.ToString("C2");
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
