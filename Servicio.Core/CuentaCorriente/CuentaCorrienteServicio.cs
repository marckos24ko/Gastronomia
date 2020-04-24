using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Core.CuentaCorriente.Dto;
using Servicio.Core.Cliente;
using DAL;
using Servicio.Core.ComprobanteSalon;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.FacturaEfectivo.Dto;

namespace Servicio.Core.CuentaCorriente
{
    public class CuentaCorrienteServicio : ICuentaCorrienteServicio
    {
        public int ObtenerSiguienteNumero()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.CuentasCorriente
                    .Any()
                    ? context.CuentasCorriente.Max(x => x.Numero) + 1
                    : 1;
            }
        }

        public void CrearCuentaCorriente(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cliente = context.Personas.OfType<DAL.Cliente>()
                      .Single(x => x.Id == ClienteId);

                var CtaCteNueva = new DAL.CuentaCorriente();

                CtaCteNueva.Numero = ObtenerSiguienteNumero();
                CtaCteNueva.Fecha = DateTime.Now;
                CtaCteNueva.EstaHabilitada = true;
                CtaCteNueva.Cliente = cliente;

                context.CuentasCorriente.Add(CtaCteNueva);

                context.SaveChanges();
            }
        }

        public void SuspenderCuentaCorriente(long ctaCteId, long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cliente = context.Personas.OfType<DAL.Cliente>()
                      .Single(x => x.Id == ClienteId);
               
                    var ctacteSuspender = context.CuentasCorriente
                        .Single(x => x.Id == ctaCteId);

                    ctacteSuspender.EstaHabilitada = false;

                    context.SaveChanges();
                
            }
        }

        public void ReactivarCuentaCorriente(long ctaCteId, long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cliente = context.Personas.OfType<DAL.Cliente>()
                      .Single(x => x.Id == ClienteId);

                var ctacteSuspender = context.CuentasCorriente
                    .Single(x => x.Id == ctaCteId);

                ctacteSuspender.EstaHabilitada = true;

                context.SaveChanges();

            }
        }

        public CuentaCorrienteDto ObtenerCuentaCorrientePorClienteId(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cuenta = context.CuentasCorriente
                    .FirstOrDefault(x => x.Cliente.Id == ClienteId && x.EstaHabilitada == true);

                if (cuenta == null) throw new ArgumentNullException("No existe la cuenta");

                var CuentaDto = new CuentaCorrienteDto
                {
                    Id = cuenta.Id,
                    Numero = cuenta.Numero,
                    Fecha = cuenta.Fecha,
                    EstaHabilitada = cuenta.EstaHabilitada,
                    ClienteId = cuenta.Cliente.Id,
                    ClienteApyNom = string.Concat(cuenta.Cliente.Apellido, " ", cuenta.Cliente.Nombre)

                };

                return CuentaDto;
            }
        }

        public CuentaCorrienteDto ObtenerCuentaCorrientePorClienteIdSinFiltro(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cuenta = context.CuentasCorriente
                    .FirstOrDefault(x => x.Cliente.Id == ClienteId);

                if (cuenta == null) throw new ArgumentNullException("No existe la cuenta");

                var CuentaDto = new CuentaCorrienteDto
                {
                    Id = cuenta.Id,
                    Numero = cuenta.Numero,
                    Fecha = cuenta.Fecha,
                    EstaHabilitada = cuenta.EstaHabilitada,
                    ClienteId = cuenta.Cliente.Id,
                    ClienteApyNom = string.Concat(cuenta.Cliente.Apellido, " ", cuenta.Cliente.Nombre)

                };

                return CuentaDto;
            }
        }

        public bool verificarSiTieneCtaCte(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cuenta = context.CuentasCorriente
                    .Any(x => x.Cliente.Id == ClienteId);

                return cuenta;

            }
        }

        public void PagarFactura(long facturaId, decimal montoDinero, long empleadoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var factura = context.Facturas.Single
                    (x => (x.Id == facturaId)
                    &&(x.CuentaCorrienteId != null)
                    &&(x.Estado == EstadoFactura.Impagada || x.Estado == EstadoFactura.PagadaParcial));

                if (montoDinero < factura.Total)
                {
                    factura.Total = factura.Total - montoDinero;
                    factura.Estado = EstadoFactura.PagadaParcial;

                    context.SaveChanges();
                }

                if (montoDinero >= factura.Total)
                {
                    factura.Total = 0m;
                    factura.Estado = EstadoFactura.Pagada;

                    context.SaveChanges();
                }


            }
        }
    }
}


