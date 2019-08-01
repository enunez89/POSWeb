using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSWeb.Entidades;
using POSWeb.LogicaNegocios;

namespace POSWeb.WebApi.Controllers
{
    [RoutePrefix("Productos")]
    public class ProductosController : ApiController
    {
        #region Variables y constantes globales

        ProductoLogicaNegocio lnProducto = new ProductoLogicaNegocio();

        #endregion

        /// <summary>
        /// Método web api que inserta en bd un nuevo producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Guardar")]
        public RespuestaProducto Guardar(Producto producto)
        {
            return lnProducto.InsertarProducto(producto);
        }

        /// <summary>
        /// Obtiene la información de un producto por codigo de barra
        /// </summary>
        /// <param name="codigoBarra"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerPorCodigo")]
        public RespuestaProducto ObtenerPorCodigo(string codigoBarra)
        {
            var producto = new Producto
            {
                CodigoBarra = codigoBarra
            };
            return lnProducto.ObtenerProducto(producto);
        }


    }
}