using System;
using System.Windows.Forms;
using Presentacion.Base.Varios;

namespace Presentacion.Base
{
    public partial class FormularioConsulta : FormularioBase
    {
        private bool puedeEjecutarComando;

        public object elementoSeleccionado;

        public bool ModoConsulta {
            set
            {
                this.btnNuevo.Visible = !value;
                this.btnEliminar.Visible = !value;
                this.btnModificar.Visible = !value;
                this.Separador.Visible = !value;
                this.btnCuentaCorriente.Visible = !value;
            }
        }

        public string Titulo {
            set { lblTitulo.Text = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormularioConsulta()
        {
            InitializeComponent();

            // Color cuando recibe o pierde el foco
            this.txtBuscar.Leave += new EventHandler(this.txt_Leave);
            this.txtBuscar.Enter += new EventHandler(this.txt_Enter);

            this.puedeEjecutarComando = false;

            this.btnNuevo.Image = Constante.ImagenControl.BotonNuevo;
            this.btnEliminar.Image = Constante.ImagenControl.BotonEliminar;
            this.btnModificar.Image = Constante.ImagenControl.BotonModificar;
            this.btnActualizar.Image = Constante.ImagenControl.BotonActualizar;
            this.btnSalir.Image = Constante.ImagenControl.BotonSalir;
            this.imgBuscar.Image = Constante.ImagenControl.Buscar;
            this.lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";

            this.btnCuentaCorriente.Image = Constante.ImagenControl.CtaCte;
            this.btnCuentaCorriente.Visible = false;
        }

        public virtual  void btnNuevo_Click(object sender, EventArgs e)
        {
            if (EjecutarComandoNuevo())
            {
                ActualizarDatos(string.Empty);
            }
        }

        public virtual void btnEliminar_Click(object sender, EventArgs e)
        {
            if (puedeEjecutarComando)
            {
                if (EjecutarComandoEliminar())
                {
                    ActualizarDatos(string.Empty);
                }
            }
            else
            {
                Mensaje.Mostrar(Constante.Mensaje.NoHayDatosCargados, Mensaje.Tipo.Informacion);
            }
        }

        public virtual void btnModificar_Click(object sender, EventArgs e)
        {
            if (puedeEjecutarComando)
            {
                if (EjecutarComandoModificar())
                {
                    ActualizarDatos(string.Empty);
                }
            }
            else
            {
                Mensaje.Mostrar(Constante.Mensaje.NoHayDatosCargados, Mensaje.Tipo.Informacion);
            }
        }
        
        public virtual void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
            txtBuscar.Clear();
        }        

        public virtual void btnImprimir_Click(object sender, EventArgs e)
        {
            if (puedeEjecutarComando)
            {
                EjecutarComandoImprimir();
            }
        }

        public virtual void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(this.txtBuscar.Text.Trim());
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            if (puedeEjecutarComando)
            {
                EjecutarComandoCuenteCorriente();
            }
            else
            {
                Mensaje.Mostrar(Constante.Mensaje.NoHayDatosCargados, Mensaje.Tipo.Informacion);
            }

        }

        public virtual void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.EntidadId = this.dgvGrilla.RowCount > 0 ? Convert.ToInt64(this.dgvGrilla["Id", e.RowIndex].Value) : (long?)null;

            elementoSeleccionado = (object)dgvGrilla.Rows[e.RowIndex].DataBoundItem;
        }

        public virtual void ActualizarDatos(string cadenaBuscar)
        {

        }

        public virtual bool EjecutarComandoNuevo()
        {
            return true;
        }

        public virtual bool EjecutarComandoModificar()
        {
            return true;
        }
        
        public virtual bool EjecutarComandoEliminar()
        {
            return true;
        }

        public virtual bool EjecutarComandoImprimir()
        {
            return true;
        }

        public virtual bool EjecutarComandoCuenteCorriente()
        {
            return true;
        }

        public virtual void dgvGrilla_DataSourceChanged(object sender, EventArgs e)
        {
            this.puedeEjecutarComando = this.dgvGrilla.RowCount > 0;
        }

        private void FormularioConsulta_Load(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

       
    }
}
