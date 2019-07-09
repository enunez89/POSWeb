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
    /// Propósito:           Acceso Datos clase   CatalogoAccesoDatos
    /// Ultima modificacion: 18/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class CatalogoAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Catalogo de la capa de Acceso a Datos
        /// </summary>
        public CatalogoAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Consulta en la base de datos  la tabla Catalogo
        /// </summary>
        /// <param name="pCatalogo"></param>
        /// <returns></returns>
        public RespuestaListaCatalogo ObtenerCatalogos(Catalogo pCatalogo)
        {
            RespuestaListaCatalogo respuesta = new RespuestaListaCatalogo();
            respuesta.ListaCatalogo = new List<Catalogo>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerCatalogo);

            //IN PARAMETERS 

            if (pCatalogo.IdCatalogo != 0)
                database1.AddInParameter(dbCommand, parameterName(Catalogo.idCatalogoProperty), DbType.Int64, pCatalogo.IdCatalogo);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.identificadorProperty), DbType.AnsiString, pCatalogo.Identificador);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.codigoParametroProperty), DbType.AnsiString, pCatalogo.CodigoParametro);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.descripcionProperty), DbType.AnsiString, pCatalogo.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pCatalogo.IdEntidad);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaCatalogo.Add(new Catalogo(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Catalogo
        /// </summary>
        /// <param name="pCatalogo"></param>
        /// <returns></returns>
        public RespuestaCatalogo ModificarCatalogo(Catalogo pCatalogo)
        {
            int filasAfectadas;
            RespuestaCatalogo respuesta = new RespuestaCatalogo();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarCatalogo);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Catalogo.idCatalogoProperty), DbType.Int64, pCatalogo.IdCatalogo);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.identificadorProperty), DbType.AnsiString, pCatalogo.Identificador);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.codigoParametroProperty), DbType.AnsiString, pCatalogo.CodigoParametro);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Boolean, pCatalogo.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pCatalogo.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditCatalogo, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Catalogo
        /// </summary>
        /// <param name="pCatalogo"></param>
        /// <returns></returns>
        public RespuestaListaCatalogo ObtenerCatalogoPaginado(Catalogo pCatalogo, Paginacion pPaginacion)
        {
            RespuestaListaCatalogo respuesta = new RespuestaListaCatalogo();
            respuesta.ListaCatalogo = new List<Catalogo>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerCatalogoPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Catalogo.idCatalogoProperty), DbType.Int64, pCatalogo.IdCatalogo);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.identificadorProperty), DbType.AnsiString, pCatalogo.Identificador);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.codigoParametroProperty), DbType.AnsiString, pCatalogo.CodigoParametro);
            database1.AddInParameter(dbCommand, parameterName(Catalogo.descripcionProperty), DbType.AnsiString, pCatalogo.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pCatalogo.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina);


            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaCatalogo.Add(new Catalogo(Reader));
                }
            }

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
            respuesta.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));
            respuesta.NumPagina = pPaginacion.NumPagina;
            respuesta.TamanoPagina = pPaginacion.TamanoPagina;

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

    } //fin de clase
}