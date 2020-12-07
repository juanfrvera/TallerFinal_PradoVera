using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoservicioUnitTest
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      [ExpectedException(typeof(TallerFinal_PradoVera.DAL.Excepciones.ClienteNoEncontrado))]
      public void IngresarInvalido()
      {
         TallerFinal_PradoVera.Program.Ingresar("dniInvalido", "claveInvalida");
      }

      [TestMethod]
      public void IngresarValido()
      {
         var cliente = TallerFinal_PradoVera.Program.Ingresar("12345678", "1234");

         Assert.AreEqual("Juan Amador", cliente.Nombre);
      }
   }
}
