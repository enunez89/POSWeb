using POSWeb.AccesoDatos.Resources;
using POSWeb.Entidades;
using POSWeb.Entidades.ResourceFiles;
using POSWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace POSWeb.AccesoDatos
{
    /// <summary>
    /// Requerimiento:       POSWeb
    /// Empresa:             Sistemas Eddy & Gianca
    /// Autor:               Eddy
    /// Propósito:           Acceso Datos clase   ProductoAccesoDatos
    /// Ultima modificacion: 11-07-2019
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class ProductoAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Producto de la capa de Acceso a Datos
        /// </summary>
        public ProductoAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaProducto InsertarProducto(Producto pProducto)
        {
            int filasAfectadas;

            RespuestaProducto respuesta = new RespuestaProducto();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarProducto);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Producto.idEntidadProperty), DbType.Int64, pProducto.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Producto.idProveedorProperty), DbType.Int32, pProducto.IdProveedor);
            database1.AddInParameter(dbCommand, parameterName(Producto.idCategoriaProperty), DbType.Int32, pProducto.IdCategoria);
            database1.AddInParameter(dbCommand, parameterName(Producto.nombreProperty), DbType.String, pProducto.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Producto.precioCostoProperty), DbType.Decimal, pProducto.PrecioCosto);
            database1.AddInParameter(dbCommand, parameterName(Producto.precioVentaProperty), DbType.Decimal, pProducto.PrecioVenta);
            database1.AddInParameter(dbCommand, parameterName(Producto.utilidadProperty), DbType.Decimal, pProducto.Utilidad);
            database1.AddInParameter(dbCommand, parameterName(Producto.impuestoProperty), DbType.Decimal, pProducto.Impuesto);
            database1.AddInParameter(dbCommand, parameterName(Producto.stockProperty), DbType.Int32, pProducto.Stock);
            database1.AddInParameter(dbCommand, parameterName(Producto.existenciaProperty), DbType.Int32, pProducto.Existencia);
            database1.AddInParameter(dbCommand, parameterName(Producto.descuentoProperty), DbType.Decimal, pProducto.Descuento);
            database1.AddInParameter(dbCommand, parameterName(Producto.codigoBarraProperty), DbType.String, pProducto.CodigoBarra);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pProducto.UsrCreacion);

            //OUT PARAMETERS

            database1.AddOutParameter(dbCommand, parameterName(Producto.idProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR

            pProducto.Id = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Producto.idProperty)));
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            respuesta.Producto = pProducto;

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateProducto, respuesta.Respuesta.CodMensaje);
            }
            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaListaProducto ObtenerProducto(Producto pProducto)
        {
            RespuestaListaProducto respuesta = new RespuestaListaProducto();
            respuesta.ListaProducto = new List<Producto>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerProducto);

            //IN PARAMETERS 
            if (pProducto.Id != 0)
                database1.AddInParameter(dbCommand, parameterName(Producto.idProperty), DbType.Int32, pProducto.Id);
            if (pProducto.IdEntidad != 0)
                database1.AddInParameter(dbCommand, parameterName(Producto.idEntidadProperty), DbType.Int64, pProducto.IdEntidad);
            if (pProducto.IdProveedor != 0)
                database1.AddInParameter(dbCommand, parameterName(Producto.idProveedorProperty), DbType.Int32, pProducto.IdProveedor);
            if (pProducto.IdCategoria != 0)
                database1.AddInParameter(dbCommand, parameterName(Producto.idCategoriaProperty), DbType.Int32, pProducto.IdCategoria);
            database1.AddInParameter(dbCommand, parameterName(Producto.nombreProperty), DbType.String, pProducto.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Producto.codigoBarraProperty), DbType.String, pProducto.CodigoBarra);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaProducto.Add(new Producto(Reader));
                }
            }

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo que se utiliza para modificar la informacion en la tabla Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaProducto ModificarProducto(Producto pProducto)
        {
            int filasAfectadas;
            RespuestaProducto respuesta = new RespuestaProducto();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarProducto);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Producto.idProperty), DbType.Int32, pProducto.Id);
            database1.AddInParameter(dbCommand, parameterName(Producto.idProveedorProperty), DbType.Int32, pProducto.IdProveedor);
            database1.AddInParameter(dbCommand, parameterName(Producto.idCategoriaProperty), DbType.Int32, pProducto.IdCategoria);
            database1.AddInParameter(dbCommand, parameterName(Producto.nombreProperty), DbType.String, pProducto.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Producto.precioCostoProperty), DbType.Decimal, pProducto.PrecioCosto);
            database1.AddInParameter(dbCommand, parameterName(Producto.precioVentaProperty), DbType.Decimal, pProducto.PrecioVenta);
            database1.AddInParameter(dbCommand, parameterName(Producto.utilidadProperty), DbType.Decimal, pProducto.Utilidad);
            database1.AddInParameter(dbCommand, parameterName(Producto.impuestoProperty), DbType.Decimal, pProducto.Impuesto);
            database1.AddInParameter(dbCommand, parameterName(Producto.stockProperty), DbType.Int32, pProducto.Stock);
            database1.AddInParameter(dbCommand, parameterName(Producto.existenciaProperty), DbType.Int32, pProducto.Existencia);
            database1.AddInParameter(dbCommand, parameterName(Producto.descuentoProperty), DbType.Decimal, pProducto.Descuento);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pProducto.UsrModificacion);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditProducto, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaProducto EliminarProducto(Producto pProducto)
        {
            int filasAfectadas;
            RespuestaProducto respuesta = new RespuestaProducto();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarProducto);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Producto.idProperty), DbType.Int32, pProducto.Id);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteProducto, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }


        /// <summary>
        /// Consulta en la base de datos  la tabla Producto
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public RespuestaListaProducto ObtenerProductoPaginado(Producto pProducto, ref Paginacion pPaginacion)
        {
            RespuestaListaProducto respuesta = new RespuestaListaProducto();
            respuesta.ListaProducto = new List<Producto>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerProductoPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Producto.idProperty), DbType.Int32, pProducto.Id);
            database1.AddInParameter(dbCommand, parameterName(Producto.idEntidadProperty), DbType.Int64, pProducto.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Producto.idProveedorProperty), DbType.Int32, pProducto.IdProveedor);
            database1.AddInParameter(dbCommand, parameterName(Producto.idCategoriaProperty), DbType.Int32, pProducto.IdCategoria);
            database1.AddInParameter(dbCommand, parameterName(Producto.nombreProperty), DbType.String, pProducto.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Producto.precioCostoProperty), DbType.Decimal, pProducto.PrecioCosto);
            database1.AddInParameter(dbCommand, parameterName(Producto.precioVentaProperty), DbType.Decimal, pProducto.PrecioVenta);
            database1.AddInParameter(dbCommand, parameterName(Producto.utilidadProperty), DbType.Decimal, pProducto.Utilidad);
            database1.AddInParameter(dbCommand, parameterName(Producto.impuestoProperty), DbType.Decimal, pProducto.Impuesto);
            database1.AddInParameter(dbCommand, parameterName(Producto.stockProperty), DbType.Int32, pProducto.Stock);
            database1.AddInParameter(dbCommand, parameterName(Producto.existenciaProperty), DbType.Int32, pProducto.Existencia);
            database1.AddInParameter(dbCommand, parameterName(Producto.descuentoProperty), DbType.Decimal, pProducto.Descuento);
            database1.AddInParameter(dbCommand, parameterName(Producto.codigoBarraProperty), DbType.String, pProducto.CodigoBarra);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina);



            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaProducto.Add(new Producto(Reader));
                }
            }

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
            pPaginacion.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));
            pPaginacion.TamanoPagina = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.tamanoPaginaProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

    } //fin de clase
}