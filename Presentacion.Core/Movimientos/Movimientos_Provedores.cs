using Presentacion.Base.Varios;
using Servicio.Core.FacturaEfectivo;
using Servicio.Core.Movimientos;
using Servicio.Core.Proveedores;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Movimientos
{
    public partial class Movimientos_Provedores : Form
    {
        private IProveedorServicio _proveedoresServicio;
        private IFacturaServicio _facturaServicio;
        private IMovimientoServicio _movimientoServicio;
        private ProveedorDto _proveedorSeleccionado;

        public Movimientos_Provedores()
        {
            InitializeComponent();

            _proveedoresServicio = new ProvedoresServicio();
            _facturaServicio = new FacturaServicio();
            _movimientoServicio = new MovimientoServicio();

            ActualizarDatosProveedores(txtBuscarProveedores.Text);

            txtBuscarMovimientos.KeyPress += Validacion.NoInyeccion;
            txtBuscarMovimientos.KeyPress += Validacion.NoSimbolos;

            txtBuscarProveedores.KeyPress += Validacion.NoInyeccion;
            txtBuscarProveedores.KeyPress += Validacion.NoSimbolos;

            txtBuscarMovimientos.Enter += txt_Enter;
            txtBuscarMovimientos.Leave += txt_Leave;

            txtBuscarProveedores.Enter += txt_Enter;
            txtBuscarProveedores.Leave += txt_Leave;

            this.lblUsuarioLogin.Text = $"Usuario: {Identidad.Empleado}";
            lblUsuarioLogin.Image = Constante.ImagenControl.Usuario;



        }

        private void ActualizarDatosProveedores(string cadenaBuscar)
        {
            dgvGrillaProveedores.DataSource = _proveedoresServicio.ObtenerPorFiltro(cadenaBuscar).ToList();

            FormatearGrillaProveedores(dgvGrillaProveedores);

            if (dgvGrillaProveedores.RowCount == 0)
            {
                _proveedorSeleccionado = null;

                dgvGrillaMovimientos.DataSource = _movimientoServicio.ObtenerMovimientoPorProveedorId(null, string.Empty).ToList();

                FormatearGrillaMovimientos(dgvGrillaMovimientos);


            }

        }

        private void ActualizarDatosMovimientos(string cadenaBuscar)
        {
            if (_proveedorSeleccionado != null)
            {
                dgvGrillaMovimientos.DataSource = _movimientoServicio.ObtenerMovimientoPorProveedorId(_proveedorSeleccionado.Id, cadenaBuscar).ToList();

                FormatearGrillaMovimientos(dgvGrillaMovimientos);
            }


        }

        private void FormatearGrillaMovimientos(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["ClienteId"].Visible = false;
            dgvGrilla.Columns["FacturaId"].Visible = false;
            dgvGrilla.Columns["ClienteApyNom"].Visible = false;

            dgvGrilla.Columns["Numero"].Visible = true;
            dgvGrilla.Columns["Numero"].HeaderText = @"Numero";
            dgvGrilla.Columns["Numero"].DisplayIndex = 0;

            dgvGrilla.Columns["ProveedorNombre"].Visible = true;
            dgvGrilla.Columns["ProveedorNombre"].HeaderText = @"Proveedor";
            dgvGrilla.Columns["ProveedorNombre"].DisplayIndex = 1;

            dgvGrilla.Columns["Monto"].Visible = true;
            dgvGrilla.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["Monto"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["Monto"].HeaderText = @"Monto";
            dgvGrilla.Columns["Monto"].DisplayIndex = 2;

            dgvGrilla.Columns["TipoMovimiento"].Visible = true;
            dgvGrilla.Columns["TipoMovimiento"].HeaderText = @"Tipo de Movimiento";
            dgvGrilla.Columns["TipoMovimiento"].DisplayIndex = 3;

            dgvGrilla.Columns["Fecha"].Visible = true;
            dgvGrilla.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvGrilla.Columns["Fecha"].HeaderText = @"Fecha";
            dgvGrilla.Columns["Fecha"].DisplayIndex = 4;

        }

        private void FormatearGrillaProveedores(DataGridView dgvGrilla)
        {
            dgvGrilla.Columns["Id"].Visible = false;
            dgvGrilla.Columns["RubroId"].Visible = false;
            dgvGrilla.Columns["CondicionIvaId"].Visible = false;
            dgvGrilla.Columns["EstaEliminado"].Visible = false;
            dgvGrilla.Columns["Direccion"].Visible = false;
            dgvGrilla.Columns["Telefono"].Visible = false;
            dgvGrilla.Columns["Cuit"].Visible = false;
            dgvGrilla.Columns["RazonSocial"].Visible = false;
            dgvGrilla.Columns["CondicionIvaStr"].Visible = false;
            dgvGrilla.Columns["productos"].Visible = false;

            dgvGrilla.Columns["RubroStr"].Visible = true;
            dgvGrilla.Columns["RubroStr"].HeaderText = @"Rubro";
            dgvGrilla.Columns["RubroStr"].DisplayIndex = 1;

            dgvGrilla.Columns["NombreFantasia"].Visible = true;
            dgvGrilla.Columns["NombreFantasia"].HeaderText = @"Nombre de la Empresa";
            dgvGrilla.Columns["NombreFantasia"].DisplayIndex = 0;

            dgvGrilla.Columns["ApyNomContacto"].Visible = true;
            dgvGrilla.Columns["ApyNomContacto"].HeaderText = @"Contacto";
            dgvGrilla.Columns["ApyNomContacto"].DisplayIndex = 2;

            dgvGrilla.Columns["IngresosBrutos"].Visible = true;
            dgvGrilla.Columns["IngresosBrutos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrilla.Columns["IngresosBrutos"].DefaultCellStyle.Format = "C2";
            dgvGrilla.Columns["IngresosBrutos"].HeaderText = @"Ingresos Brutos";
            dgvGrilla.Columns["IngresosBrutos"].DisplayIndex = 3;

            dgvGrilla.Columns["FechaInicioActividad"].Visible = true;
            dgvGrilla.Columns["FechaInicioActividad"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvGrilla.Columns["FechaInicioActividad"].HeaderText = @"Inicio Actividades";
            dgvGrilla.Columns["FechaInicioActividad"].DisplayIndex = 4;

        }

        private void btnBuscarProvedoores_Click(object sender, EventArgs e)
        {
            ActualizarDatosProveedores(txtBuscarProveedores.Text.Trim());
        }

        private void dgvGrillaProveedores_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

          if (dgvGrillaProveedores.RowCount > 0)
          {

            _proveedorSeleccionado = (ProveedorDto)dgvGrillaProveedores.Rows[e.RowIndex].DataBoundItem;

          }
            
        }

        private void dgvGrillaProveedores_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaProveedores.RowCount > 0)
            {

                _proveedorSeleccionado = (ProveedorDto)dgvGrillaProveedores.Rows[e.RowIndex].DataBoundItem;

                ActualizarDatosMovimientos(txtBuscarMovimientos.Text.Trim());

                FormatearGrillaMovimientos(dgvGrillaMovimientos);

            }
        }

        private void btnBuscarMovimientos_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionado != null)
            {
                ActualizarDatosMovimientos(txtBuscarMovimientos.Text.Trim());
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

        private void Movimientos_Provedores_Load(object sender, EventArgs e)
        {
            ActualizarDatosProveedores(string.Empty);
            ActualizarDatosMovimientos(string.Empty);
        }
    }
}
