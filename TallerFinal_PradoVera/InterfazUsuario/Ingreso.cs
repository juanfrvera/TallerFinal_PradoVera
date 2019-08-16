using System;
using System.Windows.Forms;

namespace TallerFinal_PradoVera
{
		public partial class Ingreso : Form
		{
				private ToolTip toolTip;
				public Ingreso()
				{
						InitializeComponent();
						this.CenterToScreen();
				}

				private void ClaveToolTip(object sender, EventArgs e)
				{
						if (toolTip == null)
						{
								toolTip = new ToolTip();
						}
						toolTip.Show("Clave de HomeBanking", (Control)sender);
				}

				/// <summary>
				/// Prepara la interfaz para el proximo login, de esta manera al volver a abrir esta ventana, no quedan guardados
				/// los datos del cliente anterior
				/// </summary>
				private void PrepararParaProximoUso()
				{
						textBoxDNI.Text = "";
						textBoxClave.Text = "";
						textBoxDNI.Focus();
				}
				private void buttonIngresar_Click(object sender, EventArgs e)
				{
						labelIngresando.Visible = true;
						try
						{
								Program.Ingresar(textBoxDNI.Text, textBoxClave.Text);
								this.Hide();//Se cierra pues no hubo excepcion
																				//Se usa Hide() porque al usar Close() y no haber otro Form abierto, se cierra la app
																				//Y ademas volveremos a abrir esta ventana al cerrar sesion


								//Se limpian los campos para posterior reuso
								PrepararParaProximoUso();
						}
						catch (DAL.Excepciones.ClienteNoEncontrado)
						{
								MessageBox.Show("El DNI o clave ingresados son incorrectos", "Ingreso incorrecto",
												MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
						labelIngresando.Visible = false;
				}

				private void textBoxClave_KeyDown(object sender, KeyEventArgs e)
				{
						//Si presiona la tecla enter ingresar
						if (e.KeyCode == Keys.Enter)
								buttonIngresar_Click(this, e);
				}
		}
}
