using API_Administracion.CAPA_DATOS;
using API_Administracion.CLASES;
using API_Administracion.Interfaces;
using API_Administracion.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class Administracion : ControllerBase
    {

        private readonly IRepository<Marca> _MarcaRepository;
        #region constructor de la clase

        public Administracion(IRepository<ProvedoresController> productRepository, IRepository<Marca> MarcaRepository)
        {
            
            _MarcaRepository = MarcaRepository;
        }
        #endregion

     

        #region marcas  
        [HttpPost("RegistrarMarca")]
        public ResponseGeneral RegistrarMarca(Marca datos)
        {

            ResponseGeneral respuesta = new ResponseGeneral();
           
            try
            {
                // Suponiendo que Marca contiene los datos de la nueva marca
                var nombreMarca = new SqlParameter("@NombreMarca", SqlDbType.NVarChar) { Value = datos.NombreMarca };
                var descripcion = new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = datos.Descripcion };
                var idStatus = new SqlParameter("@idStatus", SqlDbType.Int) { Value = datos.idStatus };

                //se ejecuta el metodo del repositorio para ejecutar el store
                var result = _MarcaRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarMarca @NombreMarca, @Descripcion, @idStatus", nombreMarca, descripcion, idStatus);

                if (result.codigo == 0)
                {
                    respuesta.codigo = 0;
                    respuesta.Mensaje = "Marca registrada correctamente.";
                    return respuesta;
                }
                else {
                    respuesta.codigo = 1;
                    respuesta.Mensaje = "Marca No registrada correctamente.";
                    return respuesta;
                }
                
                
            }
            catch (Exception ex)
            {
                respuesta.codigo = 1;
                respuesta.Mensaje = "Marca No registrada correctamente.";
                return respuesta;                
            }

            
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
                
                //var responseRgProducto = _ipersistencia.registrarProducto(datos);
                //if (responseRgProducto)
                //{
                //    respuesta.codigo = 0;
                //    respuesta.Mensaje = "Producto registrado exitosamente";

                //}
                //else
                //{
                //    respuesta.codigo = 1;
                //    respuesta.Mensaje = "Producto no registrado exitosamente";
                //}

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
              

                //var responseCategoria = _ipersistencia.registrarCategoria(datos);
                //if (responseCategoria)
                //{
                //    respuesta.codigo = 0;
                //    respuesta.Mensaje = "Categoria registrada exitosamente";
                //}
                //else
                //{
                //    respuesta.codigo = 1;
                //    respuesta.Mensaje = "Error al registrar Ctegoria";
                //}
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

