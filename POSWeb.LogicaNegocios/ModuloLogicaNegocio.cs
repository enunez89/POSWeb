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
    /// Propósito:           Logica de Negocio clase   ModuloLogicaNegocio
    /// Ultima modificacion: 04/11/2017
    /// </summary>
    public class ModuloLogicaNegocio
    {

        ModuloAccesoDatos dal = new ModuloAccesoDatos();
        //SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        ///// <summary>
        ///// Metodo para insertar un valor de tipo Modulo
        ///// </summary>
        ///// <param name="pModulo"></param>
        ///// <returns></returns>
        //public RespuestaModulo InsertarModulo(Modulo pModulo)
        //{
        //    var respuesta = new RespuestaModulo();
        //    try
        //    {

        //        string CodigoAlerta = "ModuloCreate";
        //        List<string> mensajes = new List<string>();

        //        RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pModulo.UsrtokensAuthenticate });
        //        if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
        //        {
        //            pModulo.UsrCreacion = respS.Sesion.CodigoUsuario;
        //            //EJECUTAR: se guarda la entidad
        //            if (ValidacionesCreacion(pModulo, ref mensajes))
        //            {
        //                respuesta = dal.InsertarModulo(pModulo);
        //                //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pModulo, ACCIONES.AGREGAR);
        //                Notificacion(pModulo, CodigoAlerta);
        //            }
        //            else
        //            {
        //                //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
        //                new RespuestaModulo { Respuesta = new Respuesta(Respuesta.CodNoValido), Modulo = respuesta.Modulo };
        //            }
        //            return respuesta;
        //        }
        //        else
        //            return new RespuestaModulo { Respuesta = respS.Respuesta, Modulo = new Modulo() };
        //    }
        //    catch (Exception ex)
        //    {
        //        ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pModulo);
        //        return new RespuestaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
        //    }
        //}


        ///// <summary>
        ///// Metodo que sirve para Obtener la lista de tipo Modulo
        ///// </summary>
        ///// <param name="pModulo">Filtro de tipo Modulo</param>
        ///// <returns></returns>
        //public RespuestaListaModulo ObtenerListaModulo(Modulo pModulo)
        //{

        //    var respuesta = new RespuestaListaModulo();
        //    try
        //    {
        //        RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pModulo.UsrtokensAuthenticate });
        //        if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
        //        {
        //            respuesta = dal.ObtenerModulo(pModulo);
        //            return respuesta;
        //        }
        //        else
        //            return new RespuestaListaModulo { Respuesta = respS.Respuesta, ListaModulo = new List<Modulo>() };
        //    }
        //    catch (Exception ex)
        //    {
        //        ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pModulo);
        //        return new RespuestaListaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
        //    }
        //}

        ///// <summary>
        ///// Metodo que sirve para Obtener la lista de tipo Modulo
        ///// </summary>
        ///// <param name="pModulo">Filtro de tipo Modulo</param>
        ///// <returns></returns>
        //public RespuestaModulo ObtenerModulo(Modulo pModulo)
        //{
        //    var respuesta = new RespuestaModulo();
        //    try
        //    {
        //        RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pModulo.UsrtokensAuthenticate });
        //        if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
        //        {
        //            respuesta.Modulo = dal.ObtenerModulo(pModulo).ListaModulo?[0];
        //            return respuesta.Modulo != null ?
        //              new RespuestaModulo { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Modulo = respuesta.Modulo } :
        //              new RespuestaModulo { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Modulo = new Modulo() };
        //        }
        //        else
        //            return new RespuestaModulo { Respuesta = respS.Respuesta, Modulo = new Modulo() };
        //    }
        //    catch (Exception ex)
        //    {
        //        ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pModulo);
        //        return new RespuestaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
        //    }
        //}

        ///// <summary>
        ///// Metodo que sirve para Obtener la lista de tipo Modulo con paginacion
        ///// </summary>
        ///// <param name="pModulo">Filtro de tipo Modulo</param>
        ///// <param name="pPaginacion">Uso de Paginacion</param>
        ///// <returns></returns>
        //public RespuestaListaModulo ObtenerModuloPaginado(Modulo pModulo, ref Paginacion pPaginacion)
        //{
        //    var respuesta = new RespuestaListaModulo();
        //    try
        //    {
        //        RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pModulo.UsrtokensAuthenticate });
        //        if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
        //        {
        //            respuesta = dal.ObtenerModuloPaginado(pModulo, ref pPaginacion);
        //            return respuesta;
        //        }
        //        else
        //            return new RespuestaListaModulo { Respuesta = respS.Respuesta, ListaModulo = new List<Modulo>() };
        //    }
        //    catch (Exception ex)
        //    {
        //        ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pModulo);
        //        return new RespuestaListaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
        //    }
        //}


        ///// <summary>
        ///// Metodo que sirve para Modificar un objeto de tipo Modulo
        ///// </summary>
        ///// <param name="pModulo"></param>
        ///// <returns></returns>
        //public RespuestaModulo ModificarModulo(Modulo pModulo)
        //{
        //    var respuesta = new RespuestaModulo();
        //    try
        //    {
        //        string CodigoAlerta = "ModuloEdit";
        //        List<string> mensajes = new List<string>();

        //        RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pModulo.UsrtokensAuthenticate });
        //        if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
        //        {
        //            //VALIDACION: Entidad no puede venir vacio
        //            pModulo.UsrModificacion = respS.Sesion.CodigoUsuario;
        //            if (ValidacionesModificacion(pModulo, ref mensajes))
        //            {
        //                respuesta = dal.ModificarModulo(pModulo);
        //                //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pModulo, ACCIONES.MODIFICAR);
        //                Notificacion(pModulo, CodigoAlerta);
        //            }
        //            else
        //            {
        //                //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
        //                new RespuestaModulo { Respuesta = new Respuesta(Respuesta.CodNoValido), Modulo = respuesta.Modulo };
        //            }
        //            return respuesta;
        //        }
        //        else
        //            return new RespuestaModulo { Respuesta = respS.Respuesta, Modulo = new Modulo() };


        //    }
        //    catch (Exception ex)
        //    {
        //        ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pModulo);
        //        return new RespuestaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
        //    }
        //}


        ///// <summary>
        ///// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Modulo
        ///// </summary>
        ///// <param name="pModulo"></param>
        ///// <returns></returns>
        //public RespuestaModulo EliminarModulo(Modulo pModulo)
        //{
        //    var respuesta = new RespuestaModulo();
        //    try
        //    {
        //        string CodigoAlerta = "ModuloDelete";
        //        List<string> mensajes = new List<string>();

        //        RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pModulo.UsrtokensAuthenticate });
        //        if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
        //        {
        //            //VALIDACION: Entidad no puede venir vacio
        //            if (ValidacionesEliminar(pModulo, ref mensajes))
        //            {
        //                //CONSULTA A ACCESO A DATOS
        //                respuesta = dal.EliminarModulo(pModulo);
        //                //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pModulo, ACCIONES.BORRAR);
        //                Notificacion(pModulo, CodigoAlerta);
        //            }
        //            else
        //            {
        //                //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
        //                new RespuestaModulo { Respuesta = new Respuesta(Respuesta.CodNoValido), Modulo = respuesta.Modulo };
        //            }
        //            return respuesta;
        //        }
        //        else
        //            return new RespuestaModulo { Respuesta = respS.Respuesta, Modulo = new Modulo() };


        //    }
        //    catch (Exception ex)
        //    {
        //        ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pModulo);
        //        return new RespuestaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
        //    }
        //}

        /// <summary>
        /// Obtener menu del usuario logeado.
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaListaModulo ObtenerMenuUsuario(Usuario pUsuario)
        {

            var respuesta = new RespuestaListaModulo();
            try
            {
                respuesta = dal.ObtenerMenuUsuario(pUsuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);
                return new RespuestaListaModulo { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pModulo"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(Modulo pModulo, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pModulo != null)
            {
                isValid = Utilidades.Util.EntidadValida(pModulo, ref pMensajes);

                if (String.IsNullOrEmpty(pModulo.UsrCreacion))
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
        /// <param name="pModulo"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(Modulo pModulo, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pModulo != null)
            {
                isValid = Utilidades.Util.EntidadValida(pModulo, ref pMensajes);

                if (String.IsNullOrEmpty(pModulo.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
                    isValid = false;
                }

                if (pModulo.IdModulo == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Modulo"));
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
        /// <param name="pModulo"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(Modulo pModulo, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pModulo != null)
            {
                if (pModulo.IdModulo == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Modulo"));
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
        /// <param name="pModulo"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Modulo pModulo, string CodigoAlerta)
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