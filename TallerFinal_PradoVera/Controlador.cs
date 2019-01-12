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
        private string dniActual;

        public Controlador()
        {
            iAdaptador = new Adaptador();
        }
        /// <summary>
        /// Si el cliente no es encontrado se arroja una NullReferenceException
        /// Tambien puede haber una WebException si hay problemas al conectar con el servidor
        /// </summary>
        /// <param name="pDni"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public ClienteDTO Login(String pDni, String pClave)
        {
            dniActual = pDni;
            return iAdaptador.ValidarCliente(pDni, pClave);
        }
        public IList<ProductoDTO> ObtenerProductos()
        {
            return iAdaptador.ObtenerProductos(dniActual);
        }
        #region Operaciones de usuario validado
        public void BlanquearPin(string numeroTarjeta)
        {
            iAdaptador.BlanquearPin(numeroTarjeta);
        }

        #endregion
    }
}
