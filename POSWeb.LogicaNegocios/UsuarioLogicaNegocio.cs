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
using POSWeb.LogicaNegocios.Tools;
using POSWeb.Encriptacion;

namespace POSWeb.LogicaNegocios
{
    /// <summary>
    /// Requerimiento:       MobileToken
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Logica de Negocio clase   UsuarioLogicaNegocio
    /// Ultima modificacion: 02/11/2017
    /// </summary>
    public class UsuarioLogicaNegocio
    {

        UsuarioAccesoDatos dal = new UsuarioAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();
        AutorizacionLogicaNegocio autorizacionLn = new AutorizacionLogicaNegocio();
        Encriptador oEncriptador = new Encriptador();


        /// <summary>
        /// Metodo para insertar un valor de tipo Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario InsertarUsuario(Usuario pUsuario)
        {
            var respuesta = new RespuestaUsuario();
            try
            {

                string CodigoAlerta = "UsuarioCreate";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pUsuario.UsrCreacion = respS.Sesion.CodigoUsuario;

                    //**** Contraseña temporal para el nuevo usuario***** 
                    //TODO: encriptar contraseña
                    string clave = new LnGenPassword().GetNewPassword();

                    //encriptamos la clave
                    pUsuario.Clave = oEncriptador.Encriptar(clave);

                    //EJECUTAR: se guarda la entidad
                    if (ValidacionesCreacion(pUsuario, ref mensajes))
                    {

                        //se define los datos necesarios para el usuario
                        pUsuario.IdEntidad = respS.Sesion.IdEntidad;

                        //obtenemos los roles del usuario
                        pUsuario.XMLData = Util.SerializarObjeto(pUsuario.Roles);

                        respuesta = dal.InsertarUsuario(pUsuario);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pUsuario, CodigoAlerta);

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

                        //****Se envia a crear la primera autorización***
                        Task.Factory.StartNew(() => InsertarAutorizacion(respuesta.Usuario));
                        //***********************************************

                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaUsuario { Respuesta = new Respuesta(Respuesta.CodNoValido), Usuario = respuesta.Usuario };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaUsuario { Respuesta = respS.Respuesta, Usuario = new Usuario() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Usuario
        /// </summary>
        /// <param name="pUsuario">Filtro de tipo Usuario</param>
        /// <returns></returns>
        public RespuestaListaUsuario ObtenerListaUsuario(Usuario pUsuario)
        {

            var respuesta = new RespuestaListaUsuario();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerUsuario(pUsuario);
                    return respuesta;
                }
                else
                    return new RespuestaListaUsuario { Respuesta = respS.Respuesta, ListaUsuario = new List<Usuario>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Usuario
        /// </summary>
        /// <param name="pUsuario">Filtro de tipo Usuario</param>
        /// <returns></returns>
        public RespuestaUsuario ObtenerUsuario(Usuario pUsuario)
        {
            var respuesta = new RespuestaUsuario();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pUsuario.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta.Usuario = dal.ObtenerUsuario(pUsuario).ListaUsuario?[0];
                    return respuesta.Usuario != null ?
                      new RespuestaUsuario { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Usuario = respuesta.Usuario } :
                      new RespuestaUsuario { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Usuario = new Usuario() };
                }
                else
                    return new RespuestaUsuario { Respuesta = respS.Respuesta, Usuario = new Usuario() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Usuario con paginacion
        /// </summary>
        /// <param name="pUsuario">Filtro de tipo Usuario</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaUsuario ObtenerUsuarioPaginado(Usuario pUsuario, Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaUsuario();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pUsuario.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta = dal.ObtenerUsuarioPaginado(pUsuario, pPaginacion);
                    return respuesta;
                }
                else
                    return new RespuestaListaUsuario { Respuesta = respS.Respuesta, ListaUsuario = new List<Usuario>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario ModificarUsuario(Usuario pUsuario)
        {
            var respuesta = new RespuestaUsuario();
            try
            {
                string CodigoAlerta = "UsuarioEdit";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    pUsuario.UsrModificacion = respS.Sesion.CodigoUsuario;
                    if (ValidacionesModificacion(pUsuario, ref mensajes))
                    {
                        //obtenemos los roles del usuario
                        pUsuario.XMLData = Util.SerializarObjeto(pUsuario.Roles);

                        respuesta = dal.ModificarUsuario(pUsuario);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaModificar,
                           TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pUsuario, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaUsuario { Respuesta = new Respuesta(Respuesta.CodNoValido), Usuario = respuesta.Usuario };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaUsuario { Respuesta = respS.Respuesta, Usuario = new Usuario() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario EliminarUsuario(Usuario pUsuario)
        {
            var respuesta = new RespuestaUsuario();
            try
            {
                string CodigoAlerta = "UsuarioDelete";
                List<string> mensajes = new List<string>();

                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pUsuario.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    if (ValidacionesEliminar(pUsuario, ref mensajes))
                    {
                        pUsuario.IdEntidad = respS.Sesion.IdEntidad;
                        //CONSULTA A ACCESO A DATOS
                        respuesta = dal.EliminarUsuario(pUsuario);
                        BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaEliminar,
                           TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                        Notificacion(pUsuario, CodigoAlerta);
                    }
                    else
                    {
                        //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                        respuesta = new RespuestaUsuario { Respuesta = new Respuesta(Respuesta.CodNoValido), Usuario = respuesta.Usuario };
                    }
                    return respuesta;
                }
                else
                    return new RespuestaUsuario { Respuesta = respS.Respuesta, Usuario = new Usuario() };


            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Valida el login de un usuario en el sistema.
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario ValidarUsuarioLogin(Usuario pUsuario)
        {
            var respuesta = new RespuestaUsuario();
            try
            {
                respuesta = dal.ValidarUsuarioLogin(pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaValidacion,
                           TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                //Se valida error
                if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
                {
                    //se procede a obtener el mensaje del error
                    var oResp = new Respuesta(respuesta.Respuesta.CodMensaje);
                    respuesta.Respuesta = oResp;
                    return respuesta;
                }

                //si todo estuvo correcto se crea una sesion al usuario
                var pSesion = new Sesion()
                {
                    IdEntidad = pUsuario.IdEntidad,
                    CodigoUsuario = pUsuario.CodigoUsuario,
                    IP = pUsuario.IP
                };
                var RespSesion = sesion.InsertarSesion(pSesion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                          TraceData.Utilities.Enums.TypeTrace.Info, RespSesion, MethodBase.GetCurrentMethod().Name);

                if (RespSesion.Respuesta.CodMensaje != Respuesta.CodExitoso)
                {
                    respuesta.Respuesta = RespSesion.Respuesta;
                    return respuesta;
                }

                //se da la respuesta con el token de sesion
                var oSesion = RespSesion.Sesion;
                respuesta.Usuario = new Usuario()
                {
                    UsrtokensAuthenticate = oSesion.Token
                };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
            return respuesta;
        }

        /// <summary>
        /// Método que cambia la contraseña del usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario CambiarContrasena(Usuario pUsuario)
        {
            var respuesta = new RespuestaUsuario();
            try
            {
                string CodigoAlerta = "UsuarioCambioPass";

                //validamos la contraseña a cambiar
                var oRespContrasena = ValidarContrasenna(pUsuario);
                if (oRespContrasena.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.CambiarContrasena(pUsuario);
                    BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaCambioContrasena,
                       TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                    Notificacion(pUsuario, CodigoAlerta);

                    return respuesta;
                }
                else
                {
                    return oRespContrasena;
                }
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// se envia la primera autorizacion
        /// </summary>
        /// <param name="pUsuario"></param>
        public void InsertarAutorizacion(Usuario pUsuario)
        {
            var pAutorizacion = new Autorizacion()
            {
                Recurso = pUsuario.NombreControlador,
                IdRecurso = pUsuario.CodigoUsuario,
                Descripcion = pUsuario.Nombre,
                IdEntidad = pUsuario.IdEntidad,
                UsrCreacion = pUsuario.UsrCreacion,
                NombreControlador = pUsuario.NombreControlador
            };

            autorizacionLn.PrimeraAutorizacion(pAutorizacion);
        }

        /// <summary>
        /// Método que valida la contraseña del usuario.
        /// </summary>
        /// <param name="oUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario ValidarContrasenna(Usuario oUsuario)
        {
            var oRespuesta = new RespuestaUsuario { Respuesta = new Respuesta() { CodMensaje = Respuesta.CodError } };
            try
            {
                var catalogoLn = new CatalogoLogicaNegocio();

                //desencriptamos la contraseña enviada
                string clave = oEncriptador.DesEncriptar(oUsuario.Clave);

                /*************  Obtener Longitud *******************/
                var longitudPassword = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Catalogo() { CodigoParametro = Constantes.Catalogo.LongitudPassword }).Catalogo.Identificador);
                /*************  Obtener Numeros *******************/
                var cantidadNumeros = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Catalogo() { CodigoParametro = Constantes.Catalogo.CntMinNumPassword }).Catalogo.Identificador);
                /*************  Obtener Mayusculas *******************/
                var cantidadMayusculas = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Catalogo() { CodigoParametro = Constantes.Catalogo.CntMinMayusculasPassword }).Catalogo.Identificador);
                /*************  Obtener Simbolos *******************/
                var cantidadSimbolos = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Catalogo() { CodigoParametro = Constantes.Catalogo.CntMinSimbolosPassword }).Catalogo.Identificador);


                int contNum = 0; int contMayus = 0; int contSymbol = 0;
                foreach (char c in clave)
                {
                    if (char.IsNumber(c))
                    {
                        contNum++;
                    }
                    else if (char.IsUpper(c))
                    {
                        contMayus++;
                    }
                    else if (char.IsSymbol(c) | char.IsPunctuation(c))
                    {
                        contSymbol++;
                    }
                }
                var oMensajes = new List<string>();
                //Valida la cantidad mínima de caracteres
                if (oUsuario.Clave.Length < longitudPassword)
                {
                    oMensajes.Add(string.Format(MensajesValidaciones.PassMinCaracteres, longitudPassword));
                }
                //Valida la cantidad mínima de números
                if (contNum < cantidadNumeros)
                {
                    oMensajes.Add(string.Format(MensajesValidaciones.PassMinNumeros, cantidadNumeros));
                }
                //Valida la cantidad mínima de mayúsculas
                if (contMayus < cantidadMayusculas)
                {
                    oMensajes.Add(string.Format(MensajesValidaciones.PassMinMayusculas, cantidadMayusculas));
                }
                //Valida la cantidad mínima de símbolos 
                if (contSymbol < cantidadSimbolos)
                {
                    oMensajes.Add(string.Format(MensajesValidaciones.PassMinSimbolos, cantidadSimbolos));
                }

                if (oMensajes.Count != 0)
                {
                    //Contraseña incorrecta
                    oRespuesta.Respuesta.Mensaje = oMensajes.FirstOrDefault();
                    oRespuesta.Respuesta.Mensajes = oMensajes;
                    return oRespuesta;
                }

                //contraseña correcta
                return new RespuestaUsuario()
                {
                    Respuesta = new Respuesta()
                    {
                        CodMensaje = Respuesta.CodExitoso
                    }
                };

            }
            catch (Exception oException)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(oException, oUsuario);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, oException, MethodBase.GetCurrentMethod().Name);

                return new RespuestaUsuario { Respuesta = new Respuesta(TipoRespuesta.Excepcion, oException.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(Usuario pUsuario, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pUsuario != null)
            {
                isValid = Utilidades.Util.EntidadValida(pUsuario, ref pMensajes);

                if (String.IsNullOrEmpty(pUsuario.UsrCreacion))
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
        /// <param name="pUsuario"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(Usuario pUsuario, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pUsuario != null)
            {
                //isValid = Utilidades.Util.EntidadValida(pUsuario, ref pMensajes);

                if (String.IsNullOrEmpty(pUsuario.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
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
        /// <param name="pUsuario"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(Usuario pUsuario, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pUsuario != null)
            {
                if (string.IsNullOrEmpty(pUsuario.CodigoUsuario))
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Usuario"));
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
        /// <param name="pUsuario"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Usuario pUsuario, string CodigoAlerta)
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
