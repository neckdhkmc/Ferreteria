using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CLASES
{
    public class DatosProducto
    {
        public string IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioMayoreo { get; set; }
        public int Cantidad { get; set; }
        public int IdMarca { get; set; }
        public string CodigoBarra { get; set; }
        public int IdStatus { get; set; }
        public int IdCategoria { get; set; }
        public string UnidadMedida { get; set; }
    }
}
