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
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Acceso Datos clase   AlertaEntidadAccesoDatos
    /// Ultima modificacion: 01/12/2017
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class AlertaEntidadAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase AlertaEntidad de la capa de Acceso a Datos
        /// </summary>
        public AlertaEntidadAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public RespuestaAlertaEntidad InsertarAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
            int filasAfectadas;

			RespuestaAlertaEntidad respuesta = new RespuestaAlertaEntidad();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarAlertaEntidad);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pAlertaEntidad.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idAlertaProperty), DbType.Int64, pAlertaEntidad.IdAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.tituloProperty), DbType.AnsiString, pAlertaEntidad.Titulo);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.bodyProperty), DbType.AnsiString, pAlertaEntidad.HtmlContent);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idCuentaProperty), DbType.Int64, pAlertaEntidad.IdCuenta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.activaProperty), DbType.Boolean, pAlertaEntidad.Activa);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pAlertaEntidad.UsrCreacion);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(AlertaEntidad.idProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
            pAlertaEntidad.Id = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(AlertaEntidad.idProperty)));
            respuesta.Respuesta = new Respuesta();
            respuesta.AlertaEntidad = pAlertaEntidad;
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditAlertaEntidad, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

		/// <summary>
        /// Consulta en la base de datos  la tabla AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public RespuestaListaAlertaEntidad ObtenerAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
			RespuestaListaAlertaEntidad respuesta = new RespuestaListaAlertaEntidad();
			respuesta.ListaAlertaEntidad = new List<AlertaEntidad>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAlertaEntidad);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pAlertaEntidad.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idProperty), DbType.Int64, pAlertaEntidad.Id);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idAlertaProperty), DbType.Int64, pAlertaEntidad.IdAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.tituloProperty), DbType.AnsiString, pAlertaEntidad.Titulo);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.bodyProperty), DbType.AnsiString, pAlertaEntidad.HtmlContent);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idCuentaProperty), DbType.Int64, pAlertaEntidad.IdCuenta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.activaProperty), DbType.Boolean, pAlertaEntidad.Activa);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.codigoAlertaProperty), DbType.AnsiString, pAlertaEntidad.CodigoAlerta);
            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAlertaEntidad.Add(new AlertaEntidad(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public RespuestaAlertaEntidad ModificarAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
            int filasAfectadas;
			RespuestaAlertaEntidad respuesta = new RespuestaAlertaEntidad();
			
            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarAlertaEntidad);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idProperty), DbType.Int64, pAlertaEntidad.Id);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idAlertaProperty), DbType.Int64, pAlertaEntidad.IdAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.tituloProperty), DbType.AnsiString, pAlertaEntidad.Titulo);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.bodyProperty), DbType.AnsiString, pAlertaEntidad.HtmlContent);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idCuentaProperty), DbType.Int64, pAlertaEntidad.IdCuenta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.activaProperty), DbType.Boolean, pAlertaEntidad.Activa);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pAlertaEntidad.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditAlertaEntidad, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public RespuestaAlertaEntidad EliminarAlertaEntidad(AlertaEntidad pAlertaEntidad)
        {
            int filasAfectadas;
			RespuestaAlertaEntidad respuesta = new RespuestaAlertaEntidad();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarAlertaEntidad);

            //IN PARAMETERS 
            

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteAlertaEntidad, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }


		/// <summary>
        /// Consulta en la base de datos  la tabla AlertaEntidad
        /// </summary>
        /// <param name="pAlertaEntidad"></param>
        /// <returns></returns>
        public RespuestaListaAlertaEntidad ObtenerAlertaEntidadPaginado(AlertaEntidad pAlertaEntidad, Paginacion pPaginacion)
        {
            RespuestaListaAlertaEntidad respuesta = new RespuestaListaAlertaEntidad();
			respuesta.ListaAlertaEntidad = new List<AlertaEntidad>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAlertaEntidadPaginado);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pAlertaEntidad.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idProperty), DbType.Int64, pAlertaEntidad.Id);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idAlertaProperty), DbType.Int64, pAlertaEntidad.IdAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.tituloProperty), DbType.AnsiString, pAlertaEntidad.Titulo);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.bodyProperty), DbType.AnsiString, pAlertaEntidad.HtmlContent);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.idCuentaProperty), DbType.Int64, pAlertaEntidad.IdCuenta);
            database1.AddInParameter(dbCommand, parameterName( AlertaEntidad.activaProperty), DbType.Boolean, pAlertaEntidad.Activa);
			database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina );



            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);			
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);			

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAlertaEntidad.Add(new AlertaEntidad(Reader));
                }
            }
			
            //ERROR CODE AND MESSAGE COLLECTOR
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
            respuesta.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));
            respuesta.TamanoPagina = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.tamanoPaginaProperty)));
            respuesta.NumPagina = pPaginacion.NumPagina;

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }

    } //fin de clase
}