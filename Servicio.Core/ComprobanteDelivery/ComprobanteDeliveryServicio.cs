using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Core.Delivery.Dto;
using Servicio.Core.Empleado.DTO;
using Servicio.Core.Producto;
using DAL;
using System.Data.Entity;

namespace Servicio.Core.ComprobanteDelivery
{
  public class ComprobanteDeliveryServicio : IComprobanteDeliveryServicio
    {
        public void AgregarItem(long comprobanteId, int cantidad, ProductoDto productoSeleccionado, long empleadoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include(x => x.DetallesDeliveries)
                    .FirstOrDefault(x => x.Id == comprobanteId);

                if (comprobante == null) throw new Exception("Error");

                var item = comprobante.DetallesDeliveries
                    .FirstOrDefault(x => x.ProductoId == productoSeleccionado.Id
                    && x.Precio == productoSeleccionado.PrecioPublico);

                if (comprobante.CadeteId == null) comprobante.CadeteId = empleadoId;


                if (item != null)
                {

                    // en caso de que si exista el producto en el detalle

                    if (item.Precio == productoSeleccionado.PrecioPublico)

                    {
                        item.Cantidad += cantidad;
                        item.SubTotal = item.Cantidad * productoSeleccionado.PrecioPublico;

                        comprobante.SubTotal = comprobante.DetallesDeliveries.Sum(x => x.SubTotal);
                    }

                else
                {
                    comprobante.DetallesDeliveries.Add(new DetalleDelivery

                    {

                        Cantidad = cantidad,
                        Codigo = Convert.ToString(productoSeleccionado.Codigo),
                        CodigoBarra = productoSeleccionado.CodigoBarra,
                        Descripcion = productoSeleccionado.Descripcion,
                        Precio = productoSeleccionado.PrecioPublico,
                        ProductoId = productoSeleccionado.Id,
                        SubTotal = productoSeleccionado.PrecioPublico * cantidad,

                    });

                        comprobante.SubTotal = comprobante.DetallesDeliveries.Sum(x => x.SubTotal);
                }
            }

            else
            {
                    // en caso de que no exista el producto en el detalle
                    comprobante.DetallesDeliveries.Add(new DetalleDelivery

                    {

                        Cantidad = cantidad,
                        Codigo = Convert.ToString(productoSeleccionado.Codigo),
                        CodigoBarra = productoSeleccionado.CodigoBarra,
                        Descripcion = productoSeleccionado.Descripcion,
                        Precio = productoSeleccionado.PrecioPublico,
                        ProductoId = productoSeleccionado.Id,
                        SubTotal = productoSeleccionado.PrecioPublico * cantidad



                    });

                    comprobante.SubTotal = comprobante.DetallesDeliveries.Sum(x => x.SubTotal);

                }

                context.SaveChanges();

            }
        }

        public void Cerrar(long Id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include(x => x.DetallesDeliveries)
                    .FirstOrDefault(x => x.Id == Id && x.EstadoDelivery == EstadoDelivery.EnProceso);

                var subTotal = comprobante.DetallesDeliveries.Sum(x => x.SubTotal);

                comprobante.Total = comprobante.DetallesDeliveries.Sum(x => x.SubTotal);
                comprobante.SubTotal = subTotal;
                comprobante.Descuento = comprobante.Descuento;
                comprobante.EstadoDelivery = EstadoDelivery.Cobrado;
                comprobante.Fecha = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void Crear(long clienteId, long empleadoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .AsNoTracking().FirstOrDefault(x => x.Id == clienteId);



                var nuevoComprobante = new DAL.Delivery
                {
                    Fecha = DateTime.Now,
                    Total = 0m,
                    SubTotal = 0m,
                    DireccionEnvio = Cliente.Direccion,
                    EstadoDelivery = EstadoDelivery.EnProceso,
                    ClienteId = clienteId,
                    CadeteId = empleadoId,
                    Observacion = string.Empty,
                    Descuento = 0m
                };

                context.Comprobantes.Add(nuevoComprobante);

                context.SaveChanges();
            }
        }

