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
    
    public abstract partial class PersonaJuridica : Persona
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantacia { get; set; }
        public System.DateTime FechaInicioActividad { get; set; }
        public decimal IngresosBrutos { get; set; }
        public long CondicionIvaId { get; set; }
    
        public virtual CondicionIva CondicionIva { get; set; }
    }
}
