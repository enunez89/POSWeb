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
    /// Propósito:           Logica de Negocio clase   CatalogoLogicaNegocio
    /// Ultima modificacion: 18/11/2017
    /// </summary>
    public class CatalogoLogicaNegocio
    {

        CatalogoAccesoDatos dal = new CatalogoAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Catalogo
        /// </summary>
        /// <param name="pCatalogo">Filtro de tipo Catalogo</param>
        /// <returns></returns>
        public RespuestaListaCatalogo ObtenerListaCatalogo(Catalogo pCatalogo)
        {

            var respuesta = new RespuestaListaCatalogo();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCatalogo.UsrtokensAuthenticate });
                pCatalogo.IdEntidad = respS.Sesion.IdEntidad;
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerCatalogos(pCatalogo);
                    return respuesta;
                }
                else
                    return new RespuestaListaCatalogo { Respuesta = respS.Respuesta, ListaCatalogo = new List<Catalogo>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCatalogo);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaCatalogo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener un Catalogo
        /// </summary>
        /// <param name="pCatalogo">Filtro de tipo Catalogo</param>
        /// <returns></returns>
        public RespuestaCatalogo ObtenerCatalogo(Catalogo pCatalogo)
        {

            var respuesta = new RespuestaCatalogo();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCatalogo.UsrtokensAuthenticate });
                pCatalogo.IdEntidad = respS.Sesion.IdEntidad;
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.Catalogo = dal.ObtenerCatalogos(pCatalogo).ListaCatalogo?[0];
                    return respuesta.Catalogo != null ?
                      new RespuestaCatalogo { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Catalogo = respuesta.Catalogo } :
                      new RespuestaCatalogo { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Catalogo = new Catalogo() };
                }
                else
                    return new RespuestaCatalogo { Respuesta = respS.Respuesta, Catalogo = new Catalogo() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCatalogo);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaCatalogo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo Catalogo
        /// </summary>
        /// <param name="pCatalogo"></param>
        /// <returns></returns>
        public RespuestaCatalogo ModificarCatalogo(Catalogo pCatalogo)
        {
            var respuesta = new RespuestaCatalogo();
            try
            {
                string CodigoAlerta = "CatalogoEdit";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCatalogo.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    pCatalogo.UsrModificacion = respS.Sesion.CodigoUsuario;
                    pCatalogo.IdEntidad = respS.Sesion.IdEntidad;
                    if (ValidacionesModificacion(pCatalogo, ref mensajes))
                    {
                        respuesta = dal.ModificarCatalogo(pCatalogo);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaModificar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pCatalogo, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaCatalogo { Respuesta = new Respuesta(Respuesta.CodNoValido), Catalogo = respuesta.Catalogo };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaCatalogo { Respuesta = respS.Respuesta, Catalogo = new Catalogo() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCatalogo);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaCatalogo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Catalogo con paginacion
        /// </summary>
        /// <param name="pCatalogo">Filtro de tipo Catalogo</param>
		/// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaCatalogo ObtenerCatalogoPaginado(Catalogo pCatalogo, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaCatalogo();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCatalogo.UsrtokensAuthenticate });
                pCatalogo.IdEntidad = respS.Sesion.IdEntidad;
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerCatalogoPaginado(pCatalogo, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaCatalogo { Respuesta = respS.Respuesta, ListaCatalogo = new List<Catalogo>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCatalogo);
                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                          TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
                return new RespuestaListaCatalogo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Validacion en entidad para modificar
        /// </summary>
        /// <param name="pCatalogo"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(Catalogo pCatalogo, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pCatalogo != null)
            {
                isValid = Utilidades.Util.EntidadValida(pCatalogo, ref pMensajes);

                if (String.IsNullOrEmpty(pCatalogo.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
                    isValid = false;
                }

                if (pCatalogo.IdCatalogo == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Catalogo"));
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Notificacion Accion
        /// </summary>
        /// <param name="pCatalogo"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Catalogo pCatalogo, string CodigoAlerta)
        {
            AlertaLogicaNegocio al = new AlertaLogicaNegocio();
            RespuestaAlerta alerta = al.ObtenerAlerta(new AlertaBase() { CodigoAlerta = CodigoAlerta, Activo = true });
            if (alerta != null && alerta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                //al.EnviarAlerta(alerta.Alerta);
            }
        }

    }//fin de clase
}