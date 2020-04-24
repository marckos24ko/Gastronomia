using Presentacion.Base.Varios;
using Servicio.Core.Usuario;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Login
{
    public partial class WfLogin : Form
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public WfLogin()
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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Nombre + Cuil" && txtUsuario.ForeColor == Color.DarkGray)
            {
                lblErrorUsuario.Text = @"* Ingrese un usuario.";
                lblErrorUsuario.Visible = true;
                txtPassword.Text = "";
            }

            else
            {
                if (_usuarioServicio.AutenticarUsuario(txtUsuario.Text))
                {
                    if (_usuarioServicio.AutenticarPassword(txtUsuario.Text, txtPassword.Text))
                    {
                        Identidad.Usuario = txtUsuario.Text;
                        Identidad.Empleado = txtUsuario.Text;
                        // Autenticado
                        Close();
                    }

                    else
                    {

                        if (string.IsNullOrEmpty(txtPassword.Text))
                        {

                            lblErrorContraseña.Text = @"* Ingrese una password.";
                            lblErrorContraseña.Visible = true;

                        }

                        else
                        {
                            lblErrorContraseña.Text = @"* Password Incorrecto.";
                            lblErrorContraseña.Visible = true;
                            txtPassword.Clear();
                        }

                    }
                }

                else
                {
                    if (string.IsNullOrEmpty(txtUsuario.Text))
                    {
                        lblErrorUsuario.Text = @"* Ingrese un usuario.";
                        lblErrorUsuario.Visible = true;
                    }

                    else
                    {
                        lblErrorUsuario.Text = @"* El usuario ingresado no existe o es incorrecto.";
                        lblErrorUsuario.Visible = true;
                        txtUsuario.Clear();
                        txtPassword.Clear();
                    }


                }
            }

         
        }

        private void WfLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Text = @"admin";
            txtPassword.Text = @"123";

        }

        private void btnIngresar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUsuario.Text == "Nombre + Cuil" && txtUsuario.ForeColor == Color.DarkGray)
                {
                    lblErrorUsuario.Text = @"* Ingrese un usuario.";
                    lblErrorUsuario.Visible = true;
                    txtPassword.Text = "";
                }

                else
                {
                    if (_usuarioServicio.AutenticarUsuario(txtUsuario.Text))
                    {
                        if (_usuarioServicio.AutenticarPassword(txtUsuario.Text, txtPassword.Text))
                        {
                            Identidad.Usuario = txtUsuario.Text;
                            Identidad.Empleado = txtUsuario.Text;
                            // Autenticado
                            Close();
                        }

                        else
                        {

                            if (string.IsNullOrEmpty(txtPassword.Text))
                            {

                                lblErrorContraseña.Text = @"* Ingrese una password.";
                                lblErrorContraseña.Visible = true;

                            }

                            else
                            {
                                lblErrorContraseña.Text = @"* Password Incorrecto.";
                                lblErrorContraseña.Visible = true;
                                txtPassword.Clear();
                            }

                        }
                    }

                    else
                    {
                        if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                        {
                            lblErrorUsuario.Text = @"* Ingrese un usuario.";
                            lblErrorUsuario.Visible = true;
                        }

                        else
                        {
                            lblErrorUsuario.Text = @"* El usuario ingresado no existe o es incorrecto.";
                            lblErrorUsuario.Visible = true;
                            txtUsuario.Clear();
                            txtPassword.Clear();
                        }


                    }
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

        private void txtUsuario_Click(object sender, EventArgs e)
        {
            lblErrorUsuario.Visible = false;
            lblErrorContraseña.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RecuperarCuenta().ShowDialog();
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Nombre + Dni")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }

            if (txtUsuario.Text == "")
            {
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Nombre + Cuil";
                txtUsuario.ForeColor = Color.DarkGray;
            }
            else
            {
                txtUsuario.ForeColor = Color.Black;
            }
            
        }
    }
}
