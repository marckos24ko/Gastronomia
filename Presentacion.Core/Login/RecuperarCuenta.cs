using Presentacion.Base.Varios;
using Servicio.Core.Usuario;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Login
{
    public partial class RecuperarCuenta : Form

    {
        private readonly IUsuarioServicio _usuarioServicio;

        public RecuperarCuenta()
        {
            InitializeComponent();

            _usuarioServicio = new UsuarioServicio();

            txtUsuario.KeyPress += Validacion.NoInyeccion;
            txtUsuario.KeyPress += Validacion.NoSimbolos;

            txtPassword.KeyPress += Validacion.NoInyeccion;
            txtPassword.KeyPress += Validacion.NoSimbolos;

            txtUsuario.Enter += txt_Enter;
            txtUsuario.Leave += txt_Leave;

            txtPassword.Enter += txt_Enter;
            txtPassword.Leave += txt_Leave;
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lblErrorUsuario.Text = @"* Ingrese un usuario.";
                txtUsuario.Text = "";

                lblErrorUsuario.Visible = true;
            }

            else
            {
                if (txtUsuario.Text.Trim() == "admin")
                {
                    lblErrorUsuario.Text = @"* No se puede modificar el usuario Admin.";
                    lblErrorUsuario.Visible = true;
                }

                else
                {
                    lblErrorUsuario.Visible = false;

                    circularProgressBar.Visible = true;

                    timer1.Start();
                }

               
            }

           
        }

        public void txt_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Constante.ColorControl.ColorSinFoco;
            }
            else if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).BackColor = Constante.ColorControl.ColorSinFoco;
            }
        }

        public void txt_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Constante.ColorControl.ColorConFoco;
            }
            else if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).BackColor = Constante.ColorControl.ColorConFoco;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtPassword2.Text))
            {
                lblErrorContraseña.Text = @"* Completar todos los campos.";

                lblErrorContraseña.Visible = true;
            }

            else
            {
                if (txtPassword.Text.Trim() == txtPassword2.Text.Trim())
                {
                    _usuarioServicio.CambiarPassword(txtUsuario.Text, txtPassword.Text);

                    Mensaje.Mostrar("Password Actualizado", Mensaje.Tipo.Informacion);

                    Close();
                }

                else
                {
                    lblErrorContraseña.Text = @"* Las Password no coinciden.";

                    lblErrorContraseña.Visible = true;

                    txtPassword.Clear();
                    txtPassword2.Clear();
                }
            }
           
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            lblErrorContraseña.Visible = false;
        }

        private void txtUsuario_Click(object sender, EventArgs e)
        {
            lblErrorUsuario.Visible = false;
        }

        private void txtPassword2_Click(object sender, EventArgs e)
        {
            lblErrorContraseña.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (_usuarioServicio.AutenticarUsuarioSinAdmin(txtUsuario.Text))
            {
                circularProgressBar.Visible = false;

                lblPassword.Visible = true;
                lblPassword2.Visible = true;
                txtPassword.Visible = true;
                txtPassword2.Visible = true;
                btnGuardar.Visible = true;

                txtUsuario.Enabled = false;

                btnBuscar.Visible = false;

                timer1.Stop();
            }

            else
            {
                circularProgressBar.Visible = false;

                lblErrorUsuario.Text = @"* El usuario ingresado no existe o es incorrecto.";

                lblErrorUsuario.Visible = true;

                timer1.Stop();
            }

        }
    }
}
