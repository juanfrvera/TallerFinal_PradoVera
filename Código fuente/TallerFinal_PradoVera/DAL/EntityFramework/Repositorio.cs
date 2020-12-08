using System.Collections.Generic;
using System.Linq;

namespace TallerFinal_PradoVera.DAL.EntityFramework
{
   internal class Repositorio<TEntity, TDbContext> : IRepositorio<TEntity> where TEntity : class where TDbContext : OperacionDbContext
   {
      TDbContext iDbContext;
      // Constructor
      public Repositorio(TDbContext pContext)
      {
         this.iDbContext = pContext;
      }

      //----------------------------Métodos--------------------------------------------
      public void Agregar(TEntity pEntity)
      {
         iDbContext.Set<TEntity>().Add(pEntity);
      }

      public TEntity Obtener(int pId)
      {
         return iDbContext.Set<TEntity>().Find(pId);
      }

      public IEnumerable<TEntity> ObtenerTodos()
      {
         return iDbContext.Set<TEntity>();
      }

      public void Eliminar(TEntity pEntity)
      {
         iDbContext.Set<TEntity>().Remove(pEntity);
      }

      public int Count()
      {
         return iDbContext.Operacion.Count();
      }

      public void GuardarCambios()
      {
         iDbContext.SaveChanges();
      }
   }
}
