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
    public class ProvedorService
    {
        private readonly IRepository<Provedores> _provedoresRepository;
        public ProvedorService(IRepository<Provedores> ProvedoresRepository)
        {
            _provedoresRepository = ProvedoresRepository;
        }

        public ResponseGeneral RegistrarProvedor(Provedores datos)
        {
            //_logger.LogInformation("Metodo RegistroUsuario inicio");       

            ResponseGeneral respuesta = new ResponseGeneral();
            try
            {
                //1.-   se obtienen los parametros que necesita el store 

                var NombreProvedor = new SqlParameter("@NombreProvedor", SqlDbType.VarChar) { Value = datos.NombreProvedor };
                var Direccion = new SqlParameter("@Direccion", SqlDbType.VarChar) { Value = datos.Direccion };
                var telefono = new SqlParameter("@telefono", SqlDbType.VarChar) { Value = datos.telefono };
                var correo = new SqlParameter("@correo", SqlDbType.VarChar) { Value = datos.correo };
                var contacto = new SqlParameter("@contacto", SqlDbType.VarChar) { Value = datos.contacto };
                var idStatus = new SqlParameter("@idStatus", SqlDbType.Int) { Value = datos.idStatus };

                //se ejecuta el metodo del repositorio para ejecutar el store
                var result = _provedoresRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarProvedor @NombreProvedor, @Direccion, @telefono, @correo, @contacto, @idStatus, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", NombreProvedor, Direccion, telefono, correo, contacto, idStatus);

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

        public ResponseProvedores getProvedores()
        {
            ResponseProvedores respuesta = new ResponseProvedores();
            try
            {


                respuesta.listaProvedores = _provedoresRepository.GetAll();
                if (respuesta.listaProvedores.Count() == 0)
                {
                    //esta vacio
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "No hay provedores";
                }
                else
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Se obtiene la lista de los provedores con exito.";
                }

                return respuesta;
            }
            catch (Exception e)
            {


                respuesta.Codigo = 1;
                respuesta.Mensaje = "No se obtiene la lista de los provedores.";
                respuesta.listaProvedores = null;

                return respuesta;
            }

        }

        public ResponseGeneral eliminarProvedor(Provedores datos)
        {
            var respuesta = new ResponseGeneral();
            try
            {

                var id = new SqlParameter("@idProvedor", SqlDbType.Int) { Value = datos.Id };
                var result = _provedoresRepository.ExecuteStoredProcedureNonQuery("sp_EliminarProvedorPorId @idProvedor, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", id);
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
        public ResponseGeneral ActualizarProvedor(Provedores datos)
        {
            var respuesta = new ResponseGeneral();
            try
            {
                //1.-   se obtienen los parametros que necesita el store 
                var idProvedor = new SqlParameter("@idProvedor", SqlDbType.Int) { Value = datos.Id };
                var NombreProvedor = new SqlParameter("@NombreProvedor", SqlDbType.VarChar) { Value = datos.NombreProvedor };
                var Direccion = new SqlParameter("@Direccion", SqlDbType.VarChar) { Value = datos.Direccion };
                var telefono = new SqlParameter("@telefono", SqlDbType.VarChar) { Value = datos.telefono };
                var correo = new SqlParameter("@correo", SqlDbType.VarChar) { Value = datos.correo };
                var contacto = new SqlParameter("@contacto", SqlDbType.VarChar) { Value = datos.contacto };
                var idStatus = new SqlParameter("@idStatus", SqlDbType.Int) { Value = datos.idStatus };

                //parametros de saida 


                //se ejecuta el metodo del repositorio para ejecutar el store
                var result = _provedoresRepository.ExecuteStoredProcedureNonQuery("sp_ActualizarProvedor @idProvedor, @NombreProvedor, @Direccion, @telefono, @correo, @contacto, @idStatus, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", idProvedor, NombreProvedor, Direccion, telefono, correo, contacto, idStatus);

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

        public ResponseProvedorMarca getProvedorMarca()
        {
            var respuesta = new ResponseProvedorMarca();
            string viewName = "VistaProvedorMarca";
            var dataFromView = _provedoresRepository.GetFromView<ProvedorMarca>(viewName);
            respuesta.listProvedorMarca = dataFromView;

            return respuesta;
        }
    }
}
