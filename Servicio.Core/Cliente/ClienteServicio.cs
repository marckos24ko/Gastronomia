using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Servicio.Core.Cliente.DTO;
using Servicio.Core.ComprobanteSalon;

namespace Servicio.Core.Cliente
{
    public class ClienteServicio : IClienteServicio
    {
        public void ClienteDesocupado(long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var clienteModificar = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == clienteId);

                clienteModificar.EstaOcupado = false;
                context.SaveChanges();
            }
        }

        public void ClienteOcupado(ClienteDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var clienteModificar = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == dto.Id);

                clienteModificar.EstaOcupado = true;
                context.SaveChanges();
            }
        }

        public void Eliminar(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var ClienteEliminar = context.Personas.OfType<DAL.Cliente>().FirstOrDefault(x => x.Id == ClienteId);

                ClienteEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public void Insertar(ClienteDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var clienteNuevo = new DAL.Cliente();

                clienteNuevo.Codigo = dto.Codigo;
                clienteNuevo.Apellido = dto.Apellido;
                clienteNuevo.Direccion = dto.Direccion;
                clienteNuevo.Dni = dto.Dni;
                clienteNuevo.Nombre = dto.Nombre;
                clienteNuevo.Teléfono = dto.Telefono;
                clienteNuevo.Cuil = dto.Cuil;
                clienteNuevo.Celular = dto.Celular;
                clienteNuevo.EstaEliminado = false;
                clienteNuevo.ActivoParaCompra = true;
                clienteNuevo.MontoMaximo = dto.MontoMaximoCtaCte;
                clienteNuevo.DeudaTotal = dto.DeudaTotal;
                clienteNuevo.TieneCtaCte = dto.TieneCuentaCorriente;
                clienteNuevo.EstaOcupado = false;

                context.Personas.Add(clienteNuevo);

                context.SaveChanges();
            }
        }

        public void Modificar(ClienteDto dto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var clienteModificar = context.Personas.OfType<DAL.Cliente>().Single
                    (x => x.Id == dto.Id);

                clienteModificar.Apellido = dto.Apellido;
                clienteModificar.Direccion = dto.Direccion;
                clienteModificar.Dni = dto.Dni;
                clienteModificar.Nombre = dto.Nombre;
                clienteModificar.Teléfono = dto.Telefono;
                clienteModificar.Cuil = dto.Cuil;
                clienteModificar.Celular = dto.Celular;
                clienteModificar.MontoMaximo = dto.MontoMaximoCtaCte;
 
                context.SaveChanges();
            }
        }

        public void ModificarMontoCtaCte(decimal montoGastado, long clienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var clienteModificar = context.Personas.OfType<DAL.Cliente>()
                                       .First(x => (x.Id == clienteId
                                                  && x.TieneCtaCte == true));

                if (clienteModificar.MontoMaximo >= montoGastado)
                {

                    clienteModificar.MontoMaximo = clienteModificar.MontoMaximo - montoGastado;

                    context.SaveChanges();
                }

            }
        }

        public IEnumerable<ClienteDto> ObtenerClientesDesocupados(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {                           
                        var codigo = 1;
                        int.TryParse(cadenaBuscar, out codigo);

                        var clientes = context.Personas.OfType<DAL.Cliente>()
                            .AsNoTracking()
                            .Where(x => (x.Apellido.Contains(cadenaBuscar)
                                        || x.Nombre.Contains(cadenaBuscar)
                                        || x.Dni == cadenaBuscar
                                        || x.Codigo == codigo)
                                        && x.EstaEliminado == false
                                        && x.EstaOcupado == false
                                        && x.ActivoParaCompra == true)
                            .Select(x => new ClienteDto()
                            {
                                Id = x.Id,
                                Codigo = x.Codigo,
                                Nombre = x.Nombre,
                                Apellido = x.Apellido,
                                Dni = x.Dni,
                                Direccion = x.Direccion,
                                Telefono = x.Teléfono,
                                Celular = x.Celular,
                                Cuil = x.Cuil,
                                EstaEliminado = x.EstaEliminado,
                                ActivoParaCompras = x.ActivoParaCompra,
                                MontoMaximoCtaCte = x.MontoMaximo,
                                TieneCuentaCorriente = x.TieneCtaCte,
                                DeudaTotal = x.DeudaTotal


                            }).ToList();

                        return clientes;            
                               
            }
        }

        public IEnumerable<ClienteDto> ObtenerClientesDesocupadosSinFiltro()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var clientes = context.Personas.OfType<DAL.Cliente>()
                    .AsNoTracking()
                    .Where(x => x.EstaEliminado == false
                                && x.EstaOcupado == false
                                && x.ActivoParaCompra == true)
                    .Select(x => new ClienteDto()
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        Dni = x.Dni,
                        Direccion = x.Direccion,
                        Telefono = x.Teléfono,
                        Celular = x.Celular,
                        Cuil = x.Cuil,
                        EstaEliminado = x.EstaEliminado,
                        ActivoParaCompras = x.ActivoParaCompra,
                        MontoMaximoCtaCte = x.MontoMaximo,
                        TieneCuentaCorriente = x.TieneCtaCte,
                        DeudaTotal = x.DeudaTotal

                    }).ToList();

                return clientes;
            }
        }

        public IEnumerable<ClienteDto> ObtenerPorFiltro(string cadenaBuscar)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var codigo = 1;
                int.TryParse(cadenaBuscar, out codigo);

                var clientes = context.Personas.OfType<DAL.Cliente>()
                    .AsNoTracking()
                    .Where(x => (x.Apellido.Contains(cadenaBuscar)
                                || x.Nombre.Contains(cadenaBuscar)
                                || x.Dni == cadenaBuscar
                                || x.Codigo == codigo)
                                && x.EstaEliminado == false)
                    .Select(x => new ClienteDto()
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        Dni = x.Dni,
                        Direccion = x.Direccion,
                        Telefono = x.Teléfono,
                        Celular = x.Celular,
                        Cuil = x.Cuil,
                        EstaEliminado = x.EstaEliminado,
                        ActivoParaCompras = x.ActivoParaCompra,
                        MontoMaximoCtaCte = x.MontoMaximo,
                        TieneCuentaCorriente = x.TieneCtaCte,
                        DeudaTotal = x.DeudaTotal,
                        EstaOcupado = x.EstaOcupado

                                                
                    }).ToList();

                return clientes;
            }
        }

        public ClienteDto obtenerPorId(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cliente = context.Personas.OfType<DAL.Cliente>()
                    .FirstOrDefault(x => x.Id == ClienteId);

                if (cliente == null) throw new ArgumentNullException("No existe el Cliente");

                return new ClienteDto()
                {
                    Id = cliente.Id,
                    Apellido = cliente.Apellido,
                    Dni = cliente.Dni,
                    Nombre = cliente.Nombre,
                    Codigo = cliente.Codigo,
                    Telefono = cliente.Teléfono,
                    Direccion = cliente.Direccion,
                    Celular = cliente.Celular,
                    Cuil = cliente.Cuil,
                    MontoMaximoCtaCte = cliente.MontoMaximo,
                    TieneCuentaCorriente = cliente.TieneCtaCte,
                    DeudaTotal = cliente.DeudaTotal,
                    EstaOcupado = cliente.EstaOcupado

                };
            }
        }

        public int ObtenerSiguienteCodigo()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Cliente>()
                    .Any()
                    ? context.Personas.OfType<DAL.Cliente>().Max(x => x.Codigo) + 1
                    : 1;
            }
        }

        public long obtenerSiguienteId()
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Cliente>()
                    .Any()
                    ? context.Personas.OfType<DAL.Cliente>().Max(x => x.Id) + 1
                    : 1;
            }
        }

        public bool VerificarSiEstaOcupado(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Cliente>()
                    .Any(x => x.Id == ClienteId && x.EstaOcupado == true && x.estaEliminado == false);
            }
        }

        public bool VerificarSiExiste(long? id, int codigo,  string cuil)
        {
            using (var context = new ModeloGastronomiaContainer()) // aqui
            {
                return context.Personas.OfType<DAL.Cliente>()
                    .Any(x => x.Id != id && (x.Codigo == codigo || x.Cuil == cuil));
            }
        }

        public bool VerificarSiTieneDeuda(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                return context.Personas.OfType<DAL.Cliente>()
                    .Any(x => x.Id == ClienteId && x.DeudaTotal > 0m && x.estaEliminado == false);
            }
        }

        public void DesactivarParaCompras(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == ClienteId && x.estaEliminado == false);

                Cliente.ActivoParaCompra = false;

                context.SaveChanges();

            }
        }

        public void AgregarGastoActual(long ClienteId, decimal Gasto)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == ClienteId && x.estaEliminado == false);

                Cliente.DeudaTotal = Cliente.DeudaTotal + Gasto;

                context.SaveChanges();

            }
        }

        public void RestarPagoActual(long ClienteId, decimal pago)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == ClienteId && x.estaEliminado == false);

                Cliente.DeudaTotal = Cliente.DeudaTotal - pago;

                context.SaveChanges();

            }
        }

        public void ActivarParaCompras(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == ClienteId && x.estaEliminado == false);

                Cliente.ActivoParaCompra = true;

                context.SaveChanges();

            }
        }

        public void ActivarEstadoTieneCtaCte(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == ClienteId && x.estaEliminado == false);

                Cliente.TieneCtaCte = true;

                context.SaveChanges();

            }
        }

        public void DesactivarEstadoTieneCtaCte(long ClienteId)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var Cliente = context.Personas.OfType<DAL.Cliente>()
                    .Single(x => x.Id == ClienteId && x.estaEliminado == false);

                Cliente.TieneCtaCte = false;

                context.SaveChanges();

            }
        }

        public ClienteDto obtenerPorCodigo(int codigo)
        {
            using (var context = new ModeloGastronomiaContainer())
            {
                var cliente = context.Personas.OfType<DAL.Cliente>()
                    .FirstOrDefault(x => x.Codigo == codigo);

                if (cliente == null) throw new ArgumentNullException("No existe el Cliente");

                return new ClienteDto()
                {
                    Id = cliente.Id,
                    Apellido = cliente.Apellido,
                    Dni = cliente.Dni,
                    Nombre = cliente.Nombre,
                    Codigo = cliente.Codigo,
                    Telefono = cliente.Teléfono,
                    Direccion = cliente.Direccion,
                    Celular = cliente.Celular,
                    Cuil = cliente.Cuil,
                    MontoMaximoCtaCte = cliente.MontoMaximo,
                    TieneCuentaCorriente = cliente.TieneCtaCte,
                    DeudaTotal = cliente.DeudaTotal,
                    EstaOcupado = cliente.EstaOcupado

                };
            }
        }
    }
}
