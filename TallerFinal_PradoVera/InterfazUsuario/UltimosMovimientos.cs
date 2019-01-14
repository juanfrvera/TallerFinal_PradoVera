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
    public partial class UltimosMovimientos : Form
    {
        public UltimosMovimientos(IList<MovimientoDTO> movimientos)
        {
            InitializeComponent();
            this.CenterToScreen();
            if (movimientos.Count > 0)
                CargarMovimientos(movimientos);
            else
                MessageBox.Show("No hay movimientos registrados.", "Sin movimientos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarMovimientos(IList<MovimientoDTO> movimientos)
        {
            foreach (var mov in movimientos)
            {
                string[] subItems = { mov.Monto.ToString() };
                listView1.Items.Add(mov.Fecha.ToString(), 0).SubItems.AddRange(subItems);
            }
        }
        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
