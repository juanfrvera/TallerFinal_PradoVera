using System;
using System.Collections.Generic;
using Autoservicio.IO;



namespace TallerFinal_PradoVera
{
    class Controlador
    {
        private IServicios iAdaptadorServicios;
		
		private Vitacora iVitacora;

        private string iDNIActual;

        public Controlador()
        {
            iAdaptadorServicios = new AdaptadorServicios();
			iVitacora = new Vitacora();
        
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
            return iAdaptadorServicios.ValidarCliente(pDni, pClave);
        }
        public IList<ProductoDTO> ObtenerProductos()
        {
            return iAdaptadorServicios.ObtenerProductos(iDNIActual);
        }
        #region Operaciones de usuario validado
        /// <summary>
        /// Blanquea el PIN de la tarjeta cuyo número es el indicado
        /// </summary>
        /// <param name="numeroTarjeta"></param>
        public void BlanquearPin(string numeroTarjeta)
        {
            iAdaptadorServicios.BlanquearPin(numeroTarjeta);
        }
        /// <summary>
        /// Devuelve el saldo de la cuenta corriente del cliente actual
        /// </summary>
        /// <returns></returns>
        public double SaldoCC()
        {
            return iAdaptadorServicios.SaldoCC(iDNIActual);
        }
        /// <summary>
        /// Lista con los últimos movimientos
        /// </summary>
        /// <returns></returns>
        public IList<MovimientoDTO> UltimosMovimientos()
        {
            return iAdaptadorServicios.UltimosMovimientos(iDNIActual);
        }
        #endregion Operaciones de usuario validado
        /// <summary>
        /// Registra operación en la base de datos guardando la descripción y el tiempo que se tardó en operar
        /// </summary>
        /// <param name="pDescripcion"></param>
        /// <param name="pTiempo"></param>
       

    
    }
}
