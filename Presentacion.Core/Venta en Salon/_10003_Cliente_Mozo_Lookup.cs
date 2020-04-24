using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.Empleado;
using Servicio.Core.Empleado.DTO;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Venta_en_Salon
{
    public partial class _10003_Cliente_Mozo_Lookup : Form
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IComprobanteSalon _comprobanteSalon;
        private readonly IEmpleadoServicio _EmpleadoServicio;
        private readonly ICuentaCorrienteServicio _CuentaCorrienteServicio;
        private ClienteDto _clienteSeleccionado;
        private EmpleadoDto _mozoSeleccionado;
        private long _mesaId;

        public bool FacturoLaVenta { get; set; }

        public bool RealizoAlgunaOperacion { get; set; }

        public ClienteDto Cliente { get { return _clienteSeleccionado; } }

        public _10003_Cliente_Mozo_Lookup(long MesaId)
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();
            _comprobanteSalon = new ComprobanteSalon();
            _EmpleadoServicio = new EmpleadoServicio();
            _CuentaCorrienteServicio = new CuentaCorrienteServicio();
            _mesaId = MesaId;
            RealizoAlgunaOperacion = false;
            FacturoLaVenta = false;

            txtBuscar.KeyPress += Validacion.NoInyeccion;
            txtBuscar.KeyPress += Validacion.NoSimbolos;

            txtBuscar.Enter += txt_Enter;
            txtBuscar.Leave += txt_Leave;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;
        }

        public void PoblarComboBox(ComboBox cmb, object obj, string display, string valorDevuelto)
        {
            cmb.DataSource = obj;
            cmb.DisplayMember = display;
            cmb.ValueMember = valorDevuelto;
        }

        private void ActualizarDatos(string cadenaBuscar)
        {
            dgvgrilla.DataSource = _clienteServicio.ObtenerClientesDesocupados(cadenaBuscar);
            FormatearGrilla(dgvgrilla);

            if (dgvgrilla.RowCount == 0 )
            {
                _clienteSeleccionado = null;
            }
        }

        private void FormatearGrilla(DataGridView dgvgrilla)
        {

            dgvgrilla.Columns["Id"].Visible = false;

            dgvgrilla.Columns["Nombre"].Visible = false;

            dgvgrilla.Columns["Apellido"].Visible = false;

            dgvgrilla.Columns["Cuil"].Visible = false;

            dgvgrilla.Columns["Celular"].Visible = false;

            dgvgrilla.Columns["Telefono"].Visible = false;

            dgvgrilla.Columns["Direccion"].Visible = false;

            dgvgrilla.Columns["Dni"].Visible = false;

            dgvgrilla.Columns["EstaOcupado"].Visible = false;

            dgvgrilla.Columns["EstaEliminado"].Visible = false;

            dgvgrilla.Columns["Codigo"].Visible = true;
            dgvgrilla.Columns["Codigo"].Width = 50;
            dgvgrilla.Columns["Codigo"].HeaderText = @"Código";
            dgvgrilla.Columns["Codigo"].DisplayIndex = 0;

            dgvgrilla.Columns["ApyNom"].Visible = true;
            dgvgrilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            dgvgrilla.Columns["ApyNom"].DisplayIndex = 1;

            dgvgrilla.Columns["DeudaTotal"].Visible = true;
            dgvgrilla.Columns["DeudaTotal"].Width = 100;
            dgvgrilla.Columns["DeudaTotal"].HeaderText = @"DeudaTotal";
            dgvgrilla.Columns["DeudaTotal"].DisplayIndex = 2;

            dgvgrilla.Columns["TieneCuentaCorriente"].Visible = true;
            dgvgrilla.Columns["TieneCuentaCorriente"].Width = 100;
            dgvgrilla.Columns["TieneCuentaCorriente"].HeaderText = @"Tiene Cuenta Corriente";
            dgvgrilla.Columns["TieneCuentaCorriente"].DisplayIndex = 3;

            dgvgrilla.Columns["MontoMaximoCtaCte"].Visible = true;
            dgvgrilla.Columns["MontoMaximoCtaCte"].Width = 100;
            dgvgrilla.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Maximo CtaCte";
            dgvgrilla.Columns["MontoMaximoCtaCte"].DisplayIndex = 4;

            dgvgrilla.Columns["ActivoParaCompras"].Visible = true;
            dgvgrilla.Columns["ActivoParaCompras"].Width = 100;
            dgvgrilla.Columns["ActivoParaCompras"].HeaderText = @"Activo Para Compras";
            dgvgrilla.Columns["ActivoParaCompras"].DisplayIndex = 5;



        }

        private void dgvgrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvgrilla.RowCount > 0)
            {
                _clienteSeleccionado = (ClienteDto)dgvgrilla.Rows[e.RowIndex].DataBoundItem;
            }

            else
            {
                _clienteSeleccionado = null;
            }
        }

        private void _10003_Cliente_Mozo_Lookup_Load(object sender, EventArgs e)
        {
            ActualizarDatos(txtBuscar.Text.Trim());

            PoblarComboBox(cmbMozo, _EmpleadoServicio.obtenerMozos(DAL.TipoEmpleado.Mozo), "ApyNom", "Id");
        }

        private void cmbMozo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mozoSeleccionado = (EmpleadoDto)cmbMozo.SelectedItem;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(txtBuscar.Text.Trim());
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null || _mozoSeleccionado == null)
            {
                Mensaje.Mostrar("Se debe seleccionar un cliente de la grilla y un mozo para poder continuar.", Mensaje.Tipo.Informacion);
            }

            else
            {
                _comprobanteSalon.Crear(_mesaId, _mozoSeleccionado.Id, _clienteSeleccionado.Id);

                _clienteServicio.ClienteOcupado(Cliente);

                RealizoAlgunaOperacion = true;

                var ventaSalon = new _10002_Venta(_mesaId, Cliente, _mozoSeleccionado.Id);

                ventaSalon.ShowDialog();

                if (ventaSalon.RealizoAlgunaOperacion)
                {
                    FacturoLaVenta = true;
                }

                Close();
            }
        }
    }

}
