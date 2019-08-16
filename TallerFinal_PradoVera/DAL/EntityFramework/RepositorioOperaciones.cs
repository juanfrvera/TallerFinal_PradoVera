namespace TallerFinal_PradoVera.DAL.EntityFramework
{
		internal class RepositorioOperaciones : Repositorio<Dominio.Operacion, OperacionDbContext>, IRepositorioOperaciones
		{
				public RepositorioOperaciones() : base(new OperacionDbContext()) { }
		}
}
