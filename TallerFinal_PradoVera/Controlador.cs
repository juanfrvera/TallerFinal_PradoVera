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


        /// <summary>
        /// Si el cliente no es encontrado se arroja una NullReferenceException
        /// Tambien puede haber una WebException si hay problemas al conectar con el servidor
        /// </summary>
        /// <param name="pDni"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public ClienteDTO Login(String pDni, String pClave)
        {
            return iAdaptador.ValidarCliente(pDni, pClave);
        }
    }
}
