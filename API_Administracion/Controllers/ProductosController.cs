using API_Administracion.CAPA_LOGICA;
using API_Administracion.CLASES;
using API_Administracion.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosServices _productosServices;

        public ProductosController(ProductosServices ProductosServices)
        {
            _productosServices = ProductosServices;
        }

        [HttpPost("RegistrarProducto")]
        public IActionResult RegistrarProducto(Productos datos)
        {
            try
            {
                var respuesta = _productosServices.RegistrarProducto(datos);
                return Ok(respuesta);
            }
            catch (Exception e)
            {


                return StatusCode(500, "Ocurrió un error al intentar Registrar producto");
            }

        }

        [HttpGet("GetProductos")]

        public IActionResult GetProductos()
        {
            try
            {
                var respuesta = _productosServices.GetProductos();
                return Ok(respuesta);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Ocurrió un error al intentar obtener la lista de productos");
            }
        }

        [HttpDelete("EliminarProducto")]
        public IActionResult EliminarProducto(Productos datos)
        {
            try
            {
                var respuesta = _productosServices.EliminarProducto(datos);
                return Ok(respuesta);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Ocurrió un error al intentar eliminar un prodtucto");
            }

        }
        [HttpPut("ActualizarProducto")]

        public IActionResult ActualizarProducto(Productos datos)
        {
            try
            {
                var respuesta = _productosServices.ActualizarProducto(datos);
                return Ok(respuesta);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Ocurrió un error al intentar eliminar un prodtucto");
            }

        }

    }

}
