using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Modelos
{
    public class ProductosV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioMayoreo { get; set; }
        public int Cantidad { get; set; }
        public string CodigoBarras { get; set; }

        public string UnidadMedida { get; set; }
        public string NombreCategoria { get; set; }
      
        public string NombreMarca { get; set; }
        public string Estado { get; set; }
       

    }
}
