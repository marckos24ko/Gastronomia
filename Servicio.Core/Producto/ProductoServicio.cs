using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Servicio.Core.Producto
{
    public class ProductoServicio : IProductoServicio
    {
        public void Eliminar(int Id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ProdcutoEliminar = context.Productos.FirstOrDefault(x => x.Id == Id);

                ProdcutoEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public void Insertar(ProductoDto Dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ProductoNuevo = new DAL.Producto();
                {
                    ProductoNuevo.Codigo = Dto.Codigo;
                    ProductoNuevo.CodigoBarra = Dto.CodigoBarra;
                    ProductoNuevo.Descripcion = Dto.Descripcion;
                    ProductoNuevo.Stock = 0;
                    ProductoNuevo.EstaEliminado = false;
                    ProductoNuevo.MarcaId = Dto.MarcaId;
                    ProductoNuevo.SubRubroId = Dto.SubRubroId;                  


                    context.Productos.Add(ProductoNuevo);

                    context.SaveChanges();

                }
            }
        }

        public void Modificar(ProductoDto Dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ProductoModificar = context.Productos.Single(x => x.Id == Dto.Id);

                ProductoModificar.CodigoBarra = Dto.CodigoBarra;
                ProductoModificar.Descripcion = Dto.Descripcion;
                ProductoModificar.MarcaId = Dto.MarcaId;
                ProductoModificar.SubRubroId = Dto.SubRubroId;
               

                context.SaveChanges();

            }
        }

        public ProductoDto ObenerPorId(int Id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Producto = context.Productos.FirstOrDefault(x => x.Id == Id); 

                return new ProductoDto()
                {
                    Id = Producto.Id,
                    Codigo = Producto.Codigo,
                    CodigoBarra = Producto.CodigoBarra,
                    Descripcion = Producto.Descripcion,
                    MarcaStr = Producto.Marca.Descripcion,
                    SubRubroStr = Producto.SubRubro.Descripcion,
                    RubroStr = Producto.SubRubro.Rubro.Descripcion,
                    MarcaId = Producto.MarcaId,
                    SubRubroId = Producto.SubRubroId,
                    Stock = Producto.Stock
                   
                };
            }
        }

        public IEnumerable<ProductoDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var Producto = context.Productos.AsNoTracking().
                    Where(x => (x.Codigo.ToString().Contains(cadenaBuscar)
                                || x.CodigoBarra.Contains(cadenaBuscar)
                                || x.Descripcion.Contains(cadenaBuscar))
                               && x.EstaEliminado == false).Select(x => new ProductoDto()
                               {
                                   Id = x.Id,
                                   Codigo = x.Codigo,
                                   CodigoBarra = x.CodigoBarra,
                                   Descripcion = x.Descripcion,
                                   Stock = x.Stock,
                                   MarcaStr = x.Marca.Descripcion,
                                   SubRubroStr = x.SubRubro.Descripcion,
                                   RubroStr = x.SubRubro.Rubro.Descripcion,
                                   MarcaId = x.MarcaId,
                                   SubRubroId = x.SubRubroId,
                                                            
                                                    
                                  }).ToList();

                return Producto;
            }
        }

        public bool VerificarSiExiste(int? id, int codigo, string codigoBarra, string descripcion)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Productos.AsNoTracking()
                    .Any(x => x.Id != id && (x.Codigo == codigo || x.CodigoBarra == codigoBarra || x.Descripcion == descripcion));
            }
        }

        public ProductoDto Obtener(long listaPrecioId, string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var producto = context.Productos
                    .AsNoTracking()
                    .Include("ListasPreciosProductos")
                    .FirstOrDefault(x => x.CodigoBarra == cadenaBuscar || x.Descripcion == cadenaBuscar);

                if (producto == null ) return null;
                          
                
                return new ProductoDto
                {
                    Id = producto.Id,
                    Codigo = producto.Codigo,
                    CodigoBarra = producto.CodigoBarra,
                    Descripcion = producto.Descripcion,
                    Stock = producto.Stock,
                    ListaPrecioId = listaPrecioId,
                    PrecioPublico = producto.ListasPreciosProductos
                        .FirstOrDefault(l => l.ListaPrecioId == listaPrecioId
                                             && l.FechaActualizacion ==
                                             context.ListaPrecioProductos.Where(p => p.ListaPrecioId == listaPrecioId
                                                                                     && p.ProductoId == producto.Id)
                                                 .Max(f => f.FechaActualizacion)).PrecioPublico,

                    FechaActualizacion = producto.ListasPreciosProductos
                        .FirstOrDefault(l => l.ListaPrecioId == listaPrecioId
                                             && l.FechaActualizacion ==
                                             context.ListaPrecioProductos.Where(p => p.ListaPrecioId == listaPrecioId
                                                                                     && p.ProductoId == producto.Id)
                                                 .Max(f => f.FechaActualizacion)).FechaActualizacion
                };
            }
        }

        public IEnumerable<ProductoDto> ObtenerPorListaDePrecio(string cadenaBuscar, long listaPrecioId)
        {
            var context = new ModeloGastronomiaContainer();

            return context.Productos
                .Include("ListasPreciosProductos")
                .Where(x => (x.Descripcion == cadenaBuscar
                             || x.CodigoBarra == cadenaBuscar)
                            && x.ListasPreciosProductos.Any(l => l.ListaPrecioId == listaPrecioId))

                .Select(x => new ProductoDto
                {

                    Id = x.Id,
                    Codigo =x.Codigo,
                    CodigoBarra = x.CodigoBarra,
                    Descripcion = x.Descripcion,
                    Stock = x.Stock,
                    ListaPrecioId = listaPrecioId,

                    PrecioPublico = x.ListasPreciosProductos
                    .FirstOrDefault(l => l.ListaPrecioId == listaPrecioId
                    && l.FechaActualizacion ==
                    context.ListaPrecioProductos.Where(p => p.ListaPrecioId == listaPrecioId
                    && p.ProductoId == x.Id)
                    .Max(f => f.FechaActualizacion)).PrecioPublico,

                    FechaActualizacion = x.ListasPreciosProductos
                    .FirstOrDefault(l => l.ListaPrecioId == listaPrecioId
                    && l.FechaActualizacion ==
                    context.ListaPrecioProductos.Where(p => p.ListaPrecioId == listaPrecioId
                    && p.ProductoId == x.Id)
                    .Max(f => f.FechaActualizacion)).FechaActualizacion


                }).ToList();

        }

        public IEnumerable<ProductoDto> ObtenerPorListaDePrecioParaLookUp(long listaPrecioId, string cadena)
        {
            var context = new ModeloGastronomiaContainer();

            return context.Productos
                .Include("ListasPreciosProductos")
                .Where(x => x.ListasPreciosProductos.Any(l => l.ListaPrecioId == listaPrecioId) && (x.CodigoBarra == cadena || x.Descripcion.Contains(cadena)))

                .Select(x => new ProductoDto
                {

                    Id = x.Id,
                    Codigo = x.Codigo,
                    CodigoBarra = x.CodigoBarra,
                    Descripcion = x.Descripcion,
                    Stock = x.Stock,
                    ListaPrecioId = listaPrecioId,

                    PrecioPublico = x.ListasPreciosProductos
                    .FirstOrDefault(l => l.ListaPrecioId == listaPrecioId
                    && l.FechaActualizacion ==
                    context.ListaPrecioProductos.Where(p => p.ListaPrecioId == listaPrecioId
                    && p.ProductoId == x.Id)
                    .Max(f => f.FechaActualizacion)).PrecioPublico,

                    FechaActualizacion = x.ListasPreciosProductos
                    .FirstOrDefault(l => l.ListaPrecioId == listaPrecioId
                    && l.FechaActualizacion ==
                    context.ListaPrecioProductos.Where(p => p.ListaPrecioId == listaPrecioId
                    && p.ProductoId == x.Id)
                    .Max(f => f.FechaActualizacion)).FechaActualizacion


                }).ToList();

        }

        public bool Stock(ProductoDto Dto, int cantidad)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ModificarStock = context.Productos.Single(x => x.Id == Dto.Id);

                if (Dto.Stock >= cantidad)
                {
                    ModificarStock.Stock = Dto.Stock - cantidad;

                    context.SaveChanges();

                    return true;
                }
                          
                else
                {
                    return false;
                }

            }
        }

        public void AumentarStock(ProductoDto Dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ModificarStock = context.Productos.Single(x => x.Id == Dto.Id);

                ModificarStock.Stock = Dto.Stock + 1;

                context.SaveChanges();

            }
        }

        public void PedidoProducto(int id, int cantidad, long idProveedor)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var producto = context.Productos.Find(id);
                var proveedor = context.Personas.OfType<DAL.Proveedor>().Single(x => x.Id == idProveedor);

                producto.Stock += cantidad;

                producto.Proveedores.Add(proveedor);

                context.SaveChanges();
            }
        }

        public IEnumerable<ProductoDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var producto = context.Productos.AsNoTracking().ToList();

                return producto.Select(x => new ProductoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    CodigoBarra = x.CodigoBarra,
                    Descripcion = x.Descripcion,
                    Stock = x.Stock,
                    SubRubroId = x.SubRubroId,
                    MarcaId = x.MarcaId,
                    EstaEliminado = x.EstaEliminado

                }).ToList();
            }
        }
        
        public bool ObtenerPorSubRubro(long subRubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var producto = context.Productos
                    .Any(x => x.SubRubroId == subRubroId && x.EstaEliminado == false);

                return producto;

            }
        }

        public bool ObtenerPorMarca(long marcaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var producto = context.Productos
                    .Any(x => x.MarcaId == marcaId && x.EstaEliminado == false);

                return producto;

            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Productos
                    .Any()
                    ? context.Productos.Max(x => x.Codigo) + 1
                    : 1;
            }
        }

        public void AumentarStockPorCancelarVenta(ProductoDto Dto, int cantidad)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ModificarStock = context.Productos.Single(x => x.Id == Dto.Id);

                ModificarStock.Stock = Dto.Stock + cantidad;

                context.SaveChanges();

            }
        }
    }
}
