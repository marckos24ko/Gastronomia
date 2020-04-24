using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.CuentaCorriente;
using Servicio.Core.CuentaCorriente.Dto;
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
    public partial class _0030_Elegir_FormaDePago_Delivery : Form
    {

        private IComprobanteDeliveryServicio _comprobanteDelivery;
        private ICuentaCorrienteServicio _ctaCteServicio;
        private ClienteDto _cliente;
        private CuentaCorrienteDto _ctaCte;

        public bool RealizoAlgunaOperacion { get; set; }

        public _0030_Elegir_FormaDePago_Delivery(ClienteDto Cliente)
        {
            InitializeComponent();

            _comprobanteDelivery = new ComprobanteDeliveryServicio();
            _ctaCteServicio = new CuentaCorrienteServicio();
            _cliente = Cliente;

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


           

            RealizoAlgunaOperacion = false;
        }

        private void btnPagarEfectivo_Click(object sender, EventArgs e)
        {
            var form = new _0050_Abonar_Efectivo_Delivery(_cliente, _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_cliente.Id));

            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                RealizoAlgunaOperacion = true;

                Close();
            }
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            var form = new _0040_Abonar_CtaCte_Delivery(_cliente, _comprobanteDelivery.ObtenerComprobantePorCLienteSinFacturar(_cliente.Id));

            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                RealizoAlgunaOperacion = true;

                Close();
            }
        }
    }
}
