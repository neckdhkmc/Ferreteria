using API_Administracion.CAPA_DATOS;
using API_Administracion.CLASES;
using API_Administracion.Interfaces;
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

        #region constructor de la clase
        private readonly Ipersistencia _ipersistencia;
        public Administracion(Ipersistencia ipersistencia)
        {
            _ipersistencia = ipersistencia;

        }
        #endregion

        #region provedores
        [HttpPost("RegistrarProvedor")]

        public ResponseGeneral RegistrarProvedor(Datosprovedor datos)
        {

            //_logger.LogInformation("Metodo RegistroUsuario inicio");

            ResponseGeneral respuesta = new ResponseGeneral();
            try
            {
                

                var responseRegUsur = _ipersistencia.regitroProvedor(datos);
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

        [HttpPost("ConsultaMarcaProvedor")]

        public ResponseMarcaProvedores ConsultaMarcaPorProbedor(Datosprovedor datos)
        {
            ResponseMarcaProvedores response = new ResponseMarcaProvedores();
            try
            {
                

                response = _ipersistencia.consultaMarcasPorProvedor(datos);

                if (response.Codigo == 0)
                {
                    return response;


                } else
                {
                    response.Codigo = 1;
                    response.Mensaje = "no se encontraron registros";

                }

            }
            catch (Exception e)
            {

                throw;
            }

            return response;
        }

        #endregion

        #region marcas  
        [HttpPost("RegistrarMarca")]
        public ResponseGeneral RegistrarMarca(DatosMarca datos)
        {


            //_logger.LogInformation("Metodo RegistroUsuario inicio");

            ResponseGeneral respuesta = new ResponseGeneral();
            try
            {
               

                var responseRegMarca = _ipersistencia.regitroMarca(datos);
                if (responseRegMarca)
                {
                    respuesta.codigo = 0;
                    respuesta.Mensaje = "Marca registrada exitosamente";
                }
                else
                {
                    respuesta.codigo = 1;
                    respuesta.Mensaje = "Error al registrar Marca";
                }

            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.Mensaje = "Error al registrar Marca";
            }

            return respuesta;

        }

        #endregion
        #region ventas 

        #endregion

        #region inventario

        #endregion

        #region Productos

        [HttpPost("RegistrarProducto")]

        public ResponseGeneral RegistrarProducto(DatosProducto datos)
        {
            ResponseGeneral respuesta = new ResponseGeneral();

            try
            {
                
                var responseRgProducto = _ipersistencia.registrarProducto(datos);
                if (responseRgProducto)
                {
                    respuesta.codigo = 0;
                    respuesta.Mensaje = "Producto registrado exitosamente";

                }
                else
                {
                    respuesta.codigo = 1;
                    respuesta.Mensaje = "Producto no registrado exitosamente";
                }

            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.Mensaje = "Producto no registrado exitosamente";
            }
            return respuesta;
        }

        #endregion

        #region Catalogos


        [HttpPost("RegistrarCategoria")]
        public ResponseGeneral RegistrarCategoria(DatosGenericos datos)
        {
            ResponseGeneral respuesta = new ResponseGeneral();
            try
            {
              

                var responseCategoria = _ipersistencia.registrarCategoria(datos);
                if (responseCategoria)
                {
                    respuesta.codigo = 0;
                    respuesta.Mensaje = "Categoria registrada exitosamente";
                }
                else
                {
                    respuesta.codigo = 1;
                    respuesta.Mensaje = "Error al registrar Ctegoria";
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return respuesta;

        }



        #endregion
    }

}

