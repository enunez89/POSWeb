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
    /// Propósito:           Acceso Datos clase   RolAccesoDatos
    /// Ultima modificacion: 13/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class RolAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Rol de la capa de Acceso a Datos
        /// </summary>
        public RolAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaRol InsertarRol(Rol pRol)
        {
            int filasAfectadas;

            RespuestaRol respuesta = new RespuestaRol();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarRol);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Rol.nombreProperty), DbType.AnsiString, pRol.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Rol.estadoProperty), DbType.String, pRol.Estado);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pRol.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.xmlDataProperty), DbType.Xml, pRol.XMLData);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pRol.UsrCreacion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.nombreControladorProperty), DbType.String, pRol.NombreControlador);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Rol.idRolProperty), DbType.Int64, 35);
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
                pRol.IdRol = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Rol.idRolProperty)));
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateRol, respuesta.Respuesta.CodMensaje);
                respuesta.Rol = pRol;
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaListaRol ObtenerRol(Rol pRol)
        {
            RespuestaListaRol respuesta = new RespuestaListaRol();
            respuesta.ListaRol = new List<Rol>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerRol);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Rol.idRolProperty), DbType.Int64, pRol.IdRol);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pRol.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Rol.nombreProperty), DbType.AnsiString, pRol.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Rol.estadoProperty), DbType.String, pRol.Estado);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaRol.Add(new Rol(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaRol ModificarRol(Rol pRol)
        {
            int filasAfectadas;
            RespuestaRol respuesta = new RespuestaRol();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarRol);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Rol.idRolProperty), DbType.Int64, pRol.IdRol);
            database1.AddInParameter(dbCommand, parameterName(Rol.nombreProperty), DbType.AnsiString, pRol.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Rol.estadoProperty), DbType.String, pRol.Estado);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.xmlDataProperty), DbType.String, pRol.XMLData);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pRol.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditRol, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaRol EliminarRol(Rol pRol)
        {
            int filasAfectadas;
            RespuestaRol respuesta = new RespuestaRol();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarRol);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(Rol.idRolProperty), DbType.Int64, pRol.IdRol);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteRol, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaListaRol ObtenerRolPaginado(Rol pRol, Paginacion pPaginacion)
        {
            RespuestaListaRol respuesta = new RespuestaListaRol();
            respuesta.ListaRol = new List<Rol>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerRolPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Rol.idRolProperty), DbType.Int64, pRol.IdRol);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pRol.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Rol.nombreProperty), DbType.AnsiString, pRol.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Rol.estadoProperty), DbType.String, pRol.Estado);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, pPaginacion.TamanoPagina);


            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaRol.Add(new Rol(Reader));
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
        /// Obtiene una lista de roles por usuario y entidad
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaListaRol ObtenerRolPorUsuario(Usuario pUsuario)
        {
            RespuestaListaRol respuesta = new RespuestaListaRol();
            respuesta.ListaRol = new List<Rol>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerRolPorUsuario);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pUsuario.IdEntidad);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaRol.Add(new Rol(Reader));
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