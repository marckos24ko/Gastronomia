//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reserva : Comprobante
    {
        public long ClienteId { get; set; }
        public System.DateTime FechaReserva { get; set; }
        public int CantidadComensales { get; set; }
        public EstadoReserva EstadoReserva { get; set; }
        public long EmpleadoId { get; set; }
        public string Observacion { get; set; }
        public bool EstaEliminado { get; set; }
        public int Codigo { get; set; }
        public long MesaId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Mesa Mesa { get; set; }
    }
}
