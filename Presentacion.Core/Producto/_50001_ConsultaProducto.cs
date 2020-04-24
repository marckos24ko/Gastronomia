using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.Producto;
using Servicio.Core.Proveedores;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class _50001_ConsultaProducto : FormularioConsulta
    {
        private readonly IProductoServicio _productoServicio;
        private readonly IComprobanteSalon _ComprobanteSalonServicio;
        private readonly IComprobanteDeliveryServicio _ComproanteDeliveryServicio;
        private readonly IProveedorServicio _proveedorServicio;
        private ProductoDto _producto;

        public _50001_ConsultaProducto() : this(new ProductoServicio())
        {
            Titulo = "Lista de Productos";
        }

        public _50001_ConsultaProducto(IProductoServicio productoServicio)
        {
            InitializeComponent();

            _productoServicio = productoServicio;
            _ComprobanteSalonServicio = new ComprobanteSalon();
            _ComproanteDeliveryServicio = new ComprobanteDeliveryServicio();
            _proveedorServicio = new ProvedoresServicio();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _productoServicio.ObtenerPorFiltro(cadenaBuscar);
            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Código";
            dgvGrilla.Columns["Codigo"].Width = 50;
            dgvGrilla.Columns["Codigo"].DisplayIndex = 0;

            dgvGrilla.Columns["CodigoBarra"].Visible = true;
            dgvGrilla.Columns["CodigoBarra"].HeaderText = @"Código de barra";
            dgvGrilla.Columns["CodigoBarra"].Width = 150;
            dgvGrilla.Columns["CodigoBarra"].DisplayIndex = 1;

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";
            dgvGrilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["Descripcion"].DisplayIndex = 2;

            dgvGrilla.Columns["MarcaStr"].Visible = true;
            dgvGrilla.Columns["MarcaStr"].HeaderText = "Marca";
            dgvGrilla.Columns["MarcaStr"].Width = 100;
            dgvGrilla.Columns["MarcaStr"].DisplayIndex = 3;

            dgvGrilla.Columns["RubroStr"].Visible = true;
            dgvGrilla.Columns["RubroStr"].HeaderText = @"Rubro";
            dgvGrilla.Columns["RubroStr"].Width = 100;
            dgvGrilla.Columns["RubroStr"].DisplayIndex = 4;

            dgvGrilla.Columns["SubRubroStr"].Visible = true;
            dgvGrilla.Columns["SubRubroStr"].HeaderText = "Sub Rubro";
            dgvGrilla.Columns["SubRubroStr"].Width = 100;
            dgvGrilla.Columns["SubRubroStr"].DisplayIndex = 5;

            dgvGrilla.Columns["Stock"].Visible = true;
            dgvGrilla.Columns["Stock"].Width = 80;
            dgvGrilla.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["Stock"].DisplayIndex = 6;
        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _50002_ABM_Producto(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Producto"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion = true;
        }

        public override bool EjecutarComandoModificar()
        {
            _producto = (ProductoDto)elementoSeleccionado;

            if (_ComprobanteSalonServicio.ObtenerComprobantesSinFacturarPorProdcuto(_producto.Id) || _ComproanteDeliveryServicio.ObtenerComprobantesSinFacturarPorProdcuto(_producto.Id))
            {
                Mensaje.Mostrar("El producto seleccionado esta siendo usado en una venta,no se puede modificar ", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var formularioModificar = new _50002_ABM_Producto(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Producto"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion = true;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            _producto = (ProductoDto)elementoSeleccionado;

            if (_ComprobanteSalonServicio.ObtenerComprobantesSinFacturarPorProdcuto(_producto.Id) || _ComproanteDeliveryServicio.ObtenerComprobantesSinFacturarPorProdcuto(_producto.Id))
            {
                Mensaje.Mostrar("El producto seleccionado esta siendo usado en una venta, no se puede modificar ", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var formularioEliminar = new _50002_ABM_Producto(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Producto"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion = true;
            }
        }
    }
}
