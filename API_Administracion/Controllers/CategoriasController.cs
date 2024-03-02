using API_Administracion.CAPA_LOGICA;
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
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriasServices _categoriaService;
        public CategoriasController(CategoriasServices CategoriaServices)
        {
            _categoriaService = CategoriaServices;
        }
        [HttpPost("RegistrarCategoria")]
        public IActionResult RegistrarCategoria(Categorias datos)
        {
            try
            {
                var respuesta = _categoriaService.RegistrarCategoria(datos);
                return Ok(respuesta);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Ocurrió un error al intentar Registrar una categoria");
            }           

        }
        [HttpGet("GetCategorias")]
        public IActionResult GetCategorias()
        {
            try
            {
                var respuesta = _categoriaService.GetCategorias();
                return Ok(respuesta); 
            }
            catch (Exception e)
            {

                return StatusCode(500,"Mensaje:" + "Ocurrió un error al intentar consultar las categorias");
            }

        }

        [HttpDelete("EliminarCategoria")]

        public IActionResult EliminarCategoria(Categorias datos)
        {
            try
            {
                var response = _categoriaService.EliminarCategoria(datos);
                return Ok(response);
            }
            catch (Exception)
            {

                // Devuelve una respuesta de error
                return StatusCode(500, "Ocurrió un error al intentar eliminar una categoria");

            }

        }

        [HttpPut("ActualizarCategoria")]
        public IActionResult ActualizarCategoria(Categorias datos)
        {

            try
            {
                var respuesta = _categoriaService.ActualizarCategoria(datos);
                return Ok(respuesta);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Ocurrió un error al intentar Actualizar la categoria");
            }

        }
    }
}
