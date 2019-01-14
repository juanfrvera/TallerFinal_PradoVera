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
        private string iDNIActual;

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
            iDNIActual = pDni;
            return iAdaptador.ValidarCliente(pDni, pClave);
        }
        public IList<ProductoDTO> ObtenerProductos()
        {
            return iAdaptador.ObtenerProductos(iDNIActual);
        }
        #region Operaciones de usuario validado
        public void BlanquearPin(string numeroTarjeta)
        {
            iAdaptador.BlanquearPin(numeroTarjeta);
        }
        /// <summary>
        /// Devuelve el saldo de la cuenta corriente del cliente actual
        /// </summary>
        /// <returns></returns>
        public double SaldoCC()
        {
            return iAdaptador.SaldoCC(iDNIActual);
        }
        public IList<MovimientoDTO> UltimosMovimientos()
        {
            return iAdaptador.UltimosMovimientos(iDNIActual);
        }
        #endregion
    }
}
