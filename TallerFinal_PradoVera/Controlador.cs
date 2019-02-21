using System;
using System.Collections.Generic;
using System.Linq;

using Autoservicio.IO;
using TallerFinal_PradoVera.DAL;
using TallerFinal_PradoVera.DAL.EntityFramework;


namespace TallerFinal_PradoVera
{
    class Controlador
    {
        private IServicios iAdaptador;
        private IRepositorioOperaciones iRepOperaciones;

        private string iDNIActual;
        private uint ultimoIDOperacion;

        public Controlador()
        {
            iAdaptador = new Adaptador();
            iRepOperaciones = new RepositorioOperaciones();

            #region Obtener ultimo id de operacion
            if (iRepOperaciones.Count() > 0)
                ultimoIDOperacion = (uint)iRepOperaciones.ObtenerTodos().Max(e => e.Id);
            else
                ultimoIDOperacion = 0;
            #endregion
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
        public void RegistrarOperacion(string pDescripcion, TimeSpan pTiempo)
        {
            try
            {
                int nuevoIdOperacion = (int)ultimoIDOperacion + 1;
                iRepOperaciones.Agregar(new Dominio.Operacion(nuevoIdOperacion, iDNIActual, pDescripcion, pTiempo));
                //Se hace de esta forma para que no se incremente el ultimoID si llega a fallar la linea de arriba
                ultimoIDOperacion = (uint)nuevoIdOperacion;
            }
            catch (Exception)//No hay throw para que no se interrumpa el flujo de operacion
            {
            }
        }
    }
}
