using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.CuentaCorriente.Dto;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.AbonarConsumicion
{
    public partial class _101_Elegir_FormaDePago : Form
    {
        private IComprobanteSalon _comprobanteSalon;
        private ICuentaCorrienteServicio _ctaCteServicio;
        private ClienteDto _cliente;
        private CuentaCorrienteDto _ctaCte;
        private long _mesaId;
        private bool _realizoAlgunaOperacion;
        public bool RealizoAlgunaOperacion{ get { return _realizoAlgunaOperacion; } }
        private decimal? _seña;


        public _101_Elegir_FormaDePago(ClienteDto Cliente, long MesaId)
        {
            InitializeComponent();
            _cliente = Cliente;
            _mesaId = MesaId;
            _comprobanteSalon = new ComprobanteSalon();
            _ctaCteServicio = new CuentaCorrienteServicio();

            if (_ctaCteServicio.verificarSiTieneCtaCte(_cliente.Id))
            {

                _ctaCte = _ctaCteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_cliente.Id);

                if (_cliente.TieneCuentaCorriente == false || _ctaCte.EstaHabilitada == false)
                {
                    btnCuentaCorriente.Visible = false;
                    PBCtaCte.Visible = false;
                }

            }

            else
            {
                if (_cliente.TieneCuentaCorriente == false)
                {
                    btnCuentaCorriente.Visible = false;
                    PBCtaCte.Visible = false;
                }
            }

            _realizoAlgunaOperacion = false;

            
        }

        private void btnPagarEfectivo_Click(object sender, EventArgs e)
        {

            var form = new _103_Abonar_Efectivo(_cliente, _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_mesaId), _mesaId);

            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                _realizoAlgunaOperacion = true;

                Close();
            }

        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            var form = new _102_Abonar_CtaCte(_cliente, _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_mesaId), _mesaId);

            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                _realizoAlgunaOperacion = true;

                Close();
            }

        }
    }
}
