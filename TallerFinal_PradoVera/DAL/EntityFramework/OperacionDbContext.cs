using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TallerFinal_PradoVera.DAL.EntityFramework
{
   internal class OperacionDbContext : DbContext
    {
        public DbSet<Dominio.Operacion> Operacion { get; set; }

    }
}
