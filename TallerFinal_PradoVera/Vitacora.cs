using System;
using System.Linq;
using TallerFinal_PradoVera.DAL;
using TallerFinal_PradoVera.DAL.EntityFramework;


namespace TallerFinal_PradoVera
{
   public class Vitacora
   {
      private IRepositorioOperaciones iRepOperaciones;

      private uint iUltimoIDOperacion;

      public Vitacora()
      {
         iRepOperaciones = new RepositorioRegistroDeOperaciones();

         #region Obtener ultimo id de operacion
         // Lo obtengo y lo guardo en una variable para no hacer esta operación costosa cada vez que quiero averiguar el último ID
         if (iRepOperaciones.Count() > 0)
         {
            iUltimoIDOperacion = (uint)iRepOperaciones.ObtenerTodos().Max(e => e.Id);
         }
         else
         {
            iUltimoIDOperacion = 0;
         }
         #endregion
      }


      ///Metodos
      /// <summary>
      /// Registra operación en la base de datos guardando la descripción y el tiempo que se tardó en operar
      /// </summary>
      /// <param name="pDescripcion"></param>
      /// <param name="pTiempo"></param>
      public void RegistrarOperacion(string pDescripcion, TimeSpan pTiempo, string pDniCliente)
      {
         try
         {
            int nuevoIdOperacion = (int)iUltimoIDOperacion + 1;
            iRepOperaciones.Agregar(new Dominio.Operacion(nuevoIdOperacion, pDniCliente, pDescripcion, pTiempo));
            // Se hace de esta forma para que no se incremente el ultimoID si llega a fallar la linea de arriba
            iUltimoIDOperacion = (uint)nuevoIdOperacion;
         }
         catch (Exception)
         {
            throw new DAL.Excepciones.ErrorAlRegistrarOperacion();
         }
         finally
         {
            GuardarCambios();
         }
      }

      public void GuardarCambios()
      {
         try
         {
            iRepOperaciones.GuardarCambios();
         }
         catch (Exception)
         {
            throw new DAL.Excepciones.ErrorAlGuardarRegistroOperaciones();
         }
      }
   }

}
