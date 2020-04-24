using System;
using System.Windows.Forms;
using Presentacion.Base;
using Servicio.Core.Marca;
using Servicio.Core.Marca.DTO;
using static Presentacion.Base.Varios.Constante;
using Presentacion.Base.Varios;

namespace Presentacion.Core.Marca
{
    public partial class _40002_ABM_Marca : FormularioABM
    {
        private readonly IMarcaServicio _marcaServicio;
        private long? _marcaId;

        public _40002_ABM_Marca(string _tipoOperacion, long? _entidadID)
            : base(_tipoOperacion, _entidadID)
        {
            InitializeComponent();

            _marcaServicio = new MarcaServicio();

            Init(_tipoOperacion, entidadId);

            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _marcaId = entidadId;
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _marcaId = null;
                nudCodigo.Value = _marcaServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _marcaId = entidadId;
            }

            txtDescripcion.KeyPress += Validacion.NoInyeccion;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;

            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Leave;

        }

        public override void CargarDatos(long? _entidadId)
        {
            var marca = _marcaServicio.ObtenerPorid(entidadId.Value);
            {
               
                nudCodigo.Value = marca.Codigo;
                txtDescripcion.Text = marca.Descripcion;

            }
        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            txtDescripcion.Clear();

        }

        public override bool VerificarSiExiste()
        {
            return _marcaServicio.VerificarSiExiste(_marcaId, (int) nudCodigo.Value, txtDescripcion.Text);
        }

        public override bool VerificarDatosObligatorios()
        {

            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim())) return false;
            if (string.IsNullOrEmpty(nudCodigo.Value.ToString())) return false;

            return true;
        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                _marcaServicio.Insertar(new MarcaDto
                {

                    Codigo = (int)nudCodigo.Value,
                    Descripcion = txtDescripcion.Text,
                    EstaEliminado = false

                });

                MessageBox.Show("Los datos se grabaron Correctamente.","ATENCION",MessageBoxButtons.OK,MessageBoxIcon.Information);


                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Base.Varios.Mensaje.Tipo.Stop);

                return false;
            }

        }

        public override bool EjecutarComandoModificar()
        {
            try
            {
                    _marcaServicio.Modificar(new MarcaDto
                    {
                        id = entidadId.Value,
                        Codigo = (int)nudCodigo.Value,
                        Descripcion = txtDescripcion.Text,
                    }
                    );
                

                MessageBox.Show("Los datos se grabaron Correctamente.", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return false;
            }

        }

        public override bool EjecutarComandoEliminar()
        {
            try
            {
                
                    _marcaServicio.Eliminar(entidadId.Value);
                

                MessageBox.Show("Los datos se eliminaron Correctamente.", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return false;
            }
        }
    }
}
