using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace Servicio.Core.Mesa
{
    public class MesaServicio : IMesaServicio
    {
        public void Insertar(int numero, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                if (string.IsNullOrEmpty(descripcion.TrimEnd())) throw new Exception("La descripcion es Obligatoria");

                context.Mesas.Add(new DAL.Mesa
                {
                    Numero = numero,
                    Descripcion = descripcion,
                    EstadoMesa = EstadoMesa.Libre
                });

                context.SaveChanges();
            }
        }

        public void Insertar(MesaDto dto)
        {
            Insertar(dto.Numero, dto.Descripcion);
        }

        public void Modificar(long id, int numero, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                if (string.IsNullOrEmpty(descripcion.TrimEnd())) throw new Exception("La descripcion es Obligatoria");

                var mesa = context.Mesas.Find(id);

                mesa.Numero = numero;
                mesa.Descripcion = descripcion;

                context.SaveChanges();
            }
        }

        public void Modificar(MesaDto dto)
        {
            Modificar(dto.Id, dto.Numero, dto.Descripcion);
        }

        public void Eliminar(long id) // Borrado Fisico
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var mesa = context.Mesas.Find(id);
                context.Mesas.Remove(mesa);
                context.SaveChanges();
            }
        }

        public MesaDto ObtenerPorId(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var mesa = context.Mesas.Find(id);
                return new MesaDto()
                {
                    Id = mesa.Id,
                    Numero = mesa.Numero,
                    Descripcion = mesa.Descripcion,
                    EstadoMesa = mesa.EstadoMesa
                };
            }
        }

        public IEnumerable<MesaDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = -1;
                int.TryParse(cadenaBuscar, out numero);

                var mesas = context.Mesas.AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar)
                                || x.Numero == numero)
                    .ToList();

                return mesas.Select(x => new MesaDto()
                {
                    Id = x.Id,
                    Numero = x.Numero,
                    Descripcion = x.Descripcion,
                    EstadoMesa = x.EstadoMesa
                }).ToList();
            }
        }

        public IEnumerable<MesaDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var mesas = context.Mesas.AsNoTracking()
                    .ToList();

                return mesas.Select(x => new MesaDto()
                {
                    Id = x.Id,
                    Numero = x.Numero,
                    Descripcion = x.Descripcion,
                    EstadoMesa = x.EstadoMesa
                }).ToList();
            }
        }

        public bool VerificarSiExiste(long? id, int numero, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Mesas.AsNoTracking()
                    .Any(x => x.Id != id && (x.Descripcion.Contains(descripcion)
                                             || x.Numero == numero));
            }
        }

        public int ObtenerSiguienteNumero()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Mesas.Any() ? context.Mesas.Max(x => x.Numero) + 1 : 1;
            }
        }

        public void CambiarEstado(long id, EstadoMesa estado)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var mesa = context.Mesas.Find(id);
                mesa.EstadoMesa = estado;
                context.SaveChanges();
            }
        }

        public bool VerificarSiEstaUsandose(long id, EstadoMesa estado)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Mesas.Any(x => x.Id == id && x.EstadoMesa == estado);
              
            }
        }

        public IEnumerable<MesaDto> ObtenerMesasLibres(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var numero = -1;
                int.TryParse(cadenaBuscar, out numero);

                var mesas = context.Mesas.AsNoTracking()
                    .Where(x => (x.Descripcion.Contains(cadenaBuscar)
                                || x.Numero == numero)
                                && x.EstadoMesa == EstadoMesa.Libre)
                    .ToList();

                return mesas.Select(x => new MesaDto()
                {
                    Id = x.Id,
                    Numero = x.Numero,
                    Descripcion = x.Descripcion,
                    EstadoMesa = x.EstadoMesa
                }).ToList();
            }
        }
    }
}
