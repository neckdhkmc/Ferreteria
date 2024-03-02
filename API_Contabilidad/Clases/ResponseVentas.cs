using API_Contabilidad.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Clases
{
    public class ResponseVentas
    {
        public IEnumerable<Ventas> listaVentas { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
