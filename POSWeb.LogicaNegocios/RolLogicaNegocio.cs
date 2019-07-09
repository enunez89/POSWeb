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
    /// Propósito:           Logica de Negocio clase   RolLogicaNegocio
    /// Ultima modificacion: 13/11/2017
    /// </summary>
    public class RolLogicaNegocio
    {

        RolAccesoDatos dal = new RolAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();
        AutorizacionLogicaNegocio autorizacionLn = new AutorizacionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaRol InsertarRol(Rol pRol)
        {
            var respuesta = new RespuestaRol();
            try
            {

                string CodigoAlerta = "RolCreate";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pRol.UsrCreacion = respS.Sesion.CodigoUsuario;
                    //EJECUTAR: se guarda la entidad
                    if (ValidacionesCreacion(pRol, ref mensajes))
                    {
                        //obtenemos la lista de roles
                        pRol.XMLData = Util.SerializarObjeto(pRol.Perfiles);

                        respuesta = dal.InsertarRol(pRol);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                        //Se valida error
                        if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
                        {
                            //se procede a obtener el mensaje del error
                            var oResp = new Respuesta(respuesta.Respuesta.CodMensaje);

                            if (string.IsNullOrEmpty(oResp.Mensaje))
                            {
                                oResp.Mensaje = Mensajes.msjErrorGenerico;
                            }
                            respuesta.Respuesta = oResp;
                            return respuesta;
                        }

                        Notificacion(pRol, CodigoAlerta);

                        //****Se envia a crear la primera autorización***
                        Task.Factory.StartNew(() => InsertarAutorizacion(respuesta.Rol));
                        //***********************************************

                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaRol { Respuesta = new Respuesta(Respuesta.CodNoValido), Rol = respuesta.Rol };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaRol { Respuesta = respS.Respuesta, Rol = new Rol() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Rol
        /// </summary>
        /// <param name="pRol">Filtro de tipo Rol</param>
        /// <returns></returns>
        public RespuestaListaRol ObtenerListaRol(Rol pRol)
        {

            var respuesta = new RespuestaListaRol();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerRol(pRol);
                    return respuesta;
                }
                else
                    return new RespuestaListaRol { Respuesta = respS.Respuesta, ListaRol = new List<Rol>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Rol
        /// </summary>
        /// <param name="pRol">Filtro de tipo Rol</param>
        /// <returns></returns>
        public RespuestaRol ObtenerRol(Rol pRol)
        {
            var respuesta = new RespuestaRol();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.Rol = dal.ObtenerRol(pRol).ListaRol?[0];
                    return respuesta.Rol != null ?
                      new RespuestaRol { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Rol = respuesta.Rol } :
                      new RespuestaRol { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Rol = new Rol() };
                }
                else
                    return new RespuestaRol { Respuesta = respS.Respuesta, Rol = new Rol() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Rol con paginacion
        /// </summary>
        /// <param name="pRol">Filtro de tipo Rol</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaRol ObtenerRolPaginado(Rol pRol, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaRol();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerRolPaginado(pRol, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaRol { Respuesta = respS.Respuesta, ListaRol = new List<Rol>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaRol ModificarRol(Rol pRol)
        {
            var respuesta = new RespuestaRol();
            try
            {
                string CodigoAlerta = "RolEdit";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    pRol.UsrModificacion = respS.Sesion.CodigoUsuario;
                    if (ValidacionesModificacion(pRol, ref mensajes))
                    {
                        //obtenemos la lista de roles
                        pRol.XMLData = Util.SerializarObjeto(pRol.Perfiles);

                        respuesta = dal.ModificarRol(pRol);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaModificar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pRol, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaRol { Respuesta = new Respuesta(Respuesta.CodNoValido), Rol = respuesta.Rol };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaRol { Respuesta = respS.Respuesta, Rol = new Rol() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaRol EliminarRol(Rol pRol)
        {
            var respuesta = new RespuestaRol();
            try
            {
                string CodigoAlerta = "RolDelete";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    if (ValidacionesEliminar(pRol, ref mensajes))
                    {
                        //CONSULTA A ACCESO A DATOS
                        respuesta = dal.EliminarRol(pRol);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaEliminar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pRol, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaRol { Respuesta = new Respuesta(Respuesta.CodNoValido), Rol = respuesta.Rol };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaRol { Respuesta = respS.Respuesta, Rol = new Rol() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Obtiene una lista de roles por usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaListaRol ObtenerRolPorUsuario(Usuario pUsuario)
        {

            var respuesta = new RespuestaListaRol();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerRolPorUsuario(pUsuario);
                    return respuesta;
                }
                else
                    return new RespuestaListaRol { Respuesta = respS.Respuesta, ListaRol = new List<Rol>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaRol { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// se envia la primera autorizacion
        /// </summary>
        /// <param name="pRol"></param>
        public void InsertarAutorizacion(Rol pRol)
        {
            var pAutorizacion = new Autorizacion()
            {
                Recurso = pRol.NombreControlador,
                IdRecurso = pRol.IdRol.ToString(),
                Descripcion = pRol.Nombre,
                IdEntidad = pRol.IdEntidad,
                UsrCreacion = pRol.UsrCreacion,
                NombreControlador = pRol.NombreControlador
            };

            autorizacionLn.PrimeraAutorizacion(pAutorizacion);
        }

        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pRol"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(Rol pRol, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pRol != null)
            {
                isValid = Utilidades.Util.EntidadValida(pRol, ref pMensajes);

                if (String.IsNullOrEmpty(pRol.UsrCreacion))
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
        /// Validacion en entidad para modificar
        /// </summary>
        /// <param name="pRol"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(Rol pRol, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pRol != null)
            {
                isValid = Utilidades.Util.EntidadValida(pRol, ref pMensajes);

                if (String.IsNullOrEmpty(pRol.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
                    isValid = false;
                }

                if (pRol.IdRol == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Rol"));
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
        /// Validacion para entidad al Eliminar
        /// </summary>
        /// <param name="pRol"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(Rol pRol, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pRol != null)
            {
                if (pRol.IdRol == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Rol"));
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
        /// <param name="pRol"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Rol pRol, string CodigoAlerta)
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