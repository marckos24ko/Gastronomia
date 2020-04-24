using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Marca;
using Servicio.Core.Producto;
using System.Windows.Forms;

namespace Presentacion.Core.Marca
{
    public partial class _40001_ConsultaMarca : FormularioConsulta
    {
        private readonly IMarcaServicio _marcaServicio;
        private readonly IProductoServicio _productoServicio;

        public _40001_ConsultaMarca() : this(new MarcaServicio())
        {
            Titulo = "Lista de Marcas";


        }

        public _40001_ConsultaMarca(IMarcaServicio marcaServicio)
        {
            InitializeComponent();

            _marcaServicio = new MarcaServicio();
            _productoServicio = new ProductoServicio();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _marcaServicio.ObtenerPorFiltro(cadenaBuscar);

            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Codigo";

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";
        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _40002_ABM_Marca(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Marca"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion = true;
        }

        public override bool EjecutarComandoModificar()
        {
            if (_productoServicio.ObtenerPorMarca(EntidadId.Value))
            {
                Mensaje.Mostrar("La marca seleccionada esta siendo usada por uno o mas productos, no se puede modificar ", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var formularioModificar = new _40002_ABM_Marca(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Marca"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion = true;
            }
        }

        public override bool EjecutarComandoEliminar()
        {

            if (_productoServicio.ObtenerPorMarca(EntidadId.Value))
            {
                Mensaje.Mostrar("La marca seleccionada esta siendo usada por uno o mas productos, no se puede eliminar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var formularioEliminar = new _40002_ABM_Marca(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Marca"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion = true;
            }
        }
    }
}
