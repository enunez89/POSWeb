using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using POSWeb.Entidades;

namespace POSWeb.Utilidades
{
    /// <summary>
    /// Contiene métodos relacionados con los archivos de configuración
    /// </summary>
    public class AdministrarConfiguracion
    {
        /// <summary>
        /// Consulta el valor del parámetro en la configuración del sistema
        /// </summary>
        /// <param name="pNombreParametro">Parámetro a consultar</param>
        /// <returns>Retorna un string con el valor del parámetro consultado</returns>
        public static Respuesta ObtenerParametro(string pNombreParametro)
        {
            try
            {
                //Se consulta el parámetro en el archivo de configuración
                string valorParametro = ConfigurationManager.AppSettings[pNombreParametro];

                var respuesta = new Respuesta();
                respuesta.CodMensaje = Respuesta.CodExitoso;
                respuesta.Mensaje = valorParametro;
                return respuesta;
            }
            catch 
            {
                var respuesta = new Respuesta();
                return new Respuesta(Respuesta.CodNoValido);
            }

        }

        /// <summary>
        /// Permite obtener el binding a partir del nombre del servicio
        /// </summary>
        /// <param name="pNombreServicio">Nombre del servicio</param>
        /// <param name="pTipoBinding">Tipo de binding</param>
        /// <returns>string con el nombre del binding</returns>
        public static string ObtenerBinding(string pNombreServicio, string pTipoBinding)
        {
            string binding = string.Empty;

            if(!string.IsNullOrEmpty(pNombreServicio) && !string.IsNullOrEmpty(pTipoBinding))
            {
                switch (pTipoBinding)
                {
                    case "net.tcp":
                        binding = string.Format("NetTcpBinding_I{0}", pNombreServicio);
                        break;
                    case "http":
                        binding = string.Format("BasicHttpBinding_I{0}", pNombreServicio);
                        break;
                    case "https":
                        binding = string.Format("HttpsBinding_I{0}", pNombreServicio);
                        break;
                    default:
                        binding = string.Format("BasicHttpBinding_I{0}", pNombreServicio);
                        break;
                }
            }

            return binding;
        }
    }
}
