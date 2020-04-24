using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.ListaPrecioProducto;
using System.Windows.Forms;

namespace Presentacion.Core.Lista_Precio_Producto
{
    public partial class _90001_ConsultaListaPrecioProductos : FormularioConsulta
    {
        private readonly IListaPrecioProductoServicio _ListaPrecioProductoServicio;

        private readonly IComprobanteDeliveryServicio _comprobanteDeliveryServicio;

        private readonly IComprobanteSalon _comprobanteSalonServicio;

        public _90001_ConsultaListaPrecioProductos(): this(new ListaPrecioProductoServicio())
        {
            Titulo = "Lista de Pecio Productos";
        }

        public _90001_ConsultaListaPrecioProductos(IListaPrecioProductoServicio listaPrecioproductoServicio)
        {
            InitializeComponent();

            _ListaPrecioProductoServicio = new ListaPrecioProductoServicio();
            _comprobanteDeliveryServicio = new ComprobanteDeliveryServicio();
            _comprobanteSalonServicio = new ComprobanteSalon();

        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _ListaPrecioProductoServicio.Obtener();
            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = true;
            dgvGrilla.Columns["Id"].HeaderText = @"Código";
            dgvGrilla.Columns["Id"].Width = 50;
            dgvGrilla.Columns["Id"].DisplayIndex = 0;

            dgvGrilla.Columns["ProductoStr"].Visible = true;
            dgvGrilla.Columns["ProductoStr"].HeaderText = @"Producto";
            dgvGrilla.Columns["ProductoStr"].DisplayIndex = 1;
            dgvGrilla.Columns["ProductoStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["ListaPrecioStr"].Visible = true;
            dgvGrilla.Columns["ListaPrecioStr"].HeaderText = @"Lista de Precio";
            dgvGrilla.Columns["ListaPrecioStr"].DisplayIndex = 2;
            dgvGrilla.Columns["ListaPrecioStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvGrilla.Columns["PrecioCosto"].Visible = true;
            dgvGrilla.Columns["PrecioCosto"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["PrecioCosto"].HeaderText = @"Precio de Costo";
            dgvGrilla.Columns["PrecioCosto"].Width = 60;
            dgvGrilla.Columns["PrecioCosto"].DisplayIndex = 3;

            dgvGrilla.Columns["PrecioPublico"].Visible = true;
            dgvGrilla.Columns["PrecioPublico"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["PrecioPublico"].HeaderText = @"Precio Publico";
            dgvGrilla.Columns["PrecioPublico"].Width = 60;
            dgvGrilla.Columns["PrecioPublico"].DisplayIndex = 4;

            dgvGrilla.Columns["FechaActualizacion"].Visible = true;
            dgvGrilla.Columns["FechaActualizacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvGrilla.Columns["FechaActualizacion"].HeaderText = @"Fecha de Actualizacion";
            dgvGrilla.Columns["FechaActualizacion"].DisplayIndex = 5;
            dgvGrilla.Columns["FechaActualizacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["ListaPrecioId"].Visible = false;
            dgvGrilla.Columns["ProductoId"].Visible = false;
            dgvGrilla.Columns["EstaEliminado"].Visible = false;
        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _90002_ABM_ListaPrecioProducto(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Lista Precio Producto"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion = true;
        }

        public override bool EjecutarComandoModificar()
        {
            var ComprobanteSalon = _comprobanteSalonServicio.ObtenerComprobantesSinFacturar();

            var ComprobanteDelivery = _comprobanteDeliveryServicio.ObtenerComprobantesSinFacturar();



            if (ComprobanteSalon == false)
            {
                if (ComprobanteDelivery == false)
                {

                    var formularioModificar = new _90002_ABM_ListaPrecioProducto(Constante.TipoOperacion.Modificar, EntidadId)
                    {
                        Text = "Moddificar Lista Precio Producto"
                    };
                    formularioModificar.ShowDialog();
                    return formularioModificar.RealizoAlgunaOperacion = true;
                }

                else
                {

                    MessageBox.Show("No se puede modificar por que esta siendo usada, cierre todas las ventas abiertas para modificarla", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {

                MessageBox.Show("No se puede modificar por que esta siendo usada, cierre todas las ventas abiertas para modificarla", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            var ComprobanteSalon = _comprobanteSalonServicio.ObtenerComprobantesSinFacturar();

           var ComprobanteDelivery = _comprobanteDeliveryServicio.ObtenerComprobantesSinFacturar();

            if (ComprobanteSalon == false)
            {
                if (ComprobanteDelivery == false)
                {
                    var formularioEliminar = new _90002_ABM_ListaPrecioProducto(Constante.TipoOperacion.Eliminar, EntidadId)
                    {
                        Text = "Eliminar Lista Precio Producto"
                    };
                    formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion = true;
            }
                else
                {

                    MessageBox.Show("No se puede eliminar por que esta siendo usada, cierre todas las ventas abiertas para eliminarla", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            else
            {

                MessageBox.Show("No se puede eliminar por que esta siendo usada, cierre todas las ventas abiertas para eliminarla", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return true;
        }
    }
}
