using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Condicion_Iva;
using Servicio.Core.Proveedores;
using System.Windows.Forms;

namespace Presentacion.Core.Condicion_Iva
{
    public partial class _9001_ConsultaCondicionIva : FormularioConsulta
    {
        private readonly ICondicionIvaServicio _condicionIvaServicio;
        private readonly IProveedorServicio _proveedoresServicio;

        public _9001_ConsultaCondicionIva(ICondicionIvaServicio condicionIvaServicio)
        {
            InitializeComponent();
            _condicionIvaServicio = condicionIvaServicio;
            _proveedoresServicio = new ProvedoresServicio();
        }

        public _9001_ConsultaCondicionIva() : this(new CondicionIvaServicio())
        {
            Titulo = "Lista Condicion Iva";
        }
               
        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _condicionIvaServicio.ObtenerPorFiltro(cadenaBuscar);
            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Descripcion"].Visible = true;
        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _9002_ABM_CondicionIva(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Condicion Iva"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            if (_proveedoresServicio.obtenerPorCondicionIva(EntidadId.Value))
            {
                MessageBox.Show("La Condicion Iva seleccionada esta siendo usada por uno o mas proveedores, no se puede modificar ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else
            {
                var formularioModificar = new _9002_ABM_CondicionIva(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Condicion Iva"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion;

            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (_proveedoresServicio.obtenerPorCondicionIva(EntidadId.Value))
            {
                MessageBox.Show("La Condicion Iva seleccionada esta siendo usada por uno o mas proveedores, no se puede eliminar ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else
            {
                var formularioEliminar = new _9002_ABM_CondicionIva(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Condicion Iva"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion;
            }

        }

        private void _9001_ConsultaCondicionIva_Load(object sender, System.EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }
    }
}
