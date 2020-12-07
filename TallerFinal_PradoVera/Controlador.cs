using System;
using System.Collections.Generic;
using Autoservicio.IO;

namespace TallerFinal_PradoVera
{
   public class Controlador
   {
      private IServicios iAdaptadorServicios;

      public Controlador()
      {
         iAdaptadorServicios = new AdaptadorServicios();
      }

      /// <summary>
      /// Si el cliente no es encontrado se arroja una NullReferenceException
      /// Tambien puede haber una WebException si hay problemas al conectar con el servidor
      /// </summary>
      /// <param name="pDni">DNI de cliente</param>
      /// <param name="pClave">Clave de cliente</param>
      /// <returns></returns>
      public ClienteDTO Login(String pDni, String pClave)
      {
         return iAdaptadorServicios.ValidarCliente(pDni, pClave);
      }

      /// <summary>
      /// Devuelve el listado de productos
      /// </summary>
      /// <param name="pDni">DNI de cliente</param>
      /// <returns></returns>
      public IList<ProductoDTO> ObtenerProductos(string pDni)
      {
         return iAdaptadorServicios.ObtenerProductos(pDni);
      }
      #region Operaciones de usuario validado

      /// <summary>
      /// Blanquea el PIN de la tarjeta cuyo número es el indicado
      /// </summary>
      /// <param name="pDni">DNI de cliente</param>
      /// <param name="pNumeroTarjeta">Número de tarjeta que se quiere blanquear</param>
      public void BlanquearPin(string pDni, string pNumeroTarjeta)
      {
         iAdaptadorServicios.BlanquearPin(pNumeroTarjeta, pDni);
      }

      /// <summary>
      /// Devuelve el saldo de la cuenta corriente del cliente actual
      /// </summary>
      /// <param name="pDni">DNI de cliente</param>
      /// <returns></returns>
      public double SaldoCC(string pDni)
      {
         return iAdaptadorServicios.SaldoCC(pDni);
      }

      /// <summary>
      /// Lista con los últimos movimientos
      /// </summary>
      /// <param name="pDni">DNI de cliente</param>
      /// <returns></returns>
      public IList<MovimientoDTO> UltimosMovimientos(string pDni)
      {
         return iAdaptadorServicios.UltimosMovimientos(pDni);
      }
      #endregion Operaciones de usuario validado
   }
}
