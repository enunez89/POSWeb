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
    /// Propósito:           Acceso Datos clase   ModuloAccesoDatos
    /// Ultima modificacion: 03/11/2017
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class ModuloAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Modulo de la capa de Acceso a Datos
        /// </summary>
        public ModuloAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Modulo
        /// </summary>
        /// <param name="pModulo"></param>
        /// <returns></returns>
        public RespuestaModulo InsertarModulo(Modulo pModulo)
        {
            int filasAfectadas;

			RespuestaModulo respuesta = new RespuestaModulo();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarModulo);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Modulo.idModuloProperty), DbType.Int64, pModulo.IdModulo);
            database1.AddInParameter(dbCommand, parameterName( Modulo.nombreProperty), DbType.AnsiString, pModulo.Nombre);
            database1.AddInParameter(dbCommand, parameterName( Modulo.descripcionProperty), DbType.AnsiString, pModulo.Descripcion);
            database1.AddInParameter(dbCommand, parameterName( Modulo.cssClassProperty), DbType.String, pModulo.CssClass);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pModulo.UsrCreacion);

            //OUT PARAMETERS
            
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
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateModulo, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

		/// <summary>
        /// Consulta en la base de datos  la tabla Modulo
        /// </summary>
        /// <param name="pModulo"></param>
        /// <returns></returns>
        public RespuestaListaModulo ObtenerModulo(Modulo pModulo)
        {
			RespuestaListaModulo respuesta = new RespuestaListaModulo();
			respuesta.ListaModulo = new List<Modulo>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerModulo);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Modulo.idModuloProperty), DbType.Int64, pModulo.IdModulo);
            database1.AddInParameter(dbCommand, parameterName( Modulo.nombreProperty), DbType.AnsiString, pModulo.Nombre);
            database1.AddInParameter(dbCommand, parameterName( Modulo.descripcionProperty), DbType.AnsiString, pModulo.Descripcion);
            database1.AddInParameter(dbCommand, parameterName( Modulo.cssClassProperty), DbType.String, pModulo.CssClass);

            //OUT PARAMETERS
			database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaModulo.Add(new Modulo(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Modulo
        /// </summary>
        /// <param name="pModulo"></param>
        /// <returns></returns>
        public RespuestaModulo ModificarModulo(Modulo pModulo)
        {
            int filasAfectadas;
			RespuestaModulo respuesta = new RespuestaModulo();
			
            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarModulo);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Modulo.idModuloProperty), DbType.Int64, pModulo.IdModulo);
            database1.AddInParameter(dbCommand, parameterName( Modulo.nombreProperty), DbType.AnsiString, pModulo.Nombre);
            database1.AddInParameter(dbCommand, parameterName( Modulo.descripcionProperty), DbType.AnsiString, pModulo.Descripcion);
            database1.AddInParameter(dbCommand, parameterName( Modulo.cssClassProperty), DbType.String, pModulo.CssClass);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pModulo.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditModulo, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Modulo
        /// </summary>
        /// <param name="pModulo"></param>
        /// <returns></returns>
        public RespuestaModulo EliminarModulo(Modulo pModulo)
        {
            int filasAfectadas;
			RespuestaModulo respuesta = new RespuestaModulo();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarModulo);

            //IN PARAMETERS 
            

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteModulo, respuesta.Respuesta.CodMensaje);
            }
               
            return respuesta;
        }


		/// <summary>
        /// Consulta en la base de datos  la tabla Modulo
        /// </summary>
        /// <param name="pModulo"></param>
        /// <returns></returns>
        public RespuestaListaModulo ObtenerModuloPaginado(Modulo pModulo, ref Paginacion pPaginacion)
        {
            RespuestaListaModulo respuesta = new RespuestaListaModulo();
			respuesta.ListaModulo = new List<Modulo>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerModuloPaginado);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Modulo.idModuloProperty), DbType.Int64, pModulo.IdModulo);
            database1.AddInParameter(dbCommand, parameterName( Modulo.nombreProperty), DbType.AnsiString, pModulo.Nombre);
            database1.AddInParameter(dbCommand, parameterName( Modulo.descripcionProperty), DbType.AnsiString, pModulo.Descripcion);
            database1.AddInParameter(dbCommand, parameterName( Modulo.cssClassProperty), DbType.String, pModulo.CssClass);
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
                    respuesta.ListaModulo.Add(new Modulo(Reader));
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

        /// <summary>
        /// Obtiene el menu el usuario.
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaListaModulo ObtenerMenuUsuario(Usuario pUsuario)
        {
            RespuestaListaModulo respuesta = new RespuestaListaModulo();
            respuesta.ListaModulo = new List<Modulo>();
            var oListaControladores = new List<Controlador>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerMenuUsuario);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrTokenAuthenticateProperty), DbType.String, pUsuario.UsrtokensAuthenticate);


            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 1000);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaModulo.Add(new Modulo(Reader));
                }
                Reader.NextResult();
                while (Reader.Read())
                {
                    oListaControladores.Add(new Controlador(Reader));
                }
            }

            //agregamos controladores al modulo
            if (respuesta.ListaModulo.Count > 0)
            {
                foreach (var modulo in respuesta.ListaModulo)
                {
                    modulo.Controladores = new List<Controlador>();
                    if (oListaControladores.Count > 0)
                    {
                        foreach (var controlador in oListaControladores)
                        {
                            if (modulo.IdModulo == controlador.IdModulo)
                            {
                                modulo.Controladores.Add(controlador);
                            }
                        }
                    }
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

    } //fin de clase
}