using DAL;
using Servicio.Core.FacturaEfectivo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicio.Core.FacturaEfectivo
{
    public class FacturaServicio : IFacturaServicio
    {
        public void EmitirFactura(long clienteId, decimal Total, long empleadoId, long? CtaCteId, long ComprobanteId, decimal totalAbonado)
        {

            using (var context = new ModeloGastronomiaContainer())
            {

                var cliente = context.Personas.OfType<DAL.Cliente>()
                      .Single(x => x.Id == clienteId);

                var comprobante = context.Comprobantes.Single(x => x.Id == ComprobanteId);

                var FacturaNueva = new Factura
                {
                    ClienteId = clienteId,
                    Numero = ObtenerSiguienteNumero(),
                    Fecha = DateTime.Now,
                    Estado = totalAbonado > 0m ? EstadoFactura.Pagada : EstadoFactura.Impagada,
                    Total = Total,
                    TotalAbonado = totalAbonado > 0m ? totalAbonado : 0m,
                    EmpleadoId = empleadoId,
                    Comprobante = comprobante,
                    CuentaCorrienteId = CtaCteId
                };


                context.Facturas.Add(FacturaNueva);

                context.SaveChanges();

            }
        }

        public void ModificarEstado(long facturaId, decimal montoAbonado)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var facturaModificar = context.Facturas.FirstOrDefault(x => x.Id == facturaId);

                if ((montoAbonado + facturaModificar.TotalAbonado) >= facturaModificar.Total)
                {
                    facturaModificar.TotalAbonado = facturaModificar.Total;
                    facturaModificar.Estado = EstadoFactura.Pagada;

                    context.SaveChanges();
                }

                else
                {
                    facturaModificar.TotalAbonado = facturaModificar.TotalAbonado + montoAbonado;
                    facturaModificar.Estado = EstadoFactura.PagadaParcial;

                    context.SaveChanges();
                }

            }
        }

        public IEnumerable<FacturaDto> ObtenerFacturasPorCtaCte(string cadenaBuscar, long CtaCteId, long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = 1;
                int.TryParse(cadenaBuscar, out numero);

                var facturas = context.Facturas.AsNoTracking()
                                               .Where(x => (x.CuentaCorrienteId == CtaCteId)
                                                        && (x.ClienteId == clienteId)
                                                        && (x.Cliente.Apellido.Contains(cadenaBuscar)
                                                        || x.Cliente.Nombre.Contains(cadenaBuscar)
                                                        || x.Empleado.Apellido.Contains(cadenaBuscar)
                                                        || x.Empleado.Nombre.Contains(cadenaBuscar)
                                                        || x.Numero == numero))
                                                        
                                .Select(x => new FacturaDto()
                                {
                                    Id = x.Id,
                                    Numero = x.Numero,
                                    ComprobanteId = x.Comprobante.Id,
                                    Fecha = x.Fecha,
                                    Total = x.Total,
                                    TotalAbonado = x.TotalAbonado,
                                    Estado = x.Estado == EstadoFactura.Impagada ? "Impagada" : x.Estado == EstadoFactura.Pagada ? "Pagada" : "Pagada Parcial",
                                    EmpleadoId = x.EmpleadoId,
                                    ClienteId = x.ClienteId,
                                    CLienteApynom = string.Concat(x.Cliente.Apellido, " ", x.Cliente.Nombre),
                                    EmpleadoApynom = string.Concat(x.Empleado.Apellido, " ", x.Empleado.Nombre)

                                }).ToList();

                return facturas;
                                                            
                                                            
            }
        }

        public IEnumerable<FacturaDto> ObtenerFacturasPagadasCtaCte(string cadenaBuscar, long CtaCteId, EstadoFactura Estado)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = 1;
                int.TryParse(cadenaBuscar, out numero);

                var facturas = context.Facturas.AsNoTracking()
                                               .Where(x =>(x.CuentaCorrienteId == CtaCteId)
                                                           && (x.Estado == Estado)
                                                           && (x.Cliente.Apellido.Contains(cadenaBuscar) ||
                                                               x.Cliente.Nombre.Contains(cadenaBuscar) ||
                                                               x.Empleado.Apellido.Contains(cadenaBuscar) ||
                                                               x.Empleado.Nombre.Contains(cadenaBuscar) ||
                                                               x.Numero == numero))
                                .Select(x => new FacturaDto()
                                {
                                    Id = x.Id,
                                    Numero = x.Numero,
                                    ComprobanteId = x.Comprobante.Id,
                                    Fecha = x.Fecha,
                                    Total = x.Total,
                                    TotalAbonado = x.TotalAbonado,
                                    Estado =  "Pagada",
                                    EmpleadoId = x.EmpleadoId,
                                    CLienteApynom = string.Concat(x.Cliente.Apellido, " ", x.Cliente.Nombre),
                                    EmpleadoApynom = string.Concat(x.Empleado.Apellido, " ", x.Empleado.Nombre)
                                    

                                }).ToList();

                return facturas;


            }
        }

        public int ObtenerSiguienteNumero()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Facturas
                    .Any()
                    ? context.Facturas.Max(x => x.Numero) + 1
                    : 1;
            }
        }

        public FacturaDto ObtenerUltimaFacturaEmitida()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = context.Facturas.Max(x => x.Numero);

                var factura = context.Facturas.FirstOrDefault(x => x.Numero == numero);

                var facturaDto = new FacturaDto()
                {
                    Id = factura.Id,
                    Numero = factura.Numero,
                    Fecha = factura.Fecha,
                    Total = factura.Total,
                    TotalAbonado = factura.TotalAbonado,
                    ClienteId = factura.ClienteId,
                    EmpleadoId = factura.EmpleadoId,
                    CLienteApynom = string.Concat(factura.Cliente.Apellido, " ", factura.Cliente.Nombre) 
                };

                return facturaDto;

            }
        }

        public IEnumerable<FacturaDto> ObtenerFacturasPorCliente(long? clienteId, string cadenabuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = -1;

                int.TryParse(cadenabuscar, out numero);
             
                var facturas = context.Facturas.AsNoTracking()
                                               .Where(x => (x.Cliente.Apellido.Contains(cadenabuscar)
                                                         || x.Cliente.Nombre.Contains(cadenabuscar)
                                                         || x.Empleado.Apellido.Contains(cadenabuscar)
                                                         || x.Empleado.Nombre.Contains(cadenabuscar)
                                                         || x.Numero == numero)
                                                         && x.Cliente.Id == clienteId)
                                .Select(x => new FacturaDto()
                                {
                                    Id = x.Id,
                                    Numero = x.Numero,
                                    ComprobanteId = x.Comprobante.Id,
                                    Fecha = x.Fecha,
                                    Total = x.Total,
                                    TotalAbonado = x.TotalAbonado,
                                    Estado = x.Estado == EstadoFactura.Impagada ? "Impagada" : x.Estado == EstadoFactura.Pagada ? "Pagada" : "Pagada Parcial",
                                    EmpleadoId = x.EmpleadoId,
                                    ClienteId = x.Cliente.Id,
                                    CLienteApynom = string.Concat(x.Cliente.Apellido, " ", x.Cliente.Apellido),
                                    EmpleadoApynom = string.Concat(x.Empleado.Apellido, " ", x.Empleado.Nombre),
                                    CuentaCorrienteId = x.CuentaCorrienteId
                                    

                                }).ToList();

                return facturas;


            }
        }

        public IEnumerable<FacturaDto> ObtenerFacturasImpagadasCtaCte(string cadenaBuscar, long CtaCteId, EstadoFactura Estado, EstadoFactura Estado2)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = 1;
                int.TryParse(cadenaBuscar, out numero);

                var facturas = context.Facturas.AsNoTracking()
                                               .Where(x => (x.CuentaCorrienteId == CtaCteId)
                                                           && (x.Estado == Estado
                                                           || x.Estado == Estado2)
                                                            && (x.Cliente.Apellido.Contains(cadenaBuscar) ||
                                                               x.Cliente.Nombre.Contains(cadenaBuscar) ||
                                                               x.Empleado.Apellido.Contains(cadenaBuscar) ||
                                                               x.Empleado.Nombre.Contains(cadenaBuscar) ||
                                                               x.Numero == numero))
                                .Select(x => new FacturaDto()
                                {
                                    Id = x.Id,
                                    Numero = x.Numero,
                                    ComprobanteId = x.Comprobante.Id,
                                    Fecha = x.Fecha,
                                    Total = x.Total,
                                    TotalAbonado = x.TotalAbonado,
                                    Estado = x.Estado == EstadoFactura.Impagada ? "Impagada" : "Pagada Parcial",
                                    EmpleadoId = x.EmpleadoId,
                                    CLienteApynom = string.Concat(x.Cliente.Apellido, " ", x.Cliente.Nombre),
                                    EmpleadoApynom = string.Concat(x.Empleado.Apellido, " ", x.Empleado.Nombre)

                                }).ToList();

                return facturas;
            }
        }
    }
}
