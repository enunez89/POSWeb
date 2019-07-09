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
    /// Propósito:           Acceso Datos clase   PaisAccesoDatos
    /// Ultima modificacion: 06/12/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class PaisAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Pais de la capa de Acceso a Datos
        /// </summary>
        public PaisAccesoDatos()
            : base()
        { }
        
        /// <summary>
        /// Consulta en la base de datos  la tabla Pais
        /// </summary>
        /// <param name="pPais"></param>
        /// <returns></returns>
        public RespuestaListaPais ObtenerPais(Pais pPais)
        {
            RespuestaListaPais respuesta = new RespuestaListaPais();
            respuesta.ListaPais = new List<Pais>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerPais);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Pais.idPaisProperty), DbType.Int64, pPais.IdPais);
            database1.AddInParameter(dbCommand, parameterName(Pais.codigoProperty), DbType.String, pPais.Codigo);
            database1.AddInParameter(dbCommand, parameterName(Pais.nombreProperty), DbType.AnsiString, pPais.Nombre);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaPais.Add(new Pais(Reader));
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