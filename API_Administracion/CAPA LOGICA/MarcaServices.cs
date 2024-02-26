using API_Administracion.CLASES;
using API_Administracion.Interfaces;
using API_Administracion.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CAPA_LOGICA
{
    public class MarcaServices
    {
        private readonly IRepository<Marca> _marcaRepository;
      
        public MarcaServices(IRepository<Marca> MarcaRepository)
        {
            _marcaRepository = MarcaRepository;

        }

        public ResponseGeneral RegistrarMarca(Marca datos)
        {
            //_logger.LogInformation("Metodo RegistroUsuario inicio");       

            ResponseGeneral respuesta = new ResponseGeneral();
            try
            {
                //1.-   se obtienen los parametros que necesita el store 

                var nombreMarca = new SqlParameter("@NombreMarca", SqlDbType.VarChar) { Value = datos.NombreMarca };
                var Descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = datos.Descripcion };
                var idStatus = new SqlParameter("@idStatus", SqlDbType.Int) { Value = datos.idStatus };

                //se ejecuta el metodo del repositorio para ejecutar el store
                var result = _marcaRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarMarca @NombreMarca, @Descripcion, @idStatus,  @Mensaje OUTPUT, @CodigoRetorno OUTPUT", nombreMarca, Descripcion, idStatus);

                // se valida si el resultado es correcto 
                if (result.codigo == 0)
                {
                    respuesta.codigo = result.codigo;
                    respuesta.Mensaje = result.Mensaje;
                }
                else
                {
                    respuesta.codigo = result.codigo;
                    respuesta.Mensaje = result.Mensaje;
                }

            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.Mensaje = "Error al registrar Usuario";
            }

            return respuesta;

        }


        public ResponseMarca getMarca()
        {
            ResponseMarca respuesta = new ResponseMarca();
            try
            {


                respuesta.listaMarcas = _marcaRepository.GetAll();
                if (respuesta.listaMarcas.Count() == 0)
                {
                    //esta vacio
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "No hay Marcas";
                }
                else
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Se obtiene la lista de las Marcas con exito.";
                }

                return respuesta;
            }
            catch (Exception e)
            {

                var rff = e.InnerException.Message;
                respuesta.Codigo = 1;
                respuesta.Mensaje = "No se obtiene la lista de las marcas.";
                respuesta.listaMarcas = null;

                return respuesta;
            }

        }

        public ResponseGeneral EliminarMarca(Marca datos)
        {
            var respuesta = new ResponseGeneral();
            try
            {
                var id = new SqlParameter("@idMarca", SqlDbType.Int) { Value = datos.Id };
                var result = _marcaRepository.ExecuteStoredProcedureNonQuery("sp_EliminarMarcaPorId @idMarca, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", id);
                // se valida si el resultado es correcto 
                if (result.codigo == 0)
                {
                    respuesta.codigo = result.codigo;
                    respuesta.Mensaje = result.Mensaje;
                }
                else
                {
                    respuesta.codigo = result.codigo;
                    respuesta.Mensaje = result.Mensaje;
                }

            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.Mensaje = "Error al eliminar Usuario";
            }

            return respuesta;

        }
    }
}
