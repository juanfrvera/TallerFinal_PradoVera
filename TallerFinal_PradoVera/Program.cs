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
        private static string dniCliente;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            controlador = new Controlador();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        public static void Login(string pDni, string pClave)
        {
            try
            {
                ClienteDTO cliente = controlador.Login(pDni, pClave);
                dniCliente = pDni;//Si llegamos a este punto, no hubo una excepcion anteriormente
                //Crea la ventana y la muestra pero no guarda una referencia hacia ella
                (new InterfazUsuario.Operaciones(cliente.Nombre)).Show();
            }
            catch (DAL.Excepciones.ClienteNoEncontrado)
            {
                dniCliente = "";
                throw;
            }
        }
    }
}
