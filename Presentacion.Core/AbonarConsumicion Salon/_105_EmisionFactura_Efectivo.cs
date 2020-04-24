using Servicio.Core.ComprobanteSalon;
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

namespace Presentacion.Core.AbonarConsumicion
{
    public partial class _105_EmisionFactura_Efectivo : Form
    {
        private IFacturaServicio _FacturaServicio;

        public _105_EmisionFactura_Efectivo(decimal Efectivo, decimal Vuelto, ComprobanteSalonDto _comprobante)
        {
            InitializeComponent();

            _FacturaServicio = new FacturaServicio();

            lblCodigo.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Numero.ToString();
            lblFecha.Text = _FacturaServicio.ObtenerUltimaFacturaEmitida().Fecha.ToString();
            lblTotal.Text = _comprobante.Total.ToString("C2");
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
