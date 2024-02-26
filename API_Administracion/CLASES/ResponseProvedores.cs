using API_Administracion.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CLASES
{
    public class ResponseProvedores
    {
         public IEnumerable<Provedores> listaProvedores { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
