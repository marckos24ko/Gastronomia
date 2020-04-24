using Presentacion.Base;
using Presentacion.Base.Varios;
using Presentacion.Core.Condicion_Iva;
using Presentacion.Core.Rubro;
using Servicio.Core.Condicion_Iva;
using Servicio.Core.Proveedores;
using Servicio.Core.Rubro;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Proveedores
{
    public partial class _60002_ABM_Provedores : FormularioABM
    { 

    private readonly IProveedorServicio _proveedorServicio;
    private readonly ICondicionIvaServicio _condicionIvaServicio;
    private readonly IRubroServicio _rubroServicio;
    private long? _proveedorId;

    public _60002_ABM_Provedores(string tipoOperacion, long? entidadId)
            : base(tipoOperacion, entidadId)
        {
        InitializeComponent();

          _proveedorServicio = new ProvedoresServicio();
          _condicionIvaServicio = new CondicionIvaServicio();
          _rubroServicio = new RubroServicio();

            Init(tipoOperacion, entidadId);

            PoblarComboBox(cmbRubro, _rubroServicio.ObtenerTodo(), "Descripcion", "Id");
            PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.ObtenerTodo(), "Descripcion", "Id");


            if (tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _proveedorId = entidadId;
                cmbCondicionIva.DropDownStyle = ComboBoxStyle.DropDown;
            }

            if (tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _proveedorId = null;
            }

            if (tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _proveedorId = entidadId;
            }

            txtApyNomContacto.Enter += txt_Enter;
            txtApyNomContacto.Leave += txt_Leave;
            txtCuit.Enter += txt_Enter;
            txtCuit.Leave += txt_Leave;
            txtDireccion.Enter += txt_Enter;
            txtDireccion.Leave += txt_Leave;
            txtNombFantasia.Enter += txt_Enter;
            txtNombFantasia.Leave += txt_Leave;
            txtTelefono.Enter += txt_Enter;
            txtTelefono.Leave += txt_Leave;
            nudIngresosBrutos.Enter += txt_Enter;
            nudIngresosBrutos.Leave += txt_Leave;

            txtApyNomContacto.KeyPress += Validacion.NoSimbolos;
            txtApyNomContacto.KeyPress += Validacion.NoInyeccion;
            txtApyNomContacto.KeyPress += Validacion.NoNumeros;

            txtCuit.KeyPress += Validacion.NoSimbolos;
            txtCuit.KeyPress += Validacion.NoInyeccion;
            txtCuit.KeyPress += Validacion.NoLetras;

            txtDireccion.KeyPress += Validacion.NoSimbolos;
            txtDireccion.KeyPress += Validacion.NoInyeccion;

            txtNombFantasia.KeyPress += Validacion.NoSimbolos;
            txtNombFantasia.KeyPress += Validacion.NoInyeccion;

            txtTelefono.KeyPress += Validacion.NoSimbolos;
            txtTelefono.KeyPress += Validacion.NoInyeccion;
            txtTelefono.KeyPress += Validacion.NoLetras;

            nudIngresosBrutos.KeyPress += Validacion.NoInyeccion;
            nudIngresosBrutos.KeyPress += Validacion.NoSimbolos;
            nudIngresosBrutos.KeyPress += Validacion.NoNumeros;

            PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.ObtenerTodo(), "Descripcion", "Id");
            PoblarComboBox(cmbRubro, _rubroServicio.ObtenerTodo(), "Descripcion", "Id");
        }

    public override void CargarDatos(long? _entidadId)
    {
        var proveedor = _proveedorServicio.ObtenerPorId(entidadId.Value);

        txtApyNomContacto.Text = proveedor.ApyNomContacto;
        txtCuit.Text = proveedor.Cuit;
        txtDireccion.Text = proveedor.Direccion;
        txtNombFantasia.Text = proveedor.NombreFantasia;
        txtTelefono.Text = proveedor.Telefono;
        dtpFechaInicio.Value = proveedor.FechaInicioActividad;
        nudIngresosBrutos.Value = Convert.ToDecimal(proveedor.IngresosBrutos);
        cmbCondicionIva.ValueMember = proveedor.CondicionIvaStr;
        cmbRubro.ValueMember = proveedor.RubroStr;
        txtRazonSocial.Text = proveedor.RazonSocial;
    }

    public override void LimpiarDatos(object obj)
    {
        base.LimpiarDatos(obj);

        txtApyNomContacto.Clear();
        txtCuit.Clear();
        txtDireccion.Clear();
        txtNombFantasia.Clear();
        txtTelefono.Clear();
        txtRazonSocial.Clear();
        nudIngresosBrutos.Value = 100m;
    }

    public override bool VerificarSiExiste()
    {
        return _proveedorServicio.VerificarSiExiste(_proveedorId, txtNombFantasia.Text, txtCuit.Text);
    }

    public void ActualizarComboRubro()
    {
        cmbRubro.DataSource = _rubroServicio.ObtenerTodo();
        cmbRubro.SelectedItem = null;
        cmbRubro.Focus();

    }

    public void ActualizarComboCondicionIva()
    {
        cmbCondicionIva.DataSource = _condicionIvaServicio.ObtenerTodo();
        cmbCondicionIva.SelectedItem = null;
        cmbCondicionIva.Focus();

    }

    public override bool VerificarDatosObligatorios()
    {
        if (string.IsNullOrEmpty(txtApyNomContacto.Text) || string.IsNullOrEmpty(txtNombFantasia.Text)
            || string.IsNullOrEmpty(txtCuit.Text) || string.IsNullOrEmpty(txtTelefono.Text)
            || string.IsNullOrEmpty(txtDireccion.Text) || cmbCondicionIva.SelectedValue == null)
        {
            Mensaje.Mostrar("Los datos marcados con * son Obligatorios..", Mensaje.Tipo.Informacion);
            return false;
        }
        return true;
    }

    public override bool EjecutarComandoNuevo()
    {
        try
        {
            _proveedorServicio.Insertar(new ProveedorDto()
            {
                ApyNomContacto = txtApyNomContacto.Text,
                CondicionIvaId = (long)cmbCondicionIva.SelectedValue,
                Cuit = txtCuit.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                FechaInicioActividad = dtpFechaInicio.Value,
                IngresosBrutos = nudIngresosBrutos.Value,
                NombreFantasia = txtNombFantasia.Text,
                RazonSocial = txtRazonSocial.Text,
                RubroId = (long)cmbRubro.SelectedValue
              

            });
            Mensaje.Mostrar("Los datos se grabaron correctamente", Mensaje.Tipo.Informacion);
            return true;
        }
        catch (Exception ex)
        {
            Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);
        }
        return false;
    }

    public override bool EjecutarComandoModificar()
    {
        try
        {
            _proveedorServicio.Modificar(new ProveedorDto()
            {
                Id = entidadId.Value,
                ApyNomContacto = txtApyNomContacto.Text,
                CondicionIvaId = (long)cmbCondicionIva.SelectedValue,
                Cuit = txtCuit.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                FechaInicioActividad = dtpFechaInicio.Value,
                IngresosBrutos = nudIngresosBrutos.Value,
                NombreFantasia = txtNombFantasia.Text,
                RazonSocial = txtRazonSocial.Text,
                RubroId = (long)cmbRubro.SelectedValue

            });
            Mensaje.Mostrar("Los datos se grabaron correctamente.", Mensaje.Tipo.Informacion);
            return true;
        }
        catch (Exception ex)
        {
            Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);
        }
        return false;
    }

    public override bool EjecutarComandoEliminar()
    {
        try
        {
            _proveedorServicio.Eliminar(entidadId.Value);
            Mensaje.Mostrar("Los datos se Eliminaron correctamente.", Mensaje.Tipo.Informacion);
            return true;
        }
        catch (Exception ex)
        {
            Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);
        }
        return false;
    }

    private void btnCondicionIva_Click(object sender, EventArgs e)
    {
        var formulario = new _9002_ABM_CondicionIva(Constante.TipoOperacion.Nuevo, null)
        {
            Text = "Nueva Condicion Iva"
        };

        formulario.ShowDialog();

       if (formulario.RealizoAlgunaOperacion)
       {
           ActualizarComboCondicionIva();
       }
    }

    private void btnAgregarRubro_Click(object sender, EventArgs e)
    {
       var formulario = new _70002_ABM_Rubro(Constante.TipoOperacion.Nuevo, null)
       {
           Text = "Nuevo Rubro"
       };
       formulario.ShowDialog();

       if (formulario.RealizoAlgunaOperacion)
       {
           ActualizarComboRubro();
       }
    }

    }
}
