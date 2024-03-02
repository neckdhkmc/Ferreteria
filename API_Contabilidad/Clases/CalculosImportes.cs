using API_Contabilidad.Interfaces;
using API_Contabilidad.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Clases
{
    public class CalculosImportes
    {
        private readonly IRepository<Productos> _productosRepository;

        public CalculosImportes(IRepository<Productos> ProductosRepository )
        {
            _productosRepository = ProductosRepository;
           
        }

        public void PrecioUnitario(VentaRequest datos)
        {
            ////validar si el producto tiene descuento 

            foreach (var detalle in datos.detalles)
            {
               
                // Obtener el precio unitario original del producto
                
                var precioUnitarioOriginal = _productosRepository.GetPrecioUnitarioProducto(detalle.IdProducto);

                // Obtener el descuento aplicable al producto
                var descuento = _productosRepository.GetById(detalle.IdProducto).Descuento;
                decimal descuentoDecimal = descuento / 100m;

                var cantidadDescontar = precioUnitarioOriginal * descuentoDecimal;

                // Calcular el precio unitario con descuento
                var precioUnitarioConDescuento = precioUnitarioOriginal - cantidadDescontar;

                // Asignar el precio unitario con descuento al detalle de venta
                detalle.PrecioUnitario = precioUnitarioConDescuento;
            }


        }

        public void calcularTotal(VentaRequest datos)
        {
            decimal total = 0;
            foreach (var detalle in datos.detalles)
            {
                total += detalle.Importe;
            }
            datos.datosVenta.Total = total;
        }

        public void calcularimporte(VentaRequest datos)
        {
            foreach (var detalle in datos.detalles)
            {
                detalle.Importe = detalle.cantidad * detalle.PrecioUnitario;
            }

        }       

        public void AplicarDescuento(VentaRequest datos)
        {


        }

       



}
}
