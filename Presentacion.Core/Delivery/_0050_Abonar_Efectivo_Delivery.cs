using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.Delivery.Dto;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.Movimientos;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Delivery
{
    public partial class _0050_Abonar_Efectivo_Delivery : Form
    {

        private IFacturaServicio _facturaServicio;
        private IMovimientoServicio _movimientoServicio;
        private IClienteServicio _clienteServicio;
        private IComprobanteDeliveryServicio _comprobanteDelivery;
        private ClienteDto _cliente;
        private ComprobanteDeliveryDto _comprobante;

        public bool RealizoAlgunaOperacion { get; set; }

        public _0050_Abonar_Efectivo_Delivery(ClienteDto Cliente, ComprobanteDeliveryDto Comprobante)
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();
            _facturaServicio = new FacturaServicio();
            _movimientoServicio = new MovimientoServicio();
            _comprobanteDelivery = new ComprobanteDeliveryServicio();
            _cliente = Cliente;
            _comprobante = Comprobante;

            lblNombreCliente.Text = Cliente.ApyNom;
            lblTotal.Text = Comprobante.Total.ToString("C2");

            txtEfectivo.KeyPress += Validacion.NoLetras;
            txtEfectivo.KeyPress += Validacion.NoInyeccion;

            txtEfectivo.Enter += txt_Enter;
            txtEfectivo.Leave += txt_Leave; 

            RealizoAlgunaOperacion = false;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            var TotalAPagar = _comprobante.Total;

            if (string.IsNullOrEmpty(txtEfectivo.Text))
            {
                Mensaje.Mostrar("Ingrese un valor en el campo efectivo.", Mensaje.Tipo.Informacion);
                txtEfectivo.Focus();
            }

            else
            {

                if (decimal.Parse(txtEfectivo.Text.Trim()) >= TotalAPagar)
                {
                    var vuelto = decimal.Parse(txtEfectivo.Text) - TotalAPagar;

                    _facturaServicio.EmitirFactura(_comprobante.ClienteId, _comprobante.Total, _comprobante.CadeteId, null, _comprobante.Id, _comprobante.Total);

                    var facturaId = _facturaServicio.ObtenerUltimaFacturaEmitida().Id;

                    _movimientoServicio.EmitirMovimiento(_comprobante.ClienteId, TotalAPagar, TipoMovimiento.Ingreso, facturaId, null);

                    _comprobanteDelivery.Cerrar(_comprobante.Id);

                    _clienteServicio.ClienteDesocupado(_comprobante.ClienteId);

                    RealizoAlgunaOperacion = true;

                    var form = new _0070_EmisionFactura_Efectivo_Delivery(decimal.Parse(txtEfectivo.Text), vuelto, _comprobante).ShowDialog();

                    Close();
                }

                else
                {
                    Mensaje.Mostrar("No se puede abonar la factura por fondos insuficientes.",Mensaje.Tipo.Informacion);

                    txtEfectivo.Clear();
                    txtEfectivo.Focus();
                }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
