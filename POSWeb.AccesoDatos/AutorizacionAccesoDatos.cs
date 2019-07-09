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
    /// Propósito:           Acceso Datos clase   AutorizacionAccesoDatos
    /// Ultima modificacion: 28/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class AutorizacionAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Autorizacion de la capa de Acceso a Datos
        /// </summary>
        public AutorizacionAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Autorizacion
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <returns></returns>
        public RespuestaAutorizacion InsertarAutorizacion(Autorizacion pAutorizacion)
        {
            int filasAfectadas;

            RespuestaAutorizacion respuesta = new RespuestaAutorizacion();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarAutorizacion);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Autorizacion.recursoProperty), DbType.String, pAutorizacion.Recurso);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idRecursoProperty), DbType.String, pAutorizacion.IdRecurso);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.descripcionProperty), DbType.String, pAutorizacion.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pAutorizacion.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pAutorizacion.UsrCreacion);

            //OUT PARAMETERS

            database1.AddOutParameter(dbCommand, parameterName(Autorizacion.idAutorizacionProperty), DbType.Int64, 64);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR

            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));            

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                pAutorizacion.IdAutorizacion = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Autorizacion.idAutorizacionProperty)));
                respuesta.Autorizacion = pAutorizacion;
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateAutorizacion, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Autorizacion
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <returns></returns>
        public RespuestaListaAutorizacion ObtenerAutorizacion(Autorizacion pAutorizacion)
        {
            RespuestaListaAutorizacion respuesta = new RespuestaListaAutorizacion();
            respuesta.ListaAutorizacion = new List<Autorizacion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAutorizacion);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idAutorizacionProperty), DbType.Int64, pAutorizacion.IdAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.recursoProperty), DbType.String, pAutorizacion.Recurso);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idRecursoProperty), DbType.String, pAutorizacion.IdRecurso);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pAutorizacion.IdEntidad);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAutorizacion.Add(new Autorizacion(Reader));
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
        /// Consulta en la base de datos  la tabla Autorizacion
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <returns></returns>
        public RespuestaListaAutorizacion ObtenerAutorizacionPaginado(Autorizacion pAutorizacion, Paginacion pPaginacion)
        {
            RespuestaListaAutorizacion respuesta = new RespuestaListaAutorizacion();
            respuesta.ListaAutorizacion = new List<Autorizacion>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAutorizacionPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idAutorizacionProperty), DbType.Int64, pAutorizacion.IdAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.recursoProperty), DbType.String, pAutorizacion.Recurso);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idRecursoProperty), DbType.String, pAutorizacion.IdRecurso);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.conteoAutorizacionProperty), DbType.Int32, pAutorizacion.ConteoAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pAutorizacion.UsrCreacion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pAutorizacion.IdEntidad);


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
                    respuesta.ListaAutorizacion.Add(new Autorizacion(Reader));
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
        /// Inserta informacion en la tabla AutorizacionDetalle
        /// </summary>
        /// <param name="pAutorizacionDetalle"></param>
        /// <returns></returns>
        public RespuestaAutorizacionDetalle InsertarAutorizacionDetalle(AutorizacionDetalle pAutorizacionDetalle)
        {
            int filasAfectadas;

            RespuestaAutorizacionDetalle respuesta = new RespuestaAutorizacionDetalle();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarAutorizacionDetalle);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.idAutorizacionProperty), DbType.Int64, pAutorizacionDetalle.IdAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.usrAutorizadorProperty), DbType.String, pAutorizacionDetalle.UsrAutorizador);

            //OUT PARAMETERS

            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR

            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateAutorizacionDetalle, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla AutorizacionDetalle
        /// </summary>
        /// <param name="pAutorizacionDetalle"></param>
        /// <returns></returns>
        public RespuestaListaAutorizacionDetalle ObtenerAutorizacionDetalle(AutorizacionDetalle pAutorizacionDetalle)
        {
            RespuestaListaAutorizacionDetalle respuesta = new RespuestaListaAutorizacionDetalle();
            respuesta.ListaAutorizacionDetalle = new List<AutorizacionDetalle>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAutorizacionDetalle);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.idAutorizacionDetalleProperty), DbType.Int64, pAutorizacionDetalle.IdAutorizacionDetalle);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.idAutorizacionProperty), DbType.Int64, pAutorizacionDetalle.IdAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.usrAutorizadorProperty), DbType.String, pAutorizacionDetalle.UsrAutorizador);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.fechaAutorizacionProperty), DbType.DateTime, pAutorizacionDetalle.FechaAutorizacion);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaAutorizacionDetalle.Add(new AutorizacionDetalle(Reader));
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
        /// Consulta en la base de datos  la tabla AutorizacionDetalle
        /// </summary>
        /// <param name="pAutorizacionDetalle"></param>
        /// <returns></returns>
        public RespuestaListaAutorizacionDetalle ObtenerAutorizacionDetallePaginado(AutorizacionDetalle pAutorizacionDetalle, Paginacion pPaginacion)
        {
            RespuestaListaAutorizacionDetalle respuesta = new RespuestaListaAutorizacionDetalle();
            respuesta.ListaAutorizacionDetalle = new List<AutorizacionDetalle>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerAutorizacionDetallePaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.idAutorizacionDetalleProperty), DbType.Int64, pAutorizacionDetalle.IdAutorizacionDetalle);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.idAutorizacionProperty), DbType.Int64, pAutorizacionDetalle.IdAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.usrAutorizadorProperty), DbType.String, pAutorizacionDetalle.UsrAutorizador);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.fechaAutorizacionProperty), DbType.DateTime, pAutorizacionDetalle.FechaAutorizacion);
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
                    respuesta.ListaAutorizacionDetalle.Add(new AutorizacionDetalle(Reader));
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
        /// Método que valida e inserta la siguiente autorización
        /// </summary>
        /// <param name="pAutorizacion"></param>
        /// <returns></returns>
        public RespuestaAutorizacion ProcesarAutorizacion(Autorizacion pAutorizacion)
        {
            int filasAfectadas;

            RespuestaAutorizacion respuesta = new RespuestaAutorizacion();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ProcesarAutorizacion);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idAutorizacionProperty), DbType.Int64, pAutorizacion.IdAutorizacion);
            database1.AddInParameter(dbCommand, parameterName(AutorizacionDetalle.usrAutorizadorProperty), DbType.String, pAutorizacion.UsrCreacion);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.recursoProperty), DbType.String, pAutorizacion.Recurso);
            database1.AddInParameter(dbCommand, parameterName(Autorizacion.idRecursoProperty), DbType.String, pAutorizacion.IdRecurso);

            //OUT PARAMETERS

            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR

            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateAutorizacion, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

    } //fin de clase
}