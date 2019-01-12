using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera
{
    interface IServicios
    {
        ClienteDTO ValidarCliente(String pDni, String pClave);
        IList<ProductoDTO> ObtenerProductos(String pDni);
        void BlanquearPin(String pNumeroTarjeta);
        double SaldoCC(String pDni);
        IList<MovimientoDTO> UltimosMovimientos(String pDni);
    }
}
