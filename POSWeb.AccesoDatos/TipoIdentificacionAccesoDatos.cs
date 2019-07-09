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
    /// Propósito:           Acceso Datos clase   TipoIdentificacionAccesoDatos
    /// Ultima modificacion: 06/12/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class TipoIdentificacionAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase TipoIdentificacion de la capa de Acceso a Datos
        /// </summary>
        public TipoIdentificacionAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Consulta en la base de datos  la tabla TipoIdentificacion
        /// </summary>
        /// <param name="pTipoIdentificacion"></param>
        /// <returns></returns>
        public RespuestaListaTipoIdentificacion ObtenerTipoIdentificacion(TipoIdentificacion pTipoIdentificacion)
        {
            RespuestaListaTipoIdentificacion respuesta = new RespuestaListaTipoIdentificacion();
            respuesta.ListaTipoIdentificacion = new List<TipoIdentificacion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerTipoIdentificacion);

            //IN PARAMETERS 

            if (pTipoIdentificacion.IdTipo != 0)
                database1.AddInParameter(dbCommand, parameterName(TipoIdentificacion.idTipoProperty), DbType.Int64, pTipoIdentificacion.IdTipo);
            if (pTipoIdentificacion.IdPais != 0)
                database1.AddInParameter(dbCommand, parameterName(TipoIdentificacion.idPaisProperty), DbType.Int64, pTipoIdentificacion.IdPais);
            database1.AddInParameter(dbCommand, parameterName(TipoIdentificacion.descripcionProperty), DbType.AnsiString, pTipoIdentificacion.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(TipoIdentificacion.formatoProperty), DbType.String, pTipoIdentificacion.Formato);
            database1.AddInParameter(dbCommand, parameterName(TipoIdentificacion.codigoPaisProperty), DbType.String, pTipoIdentificacion.CodigoPais);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaTipoIdentificacion.Add(new TipoIdentificacion(Reader));
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