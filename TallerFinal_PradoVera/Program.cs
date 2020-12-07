using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TallerFinal_PradoVera
{
   public static class Program
   {
      private static Controlador controlador;

      // Guardamos la referencia para luego abrirlo al cerrar sesion
      private static Ingreso login;

      // Constructor estático
      static Program()
      {
         controlador = new Controlador();
      }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         // Debe ser llamado antes de crear cualquier ventana
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         login = new Ingreso();

         Application.Run(login);
      }

      /// <summary>
      /// Tratar de ingresar con el dni y la clave
      /// </summary>
      /// <param name="pDni"></param>
      /// <param name="pClave"></param>
      public static ClienteDTO Ingresar(string pDni, string pClave)
      {
         return controlador.Login(pDni, pClave);
      }
      public static IList<ProductoDTO> ObtenerProductos(string pDni)
      {
         return controlador.ObtenerProductos(pDni);
      }

      public static void BlanquearPin(string pDni, string pNumeroTarjeta)
      {
         controlador.BlanquearPin(pDni, pNumeroTarjeta);
      }
      public static double SaldoCC(string pDni)
      {
         return controlador.SaldoCC(pDni);
      }
      public static IList<MovimientoDTO> UltimosMovimientos(string pDni)
      {
         return controlador.UltimosMovimientos(pDni);
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