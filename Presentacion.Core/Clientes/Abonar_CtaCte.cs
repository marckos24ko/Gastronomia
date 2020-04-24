using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.FacturaEfectivo.Dto;
using Servicio.Core.Movimientos;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Clientes
{
    public partial class Abonar_CtaCte : Form
    {
        private IMovimientoServicio _movimientoServicio;
        private IFacturaServicio _facturServicio;
        private IClienteServicio _clienteServicio;
        private long _clienteId;
        private FacturaDto _factura;
        private ClienteDto _cliente;

        public bool RealizoAlgunaOperacion { get; set; }

        public Abonar_CtaCte(FacturaDto factura, long ClienteId)
        {
            InitializeComponent();

            _movimientoServicio = new MovimientoServicio();
            _facturServicio = new FacturaServicio();
            _clienteServicio = new ClienteServicio();
            _clienteId = ClienteId;
            _factura = factura;
            RealizoAlgunaOperacion = false;
            lblTotal.Text = (_factura.Total - _factura.TotalAbonado).ToString("C2");

            txtEfectivo.KeyPress += Validacion.NoInyeccion;
            txtEfectivo.KeyPress += Validacion.NoSimbolos;
            txtEfectivo.KeyPress += Validacion.NoLetras;

            txtEfectivo.Enter += txt_Enter;
            txtEfectivo.Leave += txt_Leave;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            var TotalAPagar = _factura.Total - _factura.TotalAbonado;

            var facturaId = _factura.Id;

            if (string.IsNullOrEmpty(txtEfectivo.Text))
            {
                Mensaje.Mostrar("Ingrese un valor en el campo efectivo.", Mensaje.Tipo.Informacion);
            }

            else
            {
                if (decimal.Parse(txtEfectivo.Text.Trim()) >= TotalAPagar)
                {
                    var vuelto = decimal.Parse(txtEfectivo.Text.Trim()) - TotalAPagar;

                    _movimientoServicio.EmitirMovimiento(_clienteId, TotalAPagar, TipoMovimiento.Ingreso, facturaId, null);

                    _facturServicio.ModificarEstado(facturaId, TotalAPagar);

                    _clienteServicio.RestarPagoActual(_clienteId, TotalAPagar);

                    _cliente = _clienteServicio.obtenerPorId(_clienteId);

                    if (_cliente.DeudaTotal < 1000m)
                    {
                        _clienteServicio.ActivarParaCompras(_clienteId);
                    }

                    RealizoAlgunaOperacion = true;

                    var form = new _Cliente_CtaCte_EmisionFactura(_factura, TotalAPagar, decimal.Parse(txtEfectivo.Text), vuelto).ShowDialog();

                    Close();
                }

                else
                {
                    _movimientoServicio.EmitirMovimiento(_clienteId, decimal.Parse(txtEfectivo.Text), TipoMovimiento.Ingreso, facturaId, null);

                    _facturServicio.ModificarEstado(facturaId, decimal.Parse(txtEfectivo.Text));

                    _clienteServicio.RestarPagoActual(_clienteId, decimal.Parse(txtEfectivo.Text.Trim()));

                    _cliente = _clienteServicio.obtenerPorId(_clienteId);

                    if (_cliente.DeudaTotal < 1000m)
                    {
                        _clienteServicio.ActivarParaCompras(_clienteId);
                    }

                    RealizoAlgunaOperacion = true;

                    var form = new _Cliente_CtaCte_EmisionFactura(_factura, decimal.Parse(txtEfectivo.Text), decimal.Parse(txtEfectivo.Text), 0m).ShowDialog();

                    Close();
                }
            }

          
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
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
