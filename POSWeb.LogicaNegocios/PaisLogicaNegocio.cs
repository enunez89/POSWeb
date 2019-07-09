using POSWeb.AccesoDatos;
using POSWeb.Entidades;
using POSWeb.Entidades.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace POSWeb.LogicaNegocios
{
    /// <summary>
    /// Requerimiento:       MobileToken
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Logica de Negocio clase   PaisLogicaNegocio
    /// Ultima modificacion: 06/12/2017
    /// </summary>
    public class PaisLogicaNegocio
    {

        PaisAccesoDatos dal = new PaisAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Pais
        /// </summary>
        /// <param name="pPais">Filtro de tipo Pais</param>
        /// <returns></returns>
        public RespuestaListaPais ObtenerListaPais(Pais pPais)
        {

            var respuesta = new RespuestaListaPais();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPais.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerPais(pPais);
                    return respuesta;
                }
                else
                    return new RespuestaListaPais { Respuesta = respS.Respuesta, ListaPais = new List<Pais>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPais);
                return new RespuestaListaPais { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Pais
        /// </summary>
        /// <param name="pPais">Filtro de tipo Pais</param>
        /// <returns></returns>
        public RespuestaPais ObtenerPais(Pais pPais)
        {
            var respuesta = new RespuestaPais();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPais.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.Pais = dal.ObtenerPais(pPais).ListaPais?[0];
                    return respuesta.Pais != null ?
                      new RespuestaPais { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Pais = respuesta.Pais } :
                      new RespuestaPais { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Pais = new Pais() };
                }
                else
                    return new RespuestaPais { Respuesta = respS.Respuesta, Pais = new Pais() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPais);
                return new RespuestaPais { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

    }//fin de clase
}