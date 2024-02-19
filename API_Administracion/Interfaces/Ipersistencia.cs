using API_Administracion.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.Interfaces
{
    public interface Ipersistencia
    {
        bool regitroProvedor(Datosprovedor datos);
        bool regitroMarca(DatosMarca datos);
        ResponseMarcaProvedores consultaMarcasPorProvedor(Datosprovedor datos);
        bool registrarCategoria(DatosGenericos datos);
        bool registrarProducto(DatosProducto datos);
    }
}
