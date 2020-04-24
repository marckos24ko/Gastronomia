using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Proveedores;
using Servicio.Core.Rubro;
using Servicio.Core.SubRubro;
using System.Windows.Forms;

namespace Presentacion.Core.Rubro
{
    public partial class _70001_ConsultaRubro : FormularioConsulta
    {
        private readonly IRubroServicio _rubroServicio;
        private readonly ISubRuroServicio _subRubroServicio;
        private readonly IProveedorServicio _proveedorServicios;

        public _70001_ConsultaRubro(): this(new RubroServicio())
        {
            Titulo = "Lista De Rubros";
        }

        public _70001_ConsultaRubro(IRubroServicio rubroServicio)
        {
            InitializeComponent();

            _rubroServicio = rubroServicio;
            _subRubroServicio = new SubRubroServicio();
            _proveedorServicios = new ProvedoresServicio();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _rubroServicio.ObtenerPorFiltro(cadenaBuscar);
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
        }

        public override bool EjecutarComandoNuevo()
        {
            var FormularioNuevo = new _70002_ABM_Rubro(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Nuevo Rubro"
            };
            FormularioNuevo.ShowDialog();
            return FormularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            if (_subRubroServicio.ObtenerPorRubroId(EntidadId.Value) || _proveedorServicios.obtenerPorRubro(EntidadId.Value))
            {
                Mensaje.Mostrar("El rubro seleccionado esta siendo usado por uno o mas subrubros o por uno o mas proveedores,no se puede modificar.", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var FormularioModificar = new _70002_ABM_Rubro(Constante.TipoOperacion.Modificar, EntidadId.Value)
                {
                    Text = "Modificar Rubro"
                };
                FormularioModificar.ShowDialog();
                return FormularioModificar.RealizoAlgunaOperacion;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (_subRubroServicio.ObtenerPorRubroId(EntidadId.Value) || _proveedorServicios.obtenerPorRubro(EntidadId.Value))
            {
                Mensaje.Mostrar("El rubro seleccionado esta siendo usado por uno o mas subrubros o por uno o mas proveedores,no se puede eliminar.", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var FormularioEliminar = new _70002_ABM_Rubro(Constante.TipoOperacion.Eliminar, EntidadId.Value)
                {
                    Text = "Eliminar Rubro"
                };
                FormularioEliminar.ShowDialog();
                return FormularioEliminar.RealizoAlgunaOperacion;
            }
        }
    }
}
