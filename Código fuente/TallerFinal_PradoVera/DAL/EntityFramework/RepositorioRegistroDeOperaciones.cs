namespace TallerFinal_PradoVera.DAL.EntityFramework
{
		internal class RepositorioRegistroDeOperaciones : Repositorio<Dominio.Operacion, OperacionDbContext>, IRepositorioOperaciones
		{
				public RepositorioRegistroDeOperaciones() : base(new OperacionDbContext()) { }
		}
}
