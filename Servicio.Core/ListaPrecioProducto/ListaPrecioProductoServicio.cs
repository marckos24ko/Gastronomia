using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Core.ListaPrecioProducto.DTO;
using DAL;

namespace Servicio.Core.ListaPrecioProducto
{
    public class ListaPrecioProductoServicio : IListaPrecioProductoServicio
    {
        public void Eliminar(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioProdcutoEliminar = context.ListaPrecioProductos.FirstOrDefault(x => x.Id == id);

                context.ListaPrecioProductos.Remove(ListaPrecioProdcutoEliminar);

                context.SaveChanges();
            }
        }

        public void Insertar(ListaPrecioProductoDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioProductoNuevo = new DAL.ListaPrecioProducto();
                {
                    ListaPrecioProductoNuevo.ListaPrecioId = dto.ListaPrecioId;
                    ListaPrecioProductoNuevo.PrecioCosto = dto.PrecioCosto;
                    ListaPrecioProductoNuevo.PrecioPublico = dto.PrecioPublico;
                    ListaPrecioProductoNuevo.ProductoId = (int)dto.ProductoId;
                    ListaPrecioProductoNuevo.EstaEliminado = false;
                    ListaPrecioProductoNuevo.FechaActualizacion = dto.FechaActualizacion;

                    context.ListaPrecioProductos.Add(ListaPrecioProductoNuevo);

                    context.SaveChanges();
                }
            }
        }

        public void Modificar(ListaPrecioProductoDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioProductoModificar = context.ListaPrecioProductos.Single(x => x.Id == dto.Id);
                {
                    ListaPrecioProductoModificar.PrecioCosto = dto.PrecioCosto;
                    ListaPrecioProductoModificar.PrecioPublico = dto.PrecioPublico;
                    ListaPrecioProductoModificar.FechaActualizacion = dto.FechaActualizacion;

                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<ListaPrecioProductoDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var ListaPrecioProducto = context.ListaPrecioProductos.AsNoTracking().
                    Where(x => (x.ListaPrecio.Descripcion.Contains(cadenaBuscar)
                                || x.Producto.Descripcion.Contains(cadenaBuscar)
                               && x.EstaEliminado == false)).Select(x => new ListaPrecioProductoDto()
                               {
                                   Id = x.Id,
                                   FechaActualizacion = x.FechaActualizacion,
                                   PrecioCosto = x.PrecioCosto,
                                   PrecioPublico = x.PrecioPublico,
                                   ListaPrecioStr = x.ListaPrecio.Descripcion,
                                   ProductoStr = x.Producto.Descripcion,
                                   ListaPrecioId = x.ListaPrecioId,
                                   ProductoId = x.ProductoId
                                   
                               }).ToList();

                return ListaPrecioProducto;
            }
        }

        public IEnumerable<ListaPrecioProductoDto>Obtener()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.ListaPrecioProductos.Where(x => x.EstaEliminado == false).
                    Select(x => new ListaPrecioProductoDto
                    {
                        Id = x.Id,
                        ListaPrecioStr=x.ListaPrecio.Descripcion,
                        ProductoStr=x.Producto.Descripcion,
                        PrecioCosto=x.PrecioCosto,
                        PrecioPublico=x.PrecioPublico,
                        FechaActualizacion=x.FechaActualizacion,

                    }).ToList();
            }
        }

        public ListaPrecioProductoDto ObtenerPorId(long? ListaPrecioProductoId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ListaPrecioProducto = context.ListaPrecioProductos.Single(x => x.Id == ListaPrecioProductoId);

                return new ListaPrecioProductoDto()
                {
                    Id = ListaPrecioProducto.Id,
                    ListaPrecioId = ListaPrecioProducto.ListaPrecioId,
                    ListaPrecioStr = ListaPrecioProducto.ListaPrecio.Descripcion,
                    ProductoStr = ListaPrecioProducto.Producto.Descripcion,
                    ProductoId = ListaPrecioProducto.ProductoId,
                    FechaActualizacion = ListaPrecioProducto.FechaActualizacion,
                    PrecioCosto = ListaPrecioProducto.PrecioCosto,
                    PrecioPublico = ListaPrecioProducto.PrecioPublico,
                                     

                };
            }
        }


        public bool VerificarSiExiste(long? id, string listaPrecio, string producto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.ListaPrecioProductos
                    .Any(x => x.Id != id && (x.ListaPrecio.Descripcion == listaPrecio && x.Producto.Descripcion == producto));
            }
        }

        public bool VerificarSiListaPrecioEstaUsandose(long? ListaPrecioId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.ListaPrecioProductos
                    .Any(x => x.ListaPrecio.Id == ListaPrecioId && x.EstaEliminado == false);
            }
        }
    }
}
