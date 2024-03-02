﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Modelos
{
    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioMayoreo { get; set; }
        public int Cantidad { get; set; }
        public int IdMarca { get; set; }
        public string CodigoBarras { get; set; }
        public int IdStatus { get; set; }
        public int idCategoria { get; set; }
        public string UnidadMedida { get; set; }
    }
}