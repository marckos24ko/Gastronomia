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
    
    public partial class Salon : Comprobante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salon()
        {
            this.DetallesSalones = new HashSet<DetalleSalon>();
        }
    
        public long MesaId { get; set; }
        public int Cubiertos { get; set; }
        public long MozoId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public EstadoSalon EstadoSalon { get; set; }
        public long ClienteId { get; set; }
    
        public virtual Mesa Mesa { get; set; }
        public virtual Empleado Mozo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleSalon> DetallesSalones { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}