using Microsoft.Extensions.Configuration;
using RegistroUsuario.Clases;
using RegistroUsuario.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroUsuario.Persistencia
{
    public class Persistence: Ipersistencia
    {
        private readonly ILoggerManager _logger;

        IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        public Persistence(ILoggerManager logger)
        {
            _logger = logger;
           
        }
        public bool conexionBD()
        {
            bool respuesta = false;
            SqlConnection conexion = new SqlConnection();
            try
            {
                // se abre la conexion a la bd 
                var serverName = ".";
                conexion.ConnectionString = "Data Source=" + serverName + ";" + "Initial Catalog=ADN;" + "User id=AccesDesarrollo;" + "Password=AccesDesarrollo123;";
                conexion.Close();
                conexion.Open();
                respuesta = true;
            }
            catch (Exception e)
            {

                respuesta = false;
            }

            return respuesta;

        }

        public bool regitroUsuario(InfoUser datos)
        {

            _logger.LogInformation("Metodo regitroUsuario");
            bool respuesta = false;
            string connectionString = configuration.GetConnectionString("MiConexionBD"); // Reemplaza con tu cadena de conexión
            _logger.LogInformation("cadena de conexion : " + connectionString);
            try
            {
                // Nombre del stored procedure
                string storedProcedureName = "sp_RegistrarUsuario";

                _logger.LogInformation("store a ejecutar  : " + storedProcedureName);
                // Parámetros del stored procedure
                int parametro1 = datos.IdUsuario;
                string parametro2 = datos.Nombre;
                int parametro3 = datos.Edad;
                string parametro4 = datos.Email;
                int parametro5 = datos.NivelSeguridad;
                string parametro6 = datos.Puesto;
                int parametro7 = datos.IdStatus;
                string parametro8 = datos.UsuarioLogin;
                string parametro9 = datos.Contraseña;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@IdUsuario", parametro1);
                        cmd.Parameters.AddWithValue("@Nombre", parametro2);
                        cmd.Parameters.AddWithValue("@Edad", parametro3);
                        cmd.Parameters.AddWithValue("@Email", parametro4);
                        cmd.Parameters.AddWithValue("@NivelSeguridad", parametro5);
                        cmd.Parameters.AddWithValue("@Puesto", parametro6);
                        cmd.Parameters.AddWithValue("@IdStatus", parametro7);
                        cmd.Parameters.AddWithValue("@UsuarioLogin", parametro8);
                        cmd.Parameters.AddWithValue("@Contraseña", parametro9);

                        _logger.LogInformation("parametros: " + cmd.Parameters.ToString());

                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el stored procedure y obtener el DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            int returnValue = Convert.ToInt32(returnParameter.Value);
                            _logger.LogInformation("valor de return value: " + returnValue.ToString());
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
                _logger.LogInformation("valor de return value: " + e.Message.ToString());
                respuesta = false;
            }

            return respuesta;

        }

        public InfoUser datosUsuarios(int id)
        {
            _logger.LogInformation("Metodo datosUsuarios");
            InfoUser respuesta = new InfoUser();
            try
            {
                string connectionString = configuration.GetConnectionString("MiConexionBD");
                // Nombre del stored procedure
                string storedProcedureName = "sp_ConsultarUsuarioID";
                // Parámetros del stored procedure
                int parametro1 = id;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@ID", parametro1);

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
                                    respuesta.IdUsuario = Convert.ToInt32(reader["ID"]);
                                    respuesta.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                                    respuesta.Nombre = reader["Nombre"].ToString();
                                    respuesta.Email = reader["Email"].ToString();
                                    respuesta.Edad = Convert.ToInt32(reader["Edad"]);
                                    respuesta.NivelSeguridad = Convert.ToInt32(reader["NivelSeguridad"].ToString());
                                    respuesta.Puesto = reader["Puesto"].ToString();
                                    respuesta.IdStatus = Convert.ToInt32(reader["IdStatus"]);
                                    respuesta.UsuarioLogin = reader["UsuarioLogin"].ToString();

                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontraron resultados.");
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("error: " + e.Message.ToString());

            }
            return respuesta;

        }

        public bool actualizarUsuario(InfoUser datos)
        {
            _logger.LogInformation("Metodo datosUsuarios");
            bool respuesta =false;
            try
            {
                string connectionString = configuration.GetConnectionString("MiConexionBD");

                // Nombre del stored procedure
                string storedProcedureName = "sp_ActualizarUsuario";

                _logger.LogInformation("store a ejecutar  : " + storedProcedureName);
                // Parámetros del stored procedure
                int parametro1 = datos.IdUsuario;
                string parametro2 = datos.Nombre;
                int parametro3 = datos.Edad;
                string parametro4 = datos.Email;
                int parametro5 = datos.NivelSeguridad;
                string parametro6 = datos.Puesto;
                int parametro7 = datos.IdStatus;
                string parametro8 = datos.UsuarioLogin;
                string parametro9 = datos.Contraseña;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@UserID", parametro1);
                        cmd.Parameters.AddWithValue("@Nombre", parametro2);
                        cmd.Parameters.AddWithValue("@Edad", parametro3);
                        cmd.Parameters.AddWithValue("@Email", parametro4);
                        cmd.Parameters.AddWithValue("@NivelSeguridad", parametro5);
                        cmd.Parameters.AddWithValue("@Puesto", parametro6);
                        cmd.Parameters.AddWithValue("@IdStatus", parametro7);
                        cmd.Parameters.AddWithValue("@UsuarioLogin", parametro8);
                        cmd.Parameters.AddWithValue("@Contraseña", parametro9);


                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el stored procedure y obtener el DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            int returnValue = Convert.ToInt32(returnParameter.Value);
                            _logger.LogInformation("valor de return value: " + returnValue.ToString());
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

                _logger.LogInformation("error: " + e.Message.ToString());
            }
            return respuesta;
        }

        public bool eliminarUsuario(int id)
        {

            _logger.LogInformation("Metodo eliminarUsuario");
            bool respuesta = false;
            try
            {
                string connectionString = configuration.GetConnectionString("MiConexionBD");

                // Nombre del stored procedure
                string storedProcedureName = "sp_EliminarUsuarioPorID";

                _logger.LogInformation("store a ejecutar  : " + storedProcedureName);
                // Parámetros del stored procedure
                int parametro1 = id;
             

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@UserID", parametro1);
                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el stored procedure y obtener el DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            int returnValue = Convert.ToInt32(returnParameter.Value);
                            _logger.LogInformation("valor de return value: " + returnValue.ToString());
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

                _logger.LogInformation("error: " + e.Message.ToString());
            }
            return respuesta;
        }


        public bool registroStatus(StatusCLS datos)
        {

            _logger.LogInformation("Metodo registroStatus");
            bool respuesta = false;
            string connectionString = configuration.GetConnectionString("MiConexionBD"); // Reemplaza con tu cadena de conexión
            _logger.LogInformation("cadena de conexion : " + connectionString);
            try
            {
                // Nombre del stored procedure
                string storedProcedureName = "sp_RegistraStatus";

                _logger.LogInformation("store a ejecutar  : " + storedProcedureName);
                // Parámetros del stored procedure
                string parametro1 = datos.NombreStatus;
                string parametro2 = datos.descripcion;
              

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();
                    _logger.LogInformation("se abrio la conexion exitosamente ");

                    // Crear el comando con el nombre del stored procedure y la conexión
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que el comando es un stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al stored procedure
                        cmd.Parameters.AddWithValue("@NombreStatus", parametro1);
                        cmd.Parameters.AddWithValue("@descripcion", parametro2);
                       

                        _logger.LogInformation("parametros: " + cmd.Parameters.ToString());

                        // Agregar el parámetro de retorno (return value)
                        SqlParameter returnParameter = cmd.Parameters.Add("Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el stored procedure y obtener el DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            int returnValue = Convert.ToInt32(returnParameter.Value);
                            _logger.LogInformation("valor de return value: " + returnValue.ToString());
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
                _logger.LogInformation("valor de return value: " + e.Message.ToString());
                respuesta = false;
            }

            return respuesta;


        }

    }
}
