using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera.DAL
{
    //Creamos esta interface por si en algun momento tenemos un 
    //metodo especifico del repositorio de operaciones y tenemos distintas implementaciones
    interface IRepositorioOperaciones : IRepositorio<Dominio.Operacion>
    {
    }
}
