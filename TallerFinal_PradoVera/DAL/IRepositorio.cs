using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera.DAL
{
    interface IRepositorio<TEntity>
    {
        void Agregar(TEntity pEntity);
        void Eliminar(TEntity pEntity);
        TEntity Obtener(int pId);
        IEnumerable<TEntity> ObtenerTodos();
        int Count();
        void SaveChanges();
    }
}
