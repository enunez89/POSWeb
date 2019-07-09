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
    /// Propósito:           Acceso Datos clase   BitacoraAccesoDatos
    /// Ultima modificacion: 25/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class BitacoraAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Bitacora de la capa de Acceso a Datos
        /// </summary>
        public BitacoraAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Consulta en la base de datos  la tabla Bitacora
        /// </summary>
        /// <param name="pBitacora"></param>
        /// <returns></returns>
        public RespuestaListaBitacoraAplicacion ObtenerBitacoraPaginado(BitacoraAplicacion pBitacora, Paginacion pPaginacion)
        {
            RespuestaListaBitacoraAplicacion respuesta = new RespuestaListaBitacoraAplicacion();
            respuesta.ListaBitacora = new List<BitacoraAplicacion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerBitacoraPaginado);

            //IN PARAMETERS 

            if (pBitacora.IdBitacora != 0)
                database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.idBitacoraProperty), DbType.Decimal, pBitacora.IdBitacora);
            if (pBitacora.FechaRegistro != DateTime.MinValue)
                database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.fechaRegistroProperty), DbType.DateTime, pBitacora.FechaRegistro);
            if (pBitacora.FechaInicio != DateTime.MinValue)
                database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.fechaInicioProperty), DbType.DateTime, pBitacora.FechaInicio);
            if (pBitacora.FechaFinal != DateTime.MinValue)
                database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.fechaFinalProperty), DbType.DateTime, pBitacora.FechaFinal);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.tipoEventoProperty), DbType.String, pBitacora.TipoEvento);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.mensajeRegistroProperty), DbType.AnsiString, pBitacora.MensajeRegistro);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.mensajeTecnicoProperty), DbType.AnsiString, pBitacora.MensajeTecnico);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.trazadorProperty), DbType.AnsiString, pBitacora.Trazador);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pBitacora.IdEntidad);

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
                    respuesta.ListaBitacora.Add(new BitacoraAplicacion(Reader));
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

        /// <summary>
        /// Consulta en la base de datos  la tabla Bitacora
        /// </summary>
        /// <param name="pBitacora"></param>
        /// <returns></returns>
        public RespuestaListaBitacoraAplicacion ObtenerBitacora(BitacoraAplicacion pBitacora)
        {
            RespuestaListaBitacoraAplicacion respuesta = new RespuestaListaBitacoraAplicacion();
            respuesta.ListaBitacora = new List<BitacoraAplicacion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerBitacora);

            //IN PARAMETERS 

            if (pBitacora.IdBitacora != 0)
                database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.idBitacoraProperty), DbType.Decimal, pBitacora.IdBitacora);
            if (pBitacora.FechaRegistro != DateTime.MinValue)
                database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.fechaRegistroProperty), DbType.DateTime, pBitacora.FechaRegistro);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.tipoEventoProperty), DbType.String, pBitacora.TipoEvento);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.mensajeRegistroProperty), DbType.AnsiString, pBitacora.MensajeRegistro);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.mensajeTecnicoProperty), DbType.AnsiString, pBitacora.MensajeTecnico);
            database1.AddInParameter(dbCommand, parameterName(BitacoraAplicacion.trazadorProperty), DbType.AnsiString, pBitacora.Trazador);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaBitacora.Add(new BitacoraAplicacion(Reader));
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