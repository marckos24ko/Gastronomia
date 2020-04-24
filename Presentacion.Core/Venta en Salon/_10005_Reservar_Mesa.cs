using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Empleado;
using Servicio.Core.Mesa;
using Servicio.Core.Reserva;
using Servicio.Core.Reserva.DTO;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Venta_en_Salon
{

    public partial class _10005_Reservar_Mesa : Form
    {
        private readonly IComprobanteReservaServicio _reservaServicio;
        private readonly IClienteServicio _clienteServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IMesaServicio _mesaServicio;
        private long _mesaId;
        public bool RealizoAlgunaOperacion { get; set; }

        public _10005_Reservar_Mesa(long Id)
        {
            InitializeComponent();

            _mesaId = Id;
            _reservaServicio = new ReservaServicio();
            _clienteServicio = new ClienteServicio();
            _empleadoServicio = new EmpleadoServicio();
            _mesaServicio = new MesaServicio();

            PoblarComboBox(cmbCliente, _clienteServicio.ObtenerClientesDesocupadosSinFiltro(), "ApyNom", "Id");
            PoblarComboBox(cmbMozo, _empleadoServicio.obtenerMozos(DAL.TipoEmpleado.Mozo), "ApyNom", "Id");

            RealizoAlgunaOperacion = false;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;
        }

        public void PoblarComboBox(ComboBox cmb, object obj, string display, string valorDevuelto)
        {
            cmb.DataSource = obj;
            cmb.DisplayMember = display;
            cmb.ValueMember = valorDevuelto;
        }

        public bool VerificarDatosObligatorios()
        {
            if (cmbCliente.SelectedValue == null) return false;
            if (cmbMozo.SelectedValue == null) return false;
            
            return true;
        }

        public void LimpiarDatos()
        {
            txtObservacion.Clear();
            nudCoemnzales.Value = 1m;
            cmbCliente.Text = null;
            cmbMozo.Text = null;
            

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           if (VerificarDatosObligatorios())
            {
                _reservaServicio.Crear(new ReservaDto
                {

                    Codigo = int.Parse(txtCodigo.Text),
                    MesaId = _mesaId,
                    CLienteId = Convert.ToInt64(cmbCliente.SelectedValue),
                    EmpleadoId = Convert.ToInt64(cmbMozo.SelectedValue),
                    CLienteStr = cmbCliente.SelectedText,
                    EmpleadoStr = cmbMozo.SelectedText,
                    CantidadComensales = (int)nudCoemnzales.Value,
                    Estado = DAL.EstadoReserva.Reservado,
                    Fecha = (DateTime)dtpFecha.Value,
                    EstaEliminado = false,
                    Observacion = txtObservacion.Text,

                }

                );

                Mensaje.Mostrar("La reseerva se realizo correctamente.", Mensaje.Tipo.Informacion);

                RealizoAlgunaOperacion = true;

                Close();
            }

            else
            {
                Mensaje.Mostrar("Completar datos obligatorios.",Mensaje.Tipo.Informacion);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }

        private void _10005_Reservar_Mesa_Load(object sender, EventArgs e)
        {
            lblMesa.Text = _mesaServicio.ObtenerPorId(_mesaId).Numero.ToString();
            txtCodigo.Text = _reservaServicio.ObtenerSiguienteCodigo().ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
