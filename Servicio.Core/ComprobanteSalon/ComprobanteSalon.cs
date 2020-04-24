using DAL;
using Servicio.Core.Producto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Servicio.Core.ComprobanteSalon
{
    public class ComprobanteSalon : IComprobanteSalon
    {
        public void AgregarItem(long comprobanteId, int cantidad, ProductoDto producto, long empleadoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<Salon>()
                    .Include(x => x.DetallesSalones)
                    .FirstOrDefault(x => x.Id == comprobanteId);

                if (comprobante == null) throw new Exception("Error.");

                var item = comprobante.DetallesSalones
                    .FirstOrDefault(x => x.ProductoId == producto.Id && x.Precio == producto.PrecioPublico);

                if (comprobante.MozoId == null) comprobante.MozoId = empleadoId;

                if (item != null)
                {
                    // caso en el que el producto existe en el detalle

                    if (item.Precio == producto.PrecioPublico)

                    {
                        item.Cantidad += cantidad;
                        item.SubTotal = item.Cantidad * producto.PrecioPublico;

                        comprobante.Subtotal = comprobante.DetallesSalones.Sum(x => x.SubTotal);
                    }
                    else
                    {
                        comprobante.DetallesSalones.Add(new DetalleSalon

                        {

                            Cantidad = cantidad,
                            Codigo = Convert.ToString(producto.Codigo),
                            CodigoBarra = producto.CodigoBarra,
                            Descripcion = producto.Descripcion,
                            Precio = producto.PrecioPublico,
                            ProductoId = producto.Id,
                            SubTotal = producto.PrecioPublico * cantidad,

                        });

                        comprobante.Subtotal = comprobante.DetallesSalones.Sum(x => x.SubTotal);

                    }
                }

                else
                {
                    // en caso de que no exista el producto en el detalle
                    comprobante.DetallesSalones.Add(new DetalleSalon

                    {

                        Cantidad = cantidad,
                        Codigo = Convert.ToString(producto.Codigo),
                        CodigoBarra = producto.CodigoBarra,
                        Descripcion = producto.Descripcion,
                        Precio = producto.PrecioPublico,
                        ProductoId = producto.Id,
                        SubTotal = producto.PrecioPublico * cantidad



                    });

                    comprobante.Subtotal = comprobante.DetallesSalones.Sum(x => x.SubTotal);
                }


                context.SaveChanges();

            }
        }

        public void Cerrar(long Id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Salon>()
                    .Include(x => x.DetallesSalones)
                    .FirstOrDefault(x => x.Id == Id && x.EstadoSalon == EstadoSalon.Pendiente);

                var subTotal = comprobante.DetallesSalones.Sum(x => x.SubTotal);

                comprobante.Total = subTotal - ((subTotal * comprobante.Descuento) / 100m);
                comprobante.Subtotal = subTotal;
                comprobante.Descuento = comprobante.Descuento;
                comprobante.EstadoSalon = EstadoSalon.Facturado;
                comprobante.Fecha = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void Crear(long mesaId, long empleadoId, long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var nuevoComprobante = new Salon
                {
                    Fecha = DateTime.Now,
                    EstadoSalon = EstadoSalon.Pendiente,
                    MesaId = mesaId,
                    Descuento = 0m,
                    Subtotal = 0m,
                    Total = 0m,
                    Cubiertos = 1,
                    MozoId = empleadoId,
                    ClienteId = clienteId
                };

                context.Comprobantes.Add(nuevoComprobante);

                context.SaveChanges();


            }
        }

        public void DisminuirItem(long comprobanteId, int cantidad, ComprobanteSalonDetalleDto producto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<Salon>()
                    .Include(x => x.DetallesSalones)
                    .FirstOrDefault(x => x.Id == comprobanteId);

                if (comprobante == null) throw new Exception("Error.");

                var item = comprobante.DetallesSalones
                    .FirstOrDefault(x=> x.ProductoId == producto.ProductoId && x.CodigoBarra == producto.CodigoBarra);

                if (item != null)
                {

                    if (item.Precio == producto.Precio)

                    {
                        item.Cantidad -= cantidad;
                        item.SubTotal = item.Cantidad * producto.Precio;
                        comprobante.Subtotal = comprobante.DetallesSalones.Sum(x => x.SubTotal);

                    }
                }

                context.SaveChanges();
            }
        }

        public void modificar(long mesaOcupadaId, long mesaLibreId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ComprobanteModificar = context.Comprobantes.OfType<DAL.Salon>()
                    .Single(x => x.EstadoSalon == EstadoSalon.Pendiente
                            && x.MesaId == mesaOcupadaId);

                ComprobanteModificar.MesaId = mesaLibreId;

                context.SaveChanges();

            }
        }

        public ComprobanteSalonDto ObtenerComprobantePorMesaSinFacturar(long mesaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<Salon>()
                    .Include("DetallesSalones")
                    .Include("Mozo")
                    .FirstOrDefault(x => x.MesaId == mesaId
                                         && x.EstadoSalon == EstadoSalon.Pendiente);

                if (comprobante == null) throw new ArgumentNullException("Error Grave");

                var comprobanteDto = new ComprobanteSalonDto
                {
                    Id = comprobante.Id,
                    Total = comprobante.Total,
                    MesaId = comprobante.MesaId,
                    MozoId = comprobante.MozoId,
                    MozoStr = string.Concat(string.Concat(comprobante.Mozo.Apellido, " ", comprobante.Mozo.Nombre)),
                    MozoLegajo = comprobante.Mozo.Legajo.ToString(),
                    ClienteId = comprobante.ClienteId,
                    Cliente = string.Concat(string.Concat(comprobante.Cliente.Apellido, " ", comprobante.Cliente.Nombre)),
                    Descuento = comprobante.Descuento,
                    SubTotal = comprobante.Subtotal,
                    Estado = comprobante.EstadoSalon

                };

                if (comprobante.DetallesSalones != null
                    && comprobante.DetallesSalones.Any(a=>a.Cantidad > 0))
                {
                    foreach (var detalle in comprobante.DetallesSalones.Where(x => x.Cantidad > 0))
                    {
                        comprobanteDto.ComprobanteSalonDetalleDtos.Add(new ComprobanteSalonDetalleDto
                        {
                            Id = detalle.Id,
                            Descripcion = detalle.Descripcion,
                            SubTotal = detalle.SubTotal,
                            Codigo = detalle.Codigo,
                            Cantidad = detalle.Cantidad,
                            CodigoBarra = detalle.CodigoBarra,
                            ComprobanteSalonId = detalle.SalonId,
                            Precio = detalle.Precio,
                            ProductoId = detalle.ProductoId,
                            
                        });
                    }
                }

                return comprobanteDto;
            }
        }

        public void obtenerDescuento(decimal descuento, long mesaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ComprobanteModificar = context.Comprobantes.OfType<DAL.Salon>()
                    .Single(x => x.EstadoSalon == EstadoSalon.Pendiente
                            && x.MesaId == mesaId);

                ComprobanteModificar.Descuento = descuento;
                ComprobanteModificar.Total = (ComprobanteModificar.Subtotal - (ComprobanteModificar.Subtotal * ComprobanteModificar.Descuento / 100m));

                context.SaveChanges();

            }
        }

        public ComprobanteSalonDto ObtenerComprobantePorId(long Id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<Salon>()
                    .Include("DetallesSalones")
                    .Include("Mozo")
                    .FirstOrDefault(x => x.Id == Id);

                if (comprobante == null) throw new ArgumentNullException("Error Grave");

                var comprobanteDto = new ComprobanteSalonDto
                {
                    Id = comprobante.Id,
                    Total = comprobante.Total,
                    MesaId = comprobante.MesaId,
                    MozoId = comprobante.MozoId,
                    MozoStr = string.Concat(string.Concat(comprobante.Mozo.Apellido, " ", comprobante.Mozo.Nombre)),
                    MozoLegajo = comprobante.Mozo.Legajo.ToString(),
                    ClienteId = comprobante.ClienteId,
                    Cliente = string.Concat(string.Concat(comprobante.Cliente.Apellido, " ", comprobante.Cliente.Nombre)),
                    Descuento = comprobante.Descuento,
                    SubTotal = comprobante.Subtotal,
                    Estado = comprobante.EstadoSalon

                };

                if (comprobante.DetallesSalones != null
                    && comprobante.DetallesSalones.Any())
                {
                    foreach (var detalle in comprobante.DetallesSalones)
                    {
                        comprobanteDto.ComprobanteSalonDetalleDtos.Add(new ComprobanteSalonDetalleDto
                        {
                            Id = detalle.Id,
                            Descripcion = detalle.Descripcion,
                            SubTotal = detalle.SubTotal,
                            Codigo = detalle.Codigo,
                            Cantidad = detalle.Cantidad,
                            CodigoBarra = detalle.CodigoBarra,
                            ComprobanteSalonId = detalle.SalonId,
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
                var Comprobante = context.Comprobantes.OfType<DAL.Salon>()
                           .Any(x => (x.EstadoSalon == EstadoSalon.Pendiente));

                return Comprobante;
            }

        }

        public bool ObtenerComprobantesSinFacturarPorMozo(long mozoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Comprobante = context.Comprobantes.OfType<DAL.Salon>()
                           .Any(x => (x.EstadoSalon == EstadoSalon.Pendiente && x.MozoId == mozoId));

                return Comprobante;
            }

        }

        public bool ObtenerComprobantesSinFacturarPorProdcuto(long productoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<Salon>()
                    .Include("DetallesSalones")
                    .Any(x => x.EstadoSalon == EstadoSalon.Pendiente
                     && (x.DetallesSalones.Any(p => p.ProductoId == productoId)));

                return comprobante;

            }
        }

        public void Eliminar(long id, long mesaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<DAL.Salon>()
                    .Include(x => x.DetallesSalones)
                    .FirstOrDefault(x => x.Id == id && x.MesaId == mesaId && x.EstadoSalon == EstadoSalon.Pendiente);

                List<DetalleComprobante> lista = new List<DetalleComprobante>();

                foreach (var item in comprobante.DetallesSalones)
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
    }
}

