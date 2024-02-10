using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CLASES
{
    public class ResponseMarcaProvedores
    {
        public int idProvedor { get; set; }

        public string NombreProvedor { get; set; }

        public List<DatosMarca> ListaNomMarcas { get; set; }

        public int Codigo { get; set; }

        public string Mensaje { get; set; }
    }
}
