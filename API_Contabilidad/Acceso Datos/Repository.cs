using API_Contabilidad.Clases;
using API_Contabilidad.Interfaces;
using API_Contabilidad.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Acceso_Datos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyDbContext _context;

        public Repository(MyDbContext context)
        {
            _context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public bool Add(T entity)
        {

            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí si es necesario
                Console.WriteLine("Error al realizar el registro: " + ex.Message);
                return false;
            }
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        //metodo para registrar con store
        public IEnumerable<T> ExecuteStoredProcedure(string storedProcedureName, params object[] parameters)
        {
            var rf = _context.Database.SqlQuery<T>(storedProcedureName, parameters);
            // Ejecutar el stored procedure y devolver los resultados
            return rf;
        }

        //metodo para ejecutar store y retornar el mensaje y codigo de error 
        public ResponseGeneral ExecuteStoredProcedureNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            var respuesta = new ResponseGeneral();


            try
            {
                // Añade un parámetro de salida para capturar el valor de retorno del procedimiento almacenado
                var returnMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                var returnCodigoRetorno = new SqlParameter("@CodigoRetorno", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Agrega los parámetros de salida a la lista de parámetros
                var parametersList = new List<SqlParameter>(parameters);
                parametersList.Add(returnMensaje);
                parametersList.Add(returnCodigoRetorno);

                // Convierte la lista de parámetros de nuevo a un array
                parameters = parametersList.ToArray();

                // Ejecuta el procedimiento almacenado
                _context.Database.ExecuteSqlCommand(storedProcedureName, parameters);

                // Obtiene el valor de retorno del procedimiento almacenado
                var codigoRetorno = (int)returnCodigoRetorno.Value;
                var mensaje = (string)returnMensaje.Value;

                // Establece los valores de retorno en la respuesta
                respuesta.codigo = codigoRetorno;
                respuesta.Mensaje = mensaje;
            }
            catch (Exception e)
            {

                // Maneja cualquier excepción y establece el código de retorno en 1 y el mensaje en el mensaje de error
                respuesta.codigo = 1;
                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }


        //metodo para ejecutar store y obtener lista de datos

        public (List<T> dataList, int codigoRetorno, string mensaje) ExecuteListData(string storedProcedureName, params SqlParameter[] parameters)
        {
            var dataList = new List<T>();
            var codigoRetorno = 0;
            var mensaje = "";

            try
            {
                // Añade un parámetro de salida para capturar el valor de retorno del procedimiento almacenado
                var returnMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                var returnCodigoRetorno = new SqlParameter("@CodigoRetorno", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Agrega los parámetros de salida a la lista de parámetros
                var parametersList = new List<SqlParameter>(parameters);
                parametersList.Add(returnMensaje);
                parametersList.Add(returnCodigoRetorno);

                // Convierte la lista de parámetros de nuevo a un array
                parameters = parametersList.ToArray();

                // Ejecuta el procedimiento almacenado
                dataList = _context.Database.SqlQuery<T>(storedProcedureName, parameters).ToList();

                // Obtiene el valor de retorno del procedimiento almacenado
                codigoRetorno = (int)returnCodigoRetorno.Value;
                mensaje = (string)returnMensaje.Value;
            }
            catch (Exception e)
            {
                // Maneja cualquier excepción y registra el error
                Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {e.Message}");
                codigoRetorno = 1;
                mensaje = e.Message;
            }

            return (dataList, codigoRetorno, mensaje);
        }

        //metodo para consumir cualquier vista  de sql 
        public IEnumerable<TResult> GetFromView<TResult>(string viewName) where TResult : class
        {
            // Utiliza el DbContext para consultar la vista

            var rf = _context.Database.SqlQuery<TResult>($"SELECT * FROM {viewName}").ToList();
            return rf;
        }

        public decimal GetLastInsertedId(string tableName)
        {
            // Consultar el último ID registrado en la tabla especificada
            var lastInsertedId = _context.Database.SqlQuery<decimal>($"SELECT IDENT_CURRENT('{tableName}')").SingleOrDefault();

            return lastInsertedId;
        }

        //manejo de transacciones
        public DbContextTransaction BeginTransaction()
        {
             return _context.Database.BeginTransaction();
        }

        //obtener el precio unitario 

        public decimal GetPrecioUnitarioProducto(int idProducto)
        {

            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.ID == idProducto);
                return producto != null ? Convert.ToDecimal(producto.PrecioUnitario) : 0m;
            }
            catch (Exception e)
            {
                var rf = e.InnerException.Message;
                throw;
            }
           
        }

      
    }
}
