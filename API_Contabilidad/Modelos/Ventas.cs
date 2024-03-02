using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Modelos
{
    public class Ventas
    {
        [Key]
        public int IdVenta { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int? IdCliente { get; set; }
        public int IdEmpleado { get; set; }
    }
}
