using System;
using System.Collections.Generic;
using Autoservicio.IO;

namespace TallerFinal_PradoVera
{
   class Controlador
   {
      private IServicios iAdaptadorServicios;
      private string iDniActual;

      public Controlador()
      {
         iAdaptadorServicios = new AdaptadorServicios();
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
         iDniActual = pDni;

         return iAdaptadorServicios.ValidarCliente(pDni, pClave);
      }
      public IList<ProductoDTO> ObtenerProductos()
      {
         return iAdaptadorServicios.ObtenerProductos(iDniActual);
      }
      #region Operaciones de usuario validado
      /// <summary>
      /// Blanquea el PIN de la tarjeta cuyo número es el indicado
      /// </summary>
      /// <param name="numeroTarjeta"></param>
      public void BlanquearPin(string numeroTarjeta)
      {
         iAdaptadorServicios.BlanquearPin(numeroTarjeta, iDniActual);
      }
      /// <summary>
      /// Devuelve el saldo de la cuenta corriente del cliente actual
      /// </summary>
      /// <returns></returns>
      public double SaldoCC()
      {
         return iAdaptadorServicios.SaldoCC(iDniActual);
      }
      /// <summary>
      /// Lista con los últimos movimientos
      /// </summary>
      /// <returns></returns>
      public IList<MovimientoDTO> UltimosMovimientos()
      {
         return iAdaptadorServicios.UltimosMovimientos(iDniActual);
      }
      #endregion Operaciones de usuario validado
   }
}
