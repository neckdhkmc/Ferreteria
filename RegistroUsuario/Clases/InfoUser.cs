using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroUsuario.Clases
{
    public class InfoUser
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Puesto { get; set; }
        public int NivelSeguridad { get; set; }
        public int IdStatus { get; set; }
        public string UsuarioLogin { get; set; }
        public string Contraseña { get; set; }
    }
}
