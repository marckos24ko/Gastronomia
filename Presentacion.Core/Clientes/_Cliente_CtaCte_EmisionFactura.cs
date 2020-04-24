using Servicio.Core.FacturaEfectivo.Dto;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Clientes
{
    public partial class _Cliente_CtaCte_EmisionFactura : Form
    {
        public _Cliente_CtaCte_EmisionFactura(FacturaDto factura, decimal total, decimal Efectivo, decimal Vuelto)
        {
            InitializeComponent();

            lblCodigo.Text = factura.Numero.ToString();
            lblFecha.Text = factura.Fecha.ToString();
            lblTotal.Text = total.ToString("C2");
            lblCliente.Text = factura.CLienteApynom.ToString();
            lblEfectivo.Text = Efectivo.ToString("C2");
            lblVuelto.Text = Vuelto.ToString("C2");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
