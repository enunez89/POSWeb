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
using POSWeb.ControlExcepciones;

namespace POSWeb.LogicaNegocios
{
    /// <summary>
    /// Requerimiento:       MobileToken
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Logica de Negocio clase   AccionLogicaNegocio
    /// Ultima modificacion: 16/11/2017
    /// </summary>
    public class AccionLogicaNegocio
    {

		AccionAccesoDatos dal = new AccionAccesoDatos();
		SesionLogicaNegocio sesion = new SesionLogicaNegocio();
        
        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Accion
        /// </summary>
        /// <param name="pAccion">Filtro de tipo Accion</param>
        /// <returns></returns>
		public  RespuestaListaAccion ObtenerListaAccion(Accion pAccion)
        {
			          
			var respuesta = new RespuestaListaAccion();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAccion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
					respuesta = dal.ObtenerAccion(pAccion);
					return respuesta;
                }
                else
					return new RespuestaListaAccion { Respuesta = respS.Respuesta, ListaAccion = new List<Accion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAccion);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaListaAccion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Obtiene la lista de acciones permitidas para el usuario
        /// </summary>
        /// <param name="pAccion"></param>
        /// <returns></returns>
        public RespuestaListaAccion ObtenerAccionesPerfil(Accion pAccion)
        {

            var respuesta = new RespuestaListaAccion();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAccion.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerAccionesPerfil(pAccion);
                    return respuesta;
                }
                else
                    return new RespuestaListaAccion { Respuesta = respS.Respuesta, ListaAccion = new List<Accion>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAccion);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaAccion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Notificacion Accion
        /// </summary>
        /// <param name="pAccion"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Accion pAccion, string CodigoAlerta)
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