using DAL;
using Presentacion.Base.Varios;
using Presentacion.Core.Venta_en_Salon;
using Servicio.Core.Cliente;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.Mesa;
using Servicio.Core.Producto;
using Servicio.Core.Reserva;
using Servicio.Core.SalonMesa;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.ControlesUsuarios
{
    public partial class UserControlMesa : UserControl
    {
        // Atributos
        private long _id;
        private long _comprobanteId;
        private readonly IMesaServicio _mesaServicio;
        private readonly IComprobanteSalon _comprobanteSalon;
        private readonly IClienteServicio _clienteServicio;
        private readonly IComprobanteReservaServicio _reservaServicio;
        private readonly ISaloMesaServicio _salonMesaServicio;
        private readonly IProductoServicio _productoServicio;

        // Propiedades 
        public long Id {
            set { _id = value; }
        }

        public long ComprobanteId
        {
            set { _comprobanteId = value; }
        }

        private EstadoMesa _estado;

        public EstadoMesa EstadoMesa
        {
            set
            {
                _estado = value;

                switch (value)
                {
                    case EstadoMesa.Libre:
                        pnlEstado.BackColor = Color.Green;
                        cambiarDeMesaToolStripMenuItem.Visible = false;
                        cerrarMesaToolStripMenuItem.Visible = false;
                        abrirMesaToolStripMenuItem.Visible = true;
                        abrirMesaToolStripMenuItem.Text = "Abrir Mesa";
                        reservarMesaToolStripMenuItem.Visible = true;
                        habilitarMesaToolStripMenuItem.Visible = false;
                        cerrarTemporalmenteToolStripMenuItem.Visible = true;
                        cancelarVentaToolStripMenuItem.Visible = false;
                        break;

                    case EstadoMesa.Reservada:
                        pnlEstado.BackColor = Color.Blue;
                        cambiarDeMesaToolStripMenuItem.Visible = false;
                        cerrarMesaToolStripMenuItem.Visible = true;
                        cerrarMesaToolStripMenuItem.Text = "Cancelar Reserva";
                        abrirMesaToolStripMenuItem.Visible = true;
                        abrirMesaToolStripMenuItem.Text = "Confirmar Reserva";
                        reservarMesaToolStripMenuItem.Visible = false;
                        habilitarMesaToolStripMenuItem.Visible = false;
                        cerrarTemporalmenteToolStripMenuItem.Visible = false;
                        cancelarVentaToolStripMenuItem.Visible = false;
                        break;

                    case EstadoMesa.Ocupada:
                        pnlEstado.BackColor = Color.Red;
                        cambiarDeMesaToolStripMenuItem.Visible = true;
                        cerrarMesaToolStripMenuItem.Visible = false;
                        cerrarMesaToolStripMenuItem.Text = "Cerrar Mesa";
                        abrirMesaToolStripMenuItem.Visible = false;
                        reservarMesaToolStripMenuItem.Visible = false;
                        habilitarMesaToolStripMenuItem.Visible = false;
                        cerrarTemporalmenteToolStripMenuItem.Visible = false;
                        cancelarVentaToolStripMenuItem.Visible = true;
                        break;

                    case EstadoMesa.Reparacion:
                        pnlEstado.BackColor = Color.Orange;
                        habilitarMesaToolStripMenuItem.Visible = true;
                        cambiarDeMesaToolStripMenuItem.Visible = false;
                        cerrarMesaToolStripMenuItem.Visible = false;
                        abrirMesaToolStripMenuItem.Visible = false;
                        reservarMesaToolStripMenuItem.Visible = false;
                        cerrarTemporalmenteToolStripMenuItem.Visible = false;
                        cancelarVentaToolStripMenuItem.Visible = false;
                        break;

                    case EstadoMesa.Facturada:
                        pnlEstado.BackColor = Color.Black;
                        cambiarDeMesaToolStripMenuItem.Visible = false;
                        cerrarMesaToolStripMenuItem.Visible = true;
                        abrirMesaToolStripMenuItem.Visible = false;
                        reservarMesaToolStripMenuItem.Visible = false;
                        cerrarTemporalmenteToolStripMenuItem.Visible = false;
                        habilitarMesaToolStripMenuItem.Visible = false;
                        cancelarVentaToolStripMenuItem.Visible = false;
                        break;
                }
            }
        }

        public decimal Total
        {
            set { lblTotal.Text = value.ToString("C2"); }
        }

        public int Numero
        {
            set { lblNumeroMesa.Text = value.ToString(); }
        }
        public UserControlMesa()
        {
            InitializeComponent();
            _mesaServicio = new MesaServicio();
            _comprobanteSalon = new ComprobanteSalon();
            _clienteServicio = new ClienteServicio();
            _reservaServicio = new ReservaServicio();
            _salonMesaServicio = new SaloMesaServicio();
            _productoServicio = new ProductoServicio();
        }

        private void lblNumeroMesa_DoubleClick(object sender, System.EventArgs e)
        {

            switch (_estado)
            {
                case EstadoMesa.Ocupada:

                  var comprobante = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_id);

                  var cliente = _clienteServicio.obtenerPorId(comprobante.ClienteId);

                  Total = comprobante.Total;
                
                  var formulario = new _10002_Venta(_id, cliente, comprobante.MozoId);

                      formulario.ShowDialog();

                    if(formulario.RealizoAlgunaOperacion)
                    {
                        _mesaServicio.CambiarEstado(_id, EstadoMesa.Facturada);
                        EstadoMesa = EstadoMesa.Facturada;
                        Total = _mesaServicio.ObtenerPorId(_id).Total;
                    }

                    else
                    {
                        var comprobante1 = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_id);

                        var cliente2 = _clienteServicio.obtenerPorId(comprobante.ClienteId);

                        Total = comprobante1.Total;

                    }

                    break;

                 case EstadoMesa.Libre:

                    Mensaje.Mostrar("La mesa no esta abierta.",Mensaje.Tipo.Informacion);

                    break;

                case EstadoMesa.Reservada:

                    Mensaje.Mostrar("La mesa se encuentra reservada pero no abierta.", Mensaje.Tipo.Informacion);

                    break;

                case EstadoMesa.Reparacion:

                    Mensaje.Mostrar("La mesa se encuentra en reparacion, no se puede abrir.", Mensaje.Tipo.Informacion);

                    break;

                case EstadoMesa.Facturada:

                    Mensaje.Mostrar("La mesa ya ha sido facturada, cerrarla para poder abrirla.", Mensaje.Tipo.Informacion);

                    break;


            }
        }

        private void abrirMesaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

            switch (_estado)
            {
                case EstadoMesa.Libre:

                    var Formulario = new _10003_Cliente_Mozo_Lookup(_id);
                    Formulario.ShowDialog();

                    if (Formulario.RealizoAlgunaOperacion)
                    {
                        _mesaServicio.CambiarEstado(_id, EstadoMesa.Ocupada);
                        EstadoMesa = EstadoMesa.Ocupada;
                        Total = _mesaServicio.ObtenerPorId(_id).Total;
                    }

                    if(Formulario.FacturoLaVenta)
                    {
                        _mesaServicio.CambiarEstado(_id, EstadoMesa.Facturada);
                        EstadoMesa = EstadoMesa.Facturada;
                        Total = _mesaServicio.ObtenerPorId(_id).Total;
                    }

                    break;

                case EstadoMesa.Reservada:

                    var form = new _10006_ConfirmarReserva_Mesa(_id);
                    form.ShowDialog();

                    if (form.RealizoAlgunaOperacion)
                    {
                        _mesaServicio.CambiarEstado(_id, EstadoMesa.Ocupada);
                        EstadoMesa = EstadoMesa.Ocupada;
                        Total = _mesaServicio.ObtenerPorId(_id).Total;

                    }

                    if (form.FacturoLaVemta)
                    {
                        _mesaServicio.CambiarEstado(_id, EstadoMesa.Facturada);
                        EstadoMesa = EstadoMesa.Facturada;
                        Total = _mesaServicio.ObtenerPorId(_id).Total;

                    }

                    break;

            }

        }

        private void cerrarMesaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            switch (_estado)
            {
                case EstadoMesa.Ocupada:

                    var comprob = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_id);

                    if (comprob.Estado == EstadoSalon.Pendiente)
                    {
                        Mensaje.Mostrar("La mesa elegida no ha sido facturada, no se puede cerrar.",Mensaje.Tipo.Stop);
                    }

                    break;

                case EstadoMesa.Reservada:

                    if (Mensaje.Mostrar(@"Está seguro de querer cancelar la reserva?", Mensaje.Tipo.Pregunta) == System.Windows.Forms.DialogResult.Yes)

                    {
                        var clienteId = _reservaServicio.obtenerPorMesa(_id).CLienteId;

                        _reservaServicio.Cancelar(_id);

                        _mesaServicio.CambiarEstado(_id, EstadoMesa.Libre);
                        EstadoMesa = EstadoMesa.Libre;
                        Total = 0m;

                        _clienteServicio.ClienteDesocupado(clienteId);
                    }


                    break;

                case EstadoMesa.Facturada:

                _mesaServicio.CambiarEstado(_id, EstadoMesa.Libre);
                EstadoMesa = EstadoMesa.Libre;
                Total = 0m;

                break;

            }


        }

        private void cambiarDeMesaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var comprob = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_id);

                var cambiarMesa = new _10004_Mesa_LookUp();

                cambiarMesa.ShowDialog();

            if (cambiarMesa.RealizoAlgunaOperacion)
            {
                // abro la mesa a la que me cambio
                _mesaServicio.CambiarEstado(cambiarMesa.Mesa.Id, EstadoMesa.Ocupada);
                _comprobanteSalon.modificar(_id, cambiarMesa.Mesa.Id);


                //cierro la mesa de la que me cambio
                _mesaServicio.CambiarEstado(_id, EstadoMesa.Libre);
                EstadoMesa = EstadoMesa.Libre;
                Total = 0m;

                var form = Form.ActiveForm;

                form.Close();

                var form1 = new _10001_VentaSalon().ShowDialog();


            }
        }

        private void reservarMesaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var reservarMesa = new _10005_Reservar_Mesa(_id);
            reservarMesa.ShowDialog();

            if(reservarMesa.RealizoAlgunaOperacion)
            {
                _mesaServicio.CambiarEstado(_id, EstadoMesa.Reservada);
                EstadoMesa = EstadoMesa.Reservada;
            }

        }

        private void habilitarMesaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _mesaServicio.CambiarEstado(_id, EstadoMesa.Libre);
            EstadoMesa = EstadoMesa.Libre;
            Total = 0m;
        }

        private void cerrarTemporalmenteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _mesaServicio.CambiarEstado(_id, EstadoMesa.Reparacion);
            EstadoMesa = EstadoMesa.Reparacion;
            Total = 0m;
        }

        private void cancelarVentaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (Mensaje.Mostrar(@"Seguro desea cancelar la Compra?", Mensaje.Tipo.Pregunta) == System.Windows.Forms.DialogResult.Yes)
            {
                var comprobante = _comprobanteSalon.ObtenerComprobantePorMesaSinFacturar(_id);
                var listaProductos = _productoServicio.ObtenerPorFiltro(string.Empty);
                var listaProductosEnLaVenta = new List<ProductoDto>();

                foreach (var item in comprobante.ComprobanteSalonDetalleDtos)
                {
                    var producto = new ProductoDto()
                    {
                        Id = item.ProductoId,
                        Descripcion = item.Descripcion,
                        Stock = item.Cantidad
                    };

                    listaProductosEnLaVenta.Add(producto);
                }

                foreach (var item in listaProductosEnLaVenta)
                {
                    foreach (var item2 in listaProductos)
                    {
                        if (item.Id == item2.Id)
                        {
                            _productoServicio.AumentarStockPorCancelarVenta(item2, item.Stock);
                        }
                    }
                }

                _clienteServicio.ClienteDesocupado(comprobante.ClienteId);

                _comprobanteSalon.Eliminar(comprobante.Id, _id);

                _mesaServicio.CambiarEstado(_id, EstadoMesa.Libre);
                EstadoMesa = EstadoMesa.Libre;
                Total = 0m;
            }

           

           
        }
    }
}
