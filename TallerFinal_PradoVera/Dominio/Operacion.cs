using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerFinal_PradoVera.Dominio
{
    public class Operacion
    {

        //Atributos
        private int id;
        private String descripcion;
        private TimeSpan tiempo;

        //Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


        public TimeSpan Tiempo
        {
            get { return tiempo; }
            set { tiempo = value; }
        }

        //Constructor
        public Operacion(int pId, string pDescripcion, TimeSpan pTiempo)
        {
            this.Id = pId;
            this.Descripcion = pDescripcion;
            this.Tiempo = pTiempo;
        }
    }
}
