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
    /// Propósito:           Logica de Negocio clase   PerfilLogicaNegocio
    /// Ultima modificacion: 15/11/2017
    /// </summary>
    public class PerfilLogicaNegocio
    {

		PerfilAccesoDatos dal = new PerfilAccesoDatos();
		SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public  RespuestaPerfil InsertarPerfil(Perfil pPerfil)
        {
            var respuesta = new RespuestaPerfil();
            try
            {  
					
			    string CodigoAlerta = "PerfilCreate";
				List<string> mensajes = new List<string>();
				
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPerfil.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
					pPerfil.UsrCreacion = respS.Sesion.CodigoUsuario;
					//EJECUTAR: se guarda la entidad
					if (ValidacionesCreacion(pPerfil, ref mensajes))
					{
						respuesta = dal.InsertarPerfil(pPerfil);
						//LnBitacoraAuditoria.RegistrarBitacora(respuesta, pPerfil, ACCIONES.AGREGAR);
						Notificacion(pPerfil, CodigoAlerta);
					}
					else
					{   
						//NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
						respuesta = new RespuestaPerfil { Respuesta = new Respuesta(Respuesta.CodNoValido), Perfil = respuesta.Perfil };
					}	
					return respuesta;					
                }
                else
					return new RespuestaPerfil { Respuesta = respS.Respuesta, Perfil = new Perfil() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPerfil);
                return new RespuestaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }		    

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Perfil
        /// </summary>
        /// <param name="pPerfil">Filtro de tipo Perfil</param>
        /// <returns></returns>
		public  RespuestaListaPerfil ObtenerListaPerfil(Perfil pPerfil)
        {
			          
			var respuesta = new RespuestaListaPerfil();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPerfil.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pPerfil.IdEntidad = respS.Sesion.IdEntidad;
					respuesta = dal.ObtenerPerfil(pPerfil);
					return respuesta;
                }
                else
					return new RespuestaListaPerfil { Respuesta = respS.Respuesta, ListaPerfil = new List<Perfil>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPerfil);
                return new RespuestaListaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

		/// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Perfil
        /// </summary>
        /// <param name="pPerfil">Filtro de tipo Perfil</param>
        /// <returns></returns>
		public  RespuestaPerfil ObtenerPerfil(Perfil pPerfil)
        {					
            var respuesta = new RespuestaPerfil();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPerfil.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.Perfil = dal.ObtenerPerfil(pPerfil).ListaPerfil?[0];
                    return respuesta.Perfil != null ?
                      new RespuestaPerfil { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Perfil = respuesta.Perfil } :
                      new RespuestaPerfil { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Perfil = new Perfil() };					
                }
                else
					return new RespuestaPerfil { Respuesta = respS.Respuesta, Perfil = new Perfil() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPerfil);
                return new RespuestaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

		/// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Perfil con paginacion
        /// </summary>
        /// <param name="pPerfil">Filtro de tipo Perfil</param>
		/// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaPerfil ObtenerPerfilPaginado(Perfil pPerfil, ref Paginacion pPaginacion)
        {
			var respuesta = new RespuestaListaPerfil();
            try
            {
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPerfil.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
					respuesta = dal.ObtenerPerfilPaginado(pPerfil, ref pPaginacion);
					return respuesta;
                }
                else
					return new RespuestaListaPerfil { Respuesta = respS.Respuesta, ListaPerfil = new List<Perfil>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPerfil);
                return new RespuestaListaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


		 /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
		public  RespuestaPerfil ModificarPerfil(Perfil pPerfil)
        {
            var respuesta = new RespuestaPerfil();
            try
            {     
				string CodigoAlerta = "PerfilEdit";	
				List<string> mensajes = new List<string>();

				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPerfil.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
					//VALIDACION: Entidad no puede venir vacio
					pPerfil.UsrModificacion = respS.Sesion.CodigoUsuario;
					if (ValidacionesModificacion(pPerfil, ref mensajes))
					{
						respuesta = dal.ModificarPerfil(pPerfil);
						//LnBitacoraAuditoria.RegistrarBitacora(respuesta, pPerfil, ACCIONES.MODIFICAR);
						Notificacion(pPerfil, CodigoAlerta);
					}
					else
					{   
						//NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
						respuesta = new RespuestaPerfil { Respuesta = new Respuesta(Respuesta.CodNoValido), Perfil = respuesta.Perfil };
					}
					return respuesta;			
                }
                else
					return new RespuestaPerfil { Respuesta = respS.Respuesta, Perfil = new Perfil() };
					

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPerfil);
                return new RespuestaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


		/// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public RespuestaPerfil EliminarPerfil(Perfil pPerfil)
        {				
			var respuesta = new RespuestaPerfil();
            try
            {
				string CodigoAlerta = "PerfilDelete";
                List<string> mensajes = new List<string>();

				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pPerfil.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
					//VALIDACION: Entidad no puede venir vacio
					if (ValidacionesEliminar(pPerfil, ref mensajes))
					{
						//CONSULTA A ACCESO A DATOS
						respuesta = dal.EliminarPerfil(pPerfil);
						//LnBitacoraAuditoria.RegistrarBitacora(respuesta, pPerfil, ACCIONES.BORRAR);
						Notificacion(pPerfil, CodigoAlerta);
					}
					else
					{   
						//NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
						respuesta = new RespuestaPerfil { Respuesta = new Respuesta(Respuesta.CodNoValido), Perfil = respuesta.Perfil };
					}
					return respuesta;			
                }
                else
					return new RespuestaPerfil { Respuesta = respS.Respuesta, Perfil = new Perfil() };
					

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pPerfil);
                return new RespuestaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Obtiene una lista de perfiles por rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaListaPerfil ObtenerPerfilPorRol(Rol pRol)
        {

            var respuesta = new RespuestaListaPerfil();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pRol.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerPerfilPorRol(pRol);
                    return respuesta;
                }
                else
                    return new RespuestaListaPerfil { Respuesta = respS.Respuesta, ListaPerfil = new List<Perfil>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pRol);
                return new RespuestaListaPerfil { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(Perfil pPerfil, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pPerfil != null)
            {
                isValid = Utilidades.Util.EntidadValida(pPerfil, ref pMensajes);

                if (String.IsNullOrEmpty(pPerfil.UsrCreacion))
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
        /// <param name="pPerfil"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesModificacion(Perfil pPerfil, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pPerfil != null)
            {
                isValid = Utilidades.Util.EntidadValida(pPerfil, ref pMensajes);

                if (String.IsNullOrEmpty(pPerfil.UsrModificacion))
                {
                    pMensajes.Add(MensajesValidaciones.Req_UsuarioModificacion);
                    isValid = false;
                }

                if (pPerfil.IdPerfil == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Perfil"));
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
        /// <param name="pPerfil"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(Perfil pPerfil, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pPerfil != null)
            {
                if (pPerfil.IdPerfil == 0)
                {
                    pMensajes.Add(string.Format(Mensajes.vmRequeridoConsecutivoEntidad, "Perfil"));
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
        /// <param name="pPerfil"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Perfil pPerfil, string CodigoAlerta)
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