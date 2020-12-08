using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Autoservicio.IO;
using Newtonsoft.Json;

namespace TallerFinal_PradoVera
{
   class AdaptadorServicios : IServicios
   {
      private Bitacora iBitacora;

      public AdaptadorServicios()
      {
         try
         {
            iBitacora = new Bitacora();
         }
         catch (Exception exc)
         {
            // No tirar excepción ya que el registrar operaciones en la bitácora es algo
            // Que no debería parar el flujo de la aplicación en caso de errores
            Console.WriteLine("Excepción al crear bitácora. Codigo de error: " + exc.HResult.ToString());
         }
      }

      private dynamic ObtenerRespuesta(string url)
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
            try
            {
               using (Stream mResponseStream = mErrorResponse.GetResponseStream())
               {
                  StreamReader mReader = new StreamReader(mResponseStream, Encoding.GetEncoding("utf-8"));
                  String mErrorText = mReader.ReadToEnd();

                  Console.WriteLine("Error: {0}", mErrorText);
               }
            }
            catch (Exception)
            {
               throw new DAL.Excepciones.ErrorDeConexion();
            }
            throw new DAL.Excepciones.ErrorDeConexion();
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

         // Empezar el timer para saber cuánto tiempo llevó la operación
         Stopwatch timer = new Stopwatch();
         timer.Start();
         
         // Indicador que pasará a verdadero si ocurre un error
         bool fallida = false;

         try
         {
            dynamic response = ObtenerRespuesta(mUrl);

            if (response.Count >= 1)
            {
               Console.WriteLine("Item completo -> {0}", response[0].response);

               dTO.Id = response[0].id;
               dTO.Dni = pDni;
               dTO.Clave = pClave;
               dTO.Nombre = response[0].response.client.name;
               dTO.Categoria = response[0].response.client.segment;

               return dTO;
            }
            else
            {
               throw new DAL.Excepciones.ClienteNoEncontrado();
            }
         }
         catch
         {
            fallida = true;
            
            // Devolver la misma excepción para que sea tratada por el nivel anterior
            throw;
         }
         finally
         {
            RegistrarOperacion(pDni, "Validar cliente", timer, fallida);
         }
      }
      public IList<ProductoDTO> ObtenerProductos(string pDni)
      {
         IList<ProductoDTO> productos = new List<ProductoDTO>();
         var mUrl = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/products?id=" + pDni;

         // Empezar el timer para saber cuánto tiempo llevó la operación
         Stopwatch timer = new Stopwatch();
         timer.Start();

         // Indicador que pasará a verdadero si ocurre un error
         bool fallida = false;

         try
         {
            dynamic response = ObtenerRespuesta(mUrl);

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
               Console.WriteLine("Item completo -> {0}", response[0].response);

               return productos;
            }
            else
            {
               throw new DAL.Excepciones.ClienteNoTieneProductos();
            }
         }
         catch
         {
            fallida = true;

            // Devolver la misma excepción para que sea tratada por el nivel anterior
            throw;
         }
         finally
         {
            RegistrarOperacion(pDni, "Obtener productos", timer, fallida);
         }
      }

      /// <summary>
      /// Servicio 3, Blanquea el pin de una tarjeta del cliente actual
      /// </summary>
      /// <param name="pNumeroTarjeta"></param>
      /// <returns></returns>
      public void BlanquearPin(string pNumeroTarjeta, string pDni)
      {
         string url = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/product-reset?number=" + pNumeroTarjeta;

         // Empezar el timer para saber cuánto tiempo llevó la operación
         Stopwatch timer = new Stopwatch();
         timer.Start();

         // Indicador que pasará a verdadero si ocurre un error
         bool fallida = false;

         try
         {
            dynamic response = ObtenerRespuesta(url);
            if (response.Count >= 1)
            {
               if (response[0].response.error != "0")
               {
                  throw new DAL.Excepciones.ErrorAlBlanquearPin(response[0].response["error-description"].ToString());
               }
            }
            else
            {
               throw new DAL.Excepciones.ErrorAlBlanquearPin("Error irrecuperable");
            }
         }
         catch
         {
            fallida = true;

            // Devolver la misma excepción para que sea tratada por el nivel anterior
            throw;
         }
         finally
         {
            RegistrarOperacion(pDni, "Blanquear pin", timer, fallida);
         }
      }

      public double SaldoCC(string pDni)
      {
         string url = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/account-balance?id=" + pDni;

         // Empezar el timer para saber cuánto tiempo llevó la operación
         Stopwatch timer = new Stopwatch();
         timer.Start();

         // Indicador que pasará a verdadero si ocurre un error
         bool fallida = false;

         try
         {
            dynamic response = ObtenerRespuesta(url);
            if (response.Count >= 1)//Si hay respuesta
            {
               return response[0].response.balance;
            }
            else
            {
               throw new DAL.Excepciones.ErrorAlConsultarSaldo();
            }
         }
         catch
         {
            fallida = true;

            // Devolver la misma excepción para que sea tratada por el nivel anterior
            throw;
         }
         finally
         {
            RegistrarOperacion(pDni, "Saldo CC", timer, fallida);
         }
      }

      public IList<MovimientoDTO> UltimosMovimientos(string pDni)
      {
         string url = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/account-movements?id=" + pDni;
         IList<MovimientoDTO> movimientos = new List<MovimientoDTO>();

         // Empezar el timer para saber cuánto tiempo llevó la operación
         Stopwatch timer = new Stopwatch();
         timer.Start();

         // Indicador que pasará a verdadero si ocurre un error
         bool fallida = false;

         try
         {
            dynamic response = ObtenerRespuesta(url);

            if (response.Count >= 1)
            {
               for (int i = 0; i < response[0].response.movements.Count; i++)
               {
                  MovimientoDTO mov = new MovimientoDTO();
                  mov.Fecha = response[0].response.movements[i].date;
                  mov.Monto = response[0].response.movements[i].ammount;

                  movimientos.Add(mov);
               }
               Console.WriteLine("Item completo -> {0}", response[0].response);
            }
            return movimientos;
         }
         catch
         {
            fallida = true;

            // Devolver la misma excepción para que sea tratada por el nivel anterior
            throw;
         }
         finally
         {
            RegistrarOperacion(pDni, "Últimos movimientos", timer, fallida);
         }
      }

      private void RegistrarOperacion(string pDni, string pDescripcion, Stopwatch pTimer, bool pFallida)
      {
         pTimer.Stop();
         try
         {
            iBitacora.RegistrarOperacion(pDni, pDescripcion, pTimer.Elapsed, pFallida);
         }
         catch (Exception exc)
         {
            // Las excepciones del registro de operaciones no paran el flujo de la aplicación
            // Es por eso que no volvemos a hacer un throw aquí
            Console.WriteLine("Excepción al registrar operación. Codigo de error: " + exc.HResult.ToString());
         }
      }
   }
}