using DAL;
using Servicio.Core.Mesa;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Servicio.Core.SalonMesa
{
    public class SaloMesaServicio : ISaloMesaServicio
    {
        public IEnumerable<MesaDto> ObtenerMesasParaSalon()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Mesas.AsNoTracking()
                    .Include("Salones")
                    .Include("Salones.DetallesSalones")
                    .Select(x => new MesaDto()
                     {
                         Id = x.Id,
                         Numero = x.Numero,
                         Descripcion = x.Descripcion,
                         EstadoMesa = x.EstadoMesa,
                         ComprobanteId = x.Salones.Any(s => s.EstadoSalon == EstadoSalon.Pendiente)
                           ? x.Salones
                               .FirstOrDefault(s => s.EstadoSalon == EstadoSalon.Pendiente).Id
                               : -1
                    }).ToList();
            }
        }

        public static decimal ObtenerTotalVenta(long mesaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var comprobante = context.Comprobantes.OfType<Salon>()
                    .Include("DetallesSalones")
                    .FirstOrDefault(x => x.MesaId == mesaId 
                    && x.EstadoSalon == EstadoSalon.Pendiente);

                if (comprobante != null)
                {
                    if (comprobante.DetallesSalones == null) return 0m;

                   return comprobante.DetallesSalones.Any() ? comprobante.Total :  0m;
                }

                var comprobanteId = context.Comprobantes.OfType<Salon>()
               .Include("DetallesSalones")
               .Where(x => x.MesaId == mesaId
               && x.EstadoSalon == EstadoSalon.Facturado).Max(f => f.Id);

                var comprobanteFacturado = context.Comprobantes.OfType<Salon>()
               .Include("DetallesSalones")
               .FirstOrDefault(x => x.MesaId == mesaId
               && x.EstadoSalon == EstadoSalon.Facturado && x.Id == comprobanteId);

                if (comprobanteFacturado != null)
                {
                    return comprobanteFacturado.Total;
                }

                return 0m;
                
            }
        }
    }
}
