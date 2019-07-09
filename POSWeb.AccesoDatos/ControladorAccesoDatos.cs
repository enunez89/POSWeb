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
    /// Propósito:           Acceso Datos clase   ControladorAccesoDatos
    /// Ultima modificacion: 29/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class ControladorAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Controlador de la capa de Acceso a Datos
        /// </summary>
        public ControladorAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Consulta en la base de datos  la tabla Controlador
        /// </summary>
        /// <param name="pControlador"></param>
        /// <returns></returns>
        public RespuestaListaControlador ObtenerControlador(Controlador pControlador)
        {
            RespuestaListaControlador respuesta = new RespuestaListaControlador();
            respuesta.ListaControlador = new List<Controlador>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerControlador);

            //IN PARAMETERS 
            if (pControlador.IdControlador != 0)
                database1.AddInParameter(dbCommand, parameterName(Controlador.idControladorProperty), DbType.Int64, pControlador.IdControlador);
            database1.AddInParameter(dbCommand, parameterName(Controlador.codigoProperty), DbType.String, pControlador.Codigo);
            database1.AddInParameter(dbCommand, parameterName(Controlador.descripcionProperty), DbType.AnsiString, pControlador.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(Controlador.accionDefaultProperty), DbType.AnsiString, pControlador.AccionDefault);
            database1.AddInParameter(dbCommand, parameterName(Controlador.cssClassProperty), DbType.String, pControlador.CssClass);
            //database1.AddInParameter(dbCommand, parameterName(Controlador.autorizarProperty), DbType.Boolean, pControlador.Autorizar);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaControlador.Add(new Controlador(Reader));
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