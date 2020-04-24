using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Condicion_Iva;
using Servicio.Core.Proveedores;
using System.Windows.Forms;

namespace Presentacion.Core.Proveedores
{
    public partial class _60001_ConsultaProveedores : FormularioConsulta
    {
        private readonly IProveedorServicio _proveedorServicio;
        private readonly ICondicionIvaServicio _condicionIvaServicio;
        private CondicionIvaDto _condicionIva;
        private ProveedorDto _proveedor;

        public _60001_ConsultaProveedores() : this (new ProvedoresServicio())
        {
            Titulo = "Lista de Proveedores";
        }

        public _60001_ConsultaProveedores(IProveedorServicio proveedorServicio)

        {
            InitializeComponent();
            _proveedorServicio = proveedorServicio;
            _condicionIvaServicio = new CondicionIvaServicio();
        } 
            
        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _proveedorServicio.ObtenerPorFiltro(cadenaBuscar);

            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Direccion"].Visible = true;
            dgvGrilla.Columns["Direccion"].HeaderText = @"Dirección";
            dgvGrilla.Columns["Direccion"].DisplayIndex = 5;

            dgvGrilla.Columns["Telefono"].Visible = true;
            dgvGrilla.Columns["Telefono"].HeaderText = @"Teléfono";
            dgvGrilla.Columns["Telefono"].DisplayIndex = 4;

            dgvGrilla.Columns["Cuit"].Visible = true;
            dgvGrilla.Columns["Cuit"].HeaderText = @"Cuit";
            dgvGrilla.Columns["Cuit"].Width = 75;
            dgvGrilla.Columns["Cuit"].DisplayIndex = 2;

            dgvGrilla.Columns["RazonSocial"].Visible = true;
            dgvGrilla.Columns["RazonSocial"].HeaderText = @"Razón Social";
            dgvGrilla.Columns["RazonSocial"].DisplayIndex = 3;

            dgvGrilla.Columns["NombreFantasia"].Visible = true;
            dgvGrilla.Columns["NombreFantasia"].HeaderText = @"Nombre Fantasia";
            dgvGrilla.Columns["NombreFantasia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["NombreFantasia"].DisplayIndex = 1;

            dgvGrilla.Columns["ApyNomContacto"].Visible = true;
            dgvGrilla.Columns["ApyNomContacto"].HeaderText = @"Apellido y Nombre";
            dgvGrilla.Columns["ApyNomContacto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["ApyNomContacto"].DisplayIndex = 0;

            dgvGrilla.Columns["CondicionIvaStr"].Visible = true;
            dgvGrilla.Columns["CondicionIvaStr"].HeaderText = @"Condición Iva";
            dgvGrilla.Columns["CondicionIvaStr"].DisplayIndex = 6;

        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _60002_ABM_Provedores(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Proveedor"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
                var formularioModificar = new _60002_ABM_Provedores(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Proveedor"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion;

        }

        public override bool EjecutarComandoEliminar()
        {
            var formularioEliminar = new _60002_ABM_Provedores(Constante.TipoOperacion.Eliminar, EntidadId)
            {
                Text = "Eliminar Proveedor"
            };
            formularioEliminar.ShowDialog();
            return formularioEliminar.RealizoAlgunaOperacion;
        }
    }
}
