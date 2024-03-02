using API_Administracion.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CLASES
{
    public class ResponseProductos
    {
        public IEnumerable<ProductosV> listaProductos { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
