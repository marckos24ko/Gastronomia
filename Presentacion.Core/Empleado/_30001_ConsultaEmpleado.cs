using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.ComprobanteDelivery;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.Empleado;
using Servicio.Core.Empleado.DTO;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class _30001_ConsultaEmpleado : FormularioConsulta
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IComprobanteSalon _ComprobanteSalonServicio;
        private readonly IComprobanteDeliveryServicio _ComproanteDeliveryServicio;
        private EmpleadoDto _empleado;

        public _30001_ConsultaEmpleado()
            : this(new EmpleadoServicio())
        {
            Titulo = "Lista de Empleados";
        }

        public _30001_ConsultaEmpleado(IEmpleadoServicio empleadoServicio)
        {
            InitializeComponent();

            _empleadoServicio = empleadoServicio;
            _ComprobanteSalonServicio = new ComprobanteSalon();
            _ComproanteDeliveryServicio = new ComprobanteDeliveryServicio();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {

            dgvGrilla.DataSource = _empleadoServicio.ObtenerPorFiltro(cadenaBuscar);
            FormatearGrilla(dgvGrilla);
        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            base.FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Legajo"].Visible = true;
            dgvGrilla.Columns["Legajo"].Width = 50;
            dgvGrilla.Columns["Legajo"].DisplayIndex = 0;

            dgvGrilla.Columns["ApyNom"].Visible = true;
            dgvGrilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            dgvGrilla.Columns["ApyNom"].DisplayIndex = 1;

            dgvGrilla.Columns["TipoOcupacion"].Visible = true;
            dgvGrilla.Columns["TipoOcupacion"].Width = 150;
            dgvGrilla.Columns["TipoOcupacion"].HeaderText = @"Tipo de Empleado";
            dgvGrilla.Columns["TipoOcupacion"].DisplayIndex = 2;


            dgvGrilla.Columns["Direccion"].Visible = true;
            dgvGrilla.Columns["Direccion"].Width = 100;
            dgvGrilla.Columns["Direccion"].HeaderText = @"Dirección";
            dgvGrilla.Columns["Direccion"].DisplayIndex = 3;

            dgvGrilla.Columns["Dni"].Visible = true;
            dgvGrilla.Columns["Dni"].Width = 100;
            dgvGrilla.Columns["Dni"].HeaderText = @"DNI";
            dgvGrilla.Columns["Dni"].DisplayIndex = 4;


            dgvGrilla.Columns["Celular"].Visible = true;
            dgvGrilla.Columns["Celular"].Width = 100;
            dgvGrilla.Columns["Celular"].HeaderText = @"Celular";
            dgvGrilla.Columns["Celular"].DisplayIndex = 5;

            dgvGrilla.Columns["Cuil"].Visible = true;
            dgvGrilla.Columns["Cuil"].Width = 100;
            dgvGrilla.Columns["Cuil"].HeaderText = @"Cuil";
            dgvGrilla.Columns["Cuil"].DisplayIndex = 6;

        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _30002_ABM_Empleado(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Empleado"
            };
            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            _empleado = (EmpleadoDto)elementoSeleccionado;

            if (_ComprobanteSalonServicio.ObtenerComprobantesSinFacturarPorMozo(_empleado.Id) || _ComproanteDeliveryServicio.ObtenerComprobantesSinFacturarPorDelivery(_empleado.Id))
            {
                Mensaje.Mostrar("El empleado seleccionado esta siendo usado en una venta, cierre todas las ventas abiertas para poder modificarlo.", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioModificar = new _30002_ABM_Empleado(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Empleado"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            _empleado = (EmpleadoDto)elementoSeleccionado;

            if (_ComprobanteSalonServicio.ObtenerComprobantesSinFacturarPorMozo(_empleado.Id) || _ComproanteDeliveryServicio.ObtenerComprobantesSinFacturarPorDelivery(_empleado.Id))
            {
                Mensaje.Mostrar("El empleado seleccionado esta siendo usado en una venta, cierre todas las ventas abiertas para poder eliminarlo.", Mensaje.Tipo.Error);

                return false;
            }

            else
            {

                var formularioEliminar = new _30002_ABM_Empleado(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Empleado"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion;
            }
        }
    }
}
