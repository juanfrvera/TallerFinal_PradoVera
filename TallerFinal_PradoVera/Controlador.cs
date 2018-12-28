using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservicio.IO;

namespace TallerFinal_PradoVera
{
    class Controlador
    {
        private IServicios iAdaptador;

        public ClienteDTO Login(String pDni, String pClave)
        {
            return iAdaptador.ValidarCliente(pDni, pClave);
        }
    }
}
