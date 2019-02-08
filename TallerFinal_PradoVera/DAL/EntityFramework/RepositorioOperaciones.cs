using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera.DAL.EntityFramework
{
 class RepositorioOperaciones : Repositorio<Dominio.Operacion, OperacionDbContext>, IRepositorioOperaciones;
    {
        public RepositorioOperaciones(OperacionDbContext pContext) : base(pContext) { }
    }
}
