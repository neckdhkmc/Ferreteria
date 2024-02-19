using RegistroUsuario.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroUsuario.Interfaces
{
    public interface Ipersistencia
    {
        bool conexionBD();
        bool regitroUsuario(InfoUser datos);
        InfoUser datosUsuarios(int id);

        bool actualizarUsuario(InfoUser datos);

        bool eliminarUsuario(int id);
        bool registroStatus(StatusCLS datos);
      }
}
