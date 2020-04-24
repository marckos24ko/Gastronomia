using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Rubro;
using Servicio.Core.SubRubro;
using Servicio.Core.SubRubro.DTO;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.SubRubro
{
    public partial class _80002_ABM_SubRubro : FormularioABM
    {
        private readonly ISubRuroServicio _subRubroServicio;

        private readonly IRubroServicio _rubroServicio;

        private long? _subRubroId;

        public _80002_ABM_SubRubro(string _tipoOperacion, long? entidadId)
            : base(_tipoOperacion, entidadId)
        {
            InitializeComponent();

            _subRubroServicio = new SubRubroServicio();

            _rubroServicio = new RubroServicio();

            Init(_tipoOperacion, entidadId);

            PoblarComboBox(cmbRubro, _rubroServicio.ObtenerTodo(), "Descripcion", "Id");


            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _subRubroId = entidadId;
                nudCodigo.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _subRubroId = null;
                nudCodigo.Value = _subRubroServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;

            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _subRubroId = entidadId;
            }

            txtDescripcion.KeyPress += Validacion.NoInyeccion;
            txtDescripcion.KeyPress += Validacion.NoNumeros;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;

            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Enter;

            nudCodigo.Enter += txt_Enter;
            nudCodigo.Leave += txt_Enter;

        }

        public override void CargarDatos(long? _entidadId)
        {
            var SubRubro = _subRubroServicio.ObtenerPorId(_entidadId.Value);

            
            nudCodigo.Value = SubRubro.Codigo;
            txtDescripcion.Text = SubRubro.Descripcion;
            cmbRubro.Text = SubRubro.RubroId.ToString();

        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            txtDescripcion.Clear();
            cmbRubro.Text = null;
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text)) return false;
            if (string.IsNullOrEmpty(nudCodigo.Value.ToString())) return false;
            if (string.IsNullOrEmpty(cmbRubro.Text)) return false;
                                  
            return true;

        }

        public override bool VerificarSiExiste()
        {
            return _subRubroServicio.VerificarSiExiste(_subRubroId, (int) nudCodigo.Value, txtDescripcion.Text);
        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                
                    _subRubroServicio.Insertar(new SubRubroDto

                    {
                        Codigo = (int)nudCodigo.Value,
                        Descripcion = txtDescripcion.Text,
                        EstaEliminado = false,
                        RubroId = Convert.ToInt64(cmbRubro.SelectedValue)


                    });

                    MessageBox.Show("Los datos se grabaron correctamente", "ATENCION", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

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
                    _subRubroServicio.Modificar(new SubRubroDto
                    {
                        Id = entidadId.Value,
                        Codigo = (int)nudCodigo.Value,
                        Descripcion = txtDescripcion.Text,
                        RubroId = Convert.ToInt64(cmbRubro.SelectedValue)
                    });

                MessageBox.Show("Los datos se grabaron correctamente", "ATENCION", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);

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
               
                _subRubroServicio.Eliminar(entidadId.Value);

                MessageBox.Show("Los datos se eliminaron correctamente", "ATENCION", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);



                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public void ActualizarCmbRubro()
        {
            cmbRubro.DataSource = _rubroServicio.ObtenerTodo();
            cmbRubro.SelectedItem = null;
            cmbRubro.Focus();
        }

      
    }
}
