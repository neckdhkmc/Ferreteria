using API_Contabilidad.Clases;
using API_Contabilidad.Interfaces;
using API_Contabilidad.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Logica_Negocio
{
    public class VentasServices
    {
        private readonly IRepository<Ventas> _ventasRepository;
        private readonly CalculosImportes _calculosImportes;
        public VentasServices(IRepository<Ventas> _VentasRepository, CalculosImportes CalculosImportes)
        {
            _ventasRepository = _VentasRepository;
            _calculosImportes = CalculosImportes;
        }

        public ResponseGeneral RegistrarVenta(VentaRequest datos)
        {
            var respuesta = new ResponseGeneral();
            var nameTable = "Ventas";

            try
            {

                // Consumir el método PrecioUnitario
                _calculosImportes.PrecioUnitario(datos);

                //calcular descuentos

                //obtener los importes de los productos
                _calculosImportes.calcularimporte(datos);

                //obtener el total de la venta 

                _calculosImportes.calcularTotal(datos);

                //se consume clase para validaciones 
                VentasValidator.ValidateVenta(datos.datosVenta);
                VentasValidator.ValidateDetalles(datos.detalles);

            
                //se utiliza un bloque transaccional para asegurar de que se registre la venta y su detalle en caso contrario no se registra la venta

                using (var dbContextTransaction = _ventasRepository.BeginTransaction()) // Iniciar la transacción
                {
                    

                    // Registrar la venta principal
                    var Folio = new SqlParameter("@Folio", SqlDbType.VarChar) { Value = datos.datosVenta.Folio };
                    var fecha = new SqlParameter("@fecha", SqlDbType.DateTime) { Value = datos.datosVenta.Fecha };
                    var total = new SqlParameter("@total", SqlDbType.Decimal) { Value = datos.datosVenta.Total };
                   // var IdCliente = new SqlParameter("@IdCliente", SqlDbType.Int) { Value = datos.datosVenta.IdCliente };
                    var IdEmpleado = new SqlParameter("@IdEmpleado", SqlDbType.Int) { Value = datos.datosVenta.IdEmpleado };
                    
                    //se revisa si el cliente va nulo ya que la venta puede o no estar asociada con un cliente 
                    SqlParameter IdCliente;
                    if (datos.datosVenta.IdCliente != null)
                    {
                        IdCliente = new SqlParameter("@IdCliente", SqlDbType.Int) { Value = datos.datosVenta.IdCliente };
                    }
                    else
                    {
                        IdCliente = new SqlParameter("@IdCliente", DBNull.Value);
                    }
                   
                    var resultVenta = _ventasRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarVenta @Folio, @fecha, @total, @IdCliente, @IdEmpleado, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", Folio, fecha, total, IdCliente, IdEmpleado);

                    // Si hubo un error en la venta principal, revertir la transacción
                    if (resultVenta.codigo != 0)
                    {
                        dbContextTransaction.Rollback();
                        respuesta.codigo = resultVenta.codigo;
                        respuesta.Mensaje = resultVenta.Mensaje;
                        return respuesta;
                    }

                    var ultimoIdVenta = _ventasRepository.GetLastInsertedId(nameTable);

                    // Registrar el detalle de venta
                    foreach (var detalle in datos.detalles)
                    {
                        // Crear los parámetros para el detalle de venta
                        var idVenta = new SqlParameter("@IdVenta", SqlDbType.Int) { Value = ultimoIdVenta };
                        var cantidad = new SqlParameter("@Cantidad", SqlDbType.Int) { Value = detalle.cantidad };
                        var precioUnitario = new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = detalle.PrecioUnitario };
                        var importe = new SqlParameter("@Importe", SqlDbType.Decimal) { Value = detalle.Importe };
                        var idProducto = new SqlParameter("@IdProducto", SqlDbType.Int) { Value = detalle.IdProducto };

                        // Ejecutar el procedimiento almacenado para registrar el detalle de venta
                        var resultDetalle = _ventasRepository.ExecuteStoredProcedureNonQuery("sp_RegistrarDetalleVenta @IdVenta, @Cantidad, @PrecioUnitario, @Importe, @IdProducto, @Mensaje OUTPUT, @CodigoRetorno OUTPUT", idVenta, cantidad, precioUnitario, importe, idProducto);

                        // Si hubo un error en el detalle de venta, revertir la transacción
                        if (resultDetalle.codigo != 0)
                        {
                            dbContextTransaction.Rollback();
                            respuesta.codigo = resultDetalle.codigo;
                            respuesta.Mensaje = "Ocurrió un error al realizar la venta:"+ resultDetalle.Mensaje;
                            return respuesta;
                        }
                    }

                    // Confirmar la transacción si todo salió bien
                    dbContextTransaction.Commit();
                }

                // Si se llega aquí, la venta y el detalle de venta se registraron correctamente
                respuesta.codigo = 0;
                respuesta.Mensaje = "Venta y detalle de venta registrados correctamente";
            }
            catch (ArgumentException ex)
            {
                respuesta.codigo = 1;
                respuesta.Mensaje = "Error al validar los datos de entrada: " + ex.Message;
            }
            catch (SqlException ex)
            {
                respuesta.codigo = 1;
                respuesta.Mensaje = "Error de base de datos al realizar la venta: " + ex.Message;
            }
            catch (Exception e)
            {
                // Manejar excepciones
                respuesta.codigo = 1;
                respuesta.Mensaje = "Ocurrió un error al realizar la venta: " + e.Message;
            }

            return respuesta;
        }

        public ResponseVentas ConsultarVentas(InfoVentas datos)
        {
            var respuesta = new ResponseVentas();

            try
            {


                //obtener parametro 
                SqlParameter Folio = new SqlParameter("@Folio", SqlDbType.VarChar) { Value = datos.Folio != null ? datos.Folio : DBNull.Value };
                SqlParameter IdEmpleado = new SqlParameter("@idEmpleado", SqlDbType.Int) { Value = datos.IdEmpleado != 0 ? datos.IdEmpleado : DBNull.Value };
                SqlParameter idCliente = new SqlParameter("@idCliente", SqlDbType.Int) { Value = datos.IdCliente != 0 ? datos.IdCliente : DBNull.Value };

                SqlParameter fechaInicioParam = new SqlParameter("@FechaInicio", SqlDbType.DateTime);
                fechaInicioParam.Value = datos.FechaInicio != null ? (object)datos.FechaInicio : DBNull.Value;

                SqlParameter fechaFinParam = new SqlParameter("@FechaFin", SqlDbType.DateTime);
                fechaFinParam.Value = datos.FechaFin != null ? (object)datos.FechaFin : DBNull.Value;


                // Ejecutar el procedimiento almacenado para registrar el detalle de venta
                var resultDetalle = _ventasRepository.ExecuteListData("sp_ConsultaVentas @Folio, @idEmpleado, @idCliente, @FechaInicio, @FechaFin, @CodigoRetorno OUTPUT, @Mensaje OUTPUT", Folio, IdEmpleado, idCliente, fechaInicioParam, fechaFinParam);

                if (resultDetalle.codigoRetorno == 0)
                {
                    respuesta.listaVentas = resultDetalle.dataList;
                    respuesta.Codigo = resultDetalle.codigoRetorno;
                    respuesta.Mensaje = resultDetalle.mensaje;
                }
                else
                {
                    respuesta.listaVentas =null;
                    respuesta.Codigo = resultDetalle.codigoRetorno;
                    respuesta.Mensaje = resultDetalle.mensaje;
                }

            }
            catch (Exception e)
            {

                respuesta.listaVentas = null;
                respuesta.Codigo = 1;
                respuesta.Mensaje = "Datos no encontrados";
            }

            return respuesta;
                
        
        }

    }
}
