using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Autoservicio.IO;
using Newtonsoft.Json;

namespace TallerFinal_PradoVera
{
    class Adaptador : IServicios
    {
        /// <summary>
        /// Servicio 3
        /// </summary>
        /// <param name="pNumeroTarjeta"></param>
        /// <returns></returns>
        public bool BlanquearPin(string pNumeroTarjeta)
        {
            throw new NotImplementedException();
        }

        public IList<ProductoDTO> ObtenerProductos(string pDni)
        {
            throw new NotImplementedException();
        }

        public double SaldoCC(string pDni)
        {
            throw new NotImplementedException();
        }

        public IList<MovimientoDTO> UltimosMovimientos(string pDni)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Servicio 1
        /// </summary>
        /// <param name="pDni"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public ClienteDTO ValidarCliente(string pDni, string pClave)
        {
            ClienteDTO dTO = new ClienteDTO();
            var mUrl = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/clients?id="+pDni+"&pass="+pClave;

            // Se crea el request http
            HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(mUrl);

            try
            {
                // Se ejecuta la consulta
                WebResponse mResponse = mRequest.GetResponse();

                // Se obtiene los datos de respuesta
                using (Stream responseStream = mResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                    // Se parsea la respuesta y se serializa a JSON a un objeto dynamic
                    dynamic mResponseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

                    if (mResponseJSON.Count >= 1)
                    {
                        System.Console.WriteLine("Item completo -> {0}", mResponseJSON[0].response);

                        dTO.Id = mResponseJSON[0].id;
                        dTO.Clave = pClave;
                        dTO.Nombre = mResponseJSON[0].response.client.name;
                        dTO.Categoria = mResponseJSON[0].response.client.segment;

                        return dTO;
                    }
                    else
                        throw new DAL.Excepciones.ClienteNoEncontrado();
                }
            }
            catch (WebException ex)
            {
                WebResponse mErrorResponse = ex.Response;
                using (Stream mResponseStream = mErrorResponse.GetResponseStream())
                {
                    StreamReader mReader = new StreamReader(mResponseStream, Encoding.GetEncoding("utf-8"));
                    String mErrorText = mReader.ReadToEnd();

                    System.Console.WriteLine("Error: {0}", mErrorText);
                }
                throw;
            }

        }
    }
}
