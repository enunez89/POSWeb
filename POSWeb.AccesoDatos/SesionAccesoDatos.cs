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
    /// Propósito:           Acceso Datos clase   SesionAccesoDatos
    /// Ultima modificacion: 04/11/2017
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class SesionAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Sesion de la capa de Acceso a Datos
        /// </summary>
        public SesionAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaSesion InsertarSesion(Sesion pSesion)
        {
            int filasAfectadas;

			RespuestaSesion respuesta = new RespuestaSesion();
            respuesta.Sesion = new Sesion();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarSesion);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Sesion.idEntidadProperty), DbType.Int64, pSesion.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( Sesion.codigoUsuarioProperty), DbType.String, pSesion.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName( Sesion.tokenProperty), DbType.AnsiString, pSesion.Token);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ipProperty), DbType.AnsiString, pSesion.Ip);
            database1.AddInParameter(dbCommand, parameterName( Sesion.numCelularProperty), DbType.AnsiString, pSesion.NumCelular);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ubicacionProperty), DbType.AnsiString, pSesion.Ubicacion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ambienteProperty), DbType.AnsiString, pSesion.Ambiente);

            //OUT PARAMETERS
            
            database1.AddOutParameter(dbCommand, parameterName( Sesion.idSesionProperty ), DbType.Int64, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
            
            pSesion.IdSesion = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Sesion.idSesionProperty )));
            respuesta.Sesion = pSesion;
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateSesion, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

		/// <summary>
        /// Consulta en la base de datos  la tabla Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaListaSesion ObtenerSesion(Sesion pSesion)
        {
			RespuestaListaSesion respuesta = new RespuestaListaSesion();
			respuesta.ListaSesion = new List<Sesion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerSesion);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Sesion.idSesionProperty), DbType.Int64, pSesion.IdSesion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.idEntidadProperty), DbType.Int64, pSesion.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( Sesion.codigoUsuarioProperty), DbType.String, pSesion.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName( Sesion.tokenProperty), DbType.AnsiString, pSesion.Token);
            database1.AddInParameter(dbCommand, parameterName( Sesion.fechaConexionProperty), DbType.DateTime, pSesion.FechaConexion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ipProperty), DbType.AnsiString, pSesion.Ip);
            database1.AddInParameter(dbCommand, parameterName( Sesion.numCelularProperty), DbType.AnsiString, pSesion.NumCelular);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ubicacionProperty), DbType.AnsiString, pSesion.Ubicacion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ambienteProperty), DbType.AnsiString, pSesion.Ambiente);
            database1.AddInParameter(dbCommand, parameterName( Sesion.fechaDesconexionProperty), DbType.DateTime, pSesion.FechaDesconexion);

            //OUT PARAMETERS
			database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaSesion.Add(new Sesion(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaSesion ModificarSesion(Sesion pSesion)
        {
            int filasAfectadas;
			RespuestaSesion respuesta = new RespuestaSesion();
			
            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarSesion);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Sesion.idSesionProperty), DbType.Int64, pSesion.IdSesion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.idEntidadProperty), DbType.Int64, pSesion.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( Sesion.codigoUsuarioProperty), DbType.String, pSesion.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName( Sesion.tokenProperty), DbType.AnsiString, pSesion.Token);
            database1.AddInParameter(dbCommand, parameterName( Sesion.fechaConexionProperty), DbType.DateTime, pSesion.FechaConexion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ipProperty), DbType.AnsiString, pSesion.Ip);
            database1.AddInParameter(dbCommand, parameterName( Sesion.numCelularProperty), DbType.AnsiString, pSesion.NumCelular);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ubicacionProperty), DbType.AnsiString, pSesion.Ubicacion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ambienteProperty), DbType.AnsiString, pSesion.Ambiente);
            database1.AddInParameter(dbCommand, parameterName( Sesion.fechaDesconexionProperty), DbType.DateTime, pSesion.FechaDesconexion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pSesion.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditSesion, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaSesion EliminarSesion(Sesion pSesion)
        {
            int filasAfectadas;
			RespuestaSesion respuesta = new RespuestaSesion();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarSesion);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Sesion.tokenProperty), DbType.String, pSesion.Token);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteSesion, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }


		/// <summary>
        /// Consulta en la base de datos  la tabla Sesion
        /// </summary>
        /// <param name="pSesion"></param>
        /// <returns></returns>
        public RespuestaListaSesion ObtenerSesionPaginado(Sesion pSesion, ref Paginacion pPaginacion)
        {
            RespuestaListaSesion respuesta = new RespuestaListaSesion();
			respuesta.ListaSesion = new List<Sesion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerSesionPaginado);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Sesion.idSesionProperty), DbType.Int64, pSesion.IdSesion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.idEntidadProperty), DbType.Int64, pSesion.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( Sesion.codigoUsuarioProperty), DbType.String, pSesion.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName( Sesion.tokenProperty), DbType.AnsiString, pSesion.Token);
            database1.AddInParameter(dbCommand, parameterName( Sesion.fechaConexionProperty), DbType.DateTime, pSesion.FechaConexion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ipProperty), DbType.AnsiString, pSesion.Ip);
            database1.AddInParameter(dbCommand, parameterName( Sesion.numCelularProperty), DbType.AnsiString, pSesion.NumCelular);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ubicacionProperty), DbType.AnsiString, pSesion.Ubicacion);
            database1.AddInParameter(dbCommand, parameterName( Sesion.ambienteProperty), DbType.AnsiString, pSesion.Ambiente);
            database1.AddInParameter(dbCommand, parameterName( Sesion.fechaDesconexionProperty), DbType.DateTime, pSesion.FechaDesconexion);
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
                    respuesta.ListaSesion.Add(new Sesion(Reader));
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