using Presentacion.Base.Varios;
using Presentacion.Core.Delivery;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.Empleado;
using Servicio.Core.Empleado.DTO;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Clientes
{
    public partial class Clientes_Cadete_LookUp : Form
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IComprobanteDeliveryServicio _comprobanteDelivery;
        private readonly IEmpleadoServicio _EmpleadoServicio;
        private readonly ICuentaCorrienteServicio _CuentaCorrienteServicio;
        private ClienteDto _clienteSeleccionado;
        private EmpleadoDto _CadeteSeleccionado;
        public bool RealizoAlgunaOperacion { get; set; }

        public ClienteDto Cliente { get { return _clienteSeleccionado; } }

        public Clientes_Cadete_LookUp()
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();
            _comprobanteDelivery = new ComprobanteDeliveryServicio();
            _EmpleadoServicio = new EmpleadoServicio();
            _CuentaCorrienteServicio = new CuentaCorrienteServicio();
            RealizoAlgunaOperacion = false;

            txtBuscar.KeyPress += Validacion.NoInyeccion;

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

            if (dgvgrilla.RowCount == 0)
            {
                _clienteSeleccionado = null;
            }

            FormatearGrilla(dgvgrilla);
        }

        private void FormatearGrilla(DataGridView dgvgrilla)
        {

            dgvgrilla.Columns["Id"].Visible = false;
            dgvgrilla.Columns["Nombre"].Visible = false;
            dgvgrilla.Columns["Apellido"].Visible = false;
            dgvgrilla.Columns["Dni"].Visible = false;
            dgvgrilla.Columns["Cuil"].Visible = false;
            dgvgrilla.Columns["EstaEliminado"].Visible = false;
            dgvgrilla.Columns["EstaOcupado"].Visible = false;
            dgvgrilla.Columns["Direccion"].Visible = false;
            dgvgrilla.Columns["Celular"].Visible = false;
            dgvgrilla.Columns["Telefono"].Visible = false;
            dgvgrilla.Columns["Codigo"].Visible = false;
           

            dgvgrilla.Columns["ApyNom"].Visible = true;
            dgvgrilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            dgvgrilla.Columns["ApyNom"].DisplayIndex = 0;

            dgvgrilla.Columns["TieneCuentaCorriente"].Visible = true;
            dgvgrilla.Columns["TieneCuentaCorriente"].HeaderText = @"Cuenta Corriente";
            dgvgrilla.Columns["TieneCuentaCorriente"].DisplayIndex = 1;

            dgvgrilla.Columns["MontoMaximoCtaCte"].Visible = true;
            dgvgrilla.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Maximo Cta Cte";
            dgvgrilla.Columns["MontoMaximoCtaCte"].DisplayIndex = 2;

            dgvgrilla.Columns["DeudaTotal"].Visible = true;
            dgvgrilla.Columns["DeudaTotal"].DefaultCellStyle.Format = "C2";
            dgvgrilla.Columns["DeudaTotal"].HeaderText = @"Deuda Total";
            dgvgrilla.Columns["DeudaTotal"].DisplayIndex = 3;

            dgvgrilla.Columns["ActivoParaCompras"].Visible = true;
            dgvgrilla.Columns["ActivoParaCompras"].HeaderText = @"Activo para Compras";
            dgvgrilla.Columns["ActivoParaCompras"].DisplayIndex = 4;



        }

        private void dgvgrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
          
         _clienteSeleccionado = (ClienteDto)dgvgrilla.Rows[e.RowIndex].DataBoundItem;

        }

        private void Clientes1_Lookup_Load(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);

            PoblarComboBox(cmbCadete, _EmpleadoServicio.obtenerCadetes(DAL.TipoEmpleado.Cadete), "ApyNom", "Id");

        }

        private void cmbCadete_SelectedValueChanged(object sender, EventArgs e)
        {
            _CadeteSeleccionado = (EmpleadoDto)cmbCadete.SelectedItem;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(txtBuscar.Text.Trim());
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

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null || _CadeteSeleccionado == null)
            {
                Mensaje.Mostrar("Se debe seleccionar un cliente de la grilla y un cadete para poder continuar.", Mensaje.Tipo.Informacion);
            }

            else
            {

                _comprobanteDelivery.Crear(_clienteSeleccionado.Id, _CadeteSeleccionado.Id);

                _clienteServicio.ClienteOcupado(Cliente);

                RealizoAlgunaOperacion = true;

                Close();

                var ventaDelivery = new _0010_VentaDelivery(Cliente, _CadeteSeleccionado.Id);
                ventaDelivery.ShowDialog();

            }
        }
    }
}
