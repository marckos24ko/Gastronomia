using DAL;
using Servicio.Core.Reserva.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Servicio.Core.Reserva
{
    public class ReservaServicio : IComprobanteReservaServicio
    {
        public void Cancelar(long MesaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ReservaCancelar = context.Comprobantes.OfType<DAL.Reserva>().FirstOrDefault(x => x.MesaId == MesaId);

                context.Comprobantes.Remove(ReservaCancelar);

                context.SaveChanges();
            }
        }

        public void Cerrar(long ReservaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ReservaCerrar = context.Comprobantes.OfType<DAL.Reserva>().FirstOrDefault(x => x.Id == ReservaId);

                ReservaCerrar.EstadoReserva = EstadoReserva.Confirmado;

                context.SaveChanges();
            }
        }

        public void Crear(ReservaDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ReservaNueva = new DAL.Reserva

                {
                    Fecha = DateTime.Now,
                    MesaId = dto.MesaId,
                    ClienteId = dto.CLienteId,
                    EmpleadoId = dto.EmpleadoId,
                    CantidadComensales = dto.CantidadComensales,
                    EstaEliminado = false,
                    FechaReserva = dto.Fecha,
                    Observacion = dto.Observacion,
                    EstadoReserva = EstadoReserva.Reservado,
                    Codigo = dto.Codigo,

                };

                context.Comprobantes.Add(ReservaNueva);

                context.SaveChanges();
            }
        }

        public void Modificar(ReservaDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ReservaModificar = context.Comprobantes.OfType<DAL.Reserva>()
                    .Single(x => x.Id == dto.Id);

                ReservaModificar.CantidadComensales = dto.CantidadComensales;
                ReservaModificar.FechaReserva = dto.Fecha;
                ReservaModificar.Observacion = dto.Observacion;

                context.SaveChanges();
            }
        }

        public IEnumerable<ReservaDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var reservas = context.Comprobantes.OfType<DAL.Reserva>()
                    .AsNoTracking()
                    .Where(x => (x.Cliente.Apellido.Contains(cadenaBuscar)
                                || x.Cliente.Nombre.Contains(cadenaBuscar)
                                || x.Empleado.Apellido.Contains(cadenaBuscar)
                                || x.Empleado.Nombre.Contains(cadenaBuscar))
                                && x.EstaEliminado == false
                                && x.EstadoReserva == EstadoReserva.Reservado)
                    .Select(x => new ReservaDto()
                    {
                        CLienteStr = string.Concat(string.Concat(x.Cliente.Apellido, " ", x.Cliente.Nombre)),
                        EmpleadoStr = string.Concat(string.Concat(x.Empleado.Apellido, " ", x.Empleado.Nombre)),
                        CantidadComensales = x.CantidadComensales,
                        Estado = x.EstadoReserva,
                        Observacion = x.Observacion

                    }).ToList();

                return reservas;
            }
        }

        public ReservaDto obtenerPorId(long reservaId)
            {
            using (var context = new ModeloGastronomiaContainer())
            {
                var reserva = context.Comprobantes.OfType<DAL.Reserva>()
                    .FirstOrDefault(x => x.Id == reservaId
                                    && x.EstaEliminado == false);

                if (reserva == null) throw new ArgumentNullException("No existe la reserva");

                return new ReservaDto()
                {
                    CLienteId = reserva.ClienteId,
                    EmpleadoId = reserva.EmpleadoId,
                    MesaId = reserva.MesaId,
                    Fecha = reserva.FechaReserva,
                    CantidadComensales = reserva.CantidadComensales,
                    Observacion = reserva.Observacion,
                    Codigo = reserva.Codigo
                };
            }
        }

        public ReservaDto obtenerPorMesa(long mesaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var reserva = context.Comprobantes.OfType<DAL.Reserva>()
                    .FirstOrDefault(x => x.MesaId == mesaId
                                    && x.EstaEliminado == false);

                if (reserva == null) throw new ArgumentNullException("No existe la reserva");

                return new ReservaDto()
                {
                    CLienteId = reserva.ClienteId,
                    CLienteStr = string.Concat(string.Concat(reserva.Cliente.Apellido, " ", reserva.Cliente.Nombre)),
                    EmpleadoId = reserva.EmpleadoId,
                    MesaId = mesaId,
                    Fecha = reserva.FechaReserva,
                    CantidadComensales = reserva.CantidadComensales,
                    Observacion = reserva.Observacion,
                    Codigo = reserva.Codigo,
                    Id = reserva.Id

                };
            }
        }

        public int ObtenerSiguienteCodigo()
            {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Comprobantes.OfType<DAL.Reserva>()
                    .Any()
                    ? context.Comprobantes.OfType<DAL.Reserva>().Max(x => x.Codigo) + 1
                    : 1;
            }
        }

        public bool VerificarSiExiste(long ReservaId, int codigo)
            {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Comprobantes.OfType<DAL.Reserva>()
                    .Any(x => x.Id == ReservaId && x.Codigo == codigo);
            }
        }

    }
}
