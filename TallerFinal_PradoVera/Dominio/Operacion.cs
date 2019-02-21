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
        private string dniCliente;
        private String descripcion;
        private TimeSpan tiempo;

        //Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string DNICliente
        {
            get { return dniCliente; }
            set { dniCliente = value; }
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
        public Operacion(int pId, string pDNICliente, string pDescripcion, TimeSpan pTiempo)
        {
            this.Id = pId;
            this.DNICliente = pDNICliente;
            this.Descripcion = pDescripcion;
            this.Tiempo = pTiempo;
        }
    }
}
