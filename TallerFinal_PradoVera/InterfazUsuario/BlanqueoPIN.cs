using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TallerFinal_PradoVera.InterfazUsuario
{
   public partial class BlanqueoPIN : Form
   {
      private string iDniActual;

      // Cantidad de productos que seran mostrados al cliente, si la cantidad de productos
      // que el cliente tiene supera esta cantidad, se mostrara un boton "Mostrar más..."
      private uint muestrasMaximas = 3;
      private int tarjetaSeleccionada = 0;
      private IList<ProductoDTO> productos;

      public BlanqueoPIN(string pDni, IList<ProductoDTO> productos)
      {
         InitializeComponent();
         this.CenterToScreen();
       
         this.iDniActual = pDni;

         CargarProductos(productos);
      }

      private void CargarProductos(IList<ProductoDTO> productos)
      {
         this.productos = productos;

         // Hay uno ya creado porque se asume que la lista de productos al menos tiene uno
         radioButton1.Text = productos[0].Nombre;
         int x = radioButton1.Location.X;
         int ultimoY = radioButton1.Location.Y;
         int altura = radioButton1.Size.Height;
         for (int i = 1; i < Math.Min(productos.Count, muestrasMaximas); i++)
         {
            RadioButton rad = new RadioButton();
            rad.Location = new Point(x, ultimoY + altura);
            rad.Text = productos[i].Nombre;
            rad.Tag = i.ToString();

            groupBox1.Controls.Add(rad);

            ultimoY = rad.Location.Y;

            rad.CheckedChanged += Rad_CheckedChanged;
         }
         groupBox1.Size = new Size(groupBox1.Size.Width,
                     (int)(altura * (Math.Min(productos.Count, muestrasMaximas) + 2)));
      }

      /// <summary>
      /// Es llamado cuando se elige otra tarjeta
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void Rad_CheckedChanged(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
         tarjetaSeleccionada = int.Parse(((RadioButton)sender).Tag.ToString());
      }

      private void buttonCancelar_Click(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
         Cancelar();
      }

      private void buttonBlanquear_Click(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
         ProductoDTO tarjeta = productos[tarjetaSeleccionada];
         if (MessageBox.Show("Está seguro de blanquear el PIN de la tarjeta \"" + tarjeta.Nombre + "\"?", "Blanquear PIN",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
         {
            try
            {

               Program.BlanquearPin(iDniActual, tarjeta.Numero);
               MessageBox.Show("El PIN de la tarjeta " + tarjeta.Nombre + " ha sido blanqueado.", "Blanqueo de PIN",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
               Aceptar();
            }
            catch (DAL.Excepciones.ErrorAlBlanquearPin exc)
            {
               MessageBox.Show("Ha ocurrido un error al intentar blanquear el PIN." +
                           Environment.NewLine + "Descripción de error: " + exc.descripcion, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DAL.Excepciones.ErrorDeConexion)
            {
               MessageBox.Show("Hubo un error de conexión, por favor intente de nuevo más tarde.", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc2)
            {
               MessageBox.Show("Hubo un error inesperado, por favor informe a un operador." +
                           Environment.NewLine + "Codigo de error: " + exc2.HResult.ToString(), "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

      }

      private void ClickEnVentana(object sender, EventArgs e)
      {
         Operaciones.AccionRealizada();
      }
      private void Aceptar()
      {
         this.Hide();
      }
      private void Cancelar()
      {
         this.Hide();
      }
   }
}
