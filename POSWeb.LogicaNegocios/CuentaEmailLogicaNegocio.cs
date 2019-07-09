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
    /// Propósito:           Logica de Negocio clase   CuentaEmailLogicaNegocio
    /// Ultima modificacion: 25/11/2017
    /// </summary>
    public class CuentaEmailLogicaNegocio
    {

        CuentaEmailAccesoDatos dal = new CuentaEmailAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaCuentaEmail InsertarCuentaEmail(CuentaEmail pCuentaEmail)
        {
            var respuesta = new RespuestaCuentaEmail();
            try
            {
                string CodigoAlerta = "CuentaEmailCreate";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCuentaEmail.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pCuentaEmail.UsrCreacion = respS.Sesion.CodigoUsuario;
                    pCuentaEmail.IdEntidad = respS.Sesion.IdEntidad;
                    //EJECUTAR: se guarda la entidad
                    if (ValidacionesCreacion(pCuentaEmail, ref mensajes))
                    {
                        respuesta = dal.InsertarCuentaEmail(pCuentaEmail);
                        //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pCuentaEmail, ACCIONES.AGREGAR);
                        Notificacion(pCuentaEmail, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        return new RespuestaCuentaEmail { Respuesta = new Respuesta(Respuesta.CodNoValido), CuentaEmail = respuesta.CuentaEmail };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaCuentaEmail { Respuesta = respS.Respuesta, CuentaEmail = new CuentaEmail() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCuentaEmail);
                return new RespuestaCuentaEmail { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail">Filtro de tipo CuentaEmail</param>
        /// <returns></returns>
        public RespuestaListaCuentaEmail ObtenerListaCuentaEmail(CuentaEmail pCuentaEmail)
        {

            var respuesta = new RespuestaListaCuentaEmail();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCuentaEmail.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pCuentaEmail.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta = dal.ObtenerCuentaEmail(pCuentaEmail);
                    return respuesta;
                }
                else
                    return new RespuestaListaCuentaEmail { Respuesta = respS.Respuesta, ListaCuentaEmail = new List<CuentaEmail>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCuentaEmail);
                return new RespuestaListaCuentaEmail { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail">Filtro de tipo CuentaEmail</param>
        /// <returns></returns>
        public RespuestaCuentaEmail ObtenerCuentaEmail(CuentaEmail pCuentaEmail)
        {

            var respuesta = new RespuestaCuentaEmail();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCuentaEmail.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pCuentaEmail.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta.CuentaEmail = dal.ObtenerCuentaEmail(pCuentaEmail).ListaCuentaEmail?[0];
                    return respuesta.CuentaEmail != null ?
                      new RespuestaCuentaEmail { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), CuentaEmail = respuesta.CuentaEmail } :
                      new RespuestaCuentaEmail { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), CuentaEmail = new CuentaEmail() };
                }
                else
                    return new RespuestaCuentaEmail { Respuesta = respS.Respuesta, CuentaEmail = new CuentaEmail() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCuentaEmail);
                return new RespuestaCuentaEmail { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo CuentaEmail con paginacion
        /// </summary>
        /// <param name="pCuentaEmail">Filtro de tipo CuentaEmail</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaCuentaEmail ObtenerCuentaEmailPaginado(CuentaEmail pCuentaEmail, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaCuentaEmail();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCuentaEmail.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pCuentaEmail.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta = dal.ObtenerCuentaEmailPaginado(pCuentaEmail, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaCuentaEmail { Respuesta = respS.Respuesta, ListaCuentaEmail = new List<CuentaEmail>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCuentaEmail);
                return new RespuestaListaCuentaEmail { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaCuentaEmail ModificarCuentaEmail(CuentaEmail pCuentaEmail)
        {
            var respuesta = new RespuestaCuentaEmail();
            try
            {
                string CodigoAlerta = "CuentaEmailEdit";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCuentaEmail.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    pCuentaEmail.UsrModificacion = respS.Sesion.CodigoUsuario;
                    pCuentaEmail.IdEntidad = respS.Sesion.IdEntidad;
                    if (ValidacionesModificacion(pCuentaEmail, ref mensajes))
                    {
                        respuesta = dal.ModificarCuentaEmail(pCuentaEmail);
                        //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pCuentaEmail, ACCIONES.MODIFICAR);
                        Notificacion(pCuentaEmail, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        return new RespuestaCuentaEmail { Respuesta = new Respuesta(Respuesta.CodNoValido), CuentaEmail = respuesta.CuentaEmail };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaCuentaEmail { Respuesta = respS.Respuesta, CuentaEmail = new CuentaEmail() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCuentaEmail);
                return new RespuestaCuentaEmail { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaCuentaEmail EliminarCuentaEmail(CuentaEmail pCuentaEmail)
        {
            var respuesta = new RespuestaCuentaEmail();
            try
            {
                string CodigoAlerta = "CuentaEmailDelete";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pCuentaEmail.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pCuentaEmail.IdEntidad = respS.Sesion.IdEntidad;
                    //VALIDACION: Entidad no puede venir vacio
                    if (ValidacionesEliminar(pCuentaEmail, ref mensajes))
                    {
                        //CONSULTA A ACCESO A DATOS
                        respuesta = dal.EliminarCuentaEmail(pCuentaEmail);
                        //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pCuentaEmail, ACCIONES.BORRAR);
                        Notificacion(pCuentaEmail, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        return new RespuestaCuentaEmail { Respuesta = new Respuesta(Respuesta.CodNoValido), CuentaEmail = respuesta.CuentaEmail };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaCuentaEmail { Respuesta = respS.Respuesta, CuentaEmail = new CuentaEmail() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pCuentaEmail);
                return new RespuestaCuentaEmail { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(CuentaEmail pCuentaEmail, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pCuentaEmail != null)
            {
                isValid = Utilidades.Util.EntidadValida(pCuentaEmail, ref pMensajes);

                if (String.IsNullOrEmpty(pCuentaEmail.UsrCreacion))
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
        /// <param name="pCuentaEmail"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(CuentaEmail pCuentaEmail, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pCuentaEmail != null)
            {
                isValid = Utilidades.Util.EntidadValida(pCuentaEmail, ref pMensajes);

                if (String.IsNullOrEmpty(pCuentaEmail.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
                    isValid = false;
                }

                if (pCuentaEmail.Id == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "CuentaEmail"));
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
        /// <param name="pCuentaEmail"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(CuentaEmail pCuentaEmail, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pCuentaEmail != null)
            {
                if (pCuentaEmail.Id == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "CuentaEmail"));
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
        /// <param name="pCuentaEmail"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(CuentaEmail pCuentaEmail, string CodigoAlerta)
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
