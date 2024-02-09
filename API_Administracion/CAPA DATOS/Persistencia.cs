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

    }
}
