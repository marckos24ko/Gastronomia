using Presentacion.Base;
using Presentacion.Base.Varios;
using Presentacion.Core.Marca;
using Presentacion.Core.Rubro;
using Presentacion.Core.SubRubro;
using Servicio.Core.ListaPprecio;
using Servicio.Core.Marca;
using Servicio.Core.Producto;
using Servicio.Core.Rubro;
using Servicio.Core.SubRubro;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class _50002_ABM_Producto : FormularioABM
    {
        private readonly IProductoServicio _productoServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly ISubRuroServicio _subRubroServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IListaPrecioServicio _lisaListaPrecioServicio;
        private int? _productoId;

        public _50002_ABM_Producto(string _tipoOperacion, long? _entidadId)
            : base(_tipoOperacion, _entidadId)
        {
            InitializeComponent();

            _productoServicio = new ProductoServicio();
            _subRubroServicio = new SubRubroServicio();
            _marcaServicio = new MarcaServicio();
            _lisaListaPrecioServicio = new ListaPrecioServicio();
            _rubroServicio = new RubroServicio();

            Init(_tipoOperacion, _entidadId);

            PoblarComboBox(cmbSubRubro, _subRubroServicio.ObtenerPorFiltro(String.Empty), "Descripcion", "Id");

            PoblarComboBox(cmbMarca, _marcaServicio.ObtenerTodo(), "Descripcion", "Id");

            PoblarComboBox(cmbRubro, _rubroServicio.ObtenerTodo(), "Descripcion", "Id");
                             
            if (_tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _productoId = (int)entidadId;
                nudCodigo.Enabled = false;
                txtCodigoBarra.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _productoId = null;
                nudCodigo.Value = _productoServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;

                Random secuencia = new Random();

                int a = secuencia.Next(0, 1000);
                int b = secuencia.Next(1001, 2000);
                int c = secuencia.Next(2001, 3000);

                var numero = a.ToString() + b.ToString() + c.ToString();

                txtCodigoBarra.Text = numero;
                txtCodigoBarra.Enabled = false;
            }

            if (_tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _productoId = (int)entidadId;
            }



            txtDescripcion.KeyPress += Validacion.NoInyeccion;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;

            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Leave;

            nudCodigo.Enter += txt_Enter;
            nudCodigo.Leave += txt_Leave;

            txtCodigoBarra.Enter += txt_Enter;
            txtCodigoBarra.Leave += txt_Leave;

        }

        public override void CargarDatos(long? entidadId)
        {
            var Producto = _productoServicio.ObenerPorId((int)entidadId);
            {
               
                txtDescripcion.Text = Producto.Descripcion;
                nudCodigo.Value = Convert.ToDecimal(Producto.Codigo);
                txtCodigoBarra.Text = Producto.CodigoBarra;
                cmbMarca.Text = Producto.MarcaId.ToString();
                cmbSubRubro.Text = Producto.SubRubroId.ToString();
                cmbRubro.Text = Producto.RubroId.ToString();
            }
        }

        public override void LimpiarDatos(object obj)
        {

            txtDescripcion.Clear();
            cmbMarca.Text = null;
            cmbSubRubro.Text = null;
            cmbRubro.Text = null;

        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim())) return false;
            if (string.IsNullOrEmpty(cmbMarca.Text)) return false;
            if (string.IsNullOrEmpty(cmbSubRubro.Text)) return false;
            if (string.IsNullOrEmpty(cmbRubro.Text)) return false;
            if (string.IsNullOrEmpty(nudCodigo.Value.ToString())) return false;
            if (string.IsNullOrEmpty(txtCodigoBarra.Text.Trim())) return false;
          
            return true;
        }

        public override bool VerificarSiExiste()
        {
            return _productoServicio.VerificarSiExiste(_productoId, (int)nudCodigo.Value, txtCodigoBarra.Text, txtDescripcion.Text);
        }

        public void ActualizarComboMarca()
        {
            cmbMarca.DataSource = _marcaServicio.ObtenerTodo();
            cmbMarca.SelectedItem = null;
            cmbMarca.Focus();
        }

        public void ActualizarComboRubro()
        {
            cmbRubro.DataSource = _rubroServicio.ObtenerTodo();
            cmbRubro.SelectedItem = null;
            cmbRubro.Focus();

        }

        public void ActualizarComboSubRubro()
        {
            PoblarComboBox(cmbSubRubro, _subRubroServicio.ObtenerPorRubro((long)cmbRubro.SelectedValue), "Descripcion", "Id");
            cmbSubRubro.SelectedItem = null;
            cmbSubRubro.Focus();
        }

        public override bool EjecutarComandoNuevo()
        {
            _productoId = null;

            try
            {
                _productoServicio.Insertar(new ProductoDto
                {
                    Descripcion = txtDescripcion.Text,
                    Codigo = (int)nudCodigo.Value,
                    CodigoBarra = txtCodigoBarra.Text,
                    MarcaId = Convert.ToInt64(cmbMarca.SelectedValue),
                    SubRubroId = Convert.ToInt64(cmbSubRubro.SelectedValue),
                    RubroId = Convert.ToInt64(cmbRubro.SelectedValue)

                }

                );

                Mensaje.Mostrar("Los datos se grabaron Correctamente.", Mensaje.Tipo.Informacion);

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
                    _productoServicio.Modificar(new ProductoDto()
                    {
                        Id = Convert.ToInt32(entidadId.Value),
                        CodigoBarra = txtCodigoBarra.Text,
                        Descripcion = txtDescripcion.Text,
                        MarcaId = Convert.ToInt64(cmbMarca.SelectedValue),
                        SubRubroId = Convert.ToInt64(cmbSubRubro.SelectedValue),
                        RubroId = Convert.ToInt64(cmbRubro.SelectedValue)
                    });

                Mensaje.Mostrar("Los datos se grabaron Correctamente.", Mensaje.Tipo.Informacion);


                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Mensaje.Tipo.Error);

                return false;
            }

        }

        public override bool EjecutarComandoEliminar()
        {
            try
            {

                
                    _productoServicio.Eliminar((int)entidadId);


                Mensaje.Mostrar("Los datos se eliminaron Correctamente.", Mensaje.Tipo.Informacion);

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
                   
        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            var formulario = new _40002_ABM_Marca(Constante.TipoOperacion.Nuevo, null);
            formulario.Text = "Nueva Marca";
            formulario.ShowDialog();

            if (formulario.RealizoAlgunaOperacion)
            {
                ActualizarComboMarca();
            }
        }

        private void btnAgregarRubro_Click(object sender, EventArgs e)
        {
            var formulario = new _70002_ABM_Rubro(Constante.TipoOperacion.Nuevo, null);
            formulario.Text = "Nuevo Rubro";
            formulario.ShowDialog();

            if (formulario.RealizoAlgunaOperacion)
            {
                ActualizarComboRubro();
            }
        }

        private void btnAgregarSubRubro_Click(object sender, EventArgs e)
        {
            var formulario = new _80002_ABM_SubRubro(Constante.TipoOperacion.Nuevo, null);
            formulario.Text = "Nuevo Sub-Rubro";
            formulario.ShowDialog();

            if ((formulario.RealizoAlgunaOperacion) && (!string.IsNullOrEmpty(cmbRubro.Text)))
            {
                ActualizarComboSubRubro();
            }
        }

        private void cmbRubro_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            PoblarComboBox(cmbSubRubro, _subRubroServicio.ObtenerPorRubro((long)cmbRubro.SelectedValue), "Descripcion", "Id");
        }
    }
}
