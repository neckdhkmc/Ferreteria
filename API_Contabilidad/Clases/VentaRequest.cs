using API_Contabilidad.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Clases
{
    public class VentaRequest
    {
        public Ventas datosVenta { get; set; }
        public List<DetalleVenta> detalles { get; set; }
    }
}
