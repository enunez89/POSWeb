using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    public class HelperValues
    {



        /// <summary>
        /// Funcion que devuelve el valor de la columna especificada, caso contrario retornara null
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <param name="columnName"></param>
        /// <returns>Valor de la columna o NULL</returns>
        public static Nullable<T> GetValueOrNull<T>(IDataReader reader, string columnName) where T : struct
        {
            //SI EXISTE LA COLUMNA
            if (reader.GetSchemaTable().Select("ColumnName = '" + columnName + "'").Length > 0)
            {
                //SI LA COLUMNA NO ES NULA
                if (!(reader[columnName] is DBNull))
                {
                    return (T)reader[columnName];
                }
            }
            return null;
        }


        /// <summary>
        /// Funcion que devuelve el valor de la columna especificada, caso contrario retornara null
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <param name="columnName"></param>
        /// <returns>Valor de la columna o NULL</returns>
        public static Nullable<T> GetValueOrNull<T>(DataRow reader, string columnName) where T : struct
        {
            if (!(reader[columnName] is DBNull))
            {
                return (T)reader[columnName];
            }

            return null;
        }


        /// <summary>
        /// Funcion que devuelve el valor de la columna especificada
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <param name="columnName"></param>
        /// <returns>Valor de la columna</returns>
        public static T GetValue<T>(IDataReader reader, string columnName)
        {
            //SI EXISTE LA COLUMNA
            if (reader.GetSchemaTable().Select("ColumnName = '" + columnName + "'").Length > 0)
            {
                //SI LA COLUMNA NO ES NULA
                if (!(reader[columnName] is DBNull))
                {
                    return (T)reader[columnName];
                }
            }

            return default(T);
        }

        /// <summary>
        /// Funcion que devuelve el valor de la columna especificada
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <param name="columnName"></param>
        /// <returns>Valor de la columna</returns>
        public static T GetValue<T>(DataRow reader, string columnName)
        {

            if (!(reader[columnName] is DBNull))
            {
                return (T)reader[columnName];
            }

            return default(T);
        }

        /// <summary>
        /// Setea el valor de la columna con el dato especificado, caso contrario seteara la columna con null
        /// </summary>
        /// <param name="src_object"></param>
        /// <param name="target_property"></param>
        /// <returns></returns>
        public static object SetNullSafeValue(object value)
        {
            if (value == null)
            {
                value = System.DBNull.Value;
            }
            else
            {
                switch (value.GetType().Name)
                {
                    #region String

                    case "String":

                        if (string.IsNullOrEmpty(value.ToString()))
                            value = System.DBNull.Value;

                        break;

                    #endregion

                    #region Int32

                    case "Int32":

                        if ((int)value == int.MinValue)
                            value = System.DBNull.Value;

                        break;

                    #endregion

                    #region Datetime

                    case "DateTime":

                        if ((DateTime)value == DateTime.MinValue)
                            value = System.DBNull.Value;

                        break;

                    #endregion

                    #region Decimal

                    case "Decimal":

                        if ((decimal)value == decimal.MinValue)
                            value = System.DBNull.Value;

                        break;

                    #endregion
                }
            }

            return value;
        }

        /// <summary>
        /// Funcion que busca si existe una columna en un objeto DataReader
        /// </summary>
        /// <param name="reader">IDataReader </param>
        /// <param name="columnName">Nombre de la columna a buscar</param>
        /// <returns>True si existe la columna</returns>
        public static bool DataReaderHasColumn(IDataReader reader, string columnName)
        {
            return reader.GetSchemaTable().Select("ColumnName = '" + columnName + "'").Length > 0;
        }

        /// <summary>
        /// Funcion que busca si existe una columna en un objeto DataSet
        /// </summary>
        /// <param name="reader">IDataReader </param>
        /// <param name="columnName">Nombre de la columna a buscar</param>
        /// <returns>True si existe la columna</returns>
        public static bool DataReaderHasColumn(DataRow reader, string columnName)
        {
            return reader.Table.Columns.Contains(columnName);
        }
    }
}
