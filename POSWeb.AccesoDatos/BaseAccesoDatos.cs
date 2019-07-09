//#define Oracle11G
//#define Oracle10G
#define SQLServer
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSWeb.ControlExcepciones;

namespace POSWeb.AccesoDatos
{
    /// <summary>
    /// 
    ///  * Requerimiento:       BM.Enterprise
    ///  * Empresa:             BMYASOCIADOS S.A
    ///  * Autor:               Jorge Bastos
    ///  * Propósito:           Clase base de acceso a BD   
    ///  * Ultima modificacion: 14/06/2017
    /// </summary>
    public class BaseAccesoDatos
    {

        protected Database database1 = null;
#if SQLServer
        public const string parameterPrefix = "@"; /*SQL SERVER*/
#endif
#if Oracle10G
        public const string parameterPrefix = "p"; /*ORACLE with System.Data.Oracle*/
#endif
#if Oracle11G
        public const string parameterPrefix = ":"; /*ORACLE with Oracle.DataAccess.Client*/
#endif

        private static bool _initialized = false;
        private static string currentSchema = "";

        public BaseAccesoDatos()
        {
            if(!_initialized)
            {
                try
                {
                    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
                }
                catch(InvalidOperationException ex)
                {
                    ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, null);
                }

                if(!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CurrentSchema"]))
                {
                    currentSchema = ConfigurationManager.AppSettings["CurrentSchema"] + ".";
                }

                _initialized = true;
            }

            database1 = DatabaseFactory.CreateDatabase();
        }

        

        public static string parameterName(string pName)
        {
            return parameterPrefix + pName;
        }

        public static string defaultSchema
        {
            get { return  currentSchema; }
        }


        /// <summary>
        /// Crea parametros de tipo cursor por referencia utilizado para obtener los result set de las consultas
        /// </summary>
        /// <returns>Parametro creado</returns>
        public static void CrearCursor(System.Data.Common.DbCommand dbCommand)
        {
#if Oracle11G
            dbCommand.Parameters.Add(new Oracle.DataAccess.Client.OracleParameter("pSalidaCursor", Oracle.DataAccess.Client.OracleDbType.RefCursor, 0, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Current, Convert.DBNull));
#endif

#if SQLServer

#endif
#if Oracle10G
            dbCommand.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("pSalidaCursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor,
                0,  ParameterDirection.Output, true, 0, 0, String.Empty,
                DataRowVersion.Current, Convert.DBNull));

#endif
        }

    }
}

