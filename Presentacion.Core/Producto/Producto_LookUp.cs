using System.Windows.Forms;
using Presentacion.Base;
using Servicio.Core.Producto;

namespace Presentacion.Core.Producto
{
    public partial class Producto_LookUp : FormularioLookUp
    {
        private long _listaPrecioId;
        private readonly IProductoServicio _productoServicio;


        public Producto_LookUp()
        {
            InitializeComponent();
            _productoServicio = new ProductoServicio();

        }

        public Producto_LookUp(long listaPrecioId)
            :this()
        {
            _listaPrecioId = listaPrecioId;
            lblTitulo.Text = @"Búsqueda de Items";
            
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _productoServicio.ObtenerPorListaDePrecioParaLookUp((int)_listaPrecioId, cadenaBuscar);
            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["CodigoBarra"].Visible = true;
            dgvGrilla.Columns["CodigoBarra"].HeaderText = @"Código de barra";
            dgvGrilla.Columns["CodigoBarra"].Width = 150;
            dgvGrilla.Columns["CodigoBarra"].DisplayIndex = 1;

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";
            dgvGrilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["Descripcion"].DisplayIndex = 2;

            dgvGrilla.Columns["Stock"].Visible = true;
            dgvGrilla.Columns["Stock"].HeaderText = "Stock";
            dgvGrilla.Columns["Stock"].Width = 50;
            dgvGrilla.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["Stock"].DisplayIndex = 3;

            dgvGrilla.Columns["FechaActualizacion"].Visible = true;
            dgvGrilla.Columns["FechaActualizacion"].HeaderText = @"Fecha de Actualización";
            dgvGrilla.Columns["FechaActualizacion"].Width = 150;
            dgvGrilla.Columns["FechaActualizacion"].DisplayIndex = 4;

            dgvGrilla.Columns["PrecioPublico"].Visible = true;
            dgvGrilla.Columns["PrecioPublico"].HeaderText = @"Precio Público";
            dgvGrilla.Columns["PrecioPublico"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["PrecioPublico"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["PrecioPublico"].Width = 100;
            dgvGrilla.Columns["PrecioPublico"].DisplayIndex = 5;
        }
    }
}
