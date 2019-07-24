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
    /// Propósito:           Logica de Negocio clase   ProductoLogicaNegocio
    /// Ultima modificacion: 11-07-2019
    /// </summary>
    public class ProductoLogicaNegocio
    {

        ProductoAccesoDatos dal = new ProductoAccesoDatos();
        SesionLogicaNegocio sesion = new SesionLogicaNegocio();

        /// <summary>
        /// Metodo para insertar un valor de tipo Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaProducto InsertarProducto(Producto pProducto)
        {
            var respuesta = new RespuestaProducto();
            try
            {
                //Registra en bd
                respuesta = dal.InsertarProducto(pProducto);

                //Registra bitacora
                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaInsertar,
                    TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);
                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProducto);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProducto { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Producto
        /// </summary>
        /// <param name="pProducto">Filtro de tipo Producto</param>
        /// <returns></returns>
        public RespuestaListaProducto ObtenerListaProducto(Producto pProducto)
        {
            var respuesta = new RespuestaListaProducto();
            try
            {
                respuesta = dal.ObtenerProducto(pProducto);
                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProducto);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaProducto { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Producto
        /// </summary>
        /// <param name="pProducto">Filtro de tipo Producto</param>
        /// <returns></returns>
        public RespuestaProducto ObtenerProducto(Producto pProducto)
        {
            var respuesta = new RespuestaProducto();
            try
            {

                var respObtenerProducto = dal.ObtenerProducto(pProducto);
                respuesta.Producto = respObtenerProducto.ListaProducto != null || respObtenerProducto.ListaProducto.Count > 0 ?
                    respObtenerProducto.ListaProducto.FirstOrDefault() : null;
                return respuesta.Producto != null ?
                  new RespuestaProducto { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Producto = respuesta.Producto } :
                  new RespuestaProducto { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Producto = new Producto() };

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProducto);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProducto { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Producto con paginacion
        /// </summary>
        /// <param name="pProducto">Filtro de tipo Producto</param>
        /// <param name="pPaginacion">Uso de Paginacion</param>
        /// <returns></returns>
        public RespuestaListaProducto ObtenerProductoPaginado(Producto pProducto, ref Paginacion pPaginacion)
        {
            var respuesta = new RespuestaListaProducto();
            try
            {
                respuesta = dal.ObtenerProductoPaginado(pProducto, ref pPaginacion);
                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProducto);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaListaProducto { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Modificar un objeto de tipo Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaProducto ModificarProducto(Producto pProducto)
        {
            var respuesta = new RespuestaProducto();
            try
            {
                respuesta = dal.ModificarProducto(pProducto);

                //Registra bitacora
                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaModificar,
                    TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProducto);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProducto { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }


        /// <summary>
        /// Metodo que sirve para Eliminar o Desactivar un objeto de tipo Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaProducto EliminarProducto(Producto pProducto)
        {
            var respuesta = new RespuestaProducto();
            try
            {

                respuesta = dal.EliminarProducto(pProducto);

                //Registra bitacora
                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.RespuestaEliminar,
                    TraceData.Utilities.Enums.TypeTrace.Info, respuesta, MethodBase.GetCurrentMethod().Name);

                return respuesta;

            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pProducto);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                           TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaProducto { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

    }//fin de clase
}