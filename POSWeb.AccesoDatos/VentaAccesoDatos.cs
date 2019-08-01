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
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Eddy
    /// Propósito:           Acceso Datos clase   VentaAccesoDatos
    /// Ultima modificacion: 31/07/2019
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class VentaAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Venta de la capa de Acceso a Datos
        /// </summary>
        public VentaAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Venta
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public RespuestaVenta InsertarVenta(Venta pVenta)
        {
            int filasAfectadas;

			RespuestaVenta respuesta = new RespuestaVenta();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarVenta);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Venta.idClienteProperty), DbType.Int32, pVenta.IdCliente);
            database1.AddInParameter(dbCommand, parameterName( Venta.tipoProperty), DbType.AnsiString, pVenta.Tipo);
            database1.AddInParameter(dbCommand, parameterName( Venta.totalVentaProperty), DbType.Decimal, pVenta.TotalVenta);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pVenta.UsrCreacion);

            //OUT PARAMETERS
            
            database1.AddOutParameter(dbCommand, parameterName( Venta.idProperty ), DbType.Int64, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
            
            pVenta.Id = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Venta.idProperty )));
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateVenta, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

		/// <summary>
        /// Consulta en la base de datos  la tabla Venta
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public RespuestaListaVenta ObtenerVenta(Venta pVenta)
        {
			RespuestaListaVenta respuesta = new RespuestaListaVenta();
			respuesta.ListaVenta = new List<Venta>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerVenta);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Venta.idProperty), DbType.Int64, pVenta.Id);
            database1.AddInParameter(dbCommand, parameterName( Venta.idClienteProperty), DbType.Int32, pVenta.IdCliente);
            database1.AddInParameter(dbCommand, parameterName( Venta.tipoProperty), DbType.AnsiString, pVenta.Tipo);
            database1.AddInParameter(dbCommand, parameterName( Venta.totalVentaProperty), DbType.Decimal, pVenta.TotalVenta);

            //OUT PARAMETERS
			database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaVenta.Add(new Venta(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Venta
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public RespuestaVenta ModificarVenta(Venta pVenta)
        {
            int filasAfectadas;
			RespuestaVenta respuesta = new RespuestaVenta();
			
            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarVenta);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Venta.idProperty), DbType.Int64, pVenta.Id);
            database1.AddInParameter(dbCommand, parameterName( Venta.idClienteProperty), DbType.Int32, pVenta.IdCliente);
            database1.AddInParameter(dbCommand, parameterName( Venta.tipoProperty), DbType.AnsiString, pVenta.Tipo);
            database1.AddInParameter(dbCommand, parameterName( Venta.totalVentaProperty), DbType.Decimal, pVenta.TotalVenta);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pVenta.UsrModificacion);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditVenta, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Venta
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public RespuestaVenta EliminarVenta(Venta pVenta)
        {
            int filasAfectadas;
			RespuestaVenta respuesta = new RespuestaVenta();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarVenta);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Venta.idProperty), DbType.Int64, pVenta.Id);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteVenta, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }


		/// <summary>
        /// Consulta en la base de datos  la tabla Venta
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public RespuestaListaVenta ObtenerVentaPaginado(Venta pVenta, ref Paginacion pPaginacion)
        {
            RespuestaListaVenta respuesta = new RespuestaListaVenta();
			respuesta.ListaVenta = new List<Venta>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerVentaPaginado);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Venta.idProperty), DbType.Int64, pVenta.Id);
            database1.AddInParameter(dbCommand, parameterName( Venta.idClienteProperty), DbType.Int32, pVenta.IdCliente);
            database1.AddInParameter(dbCommand, parameterName( Venta.tipoProperty), DbType.AnsiString, pVenta.Tipo);
            database1.AddInParameter(dbCommand, parameterName( Venta.totalVentaProperty), DbType.Decimal, pVenta.TotalVenta);
			database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina );
            database1.AddInParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, pPaginacion.TamanoPagina );


            //OUT PARAMETERS
			database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);			

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaVenta.Add(new Venta(Reader));
                }
            }
			
            //ERROR CODE AND MESSAGE COLLECTOR
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
			pPaginacion.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));

			if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }

    } //fin de clase
}