using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TallerFinal_PradoVera
{
    static class Program
    {
        private static Controlador controlador;
        private static Login login;//Guardamos la referencia para luego abrirlo al cerrar sesion
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            controlador = new Controlador();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            login = new Login();
            Application.Run(login);
        }

        public static void Login(string pDni, string pClave)
        {
            try
            {
                ClienteDTO cliente = controlador.Login(pDni, pClave);
                //Crea la ventana y la muestra pero no guarda una referencia hacia ella
                (new InterfazUsuario.Operaciones(cliente.Nombre)).Show();
            }
            catch (DAL.Excepciones.ClienteNoEncontrado)
            {
                throw;
            }
        }
        public static IList<ProductoDTO> ObtenerProductos()
        {
            return controlador.ObtenerProductos();
        }

        public static void BlanquearPin(string numeroTarjeta)
        {
            controlador.BlanquearPin(numeroTarjeta);
        }
        public static double SaldoCC()
        {
            return controlador.SaldoCC();
        }
        public static IList<MovimientoDTO> UltimosMovimientos()
        {
            return controlador.UltimosMovimientos();
        }


        public static void RegistrarOperacion(string pDescripcion, TimeSpan pTiempo)
        {
            controlador.RegistrarOperacion(pDescripcion, pTiempo);
        }

        /// <summary>
        /// Cierra la sesion de un cliente y abre la ventana 
        /// de Login para que uno nuevo pueda entrar
        /// </summary>
        public static void CerrarSesion()
        {
            login.Show();
        }
    }
}
