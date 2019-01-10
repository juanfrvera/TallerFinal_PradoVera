using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera.DAL
{

    namespace Excepciones
    {
        public class ClienteNoEncontrado : Exception
        {
            public ClienteNoEncontrado() : base("El cliente buscado no fue encontrado") { }
        }
    }
}
