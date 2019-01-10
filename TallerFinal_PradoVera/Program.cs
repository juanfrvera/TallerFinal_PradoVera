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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            controlador = new Controlador();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Login(string pDni, string pClave)
        {
            ClienteDTO cliente = controlador.Login(pDni, pClave);
        }
    }
}
