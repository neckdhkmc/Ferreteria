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
    public class CategoriasServices
    {
        private readonly IRepository<Categorias> _categoriasRepository;
        public CategoriasServices(IRepository<Categorias> CategoriasRepository)
        {
            _categoriasRepository = CategoriasRepository;
        }

        public ResponseGeneral RegistrarCategoria(Categorias datos)
        {
            var respuesta = new  ResponseGeneral();
            try
            {
                var nombreCategoria = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = datos.Nombre};
                var Descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar) {Value =  datos.Descripcion};
                var idStatus = new SqlParameter("@idStatus", SqlDbType.Int) {Value = datos.idStatus };

                //metodo que hace el consumo del store 

                var result = _categoriasRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarCategoria @Nombre, @Descripcion, @idStatus, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", nombreCategoria, Descripcion,idStatus);

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
                respuesta.Mensaje = "Error al registrar Categoria";
            }

            return respuesta;
        }

        public ResponseCategorias GetCategorias()
        {
            var respuesta = new ResponseCategorias();
            try
            {
                respuesta.listaCategorias = _categoriasRepository.GetAll();

                if (respuesta.listaCategorias.Count() == 0)
                {
                    //esta vacio
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "No hay Categorias";
                }
                else
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Se obtiene la lista de las categorias con exito.";
                }

                return respuesta;
            }
            catch (Exception e)
            {

                var rff = e.InnerException.Message;
                respuesta.Codigo = 1;
                respuesta.Mensaje = "No se obtiene la lista de las categorias.";
                respuesta.listaCategorias = null;
            }

            return respuesta;


        }

        public ResponseGeneral EliminarCategoria(Categorias datos)
        {
            var respuesta = new ResponseGeneral();
            try
            {
                var id = new SqlParameter("@idCategoria", SqlDbType.Int) { Value = datos.idCategoria };
                var result = _categoriasRepository.ExecuteStoredProcedureNonQuery("sp_Eliminarategoria @idCategoria, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", id);
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
                respuesta.Mensaje = "Error al eliminar categoria";
            }

            return respuesta;
        
        }

        public ResponseGeneral ActualizarCategoria(Categorias datos)
        {
            var respuesta = new ResponseGeneral();
            try
            {
                var idcategoria = new SqlParameter("@idCategoria", SqlDbType.Int) { Value = datos.idCategoria }; 
                var nombreCategoria = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = datos.Nombre };
                var Descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = datos.Descripcion };
                var idStatus = new SqlParameter("@idStatus", SqlDbType.Int) { Value = datos.idStatus };

                var result = _categoriasRepository.ExecuteStoredProcedureNonQuery("sp_ActualizarCategoria @idCategoria, @Nombre, @Descripcion, @idStatus, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", idcategoria,nombreCategoria, Descripcion, idStatus);

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
                respuesta.Mensaje = "Error al Actualizar la Categoria";
            }
            return respuesta;
        }

        //private ResponseGeneral ExecStore(string NombreStore, Categorias datos)
        //{ 

        //}
    }
}
