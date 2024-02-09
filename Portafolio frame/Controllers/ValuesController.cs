using Portafolio_frame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Portafolio_frame.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {


        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public string RegistroUsuario([FromBody] InfoUser datos)
        {
            string respuesta ;
            try
            {
                //Persistence objpersistence = new Persistence();

                //var responseRegUsur = objpersistence.regitroUsuario(datos);
                //if (responseRegUsur)
                //{
                //    respuesta.Codigo = 0;
                //    respuesta.Mensaje = "Usuario registrado exitosamente";
                //}
                //else
                //{
                //    respuesta.Codigo = 1;
                //    respuesta.Mensaje = "Error al registrar Usuario";
                //}
                return respuesta = "";
            }
            catch (Exception e)
            {

                return respuesta = "";

                //respuesta.Codigo = 1;
                //respuesta.Mensaje = "Error al registrar Usuario";
            }
        }
    }
}
