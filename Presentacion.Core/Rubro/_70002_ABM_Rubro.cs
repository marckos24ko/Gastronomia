using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Rubro;
using Servicio.Core.Rubro.DTO;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Rubro
{
    public partial class _70002_ABM_Rubro : FormularioABM
    {
        private readonly IRubroServicio _rubroServicio;
        private long? _rubroId;

        public _70002_ABM_Rubro(string _tipoOperacion, long? entidadId)
            : base(_tipoOperacion, entidadId)
        {
            InitializeComponent();

            _rubroServicio = new RubroServicio();

            Init(_tipoOperacion, entidadId);

            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _rubroId = entidadId;
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _rubroId = null;
                nudCodigo.Value = _rubroServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _rubroId = entidadId;
            }

            txtDescripcion.KeyPress += Validacion.NoInyeccion;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;

            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Leave;

            nudCodigo.Enter += txt_Enter;
            nudCodigo.Leave += txt_Leave;


        }

      
        public override void CargarDatos(long? _entidadId)
        {
            var Rubro = _rubroServicio.ObtenerPorId(entidadId.Value);

            
            nudCodigo.Value = Rubro.Codigo;
            txtDescripcion.Text = Rubro.Descripcion;
        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            txtDescripcion.Clear();

        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text)) return false;

            return true;

        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                
                    _rubroServicio.Insertar(new RubroDto
                    {
                        Codigo = (int)nudCodigo.Value,
                        Descripcion = txtDescripcion.Text,
                        EstaEliminado = false,

                    });

                    MessageBox.Show("Los datos se grabaron correctamente", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public override bool EjecutarComandoModificar()
        {
            try
            {
                
                    _rubroServicio.Modificar(new RubroDto
                    {
                        Id = entidadId.Value,
                        Codigo = (int)nudCodigo.Value,
                        Descripcion = txtDescripcion.Text,

                    });

                    MessageBox.Show("Los datos se modificaron correctamente", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
               
                    _rubroServicio.Eliminar(entidadId.Value);
                
                MessageBox.Show("Los datos se eliminaron correctamente", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;

            }
        }

        public override bool VerificarSiExiste()
        {
            return _rubroServicio.VerificarSiExiste(_rubroId, (int) nudCodigo.Value, txtDescripcion.Text);
        }
    }
}
