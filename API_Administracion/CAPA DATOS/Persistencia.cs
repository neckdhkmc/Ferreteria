using API_Administracion.CLASES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CAPA_DATOS
{
    public class Persistencia
    {
        IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        public bool regitroProvedor(Datosprovedor datos)
        {

            // _logger.LogInformation("Metodo regitroUsuario");
            bool respuesta = false;
            string connectionString = configuration.GetConnectionString("MiConexionBD"); // Reemplaza con tu cadena de conexión
                                                                                         // _logger.LogInformation("cadena de conexion : " + connectionString);
            try
            {
                // Nombre del stored procedure
                string storedProcedureName = "sp_RegistrarProvedor";

                // _logger.LogInformation("store a ejecutar  : " + storedProcedureName);
                // Parámetros del stored procedure
                string parametro1 = datos.NombreProvedor;
                string parametro2 = datos.Direccion;
                int parametro3 = datos.Telefono;
                string parametro4 = datos.Correo;
                string parametro5 = datos.Contacto;
                int parametro6 = datos.IdStatus;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    // _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@NombreProvedor", parametro1);
                        cmd.Parameters.AddWithValue("@Direccion", parametro2);
                        cmd.Parameters.AddWithValue("@telefono", parametro3);
                        cmd.Parameters.AddWithValue("@correo", parametro4);
                        cmd.Parameters.AddWithValue("@contacto", parametro5);
                        cmd.Parameters.AddWithValue("@idStatus", parametro6);


                        //_logger.LogInformation("parametros: " + cmd.Parameters.ToString());

                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el stored procedure y obtener el DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            int returnValue = Convert.ToInt32(returnParameter.Value);
                            //_logger.LogInformation("valor de return value: " + returnValue.ToString());
                            if (returnValue == 0)
                            {
                                respuesta = true;
                            }
                            else
                            {
                                respuesta = false;
                            }

                        }

                    }
                }

            }
            catch (Exception e)
            {
                //_logger.LogInformation("valor de return value: " + e.Message.ToString());
                respuesta = false;
            }

            return respuesta;

        }

        public bool regitroMarca(DatosMarca datos)
        {

            // _logger.LogInformation("Metodo regitroUsuario");
            bool respuesta = false;
            string connectionString = configuration.GetConnectionString("MiConexionBD"); // Reemplaza con tu cadena de conexión
                                                                                         // _logger.LogInformation("cadena de conexion : " + connectionString);
            try
            {
                // Nombre del stored procedure
                string storedProcedureName = "sp_RegistrarMarca";

                // _logger.LogInformation("store a ejecutar  : " + storedProcedureName);
                // Parámetros del stored procedure
                string parametro1 = datos.NombreMarca;
                string parametro2 = datos.Descripcion;                
                int parametro3 = datos.IdStatus;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    // _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@NombreMarca", parametro1);
                        cmd.Parameters.AddWithValue("@Descripcion", parametro2);
                        cmd.Parameters.AddWithValue("@idStatus", parametro3);

                        //_logger.LogInformation("parametros: " + cmd.Parameters.ToString());

                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el stored procedure y obtener el DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            int returnValue = Convert.ToInt32(returnParameter.Value);
                            //_logger.LogInformation("valor de return value: " + returnValue.ToString());
                            if (returnValue == 0)
                            {
                                respuesta = true;
                            }
                            else
                            {
                                respuesta = false;
                            }

                        }

                    }
                }

            }
            catch (Exception e)
            {
                //_logger.LogInformation("valor de return value: " + e.Message.ToString());
                respuesta = false;
            }

            return respuesta;

        }

        public ResponseMarcaProvedores consultaMarcasPorProvedor(Datosprovedor datos)
        {
            ResponseMarcaProvedores rf = new ResponseMarcaProvedores();
            List<DatosMarca> listaDtosMarca = new();
            DatosMarca datosMarcabd = new DatosMarca();
            try
            {
                string connectionString = configuration.GetConnectionString("MiConexionBD");
                // Nombre del stored procedure
                string storedProcedureName = "sp_ObtenerMarcasPorProveedor";
                // Parámetros del stored procedure
                int parametro1 = datos.Id;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                   // _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@idProvedor", parametro1);

                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        //// Ejecutar el stored procedure y obtener el DataReader


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Verificar si hay filas en el resultado
                            if (reader.HasRows)
                            {
                                // Iterar a través de las filas y trabajar con los datos
                                while (reader.Read())
                                {

                                    // Acceder a las columnas por nombre o índice
                                    rf.idProvedor = Convert.ToInt32(reader["idProvedor"]);
                                    rf.NombreProvedor = reader["NombreProvedor"].ToString();
                                    datosMarcabd.NombreMarca = reader["NombreMarca"].ToString();
                                    datosMarcabd.Descripcion = reader["Descripcion"].ToString();
                                    datosMarcabd.IdStatus = Convert.ToInt32(reader["IdStatus"]);
                                    listaDtosMarca.Add(new DatosMarca { NombreMarca = datosMarcabd.NombreMarca, Descripcion = datosMarcabd.Descripcion, IdStatus = datosMarcabd.IdStatus });
                                    rf.ListaNomMarcas = listaDtosMarca;
                                    rf.Codigo = 0;
                                    rf.Mensaje = "Consulta Exitosa";
                                }
                            }
                            else
                            {

                                rf.idProvedor = 0;
                                rf.NombreProvedor = null;
                                listaDtosMarca = null;
                                rf.ListaNomMarcas = listaDtosMarca;
                                rf.Codigo = 1;
                                rf.Mensaje = "No se encontraron registros";
                            }
                        }


                        //construccion de respuesta 

                       
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rf;
        }

    }
}
