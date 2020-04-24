using DAL;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.Mesa;
using Servicio.Core.Movimientos;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.AbonarConsumicion
{
    public partial class _103_Abonar_Efectivo : Form
    {
        private IFacturaServicio _facturaServicio;
        private IMovimientoServicio _movimientoServicio;
        private IClienteServicio _clienteServicio;
        private IComprobanteSalon _comprobanteSalon;
        private ClienteDto _cliente;
        private ComprobanteSalonDto _comprobante;
        private IMesaServicio _mesaServicio;
        private long _mesaId;
        private bool _realizoAlgunaOperacion;
        private decimal totalAPagar;

        public bool RealizoAlgunaOperacion { get { return _realizoAlgunaOperacion; } }

        public _103_Abonar_Efectivo(ClienteDto Cliente, ComprobanteSalonDto Comprobante, long mesaId)
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();
            _cliente = Cliente;
            _comprobante = Comprobante;
            _comprobanteSalon = new ComprobanteSalon();
            _mesaServicio = new MesaServicio();
            _mesaId = mesaId;
            _realizoAlgunaOperacion = false;
            _facturaServicio = new FacturaServicio();
            _movimientoServicio = new MovimientoServicio();
            lblNombreCliente.Text = Cliente.ApyNom;
           
            totalAPagar = Comprobante.Total;

            lblTotal.Text = Comprobante.Total.ToString("C2");

            txtEfectivo.KeyPress += Validacion.NoLetras;
            txtEfectivo.KeyPress += Validacion.NoSimbolos;
            txtEfectivo.KeyPress += Validacion.NoInyeccion;

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEfectivo.Text))
            {
                Mensaje.Mostrar("Ingrese un valor en el campo efectivo.", Mensaje.Tipo.Informacion);

                txtEfectivo.Focus();

            }

            else
            {
                if (decimal.Parse(txtEfectivo.Text) >= totalAPagar)
                {
                    var vuelto = decimal.Parse(txtEfectivo.Text) - totalAPagar;

                    _facturaServicio.EmitirFactura(_comprobante.ClienteId, totalAPagar, _comprobante.MozoId, null, _comprobante.Id, totalAPagar);

                    var facturaId = _facturaServicio.ObtenerUltimaFacturaEmitida().Id;

                    _movimientoServicio.EmitirMovimiento(_comprobante.ClienteId, totalAPagar, TipoMovimiento.Ingreso, facturaId, null);

                    _comprobanteSalon.Cerrar(_comprobante.Id);

                    _mesaServicio.CambiarEstado(_mesaId, EstadoMesa.Facturada);

                    _clienteServicio.ClienteDesocupado(_comprobante.ClienteId);

                    _realizoAlgunaOperacion = true;

                    var form = new _105_EmisionFactura_Efectivo(decimal.Parse(txtEfectivo.Text), vuelto, _comprobante).ShowDialog();

                    Close();
                }

                else
                {
                    Mensaje.Mostrar("No se puede abonar la factura por fondos insuficientes.", Mensaje.Tipo.Stop);

                    txtEfectivo.Clear();
                    txtEfectivo.Focus();
                }
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
