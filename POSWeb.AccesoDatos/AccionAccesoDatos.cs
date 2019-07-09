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
    /// Propósito:           Acceso Datos clase   AccionAccesoDatos
    /// Ultima modificacion: 16/11/2017
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class AccionAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Accion de la capa de Acceso a Datos
        /// </summary>
        public AccionAccesoDatos()
            : base()
        { }
        
		/// <summary>
        /// Consulta en la base de datos  la tabla Accion
        /// </summary>
        /// <param name="pAccion"></param>
        /// <returns></returns>
        public RespuestaListaAccion ObtenerAccion(Accion pAccion)
        {
			RespuestaListaAccion respuesta = new RespuestaListaAccion();
			respuesta.ListaAccion = new List<Accion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAccion);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( Accion.idAccionProperty), DbType.Int64, pAccion.IdAccion);
            database1.AddInParameter(dbCommand, parameterName( Accion.nombreProperty), DbType.AnsiString, pAccion.Nombre);
            database1.AddInParameter(dbCommand, parameterName( Accion.descripcionProperty), DbType.AnsiString, pAccion.Descripcion);
            database1.AddInParameter(dbCommand, parameterName( Accion.cssClassProperty), DbType.String, pAccion.CssClass);

            //OUT PARAMETERS
			database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAccion.Add(new Accion(Reader));
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
        /// Obtiene la lista de acciones a las que tiene permitido el usuario
        /// </summary>
        /// <param name="pAccion"></param>
        /// <returns></returns>
        public RespuestaListaAccion ObtenerAccionesPerfil(Accion pAccion)
        {
            RespuestaListaAccion respuesta = new RespuestaListaAccion();
            respuesta.ListaAccion = new List<Accion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAccionesPerfil);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrTokenAuthenticateProperty), DbType.AnsiString, pAccion.UsrtokensAuthenticate);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAccion.Add(new Accion(Reader));
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

    } //fin de clase
}