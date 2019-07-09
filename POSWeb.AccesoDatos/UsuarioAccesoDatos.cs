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
    /// Propósito:           Acceso Datos clase   UsuarioAccesoDatos
    /// Ultima modificacion: 04/11/2017
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class UsuarioAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Usuario de la capa de Acceso a Datos
        /// </summary>
        public UsuarioAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario InsertarUsuario(Usuario pUsuario)
        {
            int filasAfectadas;

            RespuestaUsuario respuesta = new RespuestaUsuario();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarUsuario);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pUsuario.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Usuario.nombreProperty), DbType.String, pUsuario.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Usuario.claveProperty), DbType.AnsiString, pUsuario.Clave);
            database1.AddInParameter(dbCommand, parameterName(Usuario.idTipoIdentificacionProperty), DbType.Int64, pUsuario.IdTipoIdentificacion);
            database1.AddInParameter(dbCommand, parameterName(Usuario.idPaisProperty), DbType.Int64, pUsuario.IdPais);
            database1.AddInParameter(dbCommand, parameterName(Usuario.correoElectronicoProperty), DbType.String, pUsuario.CorreoElectronico);
            database1.AddInParameter(dbCommand, parameterName(Usuario.identificacionProperty), DbType.String, pUsuario.Identificacion);
            database1.AddInParameter(dbCommand, parameterName(Usuario.estadoProperty), DbType.String, pUsuario.Estado);
            database1.AddInParameter(dbCommand, parameterName(Usuario.fechaExpiracionClaveProperty), DbType.DateTime, pUsuario.FechaExpiracionClave);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.xmlDataProperty), DbType.Xml, pUsuario.XMLData);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pUsuario.UsrCreacion);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.nombreControladorProperty), DbType.String, pUsuario.NombreControlador);

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
                respuesta.Usuario = pUsuario;
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateUsuario, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaListaUsuario ObtenerUsuario(Usuario pUsuario)
        {
            RespuestaListaUsuario respuesta = new RespuestaListaUsuario();
            respuesta.ListaUsuario = new List<Usuario>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerUsuario);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.String, pUsuario.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Usuario.nombreProperty), DbType.AnsiString, pUsuario.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Usuario.claveProperty), DbType.AnsiString, pUsuario.Clave);
            database1.AddInParameter(dbCommand, parameterName(Usuario.identificacionProperty), DbType.AnsiString, pUsuario.Identificacion);
            database1.AddInParameter(dbCommand, parameterName(Usuario.estadoProperty), DbType.String, pUsuario.Estado);
            if (pUsuario.FechaExpiracionClave != null && pUsuario.FechaExpiracionClave != DateTime.MinValue)
                database1.AddInParameter(dbCommand, parameterName(Usuario.fechaExpiracionClaveProperty), DbType.DateTime, pUsuario.FechaExpiracionClave);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaUsuario.Add(new Usuario(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario ModificarUsuario(Usuario pUsuario)
        {
            int filasAfectadas;
            RespuestaUsuario respuesta = new RespuestaUsuario();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarUsuario);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pUsuario.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(Usuario.nombreProperty), DbType.AnsiString, pUsuario.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Usuario.claveProperty), DbType.AnsiString, pUsuario.Clave);
            database1.AddInParameter(dbCommand, parameterName(Usuario.idTipoIdentificacionProperty), DbType.Int64, pUsuario.IdTipoIdentificacion);
            database1.AddInParameter(dbCommand, parameterName(Usuario.identificacionProperty), DbType.AnsiString, pUsuario.Identificacion);
            database1.AddInParameter(dbCommand, parameterName(Usuario.estadoProperty), DbType.String, pUsuario.Estado);
            database1.AddInParameter(dbCommand, parameterName(Usuario.fechaExpiracionClaveProperty), DbType.DateTime, pUsuario.FechaExpiracionClave);
            database1.AddInParameter(dbCommand, parameterName(Usuario.intentosFallidosProperty), DbType.Int32, pUsuario.IntentosFallidos);
            database1.AddInParameter(dbCommand, parameterName(Usuario.pendienteCambioProperty), DbType.Boolean, pUsuario.PendienteCambio);
            database1.AddInParameter(dbCommand, parameterName(Usuario.correoElectronicoProperty), DbType.String, pUsuario.CorreoElectronico);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.xmlDataProperty), DbType.Xml, pUsuario.XMLData);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pUsuario.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditUsuario, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario EliminarUsuario(Usuario pUsuario)
        {
            int filasAfectadas;
            RespuestaUsuario respuesta = new RespuestaUsuario();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarUsuario);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pUsuario.IdEntidad);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteUsuario, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }


        /// <summary>
        /// Consulta en la base de datos  la tabla Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaListaUsuario ObtenerUsuarioPaginado(Usuario pUsuario, Paginacion pPaginacion)
        {
            RespuestaListaUsuario respuesta = new RespuestaListaUsuario();
            respuesta.ListaUsuario = new List<Usuario>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerUsuarioPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(Usuario.nombreProperty), DbType.AnsiString, pUsuario.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Usuario.claveProperty), DbType.AnsiString, pUsuario.Clave);
            database1.AddInParameter(dbCommand, parameterName(Usuario.identificacionProperty), DbType.AnsiString, pUsuario.Identificacion);
            database1.AddInParameter(dbCommand, parameterName(Usuario.estadoProperty), DbType.String, pUsuario.Estado);
            database1.AddInParameter(dbCommand, parameterName(Usuario.fechaExpiracionClaveProperty), DbType.DateTime, pUsuario.FechaExpiracionClave);
            if (pUsuario.IntentosFallidos != 0)
                database1.AddInParameter(dbCommand, parameterName(Usuario.intentosFallidosProperty), DbType.Int32, pUsuario.IntentosFallidos);
            database1.AddInParameter(dbCommand, parameterName(Usuario.correoElectronicoProperty), DbType.String, pUsuario.CorreoElectronico);
            if (pUsuario.IdTipoIdentificacion != 0)
                database1.AddInParameter(dbCommand, parameterName(Usuario.idTipoIdentificacionProperty), DbType.Int64, pUsuario.IdTipoIdentificacion);
            if (pUsuario.IdPais != 0)
                database1.AddInParameter(dbCommand, parameterName(Usuario.idPaisProperty), DbType.Int64, pUsuario.IdPais);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.idEntidadProperty), DbType.Int64, pUsuario.IdEntidad);
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
                    respuesta.ListaUsuario.Add(new Usuario(Reader));
                }
            }

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
            respuesta.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));
            respuesta.NumPagina = pPaginacion.NumPagina;
            respuesta.TamanoPagina = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.tamanoPaginaProperty))); ;

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Valida el inicio de sesion de un usuario en el sistema
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario ValidarUsuarioLogin(Usuario pUsuario)
        {
            RespuestaUsuario respuesta = new RespuestaUsuario();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ValidarUsuarioLogin);

            //IN PARAMETERS 
            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(Usuario.claveProperty), DbType.AnsiString, pUsuario.Clave);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            var filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            //Registro de Error en bitacora
            /*if (respuesta.Respuesta.CodMensaje != Respuesta.CodExitoso)
            {
				RegistrarError(respuesta, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }*/

            return respuesta;
        }

        /// <summary>
        /// Cambia la contraseña del usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public RespuestaUsuario CambiarContrasena(Usuario pUsuario)
        {
            int filasAfectadas;
            RespuestaUsuario respuesta = new RespuestaUsuario();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.CambiarContrasena);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Usuario.codigoUsuarioProperty), DbType.String, pUsuario.CodigoUsuario);
            database1.AddInParameter(dbCommand, parameterName(Usuario.claveProperty), DbType.AnsiString, pUsuario.Clave);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditUsuario, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

    } //fin de clase
}