using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CLASES
{
    public class Datosprovedor
    {
        public int Id { get; set; }
        public string NombreProvedor { get; set; }

        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Contacto { get; set; }
        public int IdStatus { get; set; }
    }
}
