using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RegistroUsuario.Clases;
using RegistroUsuario.Persistencia;
using RegistroUsuario.Interfaces;

namespace RegistroUsuario.Controllers
{
    //indica  que son parte de los controladores de la api 
    [ApiController]
    [Route("[controller]")]

    public class AminUsers : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private  Ipersistencia _ipersistencia;
        public AminUsers(ILoggerManager logger, Ipersistencia ipersistencia)
        {
            _logger = logger;
            _ipersistencia = ipersistencia;
        }

        //endopints 

        /// <summary>
        /// end point generico get 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Controlador de usuario registrado";
        }
        /// <summary>
        /// end point get con parametro 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getNomUser/{nombre}")]
        public string Get(string nombre)
        {
            if (nombre == "Marcos")
            {
                return "Marcos  Jesus Mendoza Avila";

            }
            else
            {
                return "Nombre Incorrecto del usuario ";

            }


        }
        /// <summary>
        /// metodo  get con diferentes parametros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="edad"></param>
        /// <param name="puesto"></param>
        /// <returns></returns>
        [HttpGet("getNomUser/{id}/{nombre}/{edad}/{puesto}")]
        public string Get(int id, string nombre, int edad, string puesto)
        {
            userInfo obj = new userInfo();
            obj.edad = 25;
            obj.nombre = "Marcos Jesus Mendoza Avila";
            obj.id = 1077120;
            obj.Puesto = "Desarrollador";

            string response = JsonConvert.SerializeObject(obj);
            return response;
        }
        //estructura de informacion de un usuario
        private struct userInfo
        {
            public string nombre;
            public string Puesto;
            public int edad;
            public int id;
        }

        //METODOS  POST 

        /// <summary>
        /// registro de usuarios
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost("RegistreUser")]
        public ResponseUser RegistroUsuario(InfoUser datos)
        {
            _logger.LogInformation("Metodo RegistroUsuario inicio");

            ResponseUser respuesta = new ResponseUser();
            try
            {
                

                var responseRegUsur = _ipersistencia.regitroUsuario(datos);
                if (responseRegUsur)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Usuario registrado exitosamente";
                }
                else
                {
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Error al registrar Usuario";
                }

            }
            catch (Exception e)
            {

                respuesta.Codigo = 1;
                respuesta.Mensaje = "Error al registrar Usuario";
            }

            return respuesta;

        }

        /// <summary>
        /// metodo que obtiene la informacion de un usuario 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("SearchUser")]
        public InfoUser BuscarUsuario(InfoUser datos)
        {
            InfoUser respuesta = new InfoUser();
            try
            {
                
                var responseRegUsur = _ipersistencia.datosUsuarios(datos.IdUsuario);
                respuesta = responseRegUsur;

            }
            catch (Exception e)
            {

                _logger.LogInformation("Error en el metodo BuscarUsuario :" + e.Message.ToString());
            }

            return respuesta;

        }
        [HttpPatch("UpdateUser")]
        public ResponseUser ActualizarUsuario(InfoUser datos)
        {
            ResponseUser respuesta = new ResponseUser();

            try
            {
                
                var responseRegUsur = _ipersistencia.actualizarUsuario(datos);

                if (responseRegUsur)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje ="El usuario se actualizo correctamente";
                }
                else
                {
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Error al actualizar usuario";
                }
               
            }
            catch (Exception e)
            {

                respuesta.Codigo = 1;
                respuesta.Mensaje = "Error al actualizar usuario";
            }
            return respuesta;

        }

        [HttpDelete("DeleteUser")]
        public ResponseUser EliminarUsuario(InfoUser datos)
        {
            ResponseUser respuesta = new ResponseUser();
            try
            {
                
                var responseRegUsur = _ipersistencia.eliminarUsuario(datos.IdUsuario);

                if (responseRegUsur)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "El usuario se eliminó correctamente";
                }
                else
                {
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Error al eliminar usuario";
                }
            }
            catch (Exception e )
            {
                respuesta.Codigo = 1;
                respuesta.Mensaje = "Error al Eliminar usuario"; throw;
            }
            return respuesta;
        }

        [HttpPost("RegistrarStatus")]

        public ResponseUser RegistrarStatus(StatusCLS datos)
        {
            _logger.LogInformation("Metodo RegistroUsuario inicio");
            ResponseUser respuesta = new ResponseUser();

            try
            {
                

                var responseRegUsur = _ipersistencia.registroStatus(datos);
                if (responseRegUsur)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Usuario registrado exitosamente";
                }
                else
                {
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Error al registrar Usuario";
                }
            }
            catch (Exception e)
            {

                throw;
            }


            return respuesta;
        
        }
    }
}
