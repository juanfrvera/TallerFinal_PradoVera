using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TallerFinal_PradoVera
{
    public partial class Login : Form
    {
        private ToolTip toolTipClave;
        public Login()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void ClaveToolTip(object sender, EventArgs e)
        {
            if (toolTipClave == null)
            {
                toolTipClave = new ToolTip();
            }
            toolTipClave.Show("Clave de HomeBanking", (Control)sender);
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Login(textBoxDNI.Text, textBoxClave.Text);
                this.Hide();//Se cierra pues no hubo excepcion
                //Se usa Hide() porque al usar Close() y no haber otro Form abierto, se cierra la app
                //Y ademas volveremos a abrir esta ventana al cerrar sesion


                //Se limpian los campos para posterior reuso
                textBoxDNI.Text = "";
                textBoxClave.Text = "";
            }
            catch (DAL.Excepciones.ClienteNoEncontrado)
            {
                MessageBox.Show("El DNI o clave ingresados son incorrectos");
            }
        }
    }
}
