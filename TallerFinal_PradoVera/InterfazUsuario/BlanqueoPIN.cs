using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TallerFinal_PradoVera.InterfazUsuario
{

    public partial class BlanqueoPIN : Form
    {
        //Cantidad de productos que seran mostrados al cliente, si la cantidad de productos
        //que el cliente tiene supera esta cantidad, se mostrara un boton "Mostrar mas"
        uint muestrasMaximas = 3;
        int tarjetaSeleccionada = 0;
        IList<ProductoDTO> productos;

        public BlanqueoPIN(IList<ProductoDTO> productos)
        {
            InitializeComponent();
            this.CenterToScreen();
            CargarProductos(productos);
        }
        private void CargarProductos(IList<ProductoDTO> productos)
        {
            this.productos = productos;
            //Hay uno ya creado porque se asume que la lista de productos al menos tiene uno
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
                (int)(altura * (Math.Min(productos.Count,muestrasMaximas) + 2)));
        }

        private void Rad_CheckedChanged(object sender, EventArgs e)
        {
            tarjetaSeleccionada = int.Parse(((RadioButton)sender).Tag.ToString());
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonBlanquear_Click(object sender, EventArgs e)
        {
            try
            {
                Program.BlanquearPin(productos[tarjetaSeleccionada].Numero);
                MessageBox.Show("El PIN de la tarjeta " + productos[tarjetaSeleccionada].Nombre + " ha sido blanqueado.");
                this.Hide();
            }
            catch (DAL.Excepciones.ErrorAlBlanquearPin)
            {
                MessageBox.Show("Ha ocurrido un error al intentar blanquear el PIN, por favor llame a un operador.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
