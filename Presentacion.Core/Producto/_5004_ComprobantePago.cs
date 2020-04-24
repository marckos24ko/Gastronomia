using Servicio.Core.PedidoProducto.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class _5004_ComprobantePago : Form
    {
        private PedidoProductoDto _pedido;

        public bool realizoAlgunaOperacion;

        public _5004_ComprobantePago(PedidoProductoDto pedido, decimal total, decimal efectivo, decimal vuelto, string Proveedor)
        {
            InitializeComponent();

            _pedido = pedido;

            lblProvedor.Text = Proveedor;

            lblCantidadProducto.Text = string.Concat(_pedido.Cantidad.ToString(), " ", _pedido.ProductoStr);

            lblFecha.Text = _pedido.Fecha.ToString();

            lblTotal.Text = total.ToString("C2");

            lblVuelto.Text = vuelto.ToString("C2");

            lblEfectivo.Text = efectivo.ToString("C2");

            realizoAlgunaOperacion = false;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            realizoAlgunaOperacion = true;

            Close();
        }

        private void _5004_ComprobantePago_FormClosed(object sender, FormClosedEventArgs e)
        {
            realizoAlgunaOperacion = true;
        }
    }
}
