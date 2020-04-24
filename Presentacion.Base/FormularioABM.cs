using Presentacion.Base.Varios;
using System;
using System.Windows.Forms;

namespace Presentacion.Base
{
    public partial class FormularioABM : FormularioBase
    {
        private string tipoOperacion;
        protected long? entidadId;
        protected long? entidadId2;
        
        /// Propiedad para saber si se realizo alguna operacion (Insertar, Modificar o Eliminar)
        public bool RealizoAlgunaOperacion { get; set; }
        public bool LanzadoPorCierre { get; set; }
        public bool LanzadoPorLimpiar { get; set; }

        public FormularioABM()
        {
            InitializeComponent();

            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;
            lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";

        }
        
        public FormularioABM(string _tipoOperacion, long? _entidadId, long ?_entidadId2)
            : this()
        {
            this.tipoOperacion = _tipoOperacion;
            this.entidadId = _entidadId;
            this.entidadId2 = _entidadId2;

            this.RealizoAlgunaOperacion = false;
            this.LanzadoPorCierre = false;
            this.LanzadoPorLimpiar = false;

            this.btnLimpiar.Image = Constante.ImagenControl.BotonLimpiar;

            switch (_tipoOperacion)
            {
                case Constante.TipoOperacion.Nuevo:
                    this.btnEjecutar.Image = Constante.ImagenControl.BotonGuardar;
                    this.btnEjecutar.Text = "Guardar";
                    this.Text = "Nuevo";
                    break;
                case Constante.TipoOperacion.Eliminar:
                    this.btnEjecutar.Image = Constante.ImagenControl.BotonEliminar;
                    this.btnEjecutar.Text = "Eliminar";
                    this.Text = "Eliminar";
                    break;
                case Constante.TipoOperacion.Modificar:
                    this.btnEjecutar.Image = Constante.ImagenControl.BotonModificar;
                    this.btnEjecutar.Text = "Guardar";
                    this.Text = "Modificación";
                    break;
            }
        }

        public FormularioABM(string _tipoOperacion, long? _entidadId = null)
            :this()
        {
            this.tipoOperacion = _tipoOperacion;
            this.entidadId = _entidadId;

            this.RealizoAlgunaOperacion = false;
            this.LanzadoPorCierre = false;
            this.LanzadoPorLimpiar = false;


            this.btnLimpiar.Image = Constante.ImagenControl.BotonLimpiar;

            switch (_tipoOperacion)
            {
                case Constante.TipoOperacion.Nuevo:
                    this.btnEjecutar.Image = Constante.ImagenControl.BotonGuardar;
                    this.btnEjecutar.Text = "Guardar";
                    this.Text = "Nuevo";
                    break;
                case Constante.TipoOperacion.Eliminar:
                    this.btnEjecutar.Image = Constante.ImagenControl.BotonEliminar;
                    this.btnEjecutar.Text = "Eliminar";
                    this.Text = "Eliminar";
                    break;
                case Constante.TipoOperacion.Modificar:
                    this.btnEjecutar.Image = Constante.ImagenControl.BotonModificar;
                    this.btnEjecutar.Text = "Guardar";
                    this.Text = "Modificación";
                    break;
            }
        }

        public virtual void Init(string _tipoOperacion, long? _entidadId)
        {
            switch (_tipoOperacion)
            {
                case Constante.TipoOperacion.Nuevo:
                    LimpiarDatos(this);
                    break;
                case Constante.TipoOperacion.Eliminar:
                    this.btnLimpiar.Visible = false;
                    ActivarControles(this, false);
                    CargarDatos(_entidadId);
                    break;
                case Constante.TipoOperacion.Modificar:
                    CargarDatos(_entidadId);
                    break;
            }
        }

        public override void LimpiarDatos(object obj)
        {

        }

        public virtual void CargarDatos(long? _entidadId)
        {

        }

        public virtual void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (Constante.TipoOperacion.Eliminar != this.tipoOperacion)
            {
                LimpiarDatos(this);
            }
            LanzadoPorLimpiar = true;
        }

        public virtual void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (!VerificarDatosObligatorios()) return;

            switch (this.tipoOperacion)
            {
                case Constante.TipoOperacion.Nuevo:
                    if (!VerificarSiExiste())
                    {
                        if (EjecutarComandoNuevo())
                        {
                            RealizoAlgunaOperacion = true;
                            LimpiarDatos(this);
                            this.Close();
                        }
                        else
                        {
                            RealizoAlgunaOperacion = false;
                        }
                    }
                    else
                    {
                        Mensaje.Mostrar("Algunos de los datos cargados ya existen.", Mensaje.Tipo.Stop);
                    }
                    break;
                case Constante.TipoOperacion.Modificar:
                    if (!VerificarSiExiste())
                    {
                        EjecutarComandoModificar();
                        RealizoAlgunaOperacion = true;
                        this.Close();
                    }
                    else
                    {
                        Mensaje.Mostrar("Algunos de los datos cargados ya existen.", Mensaje.Tipo.Stop);
                    }
                    break;
                case Constante.TipoOperacion.Eliminar:
                    EjecutarComandoEliminar();
                    RealizoAlgunaOperacion = true;
                    this.Close();
                    break;
            }
        }

        public virtual void ObtenerSiguienteCodigo()
        {

        }

        public virtual bool EjecutarComandoEliminar()
        {
            return false;
        }

        public virtual bool EjecutarComandoModificar()
        {
            return false;
        }

        public virtual bool EjecutarComandoNuevo()
        {
            return false;
        }

        public virtual bool VerificarSiExiste()
        {
            return false;
        }

        public virtual bool VerificarDatosObligatorios()
        {
            return true;
        }

        private void FormularioABM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RealizoAlgunaOperacion) return;

            var mensaje = string.Empty;

            switch (this.tipoOperacion)
            {
                case Constante.TipoOperacion.Nuevo:
                    mensaje = "Desea guardar los datos";
                    break;
                case Constante.TipoOperacion.Eliminar:
                    mensaje = "Desea Eliminar los datos";
                    break;
                case Constante.TipoOperacion.Modificar:
                    mensaje = "Desea Modificar los datos";
                    break;
            }

            if (Mensaje.Mostrar(mensaje, Mensaje.Tipo.Pregunta) != System.Windows.Forms.DialogResult.Yes) return;

            if (Constante.TipoOperacion.Nuevo == this.tipoOperacion
                || LanzadoPorLimpiar)
            {
                e.Cancel = true;
            }

            this.LanzadoPorCierre = true;
            this.btnEjecutar.PerformClick();
        }
    
    }
}
