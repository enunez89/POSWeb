using POSWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace POSWeb.IU.Servicio
{
    public class CnnWebApi
    {
        #region Constantes y Variables

        private const string urlBase = "http://localhost:8087/webapi/";

        #endregion

        #region Producto

        /// <summary>
        /// Obtiene producto por código barras
        /// </summary>
        /// <param name="codigoBarra"></param>
        /// <returns></returns>
        public RespuestaProducto ObtenerProductoPorCodigo(string codigoBarra)
        {
            var respuesta = new RespuestaProducto();
            try
            {
                using(var servicio = new HttpClient())
                {
                    //seteamos la url base donde se encuentra publicado el web api
                    servicio.BaseAddress = new Uri(urlBase);

                    //le pasamos los parametros por query string
                    string paramMetodo = $"Productos/ObtenerPorCodigo?codigoBarra={codigoBarra}";

                    //consumimos el método del web api
                    var respProducto = servicio.GetAsync(paramMetodo);
                    //esperamos la respuesta
                    respProducto.Wait();
                    //obtenemos el resultado
                    var result = respProducto.Result;

                    //validamos que todo fue correcto
                    if (result.IsSuccessStatusCode)
                    {
                        //obtenemos los datos
                        respuesta = result.Content.ReadAsAsync<RespuestaProducto>().Result;
                    }
                    else
                    {
                        //ocurrió un erro
                        respuesta.Respuesta = new Respuesta(Respuesta.CodError, "Error de comunicación");
                    }                  

                }
            }
            catch (Exception ex)
            {
                //ocurrió una exepción
                respuesta.Respuesta = new Respuesta(Respuesta.CodNoValido);
            }
            return respuesta;
        }

        #endregion
    }
}