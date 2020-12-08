using Autoservicio.IO;
using System;
using System.Collections.Generic;

namespace TallerFinal_PradoVera
{
    interface IServicios
    {
        ClienteDTO ValidarCliente(String pDni, String pClave);
        IList<ProductoDTO> ObtenerProductos(String pDni);
        void BlanquearPin(String pNumeroTarjeta, string pDni);
        double SaldoCC(String pDni);
        IList<MovimientoDTO> UltimosMovimientos(String pDni);
    }
}
