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
    public class MarcaController : ControllerBase
    {
        //private readonly IRepository<Marca> _MarcaRepository;
        private readonly MarcaServices _marcaService;
        public MarcaController(MarcaServices MarcaRepository)
        {
            _marcaService = MarcaRepository;
        }
        [HttpPost("RegistrarMarca")]
        public IActionResult RegistrarMarca(Marca datos)
        {
            var response = _marcaService.RegistrarMarca(datos);
            return Ok(response);
        }
        [HttpGet("getMarca")]
        public IActionResult getMarca()
        {
            var response = _marcaService.getMarca();
            return Ok(response);
        }

        [HttpDelete("EliminarMarca")]
        public IActionResult EliminarMarca(Marca datos)
        {
            var response = _marcaService.EliminarMarca(datos);
            return Ok(response);
        }

    }
}
