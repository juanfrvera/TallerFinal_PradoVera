using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera.DAL
{

    namespace Excepciones
    {
        public class ErrorDeConexion : Exception
        {
            public ErrorDeConexion() : base("Hubo un error de conexion") { }
        }
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
            public string descripcion;
            public ErrorAlBlanquearPin(string descripcion) : base("Error al blanquear pin, descripción: " + descripcion) {
                this.descripcion = descripcion;
            }
        }
        public class ErrorAlConsultarSaldo : Exception
        {
            public ErrorAlConsultarSaldo() : base("Error al consultar el saldo de la cuenta corriente") { }
        }
    }
}
