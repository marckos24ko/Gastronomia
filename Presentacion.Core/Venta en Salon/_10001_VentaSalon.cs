using System;
using System.Windows.Forms;
using Presentacion.Base.Varios;
using Presentacion.Core.ControlesUsuarios;
using Servicio.Core.SalonMesa;

namespace Presentacion.Core.Venta_en_Salon
{
    public partial class _10001_VentaSalon : Form
    {
        private readonly ISaloMesaServicio _salonMesaServicio;

        public _10001_VentaSalon(ISaloMesaServicio salonMesaServicio)
        {
            InitializeComponent();
            _salonMesaServicio = salonMesaServicio;
            CargarMesas();
            lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;
        }

        public _10001_VentaSalon()
            : this(new SaloMesaServicio())
        {

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public void CargarMesas()
        {
            // Obtengo las mesas para el salon de venta
            var mesas = _salonMesaServicio.ObtenerMesasParaSalon();

            foreach (var mesa in mesas)
            {
                var ctrolMesa = new UserControlMesa
                {
                    EstadoMesa = mesa.EstadoMesa,
                    Total = mesa.Total,
                    Numero = mesa.Numero,
                    Id = mesa.Id
                };

                Contenedor.Controls.Add(ctrolMesa);
            }
        }

    }

}

