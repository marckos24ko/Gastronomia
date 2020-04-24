using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.Mesa;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.AbonarConsumicion
{
    public partial class _102_Abonar_CtaCte : Form
    {
        private ICuentaCorrienteServicio _CtaCteServicio;
        private IClienteServicio _clienteServicio;
        private IComprobanteSalon _comprobanteSalon;
        private IMesaServicio _mesaServicio;
        private IFacturaServicio _facturaServicio;
        private ClienteDto _cliente;
        private ComprobanteSalonDto _comprobante; 
        private long _mesaId;
        private bool _realizoAlgunaOperacion;
        public bool RealizoAlgunaOperacion { get { return _realizoAlgunaOperacion; } }
        private decimal totalAPagar;


        public _102_Abonar_CtaCte(ClienteDto Cliente, ComprobanteSalonDto Comprobante, long mesaId)
        {
            InitializeComponent();

            _CtaCteServicio = new CuentaCorrienteServicio();
            _clienteServicio = new ClienteServicio();
            _comprobanteSalon = new ComprobanteSalon();
            _mesaServicio = new MesaServicio();
            _facturaServicio = new FacturaServicio();
            _cliente = Cliente;
            _comprobante = Comprobante;
            _mesaId = mesaId;
            _realizoAlgunaOperacion = false;

            lblNombreCliente.Text = Cliente.ApyNom;

            totalAPagar = Comprobante.Total;

           lblTotal.Text = Comprobante.Total.ToString("C2");
          

            lblNumero.Text = _CtaCteServicio.ObtenerCuentaCorrientePorClienteId(Cliente.Id).Numero.ToString();
            lblMontoDisponible.Text = Cliente.MontoMaximoCtaCte.ToString();


        }

        private void btnPagar_Click(object sender, EventArgs e)
        {

            var MontoDispinobleCtaCte = _cliente.MontoMaximoCtaCte;

            if (MontoDispinobleCtaCte >= totalAPagar)
            {

                var ctacteId = _CtaCteServicio.ObtenerCuentaCorrientePorClienteId(_cliente.Id).Id;

                _clienteServicio.ModificarMontoCtaCte(totalAPagar, _cliente.Id);

                _facturaServicio.EmitirFactura(_cliente.Id, totalAPagar, _comprobante.MozoId, ctacteId, _comprobante.Id, 0m);

                _comprobanteSalon.Cerrar(_comprobante.Id);

                _mesaServicio.CambiarEstado(_mesaId, EstadoMesa.Facturada);

                _clienteServicio.ClienteDesocupado(_comprobante.ClienteId);

                _clienteServicio.AgregarGastoActual(_cliente.Id, totalAPagar);

                _realizoAlgunaOperacion = true;

                var form = new _104_EmisionFactura_CtaCte(_cliente.Id, _comprobante).ShowDialog();

                Close();
            }

            else
            {
                Mensaje.Mostrar("No se puede emitir la factura por fondos insuficientes de la Cuenta Corriente",Mensaje.Tipo.Stop);

                Close();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
            
