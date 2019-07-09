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
    /// Propósito:           Acceso Datos clase   AlertaAccesoDatos
    /// Ultima modificacion: 01/12/2017
	/// Versión:			 V1.0
    /// </summary>
	
	
    /// </summary>
    public class AlertaAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase AlertaBase de la capa de Acceso a Datos
        /// </summary>
        public AlertaAccesoDatos()
            : base()
        { }


		/// <summary>
        /// Consulta en la base de datos  la tabla AlertaBase
        /// </summary>
        /// <param name="pAlerta"></param>
        /// <returns></returns>
        public RespuestaListaAlerta ObtenerAlerta(AlertaBase pAlerta)
        {
			RespuestaListaAlerta respuesta = new RespuestaListaAlerta();
			respuesta.ListaAlerta = new List<AlertaBase>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAlerta);

            //IN PARAMETERS 
            
            database1.AddInParameter(dbCommand, parameterName( AlertaBase.idProperty), DbType.Int64, pAlerta.Id);
            database1.AddInParameter(dbCommand, parameterName( BaseEntidad.idEntidadProperty), DbType.AnsiString, pAlerta.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName( AlertaBase.nombreAlertaProperty), DbType.AnsiString, pAlerta.NombreAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaBase.codigoAlertaProperty), DbType.AnsiString, pAlerta.CodigoAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaBase.tipoAlertaProperty), DbType.AnsiString, pAlerta.TipoAlerta);
            database1.AddInParameter(dbCommand, parameterName( AlertaBase.activaProperty), DbType.Boolean, pAlerta.Activo);
            database1.AddInParameter(dbCommand, parameterName( AlertaBase.idControladorProperty), DbType.Int64, pAlerta.IdControlador);

            //OUT PARAMETERS
			database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            

            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAlerta.Add(new AlertaBase(Reader));
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