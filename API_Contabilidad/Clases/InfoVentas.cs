using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Clases
{
    public class InfoVentas
    {
       
        public string Folio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdCliente { get; set; }
        public int IdEmpleado { get; set; }
    }
}
