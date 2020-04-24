using Presentacion.Base.Varios;
using Presentacion.Core.Clientes;
using Presentacion.Core.Condicion_Iva;
using Presentacion.Core.Delivery;
using Presentacion.Core.Empleado;
using Presentacion.Core.Informacion;
using Presentacion.Core.Lista_Precio_Producto;
using Presentacion.Core.ListaPrecio;
using Presentacion.Core.Login;
using Presentacion.Core.Marca;
using Presentacion.Core.Mesa;
using Presentacion.Core.Movimientos;
using Presentacion.Core.Producto;
using Presentacion.Core.Proveedores;
using Presentacion.Core.Rubro;
using Presentacion.Core.SubRubro;
using Presentacion.Core.Venta_en_Salon;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Gatronomia
{
    public partial class Principal : Form
    {
        private Size formSize;
        private string path;
        public Principal()
        {
            InitializeComponent();

            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;

            formSize = new Size(this.Width - 40, this.Height - 96);

            axWinMedPlay.Size = formSize;

            path = Path.GetFullPath(@"VideoPagPrincipal.mp4");
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00001_ConsultaMesas().ShowDialog();
        }

        private void ventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _10001_VentaSalon().ShowDialog();
        }

        private void consultaToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            new _30001_ConsultaEmpleado().ShowDialog();
        }

        private void nuevoEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _30002_ABM_Empleado(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Empleado"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            new _20001_ConsultaClientes().ShowDialog();
        }

        private void nuevaConsultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _20002_ABM_Clientes(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Cliente"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new _50001_ConsultaProducto().ShowDialog();
        }

        private void nuevoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _50002_ABM_Producto(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Producto"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new _40001_ConsultaMarca().ShowDialog();
        }

        private void nuevaMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _40002_ABM_Marca(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Marca"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem3_Click(object sender, EventArgs e)
        {
             new _70001_ConsultaRubro().ShowDialog();
        }

        private void nuevaRubroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _70002_ABM_Rubro(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Rubro"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            new _80001_ConsultaSubRubro().ShowDialog();
        }
        
        private void nuevoSubRubroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _80002_ABM_SubRubro(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Sub-Rubro"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            new _90001_ConsultaListaPrecioProductos().ShowDialog();
        }

        private void nuevaLIstaDePrecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _90002_ABM_ListaPrecioProducto(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Lista Precio Producto"
            };

            formularioNuevo.ShowDialog();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //axWinMedPlay.Ctlcontrols.pause();

            var wfLogin = new WfLogin();
            wfLogin.ShowDialog();

            if (Identidad.Usuario == null)
            {
                Application.ExitThread();
            }

            if (Identidad.Usuario == "admin")
            {
                administracionToolStripMenuItem.Visible = true;
                movimientosToolStripMenuItem.Visible = true;
            }

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";

            axWinMedPlay.URL = path;
            axWinMedPlay.Ctlcontrols.play();

        }

        private void nuevaMesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _00002_ABM_Mesa(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Mesa"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            new _50001_ConsultaListaPrecio().ShowDialog();
        }
        
        private void nuevaLIstaDePrecioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _50002_ABM_ListaPrecios(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Lista de Precios"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            new _60001_ConsultaProveedores().ShowDialog();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _60002_ABM_Provedores(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Proveedor"
            };

            formularioNuevo.ShowDialog();
        }

        private void consultaToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            new _9001_ConsultaCondicionIva().ShowDialog();
        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var formularioNuevo = new _9002_ABM_CondicionIva(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Condicion Iva"
            };

            formularioNuevo.ShowDialog();

        }

        private void pedidoDeProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _5003_PedidoProducto().ShowDialog();
        }

        private void ventaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            new _0020_PreventaDelivery().ShowDialog();
        }

        private void consultaProvedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Movimientos_Provedores().ShowDialog();
        }

        private void consultaClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Movimientos_Clientes().ShowDialog();
        }

        private void InformacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Informacion().ShowDialog();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Identidad.Usuario = null;

            var wfLogin = new WfLogin();
            wfLogin.ShowDialog();

            if (Identidad.Usuario == null)
            {
                Application.ExitThread();
            }

            if (Identidad.Usuario == "admin")
            {
                administracionToolStripMenuItem.Visible = true;
                movimientosToolStripMenuItem.Visible = true;
            }

            else
            {
                administracionToolStripMenuItem.Visible = false;
                movimientosToolStripMenuItem.Visible = false;
            }

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";

            axWinMedPlay.Ctlcontrols.stop();
            axWinMedPlay.Ctlcontrols.play();
        }

        private void administracionToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            administracionToolStripMenuItem.ForeColor = Color.Black;
            administracionToolStripMenuItem.BackColor = Color.Black;
        }

        private void administracionToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            administracionToolStripMenuItem.ForeColor = Color.White;
            administracionToolStripMenuItem.BackColor = Color.Black;
        }

        private void salonToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            salonToolStripMenuItem.ForeColor = Color.Black;
            salonToolStripMenuItem.BackColor = Color.Black;
        }

        private void salonToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            salonToolStripMenuItem.ForeColor = Color.White;
            salonToolStripMenuItem.BackColor = Color.Black;
        }

        private void deliveryToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            deliveryToolStripMenuItem.ForeColor = Color.Black;
            deliveryToolStripMenuItem.BackColor = Color.Black;
        }

        private void deliveryToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            deliveryToolStripMenuItem.ForeColor = Color.White;
            deliveryToolStripMenuItem.BackColor = Color.Black;
        }

        private void movimientosToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            movimientosToolStripMenuItem.ForeColor = Color.Black;
            movimientosToolStripMenuItem.BackColor = Color.Black;
        }

        private void movimientosToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            movimientosToolStripMenuItem.ForeColor = Color.White;
            movimientosToolStripMenuItem.BackColor = Color.Black;
        }

        private void Principal_SizeChanged(object sender, EventArgs e)
        {
            formSize = new Size(this.Width - 40, this.Height - 96);

            axWinMedPlay.Size = formSize;
        }

        private void axWinMedPlay_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWinMedPlay.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWinMedPlay.Ctlcontrols.play();
            }
        }
    }
}
