using System;

namespace Autoservicio.IO
{
    internal struct OperacionDTO
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String descripcion;

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private TimeSpan tiempo;

        public TimeSpan Tiempo
        {
            get { return tiempo; }
            set { tiempo = value; }
        }

    }
}

