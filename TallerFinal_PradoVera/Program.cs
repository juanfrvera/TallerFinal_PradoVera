using Autoservicio.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TallerFinal_PradoVera
{
		static class Program
		{
				private static Controlador controlador;
				private static Ingreso login;//Guardamos la referencia para luego abrirlo al cerrar sesion

				/// <summary>
				/// The main entry point for the application.
				/// </summary>
				[STAThread]
				static void Main()
				{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);//Debe ser llamado antes de crear cualquier ventana

						controlador = new Controlador();
						login = new Ingreso();

						Application.Run(login);
				}

				/// <summary>
				/// Tratar de ingresar con el dni y la clave
				/// </summary>
				/// <param name="pDni"></param>
				/// <param name="pClave"></param>
				public static void Ingresar(string pDni, string pClave)
				{
						try
						{
								ClienteDTO cliente = controlador.Login(pDni, pClave);
								//Crea la ventana y la muestra pero no guarda una referencia hacia ella
								(new InterfazUsuario.Operaciones(cliente.Nombre)).Show();
						}
						catch (DAL.Excepciones.ClienteNoEncontrado)
						{
								throw;//Tirar la excepción para que sea agarrada por la ventana que quizo ingresar
						}
				}
				public static IList<ProductoDTO> ObtenerProductos()
				{
						return controlador.ObtenerProductos();
				}

				public static void BlanquearPin(string numeroTarjeta)
				{
						controlador.BlanquearPin(numeroTarjeta);
				}
				public static double SaldoCC()
				{
						return controlador.SaldoCC();
				}
				public static IList<MovimientoDTO> UltimosMovimientos()
				{
						return controlador.UltimosMovimientos();
				}

				public static void RegistrarOperacion(string pDescripcion, TimeSpan pTiempo)
				{
						controlador.RegistrarOperacion(pDescripcion, pTiempo);
				}

				/// <summary>
				/// Cierra la sesion de un cliente y abre la ventana 
				/// de Login para que uno nuevo pueda entrar
				/// </summary>
				public static void CerrarSesion()
				{
						controlador.GuardarCambios();
						login.Show();
				}
		}
}