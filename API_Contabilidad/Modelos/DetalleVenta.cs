using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Modelos
{
    public class DetalleVenta
    {
        public int idVenta { get; set; }
        public int cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }
       
    }
}
