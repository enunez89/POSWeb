using POSWeb.AccesoDatos.Resources;
using POSWeb.Entidades;
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
using POSWeb.Entidades.ResourceFiles;

namespace POSWeb.AccesoDatos
{
    /// <summary>
    /// Requerimiento:       POSWeb
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Acceso Datos clase   CuentaEmailAccesoDatos
    /// Ultima modificacion: 25/11/2017
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class CuentaEmailAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase CuentaEmail de la capa de Acceso a Datos
        /// </summary>
        public CuentaEmailAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaCuentaEmail InsertarCuentaEmail(CuentaEmail pCuentaEmail)
        {
            int filasAfectadas;

			RespuestaCuentaEmail respuesta = new RespuestaCuentaEmail();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarCuentaEmail);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pCuentaEmail.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.correoElectronicoProperty), DbType.AnsiString, pCuentaEmail.CorreoElectronico);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.aliasProperty), DbType.AnsiString, pCuentaEmail.Alias);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.servidorProperty), DbType.AnsiString, pCuentaEmail.Servidor);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.puertoProperty), DbType.Int32, pCuentaEmail.Puerto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.usuarioProperty), DbType.AnsiString, pCuentaEmail.Usuario);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.contrasenaProperty), DbType.AnsiString, pCuentaEmail.Contrasena);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.sslProperty), DbType.Boolean, pCuentaEmail.Ssl);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.credencialesDefectoProperty), DbType.Boolean, pCuentaEmail.CredencialesDefecto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.cuentaDefectoProperty), DbType.Boolean, pCuentaEmail.CuentaDefecto);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pCuentaEmail.UsrCreacion);

            //OUT PARAMETERS
            
            database1.AddOutParameter(dbCommand, parameterName( CuentaEmail.idProperty ), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR

            
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                pCuentaEmail.Id = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(CuentaEmail.idProperty)));
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateCuentaEmail, respuesta.Respuesta.CodMensaje);
                respuesta.CuentaEmail = pCuentaEmail;
            }

            //Registro de Error en bitacora
            /*if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
            {
				RegistrarError(respuesta, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }*/

            return respuesta;
        }

		/// <summary>
        /// Consulta en la base de datos  la tabla CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaListaCuentaEmail ObtenerCuentaEmail(CuentaEmail pCuentaEmail)
        {
			RespuestaListaCuentaEmail respuesta = new RespuestaListaCuentaEmail();
			respuesta.ListaCuentaEmail = new List<CuentaEmail>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerCuentaEmail);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.idProperty), DbType.Int32, pCuentaEmail.Id);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pCuentaEmail.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.correoElectronicoProperty), DbType.AnsiString, pCuentaEmail.CorreoElectronico);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.aliasProperty), DbType.AnsiString, pCuentaEmail.Alias);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.servidorProperty), DbType.AnsiString, pCuentaEmail.Servidor);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.puertoProperty), DbType.Int32, pCuentaEmail.Puerto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.usuarioProperty), DbType.AnsiString, pCuentaEmail.Usuario);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.contrasenaProperty), DbType.AnsiString, pCuentaEmail.Contrasena);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.sslProperty), DbType.Boolean, pCuentaEmail.Ssl);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.credencialesDefectoProperty), DbType.Boolean, pCuentaEmail.CredencialesDefecto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.cuentaDefectoProperty), DbType.Boolean, pCuentaEmail.CuentaDefecto);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaCuentaEmail.Add(new CuentaEmail(Reader));
                }
            }
			
            //ERROR CODE AND MESSAGE COLLECTOR
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            //Registro de Error en bitacora
            /*if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
            {
				RegistrarError(respuesta, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }*/
               
            return respuesta;
        }

		/// <summary>
        /// Metodo que se utiliza para modificar la informacion en la tabla CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaCuentaEmail ModificarCuentaEmail(CuentaEmail pCuentaEmail)
        {
            int filasAfectadas;
			RespuestaCuentaEmail respuesta = new RespuestaCuentaEmail();
			
            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarCuentaEmail);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.idProperty), DbType.Int32, pCuentaEmail.Id);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.correoElectronicoProperty), DbType.AnsiString, pCuentaEmail.CorreoElectronico);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.aliasProperty), DbType.AnsiString, pCuentaEmail.Alias);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.servidorProperty), DbType.AnsiString, pCuentaEmail.Servidor);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.puertoProperty), DbType.Int32, pCuentaEmail.Puerto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.usuarioProperty), DbType.AnsiString, pCuentaEmail.Usuario);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.contrasenaProperty), DbType.AnsiString, pCuentaEmail.Contrasena);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.sslProperty), DbType.Boolean, pCuentaEmail.Ssl);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.credencialesDefectoProperty), DbType.Boolean, pCuentaEmail.CredencialesDefecto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.cuentaDefectoProperty), DbType.Boolean, pCuentaEmail.CuentaDefecto);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pCuentaEmail.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditCuentaEmail, respuesta.Respuesta.CodMensaje);
            }
            //Registro de Error en bitacora
            /*if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
            {
				RegistrarError(respuesta, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }*/

            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaCuentaEmail EliminarCuentaEmail(CuentaEmail pCuentaEmail)
        {
            int filasAfectadas;
			RespuestaCuentaEmail respuesta = new RespuestaCuentaEmail();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarCuentaEmail);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.idProperty), DbType.Int32, pCuentaEmail.Id);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteCuentaEmail, respuesta.Respuesta.CodMensaje);
            }
            //Registro de Error en bitacora
            /*if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
            {
				RegistrarError(respuesta, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }*/

            return respuesta;
        }


		/// <summary>
        /// Consulta en la base de datos  la tabla CuentaEmail
        /// </summary>
        /// <param name="pCuentaEmail"></param>
        /// <returns></returns>
        public RespuestaListaCuentaEmail ObtenerCuentaEmailPaginado(CuentaEmail pCuentaEmail, Paginacion pPaginacion)
        {
            RespuestaListaCuentaEmail respuesta = new RespuestaListaCuentaEmail();
			respuesta.ListaCuentaEmail = new List<CuentaEmail>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerCuentaEmailPaginado);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.idProperty), DbType.Int32, pCuentaEmail.Id);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pCuentaEmail.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.correoElectronicoProperty), DbType.AnsiString, pCuentaEmail.CorreoElectronico);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.aliasProperty), DbType.AnsiString, pCuentaEmail.Alias);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.servidorProperty), DbType.AnsiString, pCuentaEmail.Servidor);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.puertoProperty), DbType.Int32, pCuentaEmail.Puerto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.usuarioProperty), DbType.AnsiString, pCuentaEmail.Usuario);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.contrasenaProperty), DbType.AnsiString, pCuentaEmail.Contrasena);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.sslProperty), DbType.Boolean, pCuentaEmail.Ssl);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.credencialesDefectoProperty), DbType.Boolean, pCuentaEmail.CredencialesDefecto);
            database1.AddInParameter(dbCommand, parameterName( CuentaEmail.cuentaDefectoProperty), DbType.Boolean, pCuentaEmail.CuentaDefecto);
			database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina );


            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);			

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaCuentaEmail.Add(new CuentaEmail(Reader));
                }
            }
			
            //ERROR CODE AND MESSAGE COLLECTOR
			respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
            respuesta.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));
            respuesta.TamanoPagina = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.tamanoPaginaProperty)));
            respuesta.NumPagina = pPaginacion.NumPagina;

            //Registro de Error en bitacora
            /*if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
            {
				RegistrarError(respuesta, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }*/

            return respuesta;
        }

    } //fin de clase
}