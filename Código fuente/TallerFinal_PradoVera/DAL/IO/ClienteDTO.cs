using System;

namespace Autoservicio.IO
{
    public struct ClienteDTO
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private String clave;

        public String Clave
        {
            get { return clave; }
            set { clave = value; }
        }

        private String dni;

        public String Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        private String categoria;

        public String Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
    }
}
