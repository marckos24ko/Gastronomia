using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.ListaPprecio;
using Servicio.Core.ListaPrecioProducto;
using System.Windows.Forms;

namespace Presentacion.Core.ListaPrecio
{
    public partial class _50001_ConsultaListaPrecio : FormularioConsulta
    {
        private readonly IListaPrecioProductoServicio _listaPrecioProductoServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;

        public _50001_ConsultaListaPrecio() : this(new ListaPrecioServicio())
        {
            Titulo = "Lista de Precio";
        }


        public _50001_ConsultaListaPrecio(IListaPrecioServicio listaPrecioServicio)
        {
            InitializeComponent();
            _listaPrecioServicio = listaPrecioServicio;
            _listaPrecioProductoServicio = new ListaPrecioProductoServicio();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _listaPrecioServicio.Obtener();
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

            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["EstaEliminado"].Visible = false;
        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _50002_ABM_ListaPrecios(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Lista de Precio"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var ListaPrecio = _listaPrecioServicio.ObtenerPorId(EntidadId.Value);

            if (_listaPrecioProductoServicio.VerificarSiListaPrecioEstaUsandose(ListaPrecio.Id))
            {
                Mensaje.Mostrar("Lista de Precio en Uso, No se puede Modificar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioModificar = new _50002_ABM_ListaPrecios(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Lista de Precio"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion;

            }

        }

        public override bool EjecutarComandoEliminar()
        {
            var ListaPrecio = _listaPrecioServicio.ObtenerPorId(EntidadId.Value);

            if (_listaPrecioProductoServicio.VerificarSiListaPrecioEstaUsandose(ListaPrecio.Id))
            {
                Mensaje.Mostrar("Lista de Precio en Uso, No se puede Eliminar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioEliminar = new _50002_ABM_ListaPrecios(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Lista de Precio"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion;
            }
        }
    }
}
