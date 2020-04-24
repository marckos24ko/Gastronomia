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
    
    public partial class Movimiento
    {
        public long Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public decimal Monto { get; set; }
        public int Numero { get; set; }
        public Nullable<long> FacturaId { get; set; }
        public Nullable<long> ClienteId { get; set; }
        public Nullable<long> ProveedorId { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}