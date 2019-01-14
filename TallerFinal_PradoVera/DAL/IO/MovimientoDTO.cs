using System;

namespace Autoservicio.IO
{
    public struct MovimientoDTO
    {

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private double monto;

        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }

    }
}
