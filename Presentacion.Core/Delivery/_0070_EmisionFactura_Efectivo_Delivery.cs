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
    public partial class _0070_EmisionFactura_Efectivo_Delivery : Form
    {
        private IFacturaServicio _FacturaServicio;

        public _0070_EmisionFactura_Efectivo_Delivery(decimal Efectivo, decimal Vuelto, ComprobanteDeliveryDto Comprobante)
        {
            InitializeComponent();

            _FacturaServicio = new FacturaServicio();

            lblCodigo.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Numero.ToString();
            lblFecha.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Fecha.ToString("dd/MM/yyyy");
            lblTotal.Text = Comprobante.Total.ToString("C2");
            lblCliente.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().CLienteApynom.ToString();
            lblEfectivo.Text = Efectivo.ToString("C2");
            lblVuelto.Text = Vuelto.ToString("C2");
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
