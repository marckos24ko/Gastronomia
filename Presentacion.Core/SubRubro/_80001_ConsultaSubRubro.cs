using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Producto;
using Servicio.Core.SubRubro;
using System.Windows.Forms;

namespace Presentacion.Core.SubRubro
{
    public partial class _80001_ConsultaSubRubro : FormularioConsulta
    {
        private readonly ISubRuroServicio _subRubroServicio;
        private readonly IProductoServicio _productoServicio;

        public _80001_ConsultaSubRubro(): this (new SubRubroServicio())
        {
            Titulo = "Lista de Subrubros";
        }

        public _80001_ConsultaSubRubro(ISubRuroServicio subRubroServicio)
        {
            InitializeComponent();

            _subRubroServicio = subRubroServicio;
            _productoServicio = new ProductoServicio();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _subRubroServicio.ObtenerPorFiltro(cadenaBuscar);
            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].Width = 50;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Codigo";

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].Width = 75;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";

            dgvGrilla.Columns["RubroStr"].Visible = true;
            dgvGrilla.Columns["RubroStr"].Width = 75;
            dgvGrilla.Columns["RubroStr"].HeaderText = @"Rubro";
        }

        public override bool EjecutarComandoNuevo()
        {
            var FormularioNuevo = new _80002_ABM_SubRubro(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Nuevo Sub-Rubro"
            };
            FormularioNuevo.ShowDialog();
            return FormularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            if (_productoServicio.ObtenerPorSubRubro(EntidadId.Value))
            {
                Mensaje.Mostrar("El Sub Rubro seleccionado esta siendo usado por uno o mas productos,no se puede modificar.", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var FormularioModificar = new _80002_ABM_SubRubro(Constante.TipoOperacion.Modificar, EntidadId.Value)
                {
                    Text = "Modificar Sub-Rubro"
                };
                FormularioModificar.ShowDialog();
                return FormularioModificar.RealizoAlgunaOperacion;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (_productoServicio.ObtenerPorSubRubro(EntidadId.Value))
            {
                Mensaje.Mostrar("El Sub Rubro seleccionado esta siendo usado por uno o mas productos,no se puede eliminar.", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioELiminar = new _80002_ABM_SubRubro(Constante.TipoOperacion.Eliminar, EntidadId.Value)
                {
                    Text = "Eliminar Sub-Rubro"
                };
                formularioELiminar.ShowDialog();
                return formularioELiminar.RealizoAlgunaOperacion;
            }
        }
    }
}
