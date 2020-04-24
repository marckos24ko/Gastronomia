using DAL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Servicio.Core.Proveedores
{
    public class ProveedorDto
    {
        public long Id { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public bool EstaEliminado { get; set; }

        public string Cuit { get; set; }

        public string RazonSocial { get; set; }

        public string NombreFantasia { get; set; }

        public DateTime FechaInicioActividad { get; set; }

        public decimal IngresosBrutos { get; set; }

        public long CondicionIvaId { get; set; }

        public string ApyNomContacto { get; set; }

        public string CondicionIvaStr { get; set; }

        public long RubroId { get; set; }

        public string RubroStr { get; set; }

        public virtual ICollection<DAL.Producto> Productos { get; set; }
    }
}
