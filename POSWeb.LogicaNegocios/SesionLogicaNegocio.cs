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
    /// Propósito:           Logica de Negocio clase   SesionLogicaNegocio
    /// Ultima modificacion: 04/11/2017
    /// </summary>
    public class SesionLogicaNegocio
    {

        SesionAccesoDatos dal = new SesionAccesoDatos();
        HexadecimalNotation hexaNotation = new HexadecimalNotation();

        /// <summary>
        /// Metodo para insertar un valor de tipo Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaSesion InsertarSesion(Sesion pSesion)
        {
            var respuesta = new RespuestaSesion();
            try
            {
                string CodigoAlerta = "SesionCreate";
                List<string> mensajes = new List<string>();

                //creamos el token de la sesion
                string input = pSesion.IdEntidad + "-" + pSesion.CodigoUsuario + DateTime.Now.ToString();
                pSesion.Token = hexaNotation.StringAHexa(input);

                //EJECUTAR: se guarda la entidad
                if (ValidacionesCreacion(pSesion, ref mensajes))
                {
                    respuesta = dal.InsertarSesion(pSesion);
                    //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pSesion, ACCIONES.AGREGAR);
                    Notificacion(pSesion, CodigoAlerta);
                }
                else
                {
                    //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                    respuesta = new RespuestaSesion { Respuesta = new Respuesta(Respuesta.CodNoValido), Sesion = respuesta.Sesion };
                }
                return respuesta;
                //          
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pSesion);
                return new RespuestaSesion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
              

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Sesion
        /// </summary>
        /// <param name="pSesion">Filtro de tipo Sesion</param>
        /// <returns></returns>
        public RespuestaSesion ObtenerSesion(Sesion pSesion)
        {
            var respuesta = new RespuestaSesion();
            try
            {

                respuesta.Sesion = dal.ObtenerSesion(pSesion).ListaSesion?[0];
                return respuesta.Sesion != null ?
                  new RespuestaSesion { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Sesion = respuesta.Sesion } :
                  new RespuestaSesion { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Sesion = new Sesion() };
                         
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pSesion);
                return new RespuestaSesion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
        

        /// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaSesion EliminarSesion(Sesion pSesion)
        {
            var respuesta = new RespuestaSesion();
            try
            {
                string CodigoAlerta = "SesionDelete";
                List<string> mensajes = new List<string>();


                //VALIDACION: Entidad no puede venir vacio
                if (ValidacionesEliminar(pSesion, ref mensajes))
                {
                    //CONSULTA A ACCESO A DATOS
                    respuesta = dal.EliminarSesion(pSesion);
                    //LnBitacoraAuditoria.RegistrarBitacora(respuesta, pSesion, ACCIONES.BORRAR);
                    Notificacion(pSesion, CodigoAlerta);
                }
                else
                {
                    //NO COMPLETA LA VALIDACION, SE ENVIAN DE REGRESO LOS MENSAJES
                    new RespuestaSesion { Respuesta = new Respuesta(Respuesta.CodNoValido), Sesion = respuesta.Sesion };
                }
                return respuesta;



            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pSesion);
                return new RespuestaSesion { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Validacion para Insertar Entidad
        /// </summary>
        /// <param name="pSesion"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesCreacion(Sesion pSesion, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pSesion != null)
            {
                isValid = Utilidades.Util.EntidadValida(pSesion, ref pMensajes);                
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
        /// <param name="pSesion"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private Boolean ValidacionesEliminar(Sesion pSesion, ref List<String> pMensajes)
        {
            Boolean isValid = true;

            if (pSesion != null)
            {
                
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
        /// <param name="pSesion"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(Sesion pSesion, string CodigoAlerta)
        {
            //AlertaLogicaNegocio al = new AlertaLogicaNegocio();
            //AlertaRespuesta alerta = al.ObtenerAlerta( new Alerta() { CodIgoalerta = CodigoAlerta, Activo = "S" });
            //         if (alerta != null && alerta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            //         {
            //	//al.EnviarAlerta(alerta.Alerta);
            //         }
        }

    }//fin de clase
}