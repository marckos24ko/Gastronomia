using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DAL;

namespace Servicio.Core.ListaPprecio
{
    public class ListaPrecioServicio : IListaPrecioServicio
    {
        public void Eliminar(long ListaPrecioId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioEliminar = context.ListaPrecios.FirstOrDefault(x => x.Id == ListaPrecioId);

                ListaPrecioEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public void Insertar(ListaPrecioDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioNueva = new DAL.ListaPrecio();

                ListaPrecioNueva.Codigo = dto.Codigo;
                ListaPrecioNueva.Descripcion = dto.Descripcion;
                ListaPrecioNueva.EstaEliminado = false;

                context.ListaPrecios.Add(ListaPrecioNueva);

                context.SaveChanges();
            }
        }

        public void Modificar(ListaPrecioDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioModificar = context.ListaPrecios.
                    Single(x => x.Id == dto.Id);

                ListaPrecioModificar.Descripcion = dto.Descripcion;
             
                context.SaveChanges();
            }
        }

        public IEnumerable<ListaPrecioDto> Obtener()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.ListaPrecios.Where(x=> x.EstaEliminado == false).
                    Select(x => new ListaPrecioDto
                    {
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
            }
        }

        public IEnumerable<ListaPrecioDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {                var codigo = 1;
                int.TryParse(cadenaBuscar, out codigo);
                var ListaPrecio = context.ListaPrecios.AsNoTracking().Where(x =>x.Codigo == codigo || x.Descripcion.Contains(cadenaBuscar) && x.EstaEliminado == false).
                    Select(x => new ListaPrecioDto
                    {
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();

                return ListaPrecio;
            }
        }

        public ListaPrecioDto ObtenerPorId(long? ListaPrecioId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecio = context.ListaPrecios
                    .FirstOrDefault(x => x.Id == ListaPrecioId);

                if (ListaPrecio == null) throw new ArgumentNullException("No existe la Lista");

                return new ListaPrecioDto()
                {
                    Id = ListaPrecio.Id,
                    Codigo = ListaPrecio.Codigo,
                    Descripcion = ListaPrecio.Descripcion,
                    EstaEliminado = ListaPrecio.EstaEliminado

                };
            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.ListaPrecios
                    .Any()
                    ? context.ListaPrecios.Max(x => x.Codigo) + 1
                    : 1;
            }
        }

        public bool VerificarSiExiste(long? ListaPrecioId, int? codigo, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.ListaPrecios
                    .Any(x => x.Id != ListaPrecioId && (x.Codigo == codigo || x.Descripcion == descripcion));
            }
        }
    }
}
