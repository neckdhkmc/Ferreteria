using API_Administracion.CLASES;
using API_Administracion.Interfaces;
using API_Administracion.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CAPA_DATOS
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

        //metodo para consumir cualquier vista  de sql 
        public IEnumerable<TResult> GetFromView<TResult>(string viewName) where TResult : class
        {
            // Utiliza el DbContext para consultar la vista

            var rf = _context.Database.SqlQuery<TResult>($"SELECT * FROM {viewName}").ToList();
            return rf;
        }


    }
}