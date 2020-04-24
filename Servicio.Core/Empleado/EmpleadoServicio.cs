using DAL;
using Servicio.Core.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Servicio.Core.Empleado
{
    public class EmpleadoServicio : IEmpleadoServicio
    {
        public void Eliminar(long id)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var empleadoEliminar = context.Personas.OfType<DAL.Empleado>().FirstOrDefault(x => x.Id == id);

                empleadoEliminar.EstaEliminado = true;

                var cuenta = context.Usuarios.FirstOrDefault(x => x.Nombre == empleadoEliminar.Nombre + empleadoEliminar.Cuil);

                cuenta.EstaBloqueado = true;

                context.SaveChanges();
            }
        }

        public void Insertar(EmpleadoDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var empleadoNuevo = new DAL.Empleado
                {
                    Legajo = dto.Legajo,
                    Apellido = dto.Apellido,
                    Direccion = dto.Direccion,
                    Dni = dto.Dni,
                    Nombre = dto.Nombre,
                    Teléfono = dto.Telefono,
                    Cuil = dto.Cuil,
                    Celular = dto.Celular,
                    EstaEliminado = false,
                    TipoEmpleado = dto.TipoOcupacion
                };

                if (dto.TipoOcupacion == TipoEmpleado.Administrativo || dto.TipoOcupacion == TipoEmpleado.Cajero || dto.TipoOcupacion == TipoEmpleado.Mozo)
                {
                    empleadoNuevo.Usuario = context.Usuarios.Add(new DAL.Usuario()
                    {
                        Nombre = empleadoNuevo.Nombre + empleadoNuevo.Cuil,
                        EstaBloqueado = false,
                        Password = empleadoNuevo.Dni
                    });
                }

                else
                {
                    empleadoNuevo.Usuario = context.Usuarios.Add(new DAL.Usuario()
                    {
                        Nombre = "CadeteBloqueado" + dto.Cuil,
                        EstaBloqueado = true,
                        Password = "123"
                    });
                }

                context.Personas.Add(empleadoNuevo);

                context.SaveChanges();
            }
        }

        public void Modificar(EmpleadoDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var empleadoModificar = context.Personas.OfType<DAL.Empleado>()
                    .Single(x => x.Id == dto.Id);

                empleadoModificar.Legajo = dto.Legajo;
                empleadoModificar.Apellido = dto.Apellido;
                empleadoModificar.Direccion = dto.Direccion;
                empleadoModificar.Dni = dto.Dni;
                empleadoModificar.Nombre = dto.Nombre;
                empleadoModificar.Teléfono = dto.Telefono;
                empleadoModificar.Celular = dto.Celular;

                if (empleadoModificar.TipoEmpleado != TipoEmpleado.Cadete && dto.TipoOcupacion == TipoEmpleado.Cadete)
                {
                    var cuenta = context.Usuarios.FirstOrDefault(x => x.Id == empleadoModificar.Usuario.Id);

                    cuenta.EstaBloqueado = true;
                }

                else
                {
                    if (empleadoModificar.TipoEmpleado == TipoEmpleado.Cadete && dto.TipoOcupacion != TipoEmpleado.Cadete)
                    {
                        var cuenta = context.Usuarios.FirstOrDefault(x => x.Id == empleadoModificar.Usuario.Id);

                        if (cuenta.Nombre.Contains("CadeteBloqueado"))
                        {
                            cuenta.Nombre = dto.Nombre + dto.Cuil;
                            cuenta.Password = dto.Dni;
                            cuenta.EstaBloqueado = false;
                        }

                        else
                        {
                            cuenta.EstaBloqueado = false;
                        }

                        
                    }
                }

                empleadoModificar.Cuil = dto.Cuil;
                empleadoModificar.TipoEmpleado = dto.TipoOcupacion;

                context.SaveChanges();
            };
        }

        public IEnumerable<EmpleadoDto> obtenerCadetes(TipoEmpleado cadete)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return
                    context.Personas.OfType<DAL.Empleado>()
                        .Where(x => x.TipoEmpleado == cadete)
                        .Select(x => new EmpleadoDto
                        {
                            Id = x.Id,
                            Apellido = x.Apellido,
                            Nombre = x.Nombre,
                            Celular = x.Celular,
                            Cuil = x.Cuil,
                            Direccion = x.Direccion,
                            Dni = x.Dni,
                            Legajo = x.Legajo,
                            TipoOcupacion = x.TipoEmpleado,
                            Telefono = x.Teléfono

                        }).ToList();
            }
        }

        public EmpleadoDto obtenerCadetesPorFiltro(TipoEmpleado cadete, string cadenaBuscar) // no se usa por ahora
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var empleado = context.Personas.OfType<DAL.Empleado>()
                    .FirstOrDefault(x => x.TipoEmpleado == cadete
                                    && x.Legajo.ToString() == cadenaBuscar && x.EstaEliminado == false);

                if (empleado == null) return null;


                return new EmpleadoDto
                {
                    Id = empleado.Id,
                    Apellido = empleado.Apellido,
                    Dni = empleado.Dni,
                    Nombre = empleado.Nombre,
                    Legajo = empleado.Legajo,
                    Telefono = empleado.Teléfono,
                    Direccion = empleado.Direccion,
                    Celular = empleado.Celular,
                    Cuil = empleado.Cuil,
                    TipoOcupacion = empleado.TipoEmpleado,


                };
            }
        }

        public IEnumerable<EmpleadoDto> obtenerMozos(TipoEmpleado mozo)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return
                    context.Personas.OfType<DAL.Empleado>()
                        .Where(x => x.TipoEmpleado == mozo)
                        .Select(x => new EmpleadoDto
                        {
                            Id = x.Id,
                            Apellido = x.Apellido,
                            Nombre = x.Nombre,
                            Celular = x.Celular,
                            Cuil = x.Cuil,
                            Direccion = x.Direccion,
                            Dni = x.Dni,                   
                            Legajo = x.Legajo,
                            TipoOcupacion = x.TipoEmpleado,
                            Telefono = x.Teléfono
                            
                        }).ToList();
            }
        }

        public EmpleadoDto obtenerMozosPorFiltro(TipoEmpleado mozo, string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {

                var empleado = context.Personas.OfType<DAL.Empleado>()
                    .FirstOrDefault(x => x.TipoEmpleado == mozo
                                    && x.Legajo.ToString() == cadenaBuscar && x.EstaEliminado == false);

                if (empleado == null) return null;


                return new EmpleadoDto
                {
                    Id = empleado.Id,
                    Apellido = empleado.Apellido,
                    Dni = empleado.Dni,
                    Nombre = empleado.Nombre,
                    Legajo = empleado.Legajo,
                    Telefono = empleado.Teléfono,
                    Direccion = empleado.Direccion,
                    Celular = empleado.Celular,
                    Cuil = empleado.Cuil,
                    TipoOcupacion = empleado.TipoEmpleado,


                };
            }
        }

        public IEnumerable<EmpleadoDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var legajo = 1;
                int.TryParse(cadenaBuscar, out legajo);

                var empleados = context.Personas.OfType<DAL.Empleado>()
                    .AsNoTracking()
                    .Where(x => (x.Apellido.Contains(cadenaBuscar)
                                || x.Nombre.Contains(cadenaBuscar)
                                || x.Dni == cadenaBuscar
                                || x.Legajo == legajo)
                                && x.EstaEliminado == false)
                    .Select(x => new EmpleadoDto
                    {
                        Id = x.Id,
                        Legajo = x.Legajo,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        Dni = x.Dni,
                        Direccion = x.Direccion,
                        Telefono = x.Teléfono,
                        Celular = x.Celular,
                        Cuil = x.Cuil,
                        TipoOcupacion = x.TipoEmpleado,
                       


                    }).ToList();

                return empleados;
            }
        }

        public EmpleadoDto obtenerPorId(long? Empleadoid)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var empleado = context.Personas.OfType<DAL.Empleado>()
                    .FirstOrDefault(x => x.Id == Empleadoid);

                if (empleado == null) throw new ArgumentNullException("No existe el Empleado");

                return new EmpleadoDto
                {
                    Id = empleado.Id,
                    Apellido = empleado.Apellido,
                    Dni = empleado.Dni,
                    Nombre = empleado.Nombre,
                    Legajo = empleado.Legajo,
                    Telefono = empleado.Teléfono,
                    Direccion = empleado.Direccion,
                    Celular = empleado.Celular,
                    Cuil = empleado.Cuil,
                    TipoOcupacion = empleado.TipoEmpleado,
                    UsuarioId = empleado.Usuario.Id
                   

                };
            }
        }

        public int ObtenerSiguienteLegajo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Empleado>()
                    .Any()
                    ? context.Personas.OfType<DAL.Empleado>().Max(x => x.Legajo) + 1
                    : 1;
            }
        }

        public bool VerificarSiExiste(long? empleadoId, int legajo, string cuil)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Empleado>()
                    .Any(x => x.Id != empleadoId && (x.Legajo == legajo || x.Cuil == cuil));
            }
        }

   }
}
