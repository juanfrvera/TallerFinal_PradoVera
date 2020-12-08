using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TallerFinal_PradoVera.InterfazUsuario
{
   public partial class Operaciones : Form
   {
      private static Timer timer;

      private string iDniActual;

      // Se guarda una referencia para no tener que volver a crearla si se sale y se vuelve a entrar
      private BlanqueoPIN blanqueoPIN;
      private UltimosMovimientos ultimosMovimientos;
      
      // Se guarda la lista de productos para no tener que consultar varias veces
      private IList<ProductoDTO> productos;

      public Operaciones(string dniCliente, string nombreCliente)
      {
         InitializeComponent();
         labelNombre.Text = "Bienvenido, " + nombreCliente;
         this.CenterToScreen();

         this.iDniActual = dniCliente;

         try
         {
            productos = Program.ObtenerProductos(dniCliente);
         }
         catch (DAL.Excepciones.ClienteNoTieneProductos)
         {
            buttonBlanqueo.Enabled = false;
            labelSinTarjetas.Visible = true;
         }
         catch (DAL.Excepciones.ErrorDeConexion)
         {
            MessageBox.Show("Hubo un error de conexión, por favor intente de nuevo más tarde.");
         }
         catch (Exception exc)
         {
            MessageBox.Show("Hubo un error inesperado, por favor informe a un operador." +
                Environment.NewLine + "Codigo de error: " + exc.HResult.ToString());
         }
         timer = timer1;
         timer1.Start();
      }

      private void buttonBlanqueo_Click(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
         if (blanqueoPIN == null)
         {
            blanqueoPIN = new BlanqueoPIN(iDniActual, productos);
         }

         blanqueoPIN.ShowDialog();
      }
      private void ultimosMov_Click(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
         try
         {
            if (ultimosMovimientos == null)
            {
               ultimosMovimientos = new UltimosMovimientos(Program.UltimosMovimientos(iDniActual));
            }
            ultimosMovimientos.ShowDialog();
         }
         catch (DAL.Excepciones.ErrorDeConexion)
         {
            MessageBox.Show("Hubo un error de conexión, por favor intente de nuevo más tarde.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         catch (NullReferenceException)
         {
            MessageBox.Show("No hay últimos movimientos", "Últimos movimientos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
         catch (Exception exc)
         {
            MessageBox.Show("Hubo un error inesperado, por favor informe a un operador." +
                        Environment.NewLine + "Codigo de error: " + exc.HResult.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void CerrarSesion()
      {
         Program.CerrarSesion();
         this.Close();
      }

      private void buttonCerrarSesion_Click(object sender, EventArgs e)
      {
         // Si el usuario presiona 'OK' en el mensaje mostrado, se cierra la aplicacion
         if (MessageBox.Show("Está seguro de cerrar la sesión actual?", "Cerrar sesión",
             MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
         {
            CerrarSesion();
         }
         else
            Operaciones.AccionRealizada();
      }

      private void balance_Click(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
         try
         {
            double balance = Program.SaldoCC(iDniActual);
            if (MessageBox.Show("El saldo de su cuenta corriente es: $" + balance.ToString(), "Saldo",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
               Operaciones.AccionRealizada();
            }
         }
         catch (DAL.Excepciones.ErrorAlConsultarSaldo)
         {
            MessageBox.Show("Hubo un error al consultar el saldo, por favor intente de nuevo más tarde.");
         }
         catch (DAL.Excepciones.ErrorDeConexion)
         {
            MessageBox.Show("Hubo un error de conexión, por favor intente de nuevo más tarde.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         catch (Exception exc)
         {
            MessageBox.Show("Hubo un error inesperado, por favor informe a un operador." +
                        Environment.NewLine + "Codigo de error: " + exc.HResult.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      public static void AccionRealizada()
      {
         timer.Stop();
         timer.Start();
      }

      /// <summary>
      /// Este metodo se ejecuta cuando pasan 60 segundos sin actividad
      /// Es llamado por el timer
      /// </summary>
      private void Desconectar(object sender, EventArgs e)
      {
         CerrarSesion();
      }
      private void ClickEnVentana(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
      }
   }
}
