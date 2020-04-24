using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Servicio.Core.Rubro.DTO;

namespace Servicio.Core.Rubro
{
    public class RubroServicio : IRubroServicio
    {
        public void Eliminar(long RubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var RubroEliminar = context.Rubros.Single(x => x.Id == RubroId);

                RubroEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public void Insertar(RubroDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var RubroNuevo = new DAL.Rubro();

                RubroNuevo.Id = dto.Id;
                RubroNuevo.Codigo = dto.Codigo;
                RubroNuevo.Descripcion = dto.Descripcion;

                context.Rubros.Add(RubroNuevo);

                context.SaveChanges();

            }
        }

        public void Modificar(RubroDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var RubroModificar = context.Rubros.Single(x => x.Id == dto.Id);

                RubroModificar.Codigo = dto.Codigo;
                RubroModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();


            }
        }

        public IEnumerable<RubroDto> ObtenerPorFiltro(string CadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var codigo = 1;

                int.TryParse(CadenaBuscar, out codigo);

                var rubro = context.Rubros.AsNoTracking().Where
                    (x => (x.Codigo == codigo
                                || x.Descripcion.Contains(CadenaBuscar))
                               && x.EstaEliminado == false).Select(x => new RubroDto
                               {
                                   Id = x.Id,
                                   Descripcion = x.Descripcion,
                                   Codigo = x.Codigo,
                                   EstaEliminado = x.EstaEliminado

                               }).ToList();

                return rubro;
            }
        }

        public RubroDto ObtenerPorId(long RubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Rubro = context.Rubros.FirstOrDefault(x => x.Id == RubroId);

                return new RubroDto
                {
                    Id = RubroId,
                    Codigo = Rubro.Codigo,
                    Descripcion = Rubro.Descripcion
                };
            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
               return context.Rubros
                    .Any() ? context.Rubros.Max(x => x.Codigo) + 1 : 1;

            }
        }

        public long ObtenerSiguienteId()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Rubros
                    .Any()
                    ? context.Rubros.Max(x => x.Id) + 1
                    : 1;
            }
        }

        public IEnumerable<RubroDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Rubro = context.Rubros.AsNoTracking().Where(x => x.EstaEliminado == false).ToList();

                return Rubro.Select(x => new RubroDto
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion

                }).ToList();
            }
        }

        public bool VerificarSiExiste(long? RubroId, int codigo, string Descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Rubros
                    .Any(x => x.Id != RubroId && (x.Codigo == codigo || x.Descripcion == Descripcion));
            }
        }
    }
}
