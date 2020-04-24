using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Servicio.Core.Marca.DTO;
using System.Data.Entity;

namespace Servicio.Core.Marca
{
    public class MarcaServicio : IMarcaServicio
    {
        public void Eliminar(long Id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var MarcaEliminar = context.Marcas.Single(x => x.Id == Id);

                MarcaEliminar.EstaEliminado = true;

                context.SaveChanges();

            }
        }

        public void Insertar(MarcaDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var MarcaNueva = new DAL.Marca();

                MarcaNueva.Codigo = dto.Codigo;
                MarcaNueva.Descripcion = dto.Descripcion;

                context.Marcas.Add(MarcaNueva);

                context.SaveChanges();

            }
        }

        public void Modificar(MarcaDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var MarcaModificar = context.Marcas.Single(x => x.Id == dto.id);

                MarcaModificar.Codigo = dto.Codigo;
                MarcaModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();


            }
        }

        public IEnumerable<MarcaDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var codigo = 1;

                int.TryParse(cadenaBuscar, out codigo);

                var marca = context.Marcas.AsNoTracking().
                    Where(x => (x.Codigo == codigo
                                || x.Descripcion.Contains(cadenaBuscar))
                               && x.EstaEliminado == false).Select(x => new MarcaDto
                               {
                                   id = x.Id,
                                   Descripcion = x.Descripcion,
                                   Codigo = x.Codigo,
                                   EstaEliminado = x.EstaEliminado

                               }).ToList();

                return marca;
            }
        }

        public MarcaDto ObtenerPorid(long MarcaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Marca = context.Marcas.FirstOrDefault(x => x.Id == MarcaId);

                return new MarcaDto
                {
                    id = MarcaId,
                    Codigo = Marca.Codigo,
                    Descripcion = Marca.Descripcion
                };
            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Marcas.Any() ? context.Marcas.Max(x => x.Codigo) + 1 : 1;
            }
        }

        public IEnumerable<MarcaDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Marca = context.Marcas.AsNoTracking().Where(x => x.EstaEliminado == false).ToList();

                return Marca.Select(x => new MarcaDto
                {
                    id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                }).ToList();
            }
        }

        public bool VerificarSiExiste(long? MarcaId, int codigo, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Marcas
                    .Any(x => x.Id != MarcaId && (x.Codigo == codigo || x.Descripcion == descripcion));
            }
        }
    }
}
