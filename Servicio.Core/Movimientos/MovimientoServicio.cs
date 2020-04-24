using DAL;
using Servicio.Core.Movimientos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicio.Core.Movimientos
{
    public class MovimientoServicio : IMovimientoServicio
    {
        public void EmitirMovimiento(long? clienteId, decimal monto, TipoMovimiento tipoMovimiento, long? facturaId, long? proveedorId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var MovimientoNuevo = new Movimiento
                {
                    ClienteId = clienteId,
                    Numero = ObtenerSiguienteNumero(),
                    Fecha = DateTime.Now,
                    Tipo = tipoMovimiento,
                    Monto = monto,
                    FacturaId = facturaId,
                    ProveedorId = proveedorId
                };

                context.Movimientos.Add(MovimientoNuevo);

                context.SaveChanges();

            }
        }

        public IEnumerable<MovimientoDto> ObtenerMovimientoPorFacturaId(long? facturaId, string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = -1;
                int.TryParse(cadenaBuscar, out numero);

                return
                    context.Movimientos.Where(x =>(x.FacturaId == facturaId)
                                                  &&(x.Cliente.Apellido.Contains(cadenaBuscar)
                                                  || x.Cliente.Nombre.Contains(cadenaBuscar)
                                                  || x.Numero == numero))
                            .Select(x => new MovimientoDto
                            {
                                Numero = x.Numero,
                                ClienteApyNom = string.Concat(x.Cliente.Apellido, " ", x.Cliente.Nombre),
                                Monto = x.Monto,
                                TipoMovimiento = x.Tipo,
                                Fecha = x.Fecha

                            }).ToList();
            }
        }

        public IEnumerable<MovimientoDto> ObtenerMovimientoPorProveedorId(long? proveedorId, string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = -1;
                int.TryParse(cadenaBuscar, out numero);

                return
                    context.Movimientos.Where(x => (x.Numero == numero
                                                 || x.Proveedor.NombreFantacia.Contains(cadenaBuscar))
                                                 &&(x.ProveedorId == proveedorId))
                            .Select(x => new MovimientoDto
                            {
                                Numero = x.Numero,
                                ProveedorNombre = x.Proveedor.NombreFantacia,
                                Monto = x.Monto,
                                TipoMovimiento = x.Tipo,
                                Fecha = x.Fecha

                            }).ToList();
            }
        }

        public int ObtenerSiguienteNumero()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Movimientos
                    .Any()
                    ? context.Movimientos.Max(x => x.Numero) + 1
                    : 1;
            }
        }
    }
}
