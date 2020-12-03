namespace TallerFinal_PradoVera.DAL
{
    // Creamos esta interface por si en algun momento tenemos un 
    // metodo especifico del repositorio de operaciones y tenemos distintas implementaciones
    interface IRepositorioOperaciones : IRepositorio<Dominio.Operacion>
    {
    }
}
