using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Condicion_Iva
{
    public class CondicionIvaServicio : ICondicionIvaServicio
    {
        public void Eliminar(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ivaEliminar = context.CondicionIvas.Find(id);

                context.CondicionIvas.Remove(ivaEliminar);
                context.SaveChanges();
            }
        }

        public void Insertar(CondicionIvaDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ivaNuevo = new CondicionIva();
                ivaNuevo.Codigo = dto.Codigo;
                ivaNuevo.Descripcion = dto.Descripcion;

                context.CondicionIvas.Add(ivaNuevo);
                context.SaveChanges();
            }
        }

        public void Modificar(CondicionIvaDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ivaModificar = context.CondicionIvas.Single(x => x.Id == dto.Id);

                //ivaModificar.Codigo = dto.Codigo;
                ivaModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<CondicionIvaDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var codigo = 1;
                int.TryParse(cadenaBuscar, out codigo);

                var ivaBuscar = context.CondicionIvas
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar)
                                || x.Codigo == codigo).ToList();

                return ivaBuscar.Select(x => new CondicionIvaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                }).ToList();
            }
        }

        public CondicionIvaDto ObtenerPorId(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ivaId = context.CondicionIvas.Find(id);

                return new CondicionIvaDto()
                {
                    Id = ivaId.Id,
                    Codigo = ivaId.Codigo,
                    Descripcion = ivaId.Descripcion
                };
            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.CondicionIvas.Any() ? context.CondicionIvas.Max(x => x.Codigo) + 1 : 1;
            }
        }

        public IEnumerable<CondicionIvaDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ivaTodo = context.CondicionIvas.AsNoTracking().ToList();

                return ivaTodo.Select(x => new CondicionIvaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                }).ToList();
            }
        }

        public bool VerificarSiExiste(long? id, int codigo, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.CondicionIvas.Any(x => x.Id != id && (x.Codigo == codigo
                                                                     || x.Descripcion == descripcion));
            }
        }
    }
}
