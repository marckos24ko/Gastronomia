using Servicio.Core.Cliente;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.Delivery.Dto;
using Servicio.Core.FacturaEfectivo;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Delivery
{
    public partial class _0060_EmisionFactura_CtaCte_Delivery : Form
    {

        private ICuentaCorrienteServicio _ctacte;
        private IFacturaServicio _FacturaServicio;
        private IClienteServicio _clienteServicio;

        public _0060_EmisionFactura_CtaCte_Delivery(long ClienteId, ComprobanteDeliveryDto _comprobante)
        {
            InitializeComponent();

            _ctacte = new CuentaCorrienteServicio();
            _FacturaServicio = new FacturaServicio();
            _clienteServicio = new ClienteServicio();

            lblCodigo.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Numero.ToString();
            lblCodCtaCte.Text = _ctacte.ObtenerCuentaCorrientePorClienteId(ClienteId).Numero.ToString();
            lblCliente.Text = _ctacte.ObtenerCuentaCorrientePorClienteId(ClienteId).ClienteApyNom.ToString();
            lblFecha.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Fecha.ToString("dd/MM/yyyy");
            lblTotal.Text = _comprobante.Total.ToString("C2");

            var cliente = _clienteServicio.obtenerPorId(ClienteId);

            if (cliente.DeudaTotal > 1000m)
            {
                _clienteServicio.DesactivarParaCompras(cliente.Id);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
