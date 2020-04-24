using System.Windows.Forms;
using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Mesa;

namespace Presentacion.Core.Mesa
{
    public partial class _00001_ConsultaMesas : FormularioConsulta
    {
        private readonly IMesaServicio _mesaServicio;

        public _00001_ConsultaMesas()
            : this(new MesaServicio())
        {
            Titulo = "Lista de Mesas";
        }

        public _00001_ConsultaMesas(IMesaServicio mesaServicio)
        {
            InitializeComponent();
            _mesaServicio = mesaServicio;
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _mesaServicio.ObtenerPorFiltro(cadenaBuscar);

           FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Numero"].Visible = true;
            dgvGrilla.Columns["Numero"].HeaderText = @"Número";

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";

            dgvGrilla.Columns["EstadoMesa"].Visible = true;
            dgvGrilla.Columns["EstadoMesa"].HeaderText = @"Estado";
        }

        public bool VerificarSiEstaUsandose(long MesaId)
        {
            return _mesaServicio.VerificarSiEstaUsandose(MesaId, DAL.EstadoMesa.Ocupada);
        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _00002_ABM_Mesa(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Mesa"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var Mesa = _mesaServicio.ObtenerPorId(EntidadId.Value);

            if (VerificarSiEstaUsandose(Mesa.Id))
            {
                Mensaje.Mostrar("Mesa en Uso, No se puede Modificar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioModificar = new _00002_ABM_Mesa(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Mesa"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            var Mesa = _mesaServicio.ObtenerPorId(EntidadId.Value);

            if (VerificarSiEstaUsandose(Mesa.Id))
            {
                Mensaje.Mostrar("Mesa en Uso, No se puede Eliminar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var formularioEliminar = new _00002_ABM_Mesa(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Mesa"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion;
            }
        }
    }
}
