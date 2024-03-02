using API_Contabilidad.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Clases
{
    public class VentasValidator
    {
        public static void ValidateVenta(Ventas venta)
        {
          

            if (string.IsNullOrEmpty(venta.Folio))
            {
                throw new ArgumentException("El folio de la venta no puede estar vacío.");
            }
            if (venta.Fecha == default(DateTime))
            {
                throw new ArgumentException("La fecha de la venta no es válida.");
            }
            if (venta.Total <= 0)
            {
                throw new ArgumentException("El total de la venta debe ser mayor que cero.");
            }
            if (venta.IdCliente <= 0)
            {
                throw new ArgumentException("El ID del cliente no es válido.");
            }
            if (venta.IdEmpleado <= 0)
            {
                throw new ArgumentException("El ID del empleado no es válido.");
            }
        }
        public static void ValidateDetalles(List<DetalleVenta> detalles)
        {
            if (detalles == null || !detalles.Any())
            {
                throw new ArgumentException("La lista de detalles de venta está vacía.");
            }
            foreach (var detalle in detalles)
            {
                if (detalle.cantidad <= 0)
                {
                    throw new ArgumentException("La cantidad del detalle de venta debe ser mayor que cero.");
                }
                if (detalle.PrecioUnitario <= 0)
                {
                    throw new ArgumentException("El precio unitario del detalle de venta debe ser mayor que cero.");
                }
                if (detalle.Importe <= 0)
                {
                    throw new ArgumentException("El importe del detalle de venta debe ser mayor que cero.");
                }
                if (detalle.IdProducto <= 0)
                {
                    throw new ArgumentException("El ID del producto en el detalle de venta no es válido.");
                }
            }
        }

    }
}
