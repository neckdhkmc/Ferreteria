using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Administracion : ControllerBase
    {

        #region provedores
        [HttpPost("RegistrarProvedor")]

        public bool RegistrarProvedor()
        {
            var respuesta = false;

            return respuesta;
        
        }
        #endregion

        #region marcas

        #endregion
        #region ventas 

        #endregion

        #region inventario

        #endregion



    }
}
