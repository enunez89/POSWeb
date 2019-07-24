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
    /// Empresa:             Salazar & Asociados S.A.
    /// Autor:               Eddy 
    /// Propósito:           Acceso Datos clase   ProveedorAccesoDatos
    /// Ultima modificacion: 23-07-2019
    /// Versión:			 V1.0
    /// </summary>


    /// </summary>
    public class ProveedorAccesoDatos : BaseAccesoDatos
    {
        /// <summary>
        /// Contructor Clase Proveedor de la capa de Acceso a Datos
        /// </summary>
        public ProveedorAccesoDatos()
            : base()
        { }

        /// <summary>
        /// Inserta informacion en la tabla Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaProveedor InsertarProveedor(Proveedor pProveedor)
        {
            int filasAfectadas;

            RespuestaProveedor respuesta = new RespuestaProveedor();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.InsertarProveedor);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Proveedor.idEntidadProperty), DbType.Int64, pProveedor.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nombreProperty), DbType.AnsiString, pProveedor.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.descripcionProperty), DbType.AnsiString, pProveedor.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nomContactoProperty), DbType.AnsiString, pProveedor.NomContacto);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.telContactoProperty), DbType.AnsiString, pProveedor.TelContacto);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.correoContactoProperty), DbType.AnsiString, pProveedor.CorreoContacto);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrCreacionProperty), DbType.String, pProveedor.UsrCreacion);

            //OUT PARAMETERS

            database1.AddOutParameter(dbCommand, parameterName(Proveedor.idProperty), DbType.Int32, int.MaxValue);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);

            //EXECUTE PROCEDURE
            filasAfectadas = database1.ExecuteNonQuery(dbCommand);

            //ERROR CODE AND MESSAGE COLLECTOR

            pProveedor.Id = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Proveedor.idProperty)));
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(Mensajes.bmCreateProveedor, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Consulta en la base de datos  la tabla Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaListaProveedor ObtenerProveedor(Proveedor pProveedor)
        {
            RespuestaListaProveedor respuesta = new RespuestaListaProveedor();
            respuesta.ListaProveedor = new List<Proveedor>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerProveedor);

            //IN PARAMETERS 
            if (pProveedor.Id != 0)
                database1.AddInParameter(dbCommand, parameterName(Proveedor.idProperty), DbType.Int32, pProveedor.Id);
            if (pProveedor.IdEntidad != 0)
                database1.AddInParameter(dbCommand, parameterName(Proveedor.idEntidadProperty), DbType.Int64, pProveedor.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nombreProperty), DbType.AnsiString, pProveedor.Nombre);

            //OUT PARAMETERS
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.codErrorProperty), DbType.String, Constantes.BaseDatos.codErrorTamano);
            database1.AddOutParameter(dbCommand, parameterName(BaseEntidad.mensajeProperty), DbType.String, Constantes.BaseDatos.mensajeTamano);



            //EXECUTE PROCEDURE - CONVERT ROWS
            using (IDataReader Reader = database1.ExecuteReader(dbCommand))
            {
                while (Reader.Read())
                {
                    respuesta.ListaProveedor.Add(new Proveedor(Reader));
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
        /// Metodo que se utiliza para modificar la informacion en la tabla Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaProveedor ModificarProveedor(Proveedor pProveedor)
        {
            int filasAfectadas;
            RespuestaProveedor respuesta = new RespuestaProveedor();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ModificarProveedor);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Proveedor.idProperty), DbType.Int32, pProveedor.Id);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nombreProperty), DbType.AnsiString, pProveedor.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.descripcionProperty), DbType.AnsiString, pProveedor.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nomContactoProperty), DbType.AnsiString, pProveedor.NomContacto);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.telContactoProperty), DbType.AnsiString, pProveedor.TelContacto);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.correoContactoProperty), DbType.AnsiString, pProveedor.CorreoContacto);
            database1.AddInParameter(dbCommand, parameterName(BaseEntidad.usrModificacionProperty), DbType.String, pProveedor.UsrModificacion);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmEditProveedor, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo que se encarga de eliminar o desactivar un registro  de la tabla Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaProveedor EliminarProveedor(Proveedor pProveedor)
        {
            int filasAfectadas;
            RespuestaProveedor respuesta = new RespuestaProveedor();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.EliminarProveedor);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Proveedor.idProperty), DbType.Int32, pProveedor.Id);

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
                respuesta.Respuesta = new Respuesta(Mensajes.bmDeleteProveedor, respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }


        /// <summary>
        /// Consulta en la base de datos  la tabla Proveedor
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public RespuestaListaProveedor ObtenerProveedorPaginado(Proveedor pProveedor, ref Paginacion pPaginacion)
        {
            RespuestaListaProveedor respuesta = new RespuestaListaProveedor();
            respuesta.ListaProveedor = new List<Proveedor>();

            //STRORE PROCEDURE DEFINITION
            DbCommand dbCommand = database1.GetStoredProcCommand(defaultSchema + StoredProcedure.ObtenerProveedorPaginado);

            //IN PARAMETERS 

            database1.AddInParameter(dbCommand, parameterName(Proveedor.idProperty), DbType.Int32, pProveedor.Id);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.idEntidadProperty), DbType.Int64, pProveedor.IdEntidad);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nombreProperty), DbType.AnsiString, pProveedor.Nombre);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.descripcionProperty), DbType.AnsiString, pProveedor.Descripcion);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.nomContactoProperty), DbType.AnsiString, pProveedor.NomContacto);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.telContactoProperty), DbType.AnsiString, pProveedor.TelContacto);
            database1.AddInParameter(dbCommand, parameterName(Proveedor.correoContactoProperty), DbType.AnsiString, pProveedor.CorreoContacto);
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
                    respuesta.ListaProveedor.Add(new Proveedor(Reader));
                }
            }

            //ERROR CODE AND MESSAGE COLLECTOR
            respuesta.Respuesta = new Respuesta();
            respuesta.Respuesta.CodMensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.codErrorProperty)));
            respuesta.Respuesta.Mensaje = DBHelper.ReadNullSafeString(database1.GetParameterValue(dbCommand, parameterName(BaseEntidad.mensajeProperty)));
            pPaginacion.TotalRegistros = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.totalRegistrosProperty)));
            pPaginacion.TamanoPagina = DBHelper.ReadNullSafeInt(database1.GetParameterValue(dbCommand, parameterName(Paginacion.tamanoPaginaProperty)));

            if (respuesta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
                respuesta.Respuesta = new Respuesta(respuesta.Respuesta.CodMensaje);
            }

            return respuesta;
        }

    } //fin de clase
}