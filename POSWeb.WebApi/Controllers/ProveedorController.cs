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
    [RoutePrefix("Proveedor")]
    public class ProveedorController : ApiController
    {
        #region Variables y constantes globales

        ProveedorLogicaNegocio lnProveedor = new ProveedorLogicaNegocio();

        #endregion

        /// <summary>
        /// Método que inserta los datos de un proveedor en BD.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Insertar")]
        public RespuestaProveedor Insertar(Proveedor proveedor)
        {
            return lnProveedor.InsertarProveedor(proveedor);
        }

        /// <summary>
        /// Método que modifica los datos de un proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Modificar")]
        public RespuestaProveedor Modificar(Proveedor proveedor)
        {
            return lnProveedor.ModificarProveedor(proveedor);
        }

        /// <summary>
        /// Método que enlista los datos de los proveedores existentes en bd.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Listar")]
        public RespuestaListaProveedor Listar(Proveedor proveedor)
        {
            return lnProveedor.ObtenerListaProveedor(proveedor);
        }

        /// <summary>
        /// Obtiene los datos de un proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Obtener")]
        public RespuestaProveedor Obtener(Proveedor proveedor)
        {
            return lnProveedor.ObtenerProveedor(proveedor);
        }

        /// <summary>
        /// Elimina un proveedor por Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Eliminar")]
        public RespuestaProveedor Eliminar(int Id)
        {
            var proveedor = new Proveedor
            {
                Id = Id
            };

            return lnProveedor.EliminarProveedor(proveedor);
        }
    }
}
