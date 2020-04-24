using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.Delivery.Dto;
using Servicio.Core.FacturaEfectivo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Delivery
{
    public partial class _0040_Abonar_CtaCte_Delivery : Form
    {
        private ICuentaCorrienteServicio _CtaCteServicio;
        private IClienteServicio _clienteServicio;
        private IComprobanteDeliveryServicio _comprobanteDelivery;
        private IFacturaServicio _facturaServicio;
        private ClienteDto _cliente;
        private ComprobanteDeliveryDto _comprobante;

        public bool RealizoAlgunaOperacion { get; set; }

        public _0040_Abonar_CtaCte_Delivery(ClienteDto Cliente, ComprobanteDeliveryDto Comprobante)
        {
            InitializeComponent();

            _CtaCteServicio = new CuentaCorrienteServicio();
            _clienteServicio = new ClienteServicio();
            _comprobanteDelivery = new ComprobanteDeliveryServicio();
            _facturaServicio = new FacturaServicio();
            _comprobante = Comprobante;
            _cliente = Cliente;

            RealizoAlgunaOperacion = false;

            lblNombreCliente.Text = Cliente.ApyNom;
            lblTotal.Text = Comprobante.Total.ToString("C2");
            lblNumero.Text = _CtaCteServicio.ObtenerCuentaCorrientePorClienteId(Cliente.Id).Numero.ToString();
            lblMontoDisponible.Text = Cliente.MontoMaximoCtaCte.ToString();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            var TotalAPagar = _comprobante.Total;

            var MontoDispinobleCtaCte = _cliente.MontoMaximoCtaCte;

            if (MontoDispinobleCtaCte >= TotalAPagar)
            {
                var TotalPagar = _comprobante.Total;

                var ctacteId = _CtaCteServicio.ObtenerCuentaCorrientePorClienteId(_cliente.Id).Id;

                _clienteServicio.ModificarMontoCtaCte(TotalPagar, _cliente.Id);

                _facturaServicio.EmitirFactura(_cliente.Id, TotalAPagar, _comprobante.CadeteId, ctacteId, _comprobante.Id, 0m);

                _comprobanteDelivery.Cerrar(_comprobante.Id);

                _clienteServicio.ClienteDesocupado(_comprobante.ClienteId);

                _clienteServicio.AgregarGastoActual(_cliente.Id, TotalAPagar);

                RealizoAlgunaOperacion = true;

                var form = new _0060_EmisionFactura_CtaCte_Delivery(_cliente.Id, _comprobante).ShowDialog();

                Close();

            }

            else
            {
                MessageBox.Show(@"No se puede emitir la factura por fondos insuficientes de la Cuenta Corriente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Close();
            }
        }

            private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
