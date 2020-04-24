using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Servicio.Core.CuentaCorriente.Dto
{
    public class CuentaCorrienteDto
    {

        public long Id { get; set; }

        public int Numero { get; set; }

        public bool EstaHabilitada { get; set; }

        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public string ClienteApyNom { get; set; }

        public long ClienteId { get; set; }

        public long EmpleadoId { get; set; }


    }
}
