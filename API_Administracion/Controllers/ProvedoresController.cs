using API_Administracion.CAPA_LOGICA;
using API_Administracion.CLASES;
using API_Administracion.Interfaces;
using API_Administracion.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvedoresController : ControllerBase
    {
        private readonly ProvedorService _provedorService;

        public ProvedoresController(ProvedorService ProvedorService)
        {
            _provedorService = ProvedorService;
        }

        [HttpPost("RegistrarProvedor")]
        public IActionResult RegistrarProvedor(Provedores datos)
        {
            try
            {
                var response = _provedorService.RegistrarProvedor(datos);
                return Ok(response);
            }
            catch (Exception)
            {
                // Registra el error en el sistema de registro
                // Logger.LogError($"Error al intentar eliminar la marca: {ex.Message}");

                // Devuelve una respuesta de error
                return StatusCode(500, "Ocurrió un error al intentar Registrar un provedor");
                
            }
           
        }


        [HttpGet("getProvedores")]
        public IActionResult getProvedores()
        {
            try
            {
                var response = _provedorService.getProvedores();
                return Ok(response);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocurrió un error al intentar obtener la lista de provedores");
            }
           
        }

        [HttpDelete("EliminarProvedor")]
        public IActionResult EliminarProvedor(Provedores datos)
        {
            try
            {
                var response = _provedorService.eliminarProvedor(datos);
                return Ok(response);
            }
            catch (Exception)
            {
               
                // Devuelve una respuesta de error
                return StatusCode(500, "Ocurrió un error al intentar eliminar un provedor");

            }

        }

        [HttpPut("ActualizarProvedor")]
        public IActionResult ActualizarProvedor(Provedores datos)
        {
            try
            {
                var response = _provedorService.ActualizarProvedor(datos);
                return Ok(response);
            }
            catch (Exception)
            {
                
                // Devuelve una respuesta de error
                return StatusCode(500, "Ocurrió un error al intentar actualizar un provedor");

            }

        }

        [HttpGet("getProvedorMarca")]
        public IActionResult getProvedorMarca()
        {
            try
            {
                var response = _provedorService.getProvedorMarca();
                return Ok(response);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocurrió un error al intentar obtener la lista de las marcas por provedor");
            }

        }



    }
}

