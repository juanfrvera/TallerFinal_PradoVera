using System;
using System.Linq;
using TallerFinal_PradoVera.DAL;
using TallerFinal_PradoVera.DAL.EntityFramework;


namespace TallerFinal_PradoVera
{
    public class Vitacora
    {
        private IRepositorioOperaciones iRepOperaciones;
        private string iDniActual;

        private uint ultimoIDOperacion;

        public Vitacora()
        {
            iRepOperaciones = new RepositorioRegistroDeOperaciones();

            #region Obtener ultimo id de operacion
            //Lo obtengo y lo guardo en una variable para no hacer esta operación costosa cada vez que quiero averiguar el último ID
            if (iRepOperaciones.Count() > 0)
                ultimoIDOperacion = (uint)iRepOperaciones.ObtenerTodos().Max(e => e.Id);
            else
                ultimoIDOperacion = 0;
            #endregion
        }


///Metodos

         public void RegistrarOperacion(string pDescripcion, TimeSpan pTiempo)
        {
            try
            {
                int nuevoIdOperacion = (int)ultimoIDOperacion + 1;
                iRepOperaciones.Agregar(new Dominio.Operacion(nuevoIdOperacion, this.iDniActual, pDescripcion, pTiempo));
                //Se hace de esta forma para que no se incremente el ultimoID si llega a fallar la linea de arriba
                ultimoIDOperacion = (uint)nuevoIdOperacion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GuardarCambios()
        {
            try
            {
                iRepOperaciones.GuardarCambios();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
    