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
    /// Propósito:           Acceso Datos clase   PerfilAccesoDatos
    /// Ultima modificacion: 15/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class PerfilAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Perfil de la capa de Acceso a Datos
        /// </summary>
        public PerfilAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public RespuestaPerfil InsertarPerfil(Perfil pPerfil)
        {
            int filasAfectadas;

            RespuestaPerfil respuesta = new RespuestaPerfil();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarPerfil);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Perfil.idPerfilProperty), DbType.Int64, pPerfil.IdPerfil);
            database1.AddInParameter(dbCommand, parameterName(Perfil.nombreProperty), DbType.AnsiString, pPerfil.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Perfil.estadoProperty), DbType.String, pPerfil.Estado);
            database1.AddInParameter(dbCommand, parameterName(Perfil.indSuperUsuarioProperty), DbType.Boolean, pPerfil.IndSuperUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pPerfil.UsrCreacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreatePerfil, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public RespuestaListaPerfil ObtenerPerfil(Perfil pPerfil)
        {
            RespuestaListaPerfil respuesta = new RespuestaListaPerfil();
            respuesta.ListaPerfil = new List<Perfil>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerPerfil);

            //IN PARAMETERS 

            //if (pPerfil.IdPerfil != 0)
            database1.AddInParameter(dbCommand, parameterName(Perfil.idPerfilProperty), DbType.Int64, pPerfil.IdPerfil);
            database1.AddInParameter(dbCommand, parameterName(Perfil.nombreProperty), DbType.AnsiString, pPerfil.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Perfil.estadoProperty), DbType.String, pPerfil.Estado);
            database1.AddInParameter(dbCommand, parameterName(Perfil.indSuperUsuarioProperty), DbType.String, pPerfil.IndSuperUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pPerfil.IdEntidad);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaPerfil.Add(new Perfil(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public RespuestaPerfil ModificarPerfil(Perfil pPerfil)
        {
            int filasAfectadas;
            RespuestaPerfil respuesta = new RespuestaPerfil();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarPerfil);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Perfil.idPerfilProperty), DbType.Int64, pPerfil.IdPerfil);
            database1.AddInParameter(dbCommand, parameterName(Perfil.nombreProperty), DbType.AnsiString, pPerfil.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Perfil.estadoProperty), DbType.String, pPerfil.Estado);
            database1.AddInParameter(dbCommand, parameterName(Perfil.indSuperUsuarioProperty), DbType.Boolean, pPerfil.IndSuperUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pPerfil.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditPerfil, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public RespuestaPerfil EliminarPerfil(Perfil pPerfil)
        {
            int filasAfectadas;
            RespuestaPerfil respuesta = new RespuestaPerfil();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarPerfil);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeletePerfil, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Perfil
        /// </summary>
        /// <param name="pPerfil"></param>
        /// <returns></returns>
        public RespuestaListaPerfil ObtenerPerfilPaginado(Perfil pPerfil, ref Paginacion pPaginacion)
        {
            RespuestaListaPerfil respuesta = new RespuestaListaPerfil();
            respuesta.ListaPerfil = new List<Perfil>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerPerfilPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Perfil.idPerfilProperty), DbType.Int64, pPerfil.IdPerfil);
            database1.AddInParameter(dbCommand, parameterName(Perfil.nombreProperty), DbType.AnsiString, pPerfil.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Perfil.estadoProperty), DbType.String, pPerfil.Estado);
            database1.AddInParameter(dbCommand, parameterName(Perfil.indSuperUsuarioProperty), DbType.Boolean, pPerfil.IndSuperUsuario);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.numPaginaProperty), DbType.Int32, pPaginacion.NumPagina);
            database1.AddInParameter(dbCommand, parameterName(Paginacion.tamanoPaginaProperty), DbType.Int32, pPaginacion.TamanoPagina);


            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(Paginacion.totalRegistrosProperty), DbType.Int32, 32);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, 2);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, 200);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaPerfil.Add(new Perfil(Reader));
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
        /// Obtiene la lista de perfiles que tiene un rol
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public RespuestaListaPerfil ObtenerPerfilPorRol(Rol pRol)
        {
            RespuestaListaPerfil respuesta = new RespuestaListaPerfil();
            respuesta.ListaPerfil = new List<Perfil>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerPerfilPorRol);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Rol.idRolProperty), DbType.Int64, pRol.IdRol);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaPerfil.Add(new Perfil(Reader));
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