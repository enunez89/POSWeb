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
using POSWeb.Utilidades;

namespace POSWeb.LogicaNegocios
{
    /// <summary>
    /// Requerimiento:       MobileToken
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Logica de Negocio clase   AutorizacionLogicaNegocio
    /// Ultima modificacion: 28/11/2017
    /// </summary>
    public class AutorizacionLogicaNegocio
    {

        AutorizacionAccesoDatos dal = new AutorizacionAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo Autorizacion
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <returns></returns>
        public RespuestaAutorizacion InsertarAutorizacion(Autorizacion pAutorizacion)
        {
            var respuesta = new RespuestaAutorizacion();
            try
            {

                string CodigoAlerta = "AutorizacionCreate";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAutorizacion.UsrCreacion = respS.Sesion.CodigoUsuario;
                    //EJECUTAR: se guarda la entidad
                    if (ValidacionesCreacion(pAutorizacion, ref mensajes))
                    {
                        respuesta = dal.InsertarAutorizacion(pAutorizacion);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        //Notificacion(pAutorizacion);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaAutorizacion { Respuesta = new Respuesta(Respuesta.CodNoValido), Autorizacion = respuesta.Autorizacion };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaAutorizacion { Respuesta = respS.Respuesta, Autorizacion = new Autorizacion() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaAutorizacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Autorizacion
        /// </summary>
        /// <param name="pAutorizacion">Filtro de tipo Autorizacion</param>
        /// <returns></returns>
        public RespuestaListaAutorizacion ObtenerListaAutorizacion(Autorizacion pAutorizacion)
        {

            var respuesta = new RespuestaListaAutorizacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerAutorizacion(pAutorizacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaAutorizacion { Respuesta = respS.Respuesta, ListaAutorizacion = new List<Autorizacion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaAutorizacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Autorizacion
        /// </summary>
        /// <param name="pAutorizacion">Filtro de tipo Autorizacion</param>
        /// <returns></returns>
        public RespuestaAutorizacion ObtenerAutorizacion(Autorizacion pAutorizacion)
        {
            var respuesta = new RespuestaAutorizacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAutorizacion.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta.Autorizacion = dal.ObtenerAutorizacion(pAutorizacion).ListaAutorizacion?[0];
                    return respuesta.Autorizacion != null ?
                      new RespuestaAutorizacion { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Autorizacion = respuesta.Autorizacion } :
                      new RespuestaAutorizacion { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Autorizacion = new Autorizacion() };
                }
                else
                    return new RespuestaAutorizacion { Respuesta = respS.Respuesta, Autorizacion = new Autorizacion() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaAutorizacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Autorizacion con paginacion
        /// </summary>
        /// <param name="pAutorizacion">Filtro de tipo Autorizacion</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaAutorizacion ObtenerAutorizacionPaginado(Autorizacion pAutorizacion, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaAutorizacion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAutorizacion.IdEntidad = respS.Sesion.IdEntidad;
                    pAutorizacion.UsrCreacion = respS.Sesion.CodigoUsuario;
                    respuesta = dal.ObtenerAutorizacionPaginado(pAutorizacion, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaAutorizacion { Respuesta = respS.Respuesta, ListaAutorizacion = new List<Autorizacion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaAutorizacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo para insertar un valor de tipo AutorizacionDetalle
        /// </summary>
        /// <param name="pAutorizacionDetalle"></param>
        /// <returns></returns>
        public RespuestaAutorizacionDetalle InsertarAutorizacionDetalle(AutorizacionDetalle pAutorizacionDetalle)
        {
            var respuesta = new RespuestaAutorizacionDetalle();
            try
            {

                string CodigoAlerta = "AutorizacionDetalleCreate";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacionDetalle.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAutorizacionDetalle.UsrCreacion = respS.Sesion.CodigoUsuario;
                    //EJECUTAR: se guarda la entidad
                    if (ValidacionesCreacion(pAutorizacionDetalle, ref mensajes))
                    {
                        respuesta = dal.InsertarAutorizacionDetalle(pAutorizacionDetalle);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pAutorizacionDetalle, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaAutorizacionDetalle { Respuesta = new Respuesta(Respuesta.CodNoValido), AutorizacionDetalle = respuesta.AutorizacionDetalle };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaAutorizacionDetalle { Respuesta = respS.Respuesta, AutorizacionDetalle = new AutorizacionDetalle() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacionDetalle);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaAutorizacionDetalle { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo AutorizacionDetalle
        /// </summary>
        /// <param name="pAutorizacionDetalle">Filtro de tipo AutorizacionDetalle</param>
        /// <returns></returns>
		public RespuestaListaAutorizacionDetalle ObtenerListaAutorizacionDetalle(AutorizacionDetalle pAutorizacionDetalle)
        {

            var respuesta = new RespuestaListaAutorizacionDetalle();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacionDetalle.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerAutorizacionDetalle(pAutorizacionDetalle);
                    return respuesta;
                }
                else
                    return new RespuestaListaAutorizacionDetalle { Respuesta = respS.Respuesta, ListaAutorizacionDetalle = new List<AutorizacionDetalle>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacionDetalle);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaAutorizacionDetalle { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo AutorizacionDetalle
        /// </summary>
        /// <param name="pAutorizacionDetalle">Filtro de tipo AutorizacionDetalle</param>
        /// <returns></returns>
        public RespuestaAutorizacionDetalle ObtenerAutorizacionDetalle(AutorizacionDetalle pAutorizacionDetalle)
        {
            var respuesta = new RespuestaAutorizacionDetalle();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacionDetalle.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.AutorizacionDetalle = dal.ObtenerAutorizacionDetalle(pAutorizacionDetalle).ListaAutorizacionDetalle?[0];
                    return respuesta.AutorizacionDetalle != null ?
                      new RespuestaAutorizacionDetalle { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), AutorizacionDetalle = respuesta.AutorizacionDetalle } :
                      new RespuestaAutorizacionDetalle { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), AutorizacionDetalle = new AutorizacionDetalle() };
                }
                else
                    return new RespuestaAutorizacionDetalle { Respuesta = respS.Respuesta, AutorizacionDetalle = new AutorizacionDetalle() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacionDetalle);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaAutorizacionDetalle { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo AutorizacionDetalle con paginacion
        /// </summary>
        /// <param name="pAutorizacionDetalle">Filtro de tipo AutorizacionDetalle</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaAutorizacionDetalle ObtenerAutorizacionDetallePaginado(AutorizacionDetalle pAutorizacionDetalle, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaAutorizacionDetalle();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacionDetalle.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerAutorizacionDetallePaginado(pAutorizacionDetalle, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaAutorizacionDetalle { Respuesta = respS.Respuesta, ListaAutorizacionDetalle = new List<AutorizacionDetalle>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacionDetalle);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaAutorizacionDetalle { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Método que procesa la siguiente autorizacion
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <returns></returns>
        public RespuestaAutorizacion ProcesarAutorizacion(Autorizacion pAutorizacion)
        {
            var respuesta = new RespuestaAutorizacion();
            try
            {

                //string CodigoAlerta = "ValidarAutorizacionCreate";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAutorizacion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAutorizacion.UsrCreacion = respS.Sesion.CodigoUsuario;
                    //EJECUTAR: se guarda la entidad

                    respuesta = dal.ProcesarAutorizacion(pAutorizacion);
                    BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaValidacion,
                        TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                    if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
                    {
                        //se envia la notificacion segun corresponda
                       Task.Factory.StartNew(()=> Notificacion(pAutorizacion));
                    }
                    return respuesta;
                }
                else
                    return new RespuestaAutorizacion { Respuesta = respS.Respuesta, Autorizacion = new Autorizacion() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaAutorizacion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Método que verifica si el registro se debe de autorizar o no
        /// </summary>
        /// <param name="controlador"></param>
        /// <returns></returns>
        public bool AutorizarRegistro(string controlador)
        {
            var controladorLN = new ControladorLogicaNegocio();
            try
            {
                //obtenemos el controlador
                var pControlador = new Controlador() { Codigo = controlador };
                var oControlador = controladorLN.ObtenerControlador(pControlador);

                //validamos respuesta
                if (oControlador.Respuesta.CodMensaje == Respuesta.CodExitoso
                    && oControlador.Controlador != null)
                {
                    return oControlador.Controlador.Autorizar;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, controlador);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return false;
            }
        }

        /// <summary>
        /// Método que inserta la primera autorización.
        /// </summary>
        /// <param name="pAutorizacion"></param>
        public void PrimeraAutorizacion(Autorizacion pAutorizacion)
        {
            try
            {
                if(AutorizarRegistro(pAutorizacion.NombreControlador))
                {
                    //se realiza la autorizacion
                    var oRespAutorizacion = dal.InsertarAutorizacion(pAutorizacion);

                    //validamos respuesta
                    if(oRespAutorizacion.Respuesta.CodMensaje == Respuesta.CodExitoso)
                    {
                        //detalle
                        var pAutorizacionDetalle = new AutorizacionDetalle()
                        {
                            IdAutorizacion = oRespAutorizacion.Autorizacion.IdAutorizacion,
                            UsrAutorizador = pAutorizacion.UsrCreacion
                        };

                        //se guarda el detalle
                        dal.InsertarAutorizacionDetalle(pAutorizacionDetalle);
                    }
                }
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAutorizacion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

            }
        }


        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(Autorizacion pAutorizacion, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pAutorizacion != null)
            {
                isValid = Utilidades.Util.EntidadValida(pAutorizacion, ref pMensajes);

                if (String.IsNullOrEmpty(pAutorizacion.UsrCreacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioCreacion);
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
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pAutorizacionDetalle"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(AutorizacionDetalle pAutorizacionDetalle, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pAutorizacionDetalle != null)
            {
                isValid = Utilidades.Util.EntidadValida(pAutorizacionDetalle, ref pMensajes);

                if (String.IsNullOrEmpty(pAutorizacionDetalle.UsrCreacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioCreacion);
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
        /// <param name="pAutorizacion"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Autorizacion pAutorizacion)
        {
            AlertaLogicaNegocio al = new AlertaLogicaNegocio();

            switch(pAutorizacion.Recurso)
            {
                case Constantes.RecursoAlerta.Usuario:

                    //se envia la notificacion de la contraseña al usuario
                    var pUsuario = new Usuario()
                    {
                        CodigoUsuario = pAutorizacion.IdRecurso
                    };
                    al.EnviarAlerta<Usuario>(ALERTAS.ForgoutPass, pUsuario);
                    break;
                case Constantes.RecursoAlerta.Rol:
                    //realizar logica de la alerta aqui

                    var pRol = new Rol()
                    {
                        IdRol = Convert.ToInt64(pAutorizacion.IdRecurso)
                    };

                    break;
            }
        }

        /// <summary>
        /// Notificacion Accion
        /// </summary>
        /// <param name="pAutorizacionDetalle"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(AutorizacionDetalle pAutorizacionDetalle, string CodigoAlerta)
        {
            //AlertaLogicaNegocio al = new AlertaLogicaNegocio();
            //RespuestaAlerta alerta = al.ObtenerAlerta(new AlertaBase() { CodigoAlerta = CodigoAlerta, Activo = true });
            //if (alerta != null && alerta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            //{
            //    //al.EnviarAlerta(alerta.Alerta);
            //}
        }

    }//fin de clase
}