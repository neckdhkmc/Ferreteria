using API_Administracion.CAPA_DATOS;
using API_Administracion.CLASES;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Administracion : ControllerBase
    {

        #region provedores
        [HttpPost("RegistrarProvedor")]

        public ResponseGeneral RegistrarProvedor(Datosprovedor datos)
        {
           

            //_logger.LogInformation("Metodo RegistroUsuario inicio");

            ResponseGeneral respuesta = new ResponseGeneral();
            try
            {
                Persistencia objpersistence = new Persistencia();

                var responseRegUsur = objpersistence.regitroProvedor(datos);
                if (responseRegUsur)
                {
                    respuesta.codigo = 0;
                    respuesta.Mensaje = "Provedor registrado exitosamente";
                }
                else
                {
                    respuesta.codigo = 1;
                    respuesta.Mensaje = "Error al registrar provedor";
                }

            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.Mensaje = "Error al registrar Usuario";
            }

            return respuesta;          
        
        }
        #endregion

        #region marcas

        #endregion
        #region ventas 

        #endregion

        #region inventario

        #endregion



    }
}
