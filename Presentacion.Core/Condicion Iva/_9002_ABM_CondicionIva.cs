using Presentacion.Base;
using Presentacion.Base.Varios;
using Servicio.Core.Condicion_Iva;
using System;

namespace Presentacion.Core.Condicion_Iva
{
    public partial class _9002_ABM_CondicionIva : FormularioABM
    {
        private readonly ICondicionIvaServicio _condicionIvaServicio;
        private long? _condicionIvaId;

        public _9002_ABM_CondicionIva(string tipoOperacion, long? entidadId)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();
            _condicionIvaServicio = new CondicionIvaServicio();

            nudCodigo.Enter += txt_Enter;
            nudCodigo.Leave += txt_Leave;
            txtDescripcion.Enter += txt_Enter;
            txtDescripcion.Leave += txt_Leave;

            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoInyeccion;

            Init(tipoOperacion, entidadId);

            if (tipoOperacion == Constante.TipoOperacion.Modificar)
            {
                _condicionIvaId = entidadId;
                nudCodigo.Enabled = false;
            }

            if (tipoOperacion == Constante.TipoOperacion.Nuevo)
            {
                _condicionIvaId = null;
                nudCodigo.Value = _condicionIvaServicio.ObtenerSiguienteCodigo();
                nudCodigo.Enabled = false;
            }

            if (tipoOperacion == Constante.TipoOperacion.Eliminar)
            {
                _condicionIvaId = entidadId;
            }
        }

        public override void CargarDatos(long? _entidadId)
        {
            var iva = _condicionIvaServicio.ObtenerPorId(entidadId.Value);

            nudCodigo.Value = iva.Codigo;
            txtDescripcion.Text = iva.Descripcion;
        }

        public override void LimpiarDatos(object obj)
        {
            base.LimpiarDatos(obj);

            txtDescripcion.Clear();
            txtDescripcion.Focus();

            ObtenerSiguienteCodigo();
        }

        public override void ObtenerSiguienteCodigo()
        {
            nudCodigo.Value = _condicionIvaServicio.ObtenerSiguienteCodigo();
        }

        public override bool VerificarSiExiste()
        {
            return _condicionIvaServicio.VerificarSiExiste(_condicionIvaId, (int)nudCodigo.Value, txtDescripcion.Text);
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                Mensaje.Mostrar(@"La descripción es Obligatoria", Mensaje.Tipo.Informacion);
                return false;
            }
            return true;
        }

        public override bool EjecutarComandoNuevo()
        {
            try
            {
                _condicionIvaServicio.Insertar(new CondicionIvaDto()
                {
                    Codigo = (int)nudCodigo.Value,
                    Descripcion = txtDescripcion.Text
                });
                Mensaje.Mostrar("Los datos se grabaron Correctamente", Mensaje.Tipo.Informacion);
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
                _condicionIvaServicio.Modificar(new CondicionIvaDto()
                {
                    Id = entidadId.Value,
                    Descripcion = txtDescripcion.Text
                });
                Mensaje.Mostrar("Los datos se modificaron Correctamente", Mensaje.Tipo.Informacion);
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
                _condicionIvaServicio.Eliminar(entidadId.Value);
                Mensaje.Mostrar(@"Éxito al eliminar", Mensaje.Tipo.Informacion);
                return true;

            }
            catch (Exception ex)
            {
                Mensaje.Mostrar(ex.Message, Mensaje.Tipo.Stop);
            }
            return false;
        }
    }
}
