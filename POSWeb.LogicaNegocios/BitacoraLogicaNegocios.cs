using POSWeb.AccesoDatos;
using POSWeb.Entidades;
using POSWeb.Entidades.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TraceData;
using TraceData.Entities.Models;
using TraceData.Utilities;

namespace POSWeb.LogicaNegocios
{
    public class BitacoraLogicaNegocios
    {

        BitacoraAccesoDatos dal = new BitacoraAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        public static void RegistrarBitacora(string mensaje, Enums.TypeTrace tipo, object trama = null, string metodo = "")
        {
            try
            {
                var blTrace = new BlTrace();
                string mensajeTecnico = "";

                if (trama != null)
                {
                    if (trama is Exception)
                    {
                        var ex = ((Exception)trama);
                        mensajeTecnico = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }
                    else
                    {
                        mensajeTecnico = Utilidades.Util.SerializarObjeto(trama);
                    }
                }

                Trace oTraceData = new Trace()
                {
                    Type = tipo.ToString(),
                    Message = mensaje,
                    TechMessage = mensajeTecnico,
                    Tracer = metodo
                };

                blTrace.InsertTrace(oTraceData);
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex);
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Bitacora con paginacion
        /// </summary>
        /// <param name="pBitacora">Filtro de tipo Bitacora</param>
		/// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaBitacoraAplicacion ObtenerBitacoraPaginado(BitacoraAplicacion pBitacora, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaBitacoraAplicacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pBitacora.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pBitacora.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta = dal.ObtenerBitacoraPaginado(pBitacora, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaBitacoraAplicacion { Respuesta = respS.Respuesta, ListaBitacora = new List<BitacoraAplicacion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pBitacora);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaBitacoraAplicacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Bitacora
        /// </summary>
        /// <param name="pBitacora">Filtro de tipo Bitacora</param>
        /// <returns></returns>
		public RespuestaListaBitacoraAplicacion ObtenerListaBitacora(BitacoraAplicacion pBitacora)
        {

            var respuesta = new RespuestaListaBitacoraAplicacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pBitacora.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerBitacora(pBitacora);
                    return respuesta;
                }
                else
                    return new RespuestaListaBitacoraAplicacion { Respuesta = respS.Respuesta, ListaBitacora = new List<BitacoraAplicacion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pBitacora);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaBitacoraAplicacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Bitacora
        /// </summary>
        /// <param name="pBitacora">Filtro de tipo Bitacora</param>
        /// <returns></returns>
		public RespuestaBitacoraAplicacion ObtenerBitacora(BitacoraAplicacion pBitacora)
        {
            var respuesta = new RespuestaBitacoraAplicacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pBitacora.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.Bitacora = dal.ObtenerBitacora(pBitacora).ListaBitacora?[0];
                    return respuesta.Bitacora != null ?
                      new RespuestaBitacoraAplicacion { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Bitacora = respuesta.Bitacora } :
                      new RespuestaBitacoraAplicacion { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Bitacora = new BitacoraAplicacion() };
                }
                else
                    return new RespuestaBitacoraAplicacion { Respuesta = respS.Respuesta, Bitacora = new BitacoraAplicacion() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pBitacora);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaBitacoraAplicacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
    }
}
