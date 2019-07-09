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
    /// Propósito:           Logica de Negocio clase   TipoIdentificacionLogicaNegocio
    /// Ultima modificacion: 06/12/2017
    /// </summary>
    public class TipoIdentificacionLogicaNegocio
    {

        TipoIdentificacionAccesoDatos dal = new TipoIdentificacionAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();
        

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo TipoIdentificacion
        /// </summary>
        /// <param name="pTipoIdentificacion">Filtro de tipo TipoIdentificacion</param>
        /// <returns></returns>
        public RespuestaListaTipoIdentificacion ObtenerListaTipoIdentificacion(TipoIdentificacion pTipoIdentificacion)
        {

            var respuesta = new RespuestaListaTipoIdentificacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pTipoIdentificacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerTipoIdentificacion(pTipoIdentificacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaTipoIdentificacion { Respuesta = respS.Respuesta, ListaTipoIdentificacion = new List<TipoIdentificacion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pTipoIdentificacion);
                return new RespuestaListaTipoIdentificacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo TipoIdentificacion
        /// </summary>
        /// <param name="pTipoIdentificacion">Filtro de tipo TipoIdentificacion</param>
        /// <returns></returns>
        public RespuestaTipoIdentificacion ObtenerTipoIdentificacion(TipoIdentificacion pTipoIdentificacion)
        {
            var respuesta = new RespuestaTipoIdentificacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pTipoIdentificacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.TipoIdentificacion = dal.ObtenerTipoIdentificacion(pTipoIdentificacion).ListaTipoIdentificacion?[0];
                    return respuesta.TipoIdentificacion != null ?
                      new RespuestaTipoIdentificacion { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), TipoIdentificacion = respuesta.TipoIdentificacion } :
                      new RespuestaTipoIdentificacion { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), TipoIdentificacion = new TipoIdentificacion() };
                }
                else
                    return new RespuestaTipoIdentificacion { Respuesta = respS.Respuesta, TipoIdentificacion = new TipoIdentificacion() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pTipoIdentificacion);
                return new RespuestaTipoIdentificacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
        
    }//fin de clase
}