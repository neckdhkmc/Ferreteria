using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Modelos
{
    public class ProvedorMarca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Esto indica que la clave es generada automáticamente por la base de datos
        public int IdProvedor { get; set; }
        public string NombreProvedor { get; set; }
        public string nombreMarca { get; set; }
        public string Descripcion { get; set; }
        public int idStatus { get; set; }
    }
}
