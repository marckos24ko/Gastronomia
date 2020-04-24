using Gatronomia;
using System;
using System.Windows.Forms;

namespace Presentacion.Base
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                rectangleShape2.Width += 1;

                var ancho = rectangleShape2.Width;

                switch (ancho)
                {
                    case 55:
                        lblCargando.Text = "Cargando Metadatos . . .";
                        break;
                    case 102:
                        lblCargando.Text = "Iniciando Servidor . . .";
                        break;
                    case 149:
                        lblCargando.Text = "Sistema Listo . . .";
                        break;
                }

                if (rectangleShape2.Width >= 197 )
                {
                    
                    timer1.Stop();

                    var form = new Principal();

                    this.Hide();

                    form.ShowDialog();

                    Close();

                }   
        }


    }
}
