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
    public class ProductosServices
    {
        private readonly IRepository<Productos> _productosRepository;
        public ProductosServices(IRepository<Productos> ProductosRepository)
        {
            _productosRepository = ProductosRepository;
        }

        public ResponseGeneral RegistrarProducto(Productos datos)
        {
            var respuesta = new ResponseGeneral();
            try
            {

                //obtener parametros 
                var idProducto = new SqlParameter("@idProducto", SqlDbType.VarChar) { Value = datos.IdProducto };
                var nombre = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = datos.Nombre };
                var descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = datos.Descripcion };
                var precioUnitario = new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = datos.PrecioUnitario };
                var precioMayoreo = new SqlParameter("@PrecioMayoreo", SqlDbType.Decimal) { Value = datos.PrecioMayoreo };
                var cantidad = new SqlParameter("@Cantidad", SqlDbType.Int) { Value = datos.Cantidad };
                var idMarca = new SqlParameter("@IdMarca", SqlDbType.Int) { Value = datos.IdMarca };
                var codigoBarras = new SqlParameter("@CodigoBarra", SqlDbType.VarChar) { Value = datos.CodigoBarras };
                var idStatus = new SqlParameter("@IdStatus", SqlDbType.Int) { Value = datos.IdStatus };
                var idCategoria = new SqlParameter("@idCategoria", SqlDbType.Int) { Value = datos.idCategoria };
                var unidadMedida = new SqlParameter("@UnidadMedida", SqlDbType.VarChar) { Value = datos.UnidadMedida };


                var result = _productosRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarProducto @idProducto, @Nombre, @Descripcion, @PrecioUnitario, @PrecioMayoreo, @Cantidad, @IdMarca, @CodigoBarra, @IdStatus, @IdCategoria, @UnidadMedida, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", idProducto, nombre, descripcion, precioUnitario, precioMayoreo, cantidad, idMarca, codigoBarras, idStatus, idCategoria, unidadMedida);

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
                respuesta.Mensaje = "Error al Registrar Producto";
            }

            return respuesta;
        
        }

        public ResponseProductos GetProductos()
        {
            var respuesta = new ResponseProductos();
            string nameViewSql = "Vista_Producto";

            try
            {
                var result = _productosRepository.GetFromView<ProductosV>(nameViewSql);
       
                if (result.Count() == 0)
                {
                    //error

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Ocurrio un error al obtener los productos";
                    respuesta.listaProductos = null;

                }
                else {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Se obtiene la lista de productos";
                    respuesta.listaProductos = result;
                }
            }
            catch (Exception e)
            {

                respuesta.listaProductos = null;
                respuesta.Codigo = 1;
                respuesta.Mensaje = "Error al obtener los productos";
            }

            return respuesta;
        
        }

        public ResponseGeneral EliminarProducto(Productos datos)
        {
            var respuesta = new ResponseGeneral();

            try
            {
                var idProducto = new SqlParameter("@IdProducto", SqlDbType.VarChar) { Value = datos.IdProducto };
                var result = _productosRepository.ExecuteStoredProcedureNonQuery("sp_ElimiarProductoID @IdProducto, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", idProducto);
                if (result.codigo == 0)
                {
                    respuesta.codigo = result.codigo;
                    respuesta.Mensaje = result.Mensaje;
                }
                else {
                    respuesta.codigo = result.codigo;
                    respuesta.Mensaje = result.Mensaje;
                }
            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.Mensaje = "Ocurrio  un error al eliminar un producto";
            }

            return respuesta;

        
        }

        public ResponseGeneral ActualizarProducto(Productos datos)
        {
            var respuesta = new ResponseGeneral();

            try
            {
                //obtener parametros 
                var ID = new SqlParameter("ID", SqlDbType.Int) { Value = datos.ID };
                var idProducto = new SqlParameter("@idProducto", SqlDbType.VarChar) { Value = datos.IdProducto };
                var nombre = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = datos.Nombre };
                var descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = datos.Descripcion };
                var precioUnitario = new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = datos.PrecioUnitario };
                var precioMayoreo = new SqlParameter("@PrecioMayoreo", SqlDbType.Decimal) { Value = datos.PrecioMayoreo };
                var cantidad = new SqlParameter("@Cantidad", SqlDbType.Int) { Value = datos.Cantidad };
                var idMarca = new SqlParameter("@IdMarca", SqlDbType.Int) { Value = datos.IdMarca };
                var codigoBarras = new SqlParameter("@CodigoBarra", SqlDbType.VarChar) { Value = datos.CodigoBarras };
                var idStatus = new SqlParameter("@IdStatus", SqlDbType.Int) { Value = datos.IdStatus };
                var idCategoria = new SqlParameter("@idCategoria", SqlDbType.Int) { Value = datos.idCategoria };
                var unidadMedida = new SqlParameter("@UnidadMedida", SqlDbType.VarChar) { Value = datos.UnidadMedida };

                var result = _productosRepository.ExecuteStoredProcedureNonQuery("sp_ActualizarProducto @ID, @idProducto, @Nombre, @Descripcion, @PrecioUnitario, @PrecioMayoreo, @Cantidad, @IdMarca, @CodigoBarra, @IdStatus, @IdCategoria, @UnidadMedida, @Mensaje OUTPUT, @CodigoRetorno OUTPUT",ID, idProducto, nombre, descripcion, precioUnitario, precioMayoreo, cantidad, idMarca, codigoBarras, idStatus, idCategoria, unidadMedida);

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
            catch (Exception)
            {
                respuesta.codigo = 1;
                respuesta.Mensaje = "Error al actualizar el producto";
            }
            return respuesta;
        }
    }
}
