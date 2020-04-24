using Servicio.Core.Cliente;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.FacturaEfectivo;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.AbonarConsumicion
{
    public partial class _104_EmisionFactura_CtaCte : Form
    {
        private ICuentaCorrienteServicio _ctacte;
        private IFacturaServicio _FacturaServicio;
        private IClienteServicio _clienteServicio;

        public _104_EmisionFactura_CtaCte(long ClienteId, ComprobanteSalonDto _comprobante)
        {
            InitializeComponent();

            _ctacte = new CuentaCorrienteServicio();
            _FacturaServicio = new FacturaServicio();
            _clienteServicio = new ClienteServicio();

            lblCodigo.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Numero.ToString();
            lblCodCtaCte.Text = _ctacte.ObtenerCuentaCorrientePorClienteId(ClienteId).Numero.ToString();
            lblCliente.Text = _ctacte.ObtenerCuentaCorrientePorClienteId(ClienteId).ClienteApyNom.ToString();
            lblFecha.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Fecha.ToString();
            lblTotal.Text = _comprobante.Total.ToString("C2");

            var cliente = _clienteServicio.obtenerPorId(ClienteId);

            if (cliente.DeudaTotal > 1000m)
            {
                _clienteServicio.DesactivarParaCompras(ClienteId);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
