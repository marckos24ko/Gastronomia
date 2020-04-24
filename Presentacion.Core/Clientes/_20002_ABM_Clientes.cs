using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Cliente;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.CuentaCorriente;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Clientes
{
    public partial class _20002_ABM_Clientes : FormularioABM
    {
        private readonly IClienteServicio _clienteServicio;

        private readonly ICuentaCorrienteServicio _cuentaCorrienteServicio;

        private decimal? _valorMontoMaximo;

        private ClienteDto _clienteSeleccionado;

        private long? _clienteId;

        public _20002_ABM_Clientes(string _tipoOperacion, long? _entidadId)
            : base(_tipoOperacion, _entidadId)
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();

            _cuentaCorrienteServicio = new CuentaCorrienteServicio();

            Init(_tipoOperacion, entidadId);

            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _clienteId = entidadId;
                nudCodigo.Enabled = false;

                _clienteSeleccionado = _clienteServicio.obtenerPorId(entidadId.Value);

                if(_clienteSeleccionado.TieneCuentaCorriente == true)
                {
                    chkActivarCtaCte.Text = "Habilitar la Cuenta Corriente.";
                }
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _clienteId = null;
                nudCodigo.Value = _clienteServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _clienteId = entidadId;
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                 _clienteId = entidadId;
                gbCtaCte.Enabled = false;

            }



            txtNombre.KeyPress += Validacion.NoNumeros;
            txtNombre.KeyPress += Validacion.NoSimbolos;
            txtNombre.KeyPress += Validacion.NoInyeccion;

            txtApellido.KeyPress += Validacion.NoNumeros;
            txtApellido.KeyPress += Validacion.NoSimbolos;
            txtApellido.KeyPress += Validacion.NoInyeccion;

            txtDni.KeyPress += Validacion.NoLetras;
            txtDni.KeyPress += Validacion.NoSimbolos;
            txtDni.KeyPress += Validacion.NoInyeccion;

            txtTelefono.KeyPress += Validacion.NoLetras;
            txtTelefono.KeyPress += Validacion.NoSimbolos;
            txtTelefono.KeyPress += Validacion.NoInyeccion;

            txtDireccion.KeyPress += Validacion.NoInyeccion;

            txtCelular.KeyPress += Validacion.NoLetras;
            txtCelular.KeyPress += Validacion.NoSimbolos;
            txtCelular.KeyPress += Validacion.NoInyeccion;

            txtCuit.KeyPress += Validacion.NoLetras;
            txtCuit.KeyPress += Validacion.NoInyeccion;


            txtDni.Enter += txt_Enter;
            txtDni.Leave += txt_Leave;

            txtApellido.Enter += txt_Enter;
            txtApellido.Leave += txt_Leave;

            txtNombre.Enter += txt_Enter;
            txtNombre.Leave += txt_Leave;

            txtTelefono.Enter += txt_Enter;
            txtTelefono.Leave += txt_Leave;

            txtDireccion.Enter += txt_Enter;
            txtDireccion.Leave += txt_Leave;

            txtCelular.Enter += txt_Enter;
            txtCelular.Leave += txt_Leave;

            txtCuit.Enter += txt_Enter;
            txtCuit.Leave += txt_Leave;
                        
        }

        public override void CargarDatos(long? _entidadId)
        {
            var cliente = _clienteServicio.obtenerPorId(_entidadId.Value);

            txtApellido.Text = cliente.Apellido;
            txtNombre.Text = cliente.Nombre;
            txtCelular.Text = cliente.Celular;
            txtCuit.Text = cliente.Cuil;
            txtDireccion.Text = cliente.Direccion;
            txtDni.Text = cliente.Dni;
            txtTelefono.Text = cliente.Celular;
            nudCodigo.Value = cliente.Codigo;
            chkActivarCtaCte.Checked = cliente.TieneCuentaCorriente;
            if (_clienteServicio.obtenerPorId(entidadId.Value).TieneCuentaCorriente)
            {
                lblMontoMaximo.Visible = true;
                nudMontoMaximoCtaCte.Visible = true;                
                nudMontoMaximoCtaCte.Value = (decimal)cliente.MontoMaximoCtaCte;
            }

            
        }

        public override void LimpiarDatos(object obj)
        {
            txtApellido.Clear();
            txtDireccion.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtNombre.Clear();
            txtCuit.Clear();
            nudMontoMaximoCtaCte.Value = 100m;
            chkActivarCtaCte.Checked = false;
        }

        public override void ObtenerSiguienteCodigo()
        {
            nudCodigo.Value = _clienteServicio.ObtenerSiguienteCodigo();
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text.Trim())) return false;
            if (string.IsNullOrWhiteSpace(txtCelular.Text.Trim())) return false;
            if (string.IsNullOrWhiteSpace(txtCuit.Text.Trim())) return false;
            if (string.IsNullOrWhiteSpace(txtDireccion.Text.Trim())) return false;
            if (string.IsNullOrWhiteSpace(txtDni.Text.Trim())) return false;
            if (string.IsNullOrWhiteSpace(txtNombre.Text.Trim())) return false;
            if (string.IsNullOrWhiteSpace(txtTelefono.Text.Trim())) return false;
            

            return true;
        }
 
        public override bool EjecutarComandoNuevo()
        {
            try
            { 
                    _clienteServicio.Insertar(new ClienteDto
                    {
                        Apellido = txtNombre.Text,
                        Nombre = txtApellido.Text,
                        Celular = txtCelular.Text,
                        Cuil = txtCuit.Text,
                        Direccion = txtDireccion.Text,
                        Dni = txtDni.Text,
                        Codigo = (int)nudCodigo.Value,
                        Telefono = txtTelefono.Text,
                        TieneCuentaCorriente = chkActivarCtaCte.Checked,
                        MontoMaximoCtaCte = _valorMontoMaximo,
                        DeudaTotal = 0m
                    });

                    if (chkActivarCtaCte.Checked)
                    {
                    var cliente = _clienteServicio.obtenerPorCodigo((int)nudCodigo.Value);

                        _cuentaCorrienteServicio.CrearCuentaCorriente(cliente.Id);

                    }

                    Mensaje.Mostrar("Los datos se grabaron correctamente.",Mensaje.Tipo.Informacion);


                return true;

               
            }

            catch (Exception ex)
            {

               Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }
        }

        public override bool EjecutarComandoModificar()
        {
            try
            {
                _clienteSeleccionado = _clienteServicio.obtenerPorId(entidadId.Value);

                _clienteServicio.Modificar(new ClienteDto()
                {
                    Id = entidadId.Value,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Celular = txtCelular.Text,
                    Cuil = txtCuit.Text,
                    Direccion = txtDireccion.Text,
                    Dni = txtDni.Text,
                    Telefono = txtTelefono.Text,
                    MontoMaximoCtaCte = _valorMontoMaximo
                    
                });


                if (chkActivarCtaCte.Checked)
                {
                    if (_clienteSeleccionado.TieneCuentaCorriente == false)
                    {
                        _cuentaCorrienteServicio.CrearCuentaCorriente(entidadId.Value);

                        _clienteServicio.ActivarEstadoTieneCtaCte(_clienteSeleccionado.Id);
                    }

                    else
                    {
                        var ctaCte = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_clienteSeleccionado.Id);

                        if(ctaCte.EstaHabilitada == false)
                        {
                            _cuentaCorrienteServicio.ReactivarCuentaCorriente(ctaCte.Id, entidadId.Value);

                        }
                    }
                }

                else
                {
                    if (_clienteSeleccionado.TieneCuentaCorriente == true)
                    {
                        var ctaCteId = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_clienteSeleccionado.Id).Id;

                        _cuentaCorrienteServicio.SuspenderCuentaCorriente(ctaCteId, entidadId.Value);

                    }


                }


                Mensaje.Mostrar("Los datos se grabaron correctamente.", Mensaje.Tipo.Informacion);

                return true;
            }

            catch (Exception ex)
            {
                Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);

                return false;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            try
            {
                  _clienteServicio.Eliminar(entidadId.Value);

                Mensaje.Mostrar(@"Los datos se eliminaron correctamente",Mensaje.Tipo.Informacion);


                return true;

                
            }

            catch(Exception ex)
                
            {

                MessageBox.Show(ex.Message, Mensaje.Tipo.Error);

                return false;
            }
        }

        public override bool VerificarSiExiste()
        {
            return _clienteServicio.VerificarSiExiste(_clienteId, (int) nudCodigo.Value, txtCuit.Text);
        }
       
        private void chkActivarCtaCte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivarCtaCte.Checked)
            {   
                lblMontoMaximo.Enabled = true;
                lblMontoMaximo.Visible = true;
                nudMontoMaximoCtaCte.Enabled = true;
                nudMontoMaximoCtaCte.Visible = true;
                _valorMontoMaximo = nudMontoMaximoCtaCte.Value;
            }

            else

            {
                lblMontoMaximo.Enabled = false;
                lblMontoMaximo.Visible = false;
                nudMontoMaximoCtaCte.Enabled = false;
                nudMontoMaximoCtaCte.Visible = false;
                

                if (_clienteSeleccionado != null)
                {
                    if (_clienteSeleccionado.TieneCuentaCorriente == true)
                    {
                        var ctaCteEstado = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorClienteIdSinFiltro(_clienteSeleccionado.Id).EstaHabilitada;

                        if (ctaCteEstado == true)
                        {
                            _valorMontoMaximo = 100m;
                        }

                        else
                        {
                            _valorMontoMaximo = 100m;
                        }
                    }

                    else
                    {
                      _valorMontoMaximo = null;
                    }
                }

                else
                {
                  _valorMontoMaximo = null;
                }
            }
        }

        private void nudMontoMaximoCtaCte_ValueChanged(object sender, EventArgs e)
        {
            _valorMontoMaximo = nudMontoMaximoCtaCte.Value;
        }

    }
}
