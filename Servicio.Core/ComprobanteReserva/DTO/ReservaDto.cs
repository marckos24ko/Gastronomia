using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Core.Reserva.DTO
{
   public class ReservaDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long CLienteId { get; set; }

        public long EmpleadoId { get; set; }

        public string CLienteStr { get; set; }

        public string EmpleadoStr { get; set; }

        public DateTime Fecha { get; set; }

        public int CantidadComensales { get; set; }

        public EstadoReserva Estado { get; set; }

        public bool EstaEliminado { get; set; }

        public string Observacion { get; set; }

        public long MesaId { get; set; }


    }
}
