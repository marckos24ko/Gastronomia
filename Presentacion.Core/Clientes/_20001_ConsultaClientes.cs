using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Clientes
{
    public partial class _20001_ConsultaClientes : FormularioConsulta
    {
        private readonly IClienteServicio _clienteServicio;

        public _20001_ConsultaClientes()
            : this(new ClienteServicio())
        {
            Titulo = @"Lista de CLientes"; //Titulo

        }

        public _20001_ConsultaClientes(IClienteServicio clienteservicio)
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();
            btnCuentaCorriente.Visible = true;
            
            
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _clienteServicio.ObtenerPorFiltro(cadenaBuscar);
            FormatearGrilla(dgvGrilla);

        }

        public override void FormatearGrilla(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["Nombre"].Visible = false;
            dgvGrilla.Columns["Apellido"].Visible = false;
            dgvGrilla.Columns["EstaEliminado"].Visible = false;

            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].Width = 50;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Código";
            dgvGrilla.Columns["Codigo"].DisplayIndex = 0;

            dgvGrilla.Columns["ApyNom"].Visible = true;
            dgvGrilla.Columns["ApyNom"].Width = 100;
            dgvGrilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            dgvGrilla.Columns["ApyNom"].DisplayIndex = 1;

            dgvGrilla.Columns["Direccion"].Visible = true;
            dgvGrilla.Columns["Direccion"].Width = 100;
            dgvGrilla.Columns["Direccion"].HeaderText = @"Dirección";
            dgvGrilla.Columns["Direccion"].DisplayIndex = 2;

            dgvGrilla.Columns["Dni"].Visible = true;
            dgvGrilla.Columns["Dni"].Width = 100;
            dgvGrilla.Columns["Dni"].HeaderText = @"DNI";
            dgvGrilla.Columns["Dni"].DisplayIndex = 3;

            dgvGrilla.Columns["Celular"].Visible = true;
            dgvGrilla.Columns["Celular"].Width = 100;
            dgvGrilla.Columns["Celular"].HeaderText = @"Celular";
            dgvGrilla.Columns["Celular"].DisplayIndex = 4;

            dgvGrilla.Columns["Telefono"].Visible = true;
            dgvGrilla.Columns["Telefono"].Width = 100;
            dgvGrilla.Columns["Telefono"].HeaderText = @"Telefono";
            dgvGrilla.Columns["Telefono"].DisplayIndex = 5;

            dgvGrilla.Columns["Cuil"].Visible = true;
            dgvGrilla.Columns["Cuil"].Width = 100;
            dgvGrilla.Columns["Cuil"].HeaderText = @"Cuil";
            dgvGrilla.Columns["Cuil"].DisplayIndex = 6;

            dgvGrilla.Columns["TieneCuentaCorriente"].Visible = true;
            dgvGrilla.Columns["TieneCuentaCorriente"].Width = 50;
            dgvGrilla.Columns["TieneCuentaCorriente"].HeaderText = @"Cuenta Corriente";
            dgvGrilla.Columns["TieneCuentaCorriente"].DisplayIndex = 8;

            dgvGrilla.Columns["MontoMaximoCtaCte"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["MontoMaximoCtaCte"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["MontoMaximoCtaCte"].DisplayIndex = 9;

            dgvGrilla.Columns["ActivoParaCompras"].Visible = true;
            dgvGrilla.Columns["ActivoParaCompras"].Width = 50;
            dgvGrilla.Columns["ActivoParaCompras"].HeaderText = @"Activo Para Compras";
            dgvGrilla.Columns["ActivoParaCompras"].DisplayIndex = 10;

            dgvGrilla.Columns["DeudaTotal"].Visible = true;
            dgvGrilla.Columns["DeudaTotal"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["DeudaTotal"].HeaderText = @"Deuda Total";
            dgvGrilla.Columns["DeudaTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["DeudaTotal"].DisplayIndex = 7;

        }

        public override bool EjecutarComandoNuevo()
        {
            var formularioNuevo = new _20002_ABM_Clientes(Constante.TipoOperacion.Nuevo, null)
            {
                Text = "Crear Cliente"
            };

            formularioNuevo.ShowDialog();
            return formularioNuevo.RealizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {

            if (_clienteServicio.VerificarSiTieneDeuda(EntidadId.Value) || _clienteServicio.VerificarSiEstaOcupado(EntidadId.Value))
            {
                Mensaje.Mostrar("El cliente seleccionado tiene una deuda pendiente o esta realizando una compra, No se puede Modificar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioModificar = new _20002_ABM_Clientes(Constante.TipoOperacion.Modificar, EntidadId)
                {
                    Text = "Modificar Cliente"
                };
                formularioModificar.ShowDialog();
                return formularioModificar.RealizoAlgunaOperacion;
            }
        }

        public override bool EjecutarComandoEliminar()
        {


            if (_clienteServicio.VerificarSiTieneDeuda(EntidadId.Value) || _clienteServicio.VerificarSiEstaOcupado(EntidadId.Value))
            {
                Mensaje.Mostrar("El cliente seleccionado tiene una deuda pendiente o esta realizando una compra, No se puede Eliminar", Mensaje.Tipo.Error);

                return false;
            }

            else
            {
                var formularioEliminar = new _20002_ABM_Clientes(Constante.TipoOperacion.Eliminar, EntidadId)
                {
                    Text = "Eliminar Cliente"
                };
                formularioEliminar.ShowDialog();
                return formularioEliminar.RealizoAlgunaOperacion;
            }


        }

        public override bool EjecutarComandoCuenteCorriente()
        {
            var cliente = (ClienteDto)elementoSeleccionado;

            if (cliente != null)
            {

                if (cliente.TieneCuentaCorriente)
                {
                   var form = new Cliente_Cta_Cte((ClienteDto)elementoSeleccionado);
                    form.ShowDialog();

                    if (form.RealizoAlgunaOperacion)
                    {
                        ActualizarDatos(string.Empty);
                    }
                }

                else
                {
                    Mensaje.Mostrar("El cliente seleccionado no tiene cuenta corriente.", Mensaje.Tipo.Informacion);
                }
            }

            else
            {
                Mensaje.Mostrar("El cliente seleccionado no tiene cuenta corriente.", Mensaje.Tipo.Informacion);
            }
            

                return base.EjecutarComandoCuenteCorriente();
            

        }
    }
}
