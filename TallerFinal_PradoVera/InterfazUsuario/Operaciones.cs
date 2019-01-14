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
    public partial class Operaciones : Form
    {
        //Se guarda una referencia para no tener que volver a crearla si se sale y se vuelve a entrar
        private BlanqueoPIN blanqueoPIN;
        private UltimosMovimientos ultimosMovimientos;
        //Se guarda la lista de productos para no tener que consultar varias veces
        private IList<ProductoDTO> productos;


        public Operaciones(string nombreCliente)
        {
            InitializeComponent();
            labelNombre.Text = "Bienvenido, " + nombreCliente;
            this.CenterToScreen();
            try
            {
                productos = Program.ObtenerProductos();
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
        }

        private void buttonBlanqueo_Click(object sender, EventArgs e)
        {
            if (blanqueoPIN == null)
                blanqueoPIN = new BlanqueoPIN(productos);

            blanqueoPIN.Show();
        }

        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
            //Si el usuario presiona 'OK' en el mensaje mostrado, se cierra la aplicacion
            if (MessageBox.Show("Está seguro de cerrar la sesión actual?", "Cerrar sesión",
                MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Program.CerrarSesion();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double balance = Program.SaldoCC();
                MessageBox.Show("El saldo de su cuenta corriente es: $" + balance.ToString(),"Saldo",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (DAL.Excepciones.ErrorAlConsultarSaldo)
            {
                MessageBox.Show("Hubo un error al consultar el saldo, por favor intente de nuevo más tarde.");
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ultimosMovimientos == null)
                ultimosMovimientos = new UltimosMovimientos(Program.UltimosMovimientos());

            ultimosMovimientos.Show();
        }
    }
}
