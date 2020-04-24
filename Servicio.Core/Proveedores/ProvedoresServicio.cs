using DAL;
using Servicio.Core.Producto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Proveedores
{
    public class ProvedoresServicio : IProveedorServicio
    {
        public void Eliminar(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedorEliminar = context.Personas.OfType<DAL.Proveedor>().Single(x => x.Id == id);

                proveedorEliminar.EstaEliminado = true;
                context.SaveChanges();
            }
        }

        public void Insertar(ProveedorDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedorNuevo = new DAL.Proveedor();

                proveedorNuevo.Direccion = dto.Direccion;
                proveedorNuevo.Teléfono = dto.Telefono;
                proveedorNuevo.EstaEliminado = false;
                proveedorNuevo.Cuit = dto.Cuit;
                proveedorNuevo.RazonSocial = dto.RazonSocial;
                proveedorNuevo.NombreFantacia = dto.NombreFantasia;
                proveedorNuevo.FechaInicioActividad = dto.FechaInicioActividad;
                proveedorNuevo.IngresosBrutos = dto.IngresosBrutos;
                proveedorNuevo.ApyNomContacto = dto.ApyNomContacto;
                proveedorNuevo.CondicionIvaId = dto.CondicionIvaId;
                proveedorNuevo.RubroId = dto.RubroId;


                context.Personas.Add(proveedorNuevo);
                context.SaveChanges();
            }
        }

        public void Modificar(ProveedorDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedorModificar = context.Personas.OfType<DAL.Proveedor>().Single(x => x.Id == dto.Id);

                proveedorModificar.Direccion = dto.Direccion;
                proveedorModificar.Teléfono = dto.Telefono;
                proveedorModificar.Cuit = dto.Cuit;
                proveedorModificar.RazonSocial = dto.RazonSocial;
                proveedorModificar.NombreFantacia = dto.NombreFantasia;
                proveedorModificar.FechaInicioActividad = dto.FechaInicioActividad;
                proveedorModificar.IngresosBrutos = dto.IngresosBrutos;
                proveedorModificar.ApyNomContacto = dto.ApyNomContacto;
                proveedorModificar.CondicionIvaId = dto.CondicionIvaId;
                proveedorModificar.RubroId = dto.RubroId;

                context.SaveChanges();
            }
        }

        public bool obtenerPorCondicionIva(long condicionIvaId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedor = context.Personas.OfType<Proveedor>()
                    .Any(x => x.CondicionIvaId == condicionIvaId && x.EstaEliminado == false);

                return proveedor;

            }
        }

        public IEnumerable<ProveedorDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var obtener = context.Personas.OfType<DAL.Proveedor>()
                    .AsNoTracking()
                    .Where(x => x.EstaEliminado == false && (x.ApyNomContacto.Contains(cadenaBuscar)
                                                             || x.NombreFantacia.Contains(cadenaBuscar)
                                                             || x.Cuit.Contains(cadenaBuscar)
                                                             || x.Direccion.Contains(cadenaBuscar)
                                                             || x.Rubro.Descripcion.Contains(cadenaBuscar))).ToList();

                return obtener.Select(x => new ProveedorDto()
                {
                    Id = x.Id,
                    ApyNomContacto = x.ApyNomContacto,
                    Cuit = x.Cuit,
                    Direccion = x.Direccion,
                    FechaInicioActividad = x.FechaInicioActividad,
                    IngresosBrutos = x.IngresosBrutos,
                    NombreFantasia = x.NombreFantacia,
                    RazonSocial = x.RazonSocial,
                    Telefono = x.Teléfono,
                    CondicionIvaStr = x.CondicionIva.Descripcion,
                    RubroStr = x.Rubro.Descripcion,
                    RubroId = x.RubroId
                }).ToList();
            }
        }

        public ProveedorDto ObtenerPorId(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedorId = context.Personas.OfType<DAL.Proveedor>()
                    .FirstOrDefault(x => x.Id == id);

                return new ProveedorDto()
                {
                    Id = proveedorId.Id,
                    ApyNomContacto = proveedorId.ApyNomContacto,
                    Cuit = proveedorId.Cuit,
                    Direccion = proveedorId.Direccion,
                    FechaInicioActividad = proveedorId.FechaInicioActividad,
                    IngresosBrutos = proveedorId.IngresosBrutos,
                    NombreFantasia = proveedorId.NombreFantacia,
                    RazonSocial = proveedorId.RazonSocial,
                    Telefono = proveedorId.Teléfono,
                    CondicionIvaId = proveedorId.CondicionIvaId,
                    CondicionIvaStr = proveedorId.CondicionIva.Descripcion,
                    RubroId = proveedorId.RubroId,
                    RubroStr = proveedorId.Rubro.Descripcion
                };

            }
        }

        public ProveedorDto ObtenerPorProducto(int idProducto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var producto = context.Productos
                    .First(x => x.Id == idProducto);

                var proveedorId = context.Personas.OfType<DAL.Proveedor>()
                    .First(x => x.RubroId == producto.SubRubro.RubroId);

                return new ProveedorDto()
                {
                    Id = proveedorId.Id,
                    ApyNomContacto = proveedorId.ApyNomContacto,
                    Cuit = proveedorId.Cuit,
                    Direccion = proveedorId.Direccion,
                    FechaInicioActividad = proveedorId.FechaInicioActividad,
                    IngresosBrutos = proveedorId.IngresosBrutos,
                    NombreFantasia = proveedorId.NombreFantacia,
                    RazonSocial = proveedorId.RazonSocial,
                    Telefono = proveedorId.Teléfono,
                    CondicionIvaId = proveedorId.CondicionIvaId,
                    CondicionIvaStr = proveedorId.CondicionIva.Descripcion,
                    RubroId = proveedorId.RubroId,
                    RubroStr = proveedorId.Rubro.Descripcion
                };

            }
        }

        public bool VerificarSiperteneceAlProducto(long idProducto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var producto = context.Productos
                    .First(x => x.Id == idProducto);

                var proveedor = context.Personas.OfType<DAL.Proveedor>()
                    .Any(x => x.RubroId == producto.SubRubro.RubroId);

                return proveedor;
            }
        }

        public bool obtenerPorRubro(long rubroId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedor = context.Personas.OfType<Proveedor>()
                    .Any(x => x.RubroId == rubroId && x.EstaEliminado == false);

                return proveedor;
            }
        }

        public int ObtenerSiguienteCodigo()
        {
            throw new NotImplementedException();
        }  //por el momento no se utiliza

        public IEnumerable<ProveedorDto> ObtenerTodo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var proveedorTodo = context.Personas.OfType<DAL.Proveedor>()
                    .AsNoTracking().ToList();

                return proveedorTodo.Select(x => new ProveedorDto()
                {
                    Id = x.Id,
                    ApyNomContacto = x.ApyNomContacto,
                    Cuit = x.Cuit,
                    Direccion = x.Direccion,
                    EstaEliminado = x.EstaEliminado,
                    FechaInicioActividad = x.FechaInicioActividad,
                    IngresosBrutos = x.IngresosBrutos,
                    NombreFantasia = x.NombreFantacia,
                    RazonSocial = x.RazonSocial,
                    Telefono = x.Teléfono,
                    CondicionIvaId = x.CondicionIvaId,
                    RubroId = x.RubroId,

                }).ToList();
            }
        }

        public bool VerificarSiExiste(long? id, string nombre, string cuit)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Proveedor>()
                    .Any(x => x.Id != id && (x.NombreFantacia == nombre || x.Cuit == cuit));
            }
        }

    }

}