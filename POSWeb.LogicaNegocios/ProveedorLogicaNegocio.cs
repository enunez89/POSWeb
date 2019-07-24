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
    /// Requerimiento:       POSWeb
    /// Empresa:             Salazar & Asociados S.A.
    /// Autor:               Eddy 
    /// Propósito:           Logica de Negocio clase   ProveedorLogicaNegocio
    /// Ultima modificacion: 23-07-2019
    /// </summary>
    public class ProveedorLogicaNegocio
    {

        ProveedorAccesoDatos dal = new ProveedorAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaProveedor InsertarProveedor(Proveedor pProveedor)
        {
            var respuesta = new RespuestaProveedor();
            try
            {
                respuesta = dal.InsertarProveedor(pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                    TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                return respuesta;

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProveedor { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Proveedor
        /// </summary>
        /// <param name="pProveedor">Filtro de tipo Proveedor</param>
        /// <returns></returns>
        public RespuestaListaProveedor ObtenerListaProveedor(Proveedor pProveedor)
        {

            var respuesta = new RespuestaListaProveedor();
            try
            {
                respuesta = dal.ObtenerProveedor(pProveedor);
                return respuesta;

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaProveedor { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Proveedor
        /// </summary>
        /// <param name="pProveedor">Filtro de tipo Proveedor</param>
        /// <returns></returns>
        public RespuestaProveedor ObtenerProveedor(Proveedor pProveedor)
        {
            var respuesta = new RespuestaProveedor();
            try
            {
                var respProveedor = dal.ObtenerProveedor(pProveedor);
                respuesta.Proveedor = respProveedor.ListaProveedor != null || respProveedor.ListaProveedor.Count > 0 ?
                    respProveedor.ListaProveedor.FirstOrDefault() : null;
                return respuesta.Proveedor != null ?
                  new RespuestaProveedor { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Proveedor = respuesta.Proveedor } :
                  new RespuestaProveedor { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Proveedor = new Proveedor() };

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProveedor { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Proveedor con paginacion
        /// </summary>
        /// <param name="pProveedor">Filtro de tipo Proveedor</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaProveedor ObtenerProveedorPaginado(Proveedor pProveedor, ref Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaProveedor();
            try
            {

                respuesta = dal.ObtenerProveedorPaginado(pProveedor, ref pPaginacion);
                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaProveedor { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaProveedor ModificarProveedor(Proveedor pProveedor)
        {
            var respuesta = new RespuestaProveedor();
            try
            {
                respuesta = dal.ModificarProveedor(pProveedor);
                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaModificar,
                    TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProveedor { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaProveedor EliminarProveedor(Proveedor pProveedor)
        {
            var respuesta = new RespuestaProveedor();
            try
            {
                //CONSULTA A ACCESO A DATOS
                respuesta = dal.EliminarProveedor(pProveedor);
                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaEliminar,
                    TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProveedor);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProveedor { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

    }//fin de clase
}