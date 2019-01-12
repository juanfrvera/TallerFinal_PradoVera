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
        public class ClienteNoTieneProductos : Exception
        {
            public ClienteNoTieneProductos() : base("El cliente no tiene productos") { }
        }
        public class ErrorAlBlanquearPin : Exception
        {
            public ErrorAlBlanquearPin(string descripcion) : base("Error al blanquear pin, descripción: " + descripcion) { }
        }
    }
}