        public void DisminuirItem(long comprobanteId, int cantidad, ComprobanteDeliveryDetalleDto producto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include(x => x.DetallesDeliveries)
                    .FirstOrDefault(x => x.Id == comprobanteId);

                if (comprobante == null) throw new Exception("Error.");

                var item = comprobante.DetallesDeliveries
                    .FirstOrDefault(x => x.ProductoId == producto.ProductoId && x.CodigoBarra == producto.CodigoBarra);

                if (item != null)
                {

                    if (item.Precio == producto.Precio)

                    {
                        item.Cantidad -= cantidad;
                        item.SubTotal = item.Cantidad * producto.Precio;
                        comprobante.SubTotal = comprobante.DetallesDeliveries.Sum(x => x.SubTotal);
                    }
                }

                context.SaveChanges();

            }

        }

        public void Eliminar(long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include(x => x.DetallesDeliveries)
                    .FirstOrDefault(x => x.ClienteId == clienteId && x.EstadoDelivery == EstadoDelivery.EnProceso);

                List<DetalleDelivery> lista = new List<DetalleDelivery>();

                foreach (var item in comprobante.DetallesDeliveries)
                {
                    lista.Add(item);
                }

                foreach (var item in lista)
                {
                    if (lista.Any())
                    {
                        context.DetalleComprobantes.Remove(item);
                        context.SaveChanges();
                    }

                }

                context.Comprobantes.Remove(comprobante);
                context.SaveChanges();
            }
        }

        public ComprobanteDeliveryDto ObtenerComprobantePorCLienteSinFacturar(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include("DetallesDeliveries")
                    .Include("Cadete")
                    .FirstOrDefault(x => x.ClienteId == ClienteId
                                         && x.EstadoDelivery == EstadoDelivery.EnProceso);

                if (comprobante == null) throw new ArgumentNullException("Error Grave");

                var comprobanteDto = new ComprobanteDeliveryDto()
                {

                    Id = comprobante.Id,
                    Total = comprobante.Total,
                    ClienteId = comprobante.ClienteId,
                    ClienteStr = string.Concat(comprobante.Cliente.Apellido, " ", comprobante.Cliente.Nombre),
                    CadeteId = comprobante.CadeteId,
                    Deliverie = string.Concat(comprobante.Cadete.Apellido, " ", comprobante.Cadete.Nombre),
                    DelievirieLegajo = comprobante.Cadete.Legajo.ToString(),
                    Descuento = comprobante.Descuento,
                    Subtotal = comprobante.SubTotal,
                    EstadoDelivery = comprobante.EstadoDelivery,
                    Observacion = comprobante.Observacion
                };

                if (comprobante.DetallesDeliveries != null
                    && comprobante.DetallesDeliveries.Any(a => a.Cantidad > 0))
                {
                    foreach (var detalle in comprobante.DetallesDeliveries.Where(x => x.Cantidad > 0))
                    {
                        comprobanteDto.ComprobanteDeliveryDetalletos.Add(new ComprobanteDeliveryDetalleDto
                        {
                            Id = detalle.Id,
                            Descripcion = detalle.Descripcion,
                            SubTotal = detalle.SubTotal,
                            Codigo = detalle.Codigo,
                            Cantidad = detalle.Cantidad,
                            CodigoBarra = detalle.CodigoBarra,
                            ComprobanteDeliveryId = detalle.DeliveryId,
                            Precio = detalle.Precio,
                            ProductoId = detalle.ProductoId
                            
                        });
                    }
                }

                return comprobanteDto;
            }
        }

        public ComprobanteDeliveryDto ObtenerComprobantePorId(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include("DetallesDeliveries")
                    .Include("Cadete")
                    .FirstOrDefault(x => x.Id == id);

                if (comprobante == null) throw new ArgumentNullException("Error Grave");

                var comprobanteDto = new ComprobanteDeliveryDto()
                {
                    // se debera validar que haya un cadete si o si

                    Id = comprobante.Id,
                    Total = comprobante.Total,
                    ClienteId = comprobante.ClienteId,
                    ClienteStr = string.Concat(comprobante.Cliente.Apellido, " ", comprobante.Cliente.Nombre),
                    CadeteId = comprobante.CadeteId,
                    Deliverie = string.Concat(comprobante.Cadete.Apellido, " ", comprobante.Cadete.Nombre), 
                    DelievirieLegajo = comprobante.Cadete.Legajo.ToString(),
                    Descuento = comprobante.Descuento,
                    Subtotal = comprobante.SubTotal,
                    EstadoDelivery = comprobante.EstadoDelivery,
                    Observacion = comprobante.Observacion
                };

                if (comprobante.DetallesDeliveries != null
                    && comprobante.DetallesDeliveries.Any())
                {
                    foreach (var detalle in comprobante.DetallesDeliveries)
                    {
                        comprobanteDto.ComprobanteDeliveryDetalletos.Add(new ComprobanteDeliveryDetalleDto
                        {
                            Id = detalle.Id,
                            Descripcion = detalle.Descripcion,
                            SubTotal = detalle.SubTotal,
                            Codigo = detalle.Codigo,
                            Cantidad = detalle.Cantidad,
                            CodigoBarra = detalle.CodigoBarra,
                            ComprobanteDeliveryId = detalle.DeliveryId,
                            Precio = detalle.Precio,
                            ProductoId = detalle.ProductoId

                        });
                    }
                }

                return comprobanteDto;
            }
        }

        public bool ObtenerComprobantesSinFacturar()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                            .Any(x => x.EstadoDelivery == EstadoDelivery.EnProceso); 

                return Comprobante;
            }
        }

        public bool ObtenerComprobantesSinFacturarPorDelivery(long cadeteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                           .Any(x => (x.EstadoDelivery == EstadoDelivery.EnProceso && x.CadeteId == cadeteId));

                return Comprobante;
            }
        }

        public bool ObtenerComprobantesSinFacturarPorProdcuto(long productoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include("DetallesDeliveries")
                    .Any(x => x.EstadoDelivery == EstadoDelivery.EnProceso
                     && (x.DetallesDeliveries.Any(p => p.ProductoId == productoId)));

                return comprobante;

            }
        }

        public void obtenerDescuento(decimal Descuento, long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ComprobanteModificar = context.Comprobantes.OfType<DAL.Delivery>()
                    .Single(x => x.EstadoDelivery == EstadoDelivery.EnProceso
                            && x.ClienteId == clienteId);

                ComprobanteModificar.Descuento = Descuento;
                ComprobanteModificar.Total = (ComprobanteModificar.SubTotal - (ComprobanteModificar.SubTotal * ComprobanteModificar.Descuento / 100m));

                context.SaveChanges();

            }
        }

        public void ObtenerObservacion(string Observacion, long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ComprobanteModificar = context.Comprobantes.OfType<DAL.Delivery>()
                    .Single(x => (x.EstadoDelivery == EstadoDelivery.EnProceso)
                            && x.ClienteId == clienteId);

                ComprobanteModificar.Observacion = Observacion;

                context.SaveChanges();

            }
        }

        public IEnumerable<ComprobanteDeliveryDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var ComprobanteDelivery = context.Comprobantes.OfType<DAL.Delivery>()
                    .Include("DetallesDeliveries")
                    .Include("Cadete")
                    .AsNoTracking().Where(x => (x.Cliente.Apellido.Contains(cadenaBuscar)
                                               || x.Cliente.Nombre.Contains(cadenaBuscar)) 
                                               && (x.EstadoDelivery == EstadoDelivery.EnProceso))
                    .Select(x => new ComprobanteDeliveryDto()
                    {

                        Id = x.Id,
                        ClienteStr = string.Concat(string.Concat(x.Cliente.Apellido, " ", x.Cliente.Nombre)),
                        Deliverie = string.Concat(string.Concat(x.Cadete.Apellido, " ", x.Cadete.Nombre)),
                        DireccionEnvio = x.DireccionEnvio,
                        Fecha = x.Fecha,
                        Total = x.Total,
                        Descuento = x.Descuento,
                        EstadoDelivery = x.EstadoDelivery,
                        Observacion = x.Observacion,
                        ClienteId = x.ClienteId

                    }).ToList();

                return ComprobanteDelivery;

                     
            }
        }
    }
}
