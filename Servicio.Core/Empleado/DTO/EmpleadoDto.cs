using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Servicio.Core.Empleado.DTO
{
   public class EmpleadoDto
    {
        public long Id { get; set; }

        public int Legajo { get; set; }

        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string ApyNom
        {
            get { return $"{Apellido} {Nombre}"; }
        }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Celular { get; set; }

        public string Cuil { get; set; }

        public TipoEmpleado TipoOcupacion { get; set; }

        public bool EstaEliminado { get; set; }

        public long UsuarioId { get; set; }
    }
}
