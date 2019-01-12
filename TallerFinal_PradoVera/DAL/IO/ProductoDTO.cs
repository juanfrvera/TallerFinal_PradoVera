﻿using System;

namespace Autoservicio.IO
{
    public struct ProductoDTO
    {
        private String numero;

        public String Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private String tipo;

        public String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
    }
}

