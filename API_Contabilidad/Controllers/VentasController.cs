using API_Contabilidad.Clases;
using API_Contabilidad.Logica_Negocio;
using API_Contabilidad.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VentasController : ControllerBase
    {
        private readonly VentasServices _ventasServices;
        public VentasController(VentasServices VentasServices)
        {
            _ventasServices = VentasServices;
        }
        //[Authorize]
        [HttpPost("RegistrarVenta")]
        public IActionResult RegistrarVenta(VentaRequest datos)
        {
            var respuesta = _ventasServices.RegistrarVenta(datos);

            if (respuesta.codigo == 0)
            {
                // Si la venta se registra correctamente, devuelve un código 200 (OK)
                return Ok(respuesta);
            }
            else
            {
                // Si hay un error en el proceso, devuelve un código 500 (Error interno del servidor) con el mensaje de error
                return StatusCode(500, respuesta);
            }
        }

        //[Authorize]
        [HttpPost("ConsultarVentas")]
        public IActionResult ConsultarVentas(InfoVentas datos)
        {
            var respuesta = _ventasServices.ConsultarVentas(datos);
            if (respuesta.Codigo == 0)
            {
                // Si la venta se registra correctamente, devuelve un código 200 (OK)
                return Ok(respuesta);
            }
            else
            {
                // Si hay un error en el proceso, devuelve un código 500 (Error interno del servidor) con el mensaje de error
                return StatusCode(500, respuesta);
            }

        }


    }
}
