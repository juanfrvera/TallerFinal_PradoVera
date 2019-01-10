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
        BlanqueoPIN blanqueoPIN;

        public Operaciones(string nombreCliente)
        {
            InitializeComponent();
            labelNombre.Text = "Bienvenido, " + nombreCliente;
            this.CenterToScreen();
        }

        private void buttonBlanqueo_Click(object sender, EventArgs e)
        {
            if (blanqueoPIN == null)
                blanqueoPIN = new BlanqueoPIN();

            blanqueoPIN.Show();
        }

        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
            //Si el usuario presiona 'OK' en el mensaje mostrado, se cierra la aplicacion
            if (MessageBox.Show("Está seguro de cerrar la sesión actual?", "Cerrar sesión",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
                Application.Exit();
        }
    }
}
