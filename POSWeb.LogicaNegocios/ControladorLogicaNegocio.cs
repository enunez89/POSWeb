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
    /// Propósito:           Logica de Negocio clase   ControladorLogicaNegocio
    /// Ultima modificacion: 29/11/2017
    /// </summary>
    public class ControladorLogicaNegocio
    {

        ControladorAccesoDatos dal = new ControladorAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

       
        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Controlador
        /// </summary>
        /// <param name="pControlador">Filtro de tipo Controlador</param>
        /// <returns></returns>
        public RespuestaListaControlador ObtenerListaControlador(Controlador pControlador)
        {

            var respuesta = new RespuestaListaControlador();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pControlador.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta = dal.ObtenerControlador(pControlador);
                    return respuesta;
                }
                else
                    return new RespuestaListaControlador { Respuesta = respS.Respuesta, ListaControlador = new List<Controlador>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pControlador);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaControlador { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Controlador
        /// </summary>
        /// <param name="pControlador">Filtro de tipo Controlador</param>
        /// <returns></returns>
        public RespuestaControlador ObtenerControlador(Controlador pControlador)
        {
            var respuesta = new RespuestaControlador();
            try
            {
                RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pControlador.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    respuesta.Controlador = dal.ObtenerControlador(pControlador).ListaControlador?[0];
                    return respuesta.Controlador != null ?
                      new RespuestaControlador { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Controlador = respuesta.Controlador } :
                      new RespuestaControlador { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Controlador = new Controlador() };
                }
                else
                    return new RespuestaControlador { Respuesta = respS.Respuesta, Controlador = new Controlador() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pControlador);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaControlador { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
        
        /// <summary>
        /// Notificacion Accion
        /// </summary>
        /// <param name="pControlador"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Controlador pControlador, string CodigoAlerta)
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