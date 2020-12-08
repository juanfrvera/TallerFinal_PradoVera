using System;

namespace TallerFinal_PradoVera.Dominio
{
   public class Operacion
   {

      // Atributos
      private int id;
      private string dniCliente;
      private String descripcion;
      private TimeSpan tiempo;
      private bool fallida;

      // Propiedades
      public int Id
      {
         get { return id; }
         set { id = value; }
      }

      public string DNICliente
      {
         get { return dniCliente; }
         set { dniCliente = value; }
      }

      public String Descripcion
      {
         get { return descripcion; }
         set { descripcion = value; }
      }


      public TimeSpan Tiempo
      {
         get { return tiempo; }
         set { tiempo = value; }
      }

      /// <summary>
      /// Verdadero si esta operación resultó fallida
      /// </summary>
      public bool Fallida
      {
         get { return fallida; }
         set { fallida = value;  }
      }

      // Constructor
      public Operacion(int pId, string pDNICliente, string pDescripcion, TimeSpan pTiempo, bool pFallida)
      {
         this.Id = pId;
         this.DNICliente = pDNICliente;
         this.Descripcion = pDescripcion;
         this.Tiempo = pTiempo;
         this.Fallida = pFallida;
      }

      public Operacion() { }
   }
}
