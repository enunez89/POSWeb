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
    /// Propósito:           Logica de Negocio clase   AlertaEntidadLogicaNegocio
    /// Ultima modificacion: 01/12/2017
    /// </summary>
    public class AlertaEntidadLogicaNegocio
    {

		AlertaEntidadAccesoDatos dal = new AlertaEntidadAccesoDatos();
		SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public  RespuestaAlertaEntidad InsertarAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
            var respuesta = new RespuestaAlertaEntidad();
            try
            {  
					
			    string CodigoAlerta = "AlertaEntidadCreate";
				List<string> mensajes = new List<string>();
				
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlertaEntidad.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
					pAlertaEntidad.UsrCreacion = respS.Sesion.CodigoUsuario;
                    pAlertaEntidad.IdEntidad = respS.Sesion.IdEntidad;
					//EJECUTAR: se guarda la entidad
					if (ValidacionesCreacion(pAlertaEntidad, ref mensajes))
					{
						respuesta = dal.InsertarAlertaEntidad(pAlertaEntidad);
						BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar, 
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
						Notificacion(pAlertaEntidad, CodigoAlerta);
					}
					else
					{   
						//NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
						respuesta = new RespuestaAlertaEntidad { Respuesta = new Respuesta(Respuesta.CodNoValido), AlertaEntidad = respuesta.AlertaEntidad };
					}	
					return respuesta;					
                }
                else
					return new RespuestaAlertaEntidad { Respuesta = respS.Respuesta, AlertaEntidad = new AlertaEntidad() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlertaEntidad);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
		    

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad">Filtro de tipo AlertaEntidad</param>
        /// <returns></returns>
		public  RespuestaListaAlertaEntidad ObtenerListaAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
			          
			var respuesta = new RespuestaListaAlertaEntidad();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlertaEntidad.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAlertaEntidad.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta = dal.ObtenerAlertaEntidad(pAlertaEntidad);
					return respuesta;
                }
                else
					return new RespuestaListaAlertaEntidad { Respuesta = respS.Respuesta, ListaAlertaEntidad = new List<AlertaEntidad>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlertaEntidad);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaListaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

		/// <summary>
        /// Metodo que sirve para Obtener la lista de tipo AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad">Filtro de tipo AlertaEntidad</param>
        /// <returns></returns>
		public  RespuestaAlertaEntidad ObtenerAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {					
            var respuesta = new RespuestaAlertaEntidad();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlertaEntidad.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAlertaEntidad.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta.AlertaEntidad = dal.ObtenerAlertaEntidad(pAlertaEntidad).ListaAlertaEntidad?[0];
                    return respuesta.AlertaEntidad != null ?
                      new RespuestaAlertaEntidad { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), AlertaEntidad = respuesta.AlertaEntidad } :
                      new RespuestaAlertaEntidad { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), AlertaEntidad = new AlertaEntidad() };					
                }
                else
					return new RespuestaAlertaEntidad { Respuesta = respS.Respuesta, AlertaEntidad = new AlertaEntidad() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlertaEntidad);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

		/// <summary>
        /// Metodo que sirve para Obtener la lista de tipo AlertaEntidad con paginacion
        /// </summary>
        /// <param name="pAlertaEntidad">Filtro de tipo AlertaEntidad</param>
		/// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaAlertaEntidad ObtenerAlertaEntidadPaginado(AlertaEntidad pAlertaEntidad, Paginacion pPaginacion)
        {
			var respuesta = new RespuestaListaAlertaEntidad();
            try
            {
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlertaEntidad.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAlertaEntidad.IdEntidad = respS.Sesion.IdEntidad;
					respuesta = dal.ObtenerAlertaEntidadPaginado(pAlertaEntidad, pPaginacion);
					return respuesta;
                }
                else
					return new RespuestaListaAlertaEntidad { Respuesta = respS.Respuesta, ListaAlertaEntidad = new List<AlertaEntidad>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlertaEntidad);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaListaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


		 /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
		public  RespuestaAlertaEntidad ModificarAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
            var respuesta = new RespuestaAlertaEntidad();
            try
            {     
				string CodigoAlerta = "AlertaEntidadEdit";	
				List<string> mensajes = new List<string>();

				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlertaEntidad.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    pAlertaEntidad.IdEntidad = respS.Sesion.IdEntidad;
                    pAlertaEntidad.UsrModificacion = respS.Sesion.CodigoUsuario;
					if (ValidacionesModificacion(pAlertaEntidad, ref mensajes))
					{
						respuesta = dal.ModificarAlertaEntidad(pAlertaEntidad);
						BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaModificar, 
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
						Notificacion(pAlertaEntidad, CodigoAlerta);
					}
					else
					{   
						//NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
						respuesta = new RespuestaAlertaEntidad { Respuesta = new Respuesta(Respuesta.CodNoValido), AlertaEntidad = respuesta.AlertaEntidad };
					}
					return respuesta;			
                }
                else
					return new RespuestaAlertaEntidad { Respuesta = respS.Respuesta, AlertaEntidad = new AlertaEntidad() };
					

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlertaEntidad);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


		/// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public RespuestaAlertaEntidad EliminarAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {				
			var respuesta = new RespuestaAlertaEntidad();
            try
            {
				string CodigoAlerta = "AlertaEntidadDelete";
                List<string> mensajes = new List<string>();

				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlertaEntidad.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    //VALIDACION: Entidad no puede venir vacio
                    pAlertaEntidad.IdEntidad = respS.Sesion.IdEntidad;
                    if (ValidacionesEliminar(pAlertaEntidad, ref mensajes))
					{
						//CONSULTA A ACCESO A DATOS
						respuesta = dal.EliminarAlertaEntidad(pAlertaEntidad);
						BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaEliminar, 
                            TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
						Notificacion(pAlertaEntidad, CodigoAlerta);
					}
					else
					{   
						//NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
						respuesta = new RespuestaAlertaEntidad { Respuesta = new Respuesta(Respuesta.CodNoValido), AlertaEntidad = respuesta.AlertaEntidad };
					}
					return respuesta;			
                }
                else
					return new RespuestaAlertaEntidad { Respuesta = respS.Respuesta, AlertaEntidad = new AlertaEntidad() };
					

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlertaEntidad);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
						   
                return new RespuestaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(AlertaEntidad pAlertaEntidad, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pAlertaEntidad != null)
            {
                isValid = Utilidades.Util.EntidadValida(pAlertaEntidad, ref pMensajes);

                if (String.IsNullOrEmpty(pAlertaEntidad.UsrCreacion))
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
        /// <param name="pAlertaEntidad"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(AlertaEntidad pAlertaEntidad, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pAlertaEntidad != null)
            {
                isValid = Utilidades.Util.EntidadValida(pAlertaEntidad, ref pMensajes);

                if (String.IsNullOrEmpty(pAlertaEntidad.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
                    isValid = false;
                }

                if (pAlertaEntidad.Id == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "AlertaEntidad"));
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
        /// <param name="pAlertaEntidad"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(AlertaEntidad pAlertaEntidad, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pAlertaEntidad != null)
            {
                if (pAlertaEntidad.Id == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "AlertaEntidad"));
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
        /// <param name="pAlertaEntidad"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(AlertaEntidad pAlertaEntidad, string CodigoAlerta)
        {
			AlertaLogicaNegocio al = new AlertaLogicaNegocio();
			RespuestaAlerta alerta = al.ObtenerAlerta( new AlertaBase() { CodigoAlerta = CodigoAlerta, Activo = true });
            if (alerta != null && alerta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
				//al.EnviarAlerta(alerta.Alerta);
            }
        }
		
    }//fin de clase
}		