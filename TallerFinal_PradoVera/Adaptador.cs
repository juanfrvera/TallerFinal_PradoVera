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
        private dynamic GetResponse(string url)
        {
            // Se crea el request http
            HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                // Se ejecuta la consulta
                WebResponse mResponse = mRequest.GetResponse();

                // Se obtiene los datos de respuesta
                using (Stream responseStream = mResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                    // Se parsea la respuesta y se serializa a JSON a un objeto dynamic
                    return JsonConvert.DeserializeObject(reader.ReadToEnd());
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
        /// <summary>
        /// Servicio 1
        /// </summary>
        /// <param name="pDni"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public ClienteDTO ValidarCliente(string pDni, string pClave)
        {
            ClienteDTO dTO = new ClienteDTO();
            var mUrl = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/clients?id=" + pDni + "&pass=" + pClave;
            dynamic response = GetResponse(mUrl);
            
            if (response.Count >= 1)
            {
                System.Console.WriteLine("Item completo -> {0}", response[0].response);

                dTO.Id = response[0].id;
                dTO.Clave = pClave;
                dTO.Nombre = response[0].response.client.name;
                dTO.Categoria = response[0].response.client.segment;

                return dTO;
            }
            else
                throw new DAL.Excepciones.ClienteNoEncontrado();

        }
        public IList<ProductoDTO> ObtenerProductos(string pDni)
        {
            IList<ProductoDTO> productos = new List<ProductoDTO>();
            var mUrl = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/products?id=" + pDni;

            dynamic response = GetResponse(mUrl);

            if (response.Count >= 1)
            {
                for (int i = 0; i < response[0].response.product.Count; i++)
                {
                    ProductoDTO prod = new ProductoDTO();
                    prod.Numero = response[0].response.product[i].number;
                    prod.Nombre = response[0].response.product[i].name;
                    prod.Tipo = response[0].response.product[i].type;

                    productos.Add(prod);
                }
                System.Console.WriteLine("Item completo -> {0}", response[0].response);

                return productos;
            }
            else
                throw new DAL.Excepciones.ClienteNoTieneProductos();
        }

        /// <summary>
        /// Servicio 3, Blanquea el pin de una tarjeta del cliente actual
        /// </summary>
        /// <param name="pNumeroTarjeta"></param>
        /// <returns></returns>
        public void BlanquearPin(string pNumeroTarjeta)
        {
            string url = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/product-reset?number=" + pNumeroTarjeta;
            dynamic response = GetResponse(url);

            if (response.Count >= 1)
            {
                if (response[0].response.error != "0")
                    throw new DAL.Excepciones.ErrorAlBlanquearPin(response[0].response.error_description);
            }
            else
                throw new DAL.Excepciones.ErrorAlBlanquearPin("Error irrecuperable");
        }

        public double SaldoCC(string pDni)
        {
            throw new NotImplementedException();
        }

        public IList<MovimientoDTO> UltimosMovimientos(string pDni)
        {
            throw new NotImplementedException();
        }
    }
}
