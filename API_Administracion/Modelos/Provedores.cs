using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Modelos
{
    public class Provedores
    {
        public int Id { get; set; }
        public string NombreProvedor { get; set; }
        public string Direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string contacto { get; set; }
        public int idStatus { get; set; }

    }
}
