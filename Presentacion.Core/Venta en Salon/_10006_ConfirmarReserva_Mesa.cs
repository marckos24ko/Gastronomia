using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.Reserva;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Venta_en_Salon
{
    public partial class _10006_ConfirmarReserva_Mesa : Form
    {
        private long _mesaId;
        private readonly IComprobanteReservaServicio _reservaServicio;
        private readonly IClienteServicio _clienteServicio;
        private readonly ICuentaCorrienteServicio _CuentaCorrienteServicio;
        private readonly IComprobanteSalon _comprobanteSalon;

        public bool RealizoAlgunaOperacion { get; set; }

        public bool FacturoLaVemta { get; set; }

        public ClienteDto _cliente { get { return _clienteServicio.obtenerPorId(_reservaServicio.obtenerPorMesa(_mesaId).CLienteId); } }

        public long _mozoId { get { return _reservaServicio.obtenerPorMesa(_mesaId).EmpleadoId; } }

        public _10006_ConfirmarReserva_Mesa(long mesaId)
        {
            InitializeComponent();

            _mesaId = mesaId;
            _reservaServicio = new ReservaServicio();
            _clienteServicio = new ClienteServicio();
            _CuentaCorrienteServicio = new CuentaCorrienteServicio();
            _comprobanteSalon = new ComprobanteSalon();
            RealizoAlgunaOperacion = false;
            FacturoLaVemta = false;
        }

        private void _10006_ConfirmarReserva_Mesa_Load(object sender, EventArgs e)
        {
            lblCodigo.Text = _reservaServicio.obtenerPorMesa(_mesaId).Codigo.ToString();
            lblMesa.Text = _mesaId.ToString();
            lblCliente.Text = _reservaServicio.obtenerPorMesa(_mesaId).CLienteStr;
            lblComenzales.Text = _reservaServicio.obtenerPorMesa(_mesaId).CantidadComensales.ToString();
            lblFecha.Text = _reservaServicio.obtenerPorMesa(_mesaId).Fecha.ToString();

        }

        private void bntConfirmar_Click(object sender, EventArgs e)
        {
            _reservaServicio.Cerrar(_reservaServicio.obtenerPorMesa(_mesaId).Id);

            _comprobanteSalon.Crear(_mesaId, _mozoId, _cliente.Id);

            _clienteServicio.ClienteOcupado(_cliente);

            RealizoAlgunaOperacion = true;

            Close();

            var ventaSalon = new _10002_Venta(_mesaId, _cliente, _mozoId);
            
            ventaSalon.ShowDialog();

            if (ventaSalon.RealizoAlgunaOperacion)
            {
                FacturoLaVemta = true;
            }

        }
    }
}
