using DAL;
using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Empleado;
using Servicio.Core.Empleado.DTO;
using Servicio.Core.Usuario;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class _30002_ABM_Empleado : FormularioABM
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        private readonly IUsuarioServicio _usuarioServicio;

        private long? _empleadoId;

        public _30002_ABM_Empleado(string _tipoOperacion, long? _entidadId)
            : base(_tipoOperacion, _entidadId)
        {
            InitializeComponent();

            _empleadoServicio = new EmpleadoServicio();

            _usuarioServicio = new UsuarioServicio();

            cmbTipoOcupacion.DataSource = Enum.GetValues(typeof(TipoEmpleado));

            Init(_tipoOperacion, _entidadId);

            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _empleadoId = entidadId;
                nudLegajo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _empleadoId = null;
                nudLegajo.Value = _empleadoServicio.ObtenerSiguienteLegajo();
                nudLegajo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _empleadoId = entidadId;
            }

            txtApellido.KeyPress += Validacion.NoInyeccion;
            txtApellido.KeyPress += Validacion.NoNumeros;
            txtApellido.KeyPress += Validacion.NoSimbolos;

            txtNombre.KeyPress += Validacion.NoInyeccion;
            txtNombre.KeyPress += Validacion.NoNumeros;
            txtNombre.KeyPress += Validacion.NoSimbolos;

            txtDni.KeyPress += Validacion.NoInyeccion;
            txtDni.KeyPress += Validacion.NoLetras;
            txtDni.KeyPress += Validacion.NoSimbolos;

            txtTelefono.KeyPress += Validacion.NoInyeccion;
            txtTelefono.KeyPress += Validacion.NoLetras;
            txtTelefono.KeyPress += Validacion.NoSimbolos;

            txtDireccion.KeyPress += Validacion.NoInyeccion;

            txtCelular.KeyPress += Validacion.NoInyeccion;
            txtCelular.KeyPress += Validacion.NoLetras;
            txtCelular.KeyPress += Validacion.NoSimbolos;

            txtCuil.KeyPress += Validacion.NoInyeccion;
            txtCuil.KeyPress += Validacion.NoLetras;


            txtApellido.Enter += txt_Enter;
            txtApellido.Leave += txt_Leave;

            txtNombre.Enter += txt_Enter;
            txtNombre.Leave += txt_Leave;

            txtDni.Enter += txt_Enter;
            txtDni.Leave += txt_Leave;

            txtCelular.Enter += txt_Enter;
            txtCelular.Leave += txt_Leave;

            txtTelefono.Enter += txt_Enter;
            txtTelefono.Leave += txt_Leave;

            txtDireccion.Enter += txt_Enter;
            txtDireccion.Leave += txt_Leave;

            txtCuil.Enter += txt_Enter;
            txtCuil.Leave += txt_Leave;

        }

        public override void CargarDatos(long? _entidadId)
        {
            var empleado = _empleadoServicio.obtenerPorId(_entidadId.Value);

            txtApellido.Text = empleado.Apellido;
            txtNombre.Text = empleado.Nombre;
            txtCelular.Text = empleado.Celular;
            txtCuil.Text = empleado.Cuil;
            txtDireccion.Text = empleado.Direccion;
            txtDni.Text = empleado.Dni;
            txtTelefono.Text = empleado.Celular;
            cmbTipoOcupacion.SelectedItem = empleado.TipoOcupacion;
            nudLegajo.Value = empleado.Legajo;
            

        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            txtApellido.Clear();
            txtDireccion.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtNombre.Clear();
            txtCuil.Clear();
        }

        public override void ObtenerSiguienteCodigo()
        {
            nudLegajo.Value = _empleadoServicio.ObtenerSiguienteLegajo();
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtApellido.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtCelular.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtCuil.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtDireccion.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtDni.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtNombre.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtTelefono.Text.Trim())) return false;

            return true;
        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                _empleadoServicio.Insertar(new EmpleadoDto
                {
                    Apellido = txtApellido.Text,
                    Nombre = txtNombre.Text,
                    Celular = txtCelular.Text,
                    Cuil = txtCuil.Text,
                    Direccion = txtDireccion.Text,
                    Dni = txtDni.Text,
                    Legajo = (int)nudLegajo.Value,
                    Telefono = txtTelefono.Text,
                    TipoOcupacion = (TipoEmpleado)cmbTipoOcupacion.SelectedItem,
                });

                if ((TipoEmpleado)cmbTipoOcupacion.SelectedItem == TipoEmpleado.Administrativo || (TipoEmpleado)cmbTipoOcupacion.SelectedItem == TipoEmpleado.Cajero || (TipoEmpleado)cmbTipoOcupacion.SelectedItem == TipoEmpleado.Mozo)
                {
           
                    Mensaje.Mostrar(string.Format ("Los datos se grabaron correctamente. \n" +
                        "Se creo con exito la cuenta: \n" +
                        "\n" +
                        "Usuario: {0} \n" +
                        "Password: {1} \n", txtNombre.Text + txtCuil.Text, txtDni.Text
                        ), Mensaje.Tipo.Informacion);

                }

                else
                {
                    Mensaje.Mostrar("Los datos se grabaron correctamente.", Mensaje.Tipo.Informacion);
                }

                return true;
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }
        }

        public override bool EjecutarComandoModificar()
        {
            try
            {
                var mensaje = string.Empty;

                var empleado = _empleadoServicio.obtenerPorId(_empleadoId);

                var cuenta = _usuarioServicio.ObtenerPorId(_empleadoServicio.obtenerPorId(_empleadoId).UsuarioId);

                if (cuenta.Usuario.Contains("CadeteBloqueado"))
                {
                    mensaje = string.Format("Los datos se grabaron correctamente. \n" +
                    "\n" +
                    "*EL EMPLEADO QUE MODIFICO NO TENIA UNA CUENTA ASIGNADA. \n" +
                    "\n" +
                    "Se creo con exito la cuenta: \n" +
                    "\n" +
                    "Usuario: {0} \n" +
                    "Password: {1} \n", txtNombre.Text + txtCuil.Text, txtDni.Text
                    );

                }
                else
                {
                    mensaje = string.Format("Los datos se grabaron correctamente. \n" +
                    "\n" +
                    "*SE DETECTO LA CUENTA: {0} Y FUE ACTIVADA."
                     , cuenta.Usuario
                    );
                }


                _empleadoServicio.Modificar(new EmpleadoDto()
                {
                    Id = entidadId.Value,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Celular = txtCelular.Text,
                    Cuil = txtCuil.Text,
                    Direccion = txtDireccion.Text,
                    Dni = txtDni.Text,
                    Telefono = txtTelefono.Text,
                    Legajo = (int)nudLegajo.Value,
                    TipoOcupacion = (TipoEmpleado)cmbTipoOcupacion.SelectedItem
                });

                if (empleado.TipoOcupacion != TipoEmpleado.Cadete && (TipoEmpleado)cmbTipoOcupacion.SelectedItem == TipoEmpleado.Cadete)
                {
                    Mensaje.Mostrar(string.Format("Los datos se grabaron correctamente. \n" +
                        "\n" +
                        "La cuenta: {0}. permanecera inactiva mientras el empleado sea un cadete.", cuenta.Usuario), Mensaje.Tipo.Informacion);
                }

                else
                {
                    if (empleado.TipoOcupacion == TipoEmpleado.Cadete && (TipoEmpleado)cmbTipoOcupacion.SelectedItem != TipoEmpleado.Cadete)
                    {

                        Mensaje.Mostrar(mensaje, Mensaje.Tipo.Informacion); 
                    }

                    else
                    {
                        Mensaje.Mostrar("Los datos se grabaron correctamente.", Mensaje.Tipo.Informacion);
                    }
                }
               
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }

        }

        public override bool EjecutarComandoEliminar()
        {
            var empleado = _empleadoServicio.obtenerPorId(_empleadoId);
            var cuenta = empleado.Nombre + empleado.Cuil;

            try
            {
                if (empleado.TipoOcupacion == TipoEmpleado.Cadete)
                {
                    if (Mensaje.Mostrar(string.Format("Seguro desea eliminar este empleado?"), Mensaje.Tipo.Pregunta) == System.Windows.Forms.DialogResult.Yes)
                    {
                        _empleadoServicio.Eliminar(entidadId.Value);

                        return true;
                    }

                    return false;
                }
                else
                {
                    if (Mensaje.Mostrar(string.Format("Se eliminara el empleado y la cuenta: {0}. \n" + "\n" + "Seguro desea eliminar este empleado?", cuenta), Mensaje.Tipo.Pregunta) == System.Windows.Forms.DialogResult.Yes)
                    {
                        _empleadoServicio.Eliminar(entidadId.Value);

                        return true;
                    }

                    return false;
                }
               
            }
            catch
                (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public override bool VerificarSiExiste()
        {
            return _empleadoServicio.VerificarSiExiste(_empleadoId, (int)nudLegajo.Value, txtCuil.Text);
        }

    }
}
