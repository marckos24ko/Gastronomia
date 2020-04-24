using Presentacion.Base.Varios;
using Servicio.Core.Mesa;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Venta_en_Salon
{
    public partial class _10004_Mesa_LookUp : Form
    {
        private readonly IMesaServicio _mesaServicio;
        private MesaDto _mesaSeleccionada;
        public bool RealizoAlgunaOperacion { get; set; }
        public MesaDto Mesa { get { return _mesaSeleccionada; } }

        public _10004_Mesa_LookUp()
        {
            InitializeComponent();

            _mesaServicio = new MesaServicio();

            RealizoAlgunaOperacion = false;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;
        }

        private void ActualizarDatos(string cadenaBuscar)
        {
            dgvgrilla.DataSource = _mesaServicio.ObtenerMesasLibres(cadenaBuscar);
            FormatearGrilla(dgvgrilla);

            if (dgvgrilla.RowCount == 0)
            {
                _mesaSeleccionada = null;
            }
           
        }

        private void FormatearGrilla(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["ComprobanteId"].Visible = false;
            dgvGrilla.Columns["Total"].Visible = false;


            dgvGrilla.Columns["Numero"].Visible = true;
            dgvGrilla.Columns["Numero"].Width = 50;
            dgvGrilla.Columns["Numero"].HeaderText = @"Número";

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].Width = 75;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripción";

            dgvGrilla.Columns["EstadoMesa"].Visible = true;
            dgvGrilla.Columns["EstadoMesa"].Width = 75; ;
            dgvGrilla.Columns["EstadoMesa"].HeaderText = @"Estado";

        }

        private void dgvgrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvgrilla.RowCount > 0)
            {
                _mesaSeleccionada = (MesaDto)dgvgrilla.Rows[e.RowIndex].DataBoundItem;
            }

        }

        private void _10004_Mesa_LookUp_Load(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        private void dgvgrilla_DoubleClick(object sender, EventArgs e)
        {


            if (_mesaSeleccionada != null)
            {
                RealizoAlgunaOperacion = true;

                Close();
            }

            else
            {
                Mensaje.Mostrar("No hay mesas disponibles.", Mensaje.Tipo.Stop);

                Close();
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (_mesaSeleccionada != null)
            {
                RealizoAlgunaOperacion = true;

                Close();
            }

            else
            {
                Mensaje.Mostrar("No hay mesas disponibles.", Mensaje.Tipo.Stop);

                Close();
            }
        }
    }

}
