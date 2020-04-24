using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Servicio.Core.SubRubro.DTO;

namespace Servicio.Core.SubRubro
{
    public class SubRubroServicio : ISubRuroServicio
    {
        public void Eliminar(long SubRubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var SubRubroEliminar = context.SubRubros.Single(x => x.Id == SubRubroId);

                SubRubroEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public void Insertar(SubRubroDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                
                if (string.IsNullOrEmpty(dto.Descripcion.TrimEnd())) throw new Exception(@"La descripción es Obligatoria");
                if (string.IsNullOrEmpty(Convert.ToString(dto.RubroId))) throw new Exception(@"El Rubro es Obligatorio");

                context.SubRubros.Add(new DAL.SubRubro
                {
                    Codigo = dto.Codigo,
                    Descripcion = dto.Descripcion,
                    RubroId = dto.RubroId,
                    EstaEliminado = dto.EstaEliminado

                });

                context.SaveChanges();

            }
        }

        public void Modificar(SubRubroDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var SubRubroModificar = context.SubRubros.Single(x => x.Id == dto.Id);

                SubRubroModificar.Codigo = dto.Codigo;
                SubRubroModificar.Descripcion = dto.Descripcion;
                SubRubroModificar.RubroId = dto.RubroId;

                context.SaveChanges();


            }
        }

        public IEnumerable<SubRubroDto> ObtenerPorFiltro(string CadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var codigo = 1;

                int.TryParse(CadenaBuscar, out codigo);

                var Subrubro = context.SubRubros.AsNoTracking().Where
                    (x => (x.Codigo == codigo
                                || x.Descripcion.Contains(CadenaBuscar))
                               && x.EstaEliminado == false).Select(x => new SubRubroDto
                               {
                                   Id = x.Id,
                                   Descripcion = x.Descripcion,
                                   Codigo = x.Codigo,
                                   EstaEliminado = x.EstaEliminado,
                                   RubroId = x.RubroId,
                                   RubroStr = x.Rubro.Descripcion


                               }).ToList();

                return Subrubro;
            }
        }

        public SubRubroDto ObtenerPorId(long SubRubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var SubRubro = context.SubRubros.FirstOrDefault(x => x.Id == SubRubroId);

                return new SubRubroDto
                {
                    Id = SubRubroId,
                    Codigo = SubRubro.Codigo,
                    Descripcion = SubRubro.Descripcion,
                    RubroId = SubRubro.RubroId,
                    RubroStr = SubRubro.Rubro.Descripcion

                };
            }
        }

        public IEnumerable<SubRubroDto> ObtenerPorRubro(long rubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var SubRubroId = context.SubRubros.Where(x => x.RubroId == rubroId).ToList();

                return SubRubroId.Select(x => new SubRubroDto
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,


                }).ToList();
            }
        }

        public long ObtenerSiguienteId()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.SubRubros
                    .Any()
                    ? context.SubRubros.Max(x => x.Id) + 1
                    : 1;
            }
        }

        public IEnumerable<SubRubroDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var SubRubroId = context.SubRubros.AsNoTracking().Where(x => x.EstaEliminado == false).ToList();

                return SubRubroId.Select(x => new SubRubroDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,

                }).ToList();
            }
        }

        public bool VerificarSiExiste(long? SubRubroId, int codigo, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.SubRubros
                    .Any(x => x.Id != SubRubroId && (x.Codigo == codigo || x.Descripcion == descripcion));
            }
        }

        public bool ObtenerPorRubroId(long RubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Subrubro = context.SubRubros
                    .Any(x => x.RubroId == RubroId && x.EstaEliminado == false);

                return Subrubro;

            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.SubRubros
                    .Any() ? context.SubRubros.Max(x => x.Codigo) + 1 : 1;

            }
        }
    }
}
