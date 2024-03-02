using API_Contabilidad.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //obtener por  id
        T GetById(int id);
        //obtener lista de registros 
        IEnumerable<T> GetAll();
        //agregar registro 
        bool Add(T entity);
        //actualizar registro
        void Update(T entity);
        //eliminar registro
        void Delete(T entity);

        //consumo de stores 
        IEnumerable<T> ExecuteStoredProcedure(string storedProcedureName, params object[] parameters);

        //ejecutar store procedures que retornan un mensaje y un codigo
        ResponseGeneral ExecuteStoredProcedureNonQuery(string storedProcedureName, params SqlParameter[] parameters);

        // Agrega el método para consultar cualquier vista
        IEnumerable<TResult> GetFromView<TResult>(string viewName) where TResult : class;

        //obtiene el ultimo id registrado de cualquier tabla
        decimal GetLastInsertedId(string tableName);

        //manejo de transaccion con repositorio 
        DbContextTransaction BeginTransaction();
        
        //obtiene el precio unitario de cada producto
        decimal GetPrecioUnitarioProducto(int idProducto);

        //obtener lista de una entidad junto con mensaje y codigo de retorno
        (List<T> dataList, int codigoRetorno, string mensaje) ExecuteListData(string storedProcedureName, params SqlParameter[] parameters);
    }
}
