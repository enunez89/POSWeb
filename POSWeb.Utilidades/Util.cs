using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Configuration;

namespace POSWeb.Utilidades
{
    public class Util
    {
        public const string defaultDateFormat = "dd/MM/yyyy";
        public const string defaultTimeFormat = "HH:mm";
        public const string defaultTimeFormat24Hrs = "HH:mm:ss";
        public const string defaultTimeFormatAMPM = "hh:mm:ss tt";
        public const string defaultTimeFormatMS = "HH:mm:ss.fff";
        public const string minHour = "00:00:00.000";
        public const string maxHour = "23:59:59.999";
        /*
         * Para cultura de decimales con Punto   100.15   usar en-US
         * Para cultura de decimales con Comma   100,15   usar es-CR
         */
        public const string cultureDecimalPoint = "en-US";
        //public const string cultureDecimalPoint = "es-CR";

        public static string GetDefaultDateFormat()
        {
            return ConfigurationManager.AppSettings["DefaultDateFormat"] ?? defaultDateFormat;
        }

        /// <summary>
        /// Obtiene la descripción de un Enum
        /// </summary>
        /// <param name="value">Enum al que se le va a obtener la descripción.</param>
        /// <returns></returns>
        public static string ObtenerDescripcionEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Valida formato numérico
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static bool IsNumero(String numero)
        {
            try
            {
                Int32 num;
                Boolean res = Int32.TryParse(numero, out num);
                if (res == false)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida formato decimal
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static bool IsDecimal(String numero)
        {
            try
            {
                Double num;
                Boolean res = Double.TryParse(numero, out num);
                if (res == false)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida formato de fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static bool IsFecha(String fecha)
        {
            try
            {
                DateTime fec;
                Boolean res = DateTime.TryParseExact(fecha, defaultDateFormat, null, DateTimeStyles.AssumeLocal, out fec);
                if (res == false)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida formato de fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static bool IsHora(String hora)
        {
            try
            {
                DateTime h;
                Boolean res = DateTime.TryParseExact(hora, defaultTimeFormat, null, DateTimeStyles.AssumeLocal, out h);
                if (res == false)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable ConvertXML2DS(string xml)
        {
            DataTable ret = null;
            if (!String.IsNullOrEmpty(xml))
            {

                XmlDocument oXml = new XmlDocument();
                XmlNodeReader oNodoXml;

                oXml.LoadXml(xml);
                oNodoXml = new XmlNodeReader(oXml);
                DataSet dsTemporal = new DataSet();
                dsTemporal.ReadXml(oNodoXml);
                ret = new DataTable();

                ret.Columns.Add("Codigo");
                ret.Columns.Add("Valor");

                foreach (DataColumn col1 in dsTemporal.Tables[0].Columns)
                {
                    if (col1 != null && col1.ColumnName != "ExtensionData")
                    {
                        DataRow dr = ret.NewRow();
                        dr["Codigo"] = dsTemporal.Tables[0].Columns[col1.ToString()];
                        dr["Valor"] = dsTemporal.Tables[0].Rows[0][col1].ToString();
                        ret.Rows.Add(dr);
                    }
                }
            }
            return ret;
        }

        public static string SerializarObjeto(object trama)
        {
            string ret = string.Empty;

            try
            {
                if (trama != null)
                {

                    XmlSerializer xs = new XmlSerializer(trama.GetType());
                    StringWriter stringWriter = new StringWriter();

                    xs.Serialize(stringWriter, trama);

                    ret = stringWriter.ToString();

                    //if (ret.Length > (15 * 1024)) // 15 MB, no serializar objetos mas grandes que.
                    //    ret = string.Empty;
                }
            }
            catch
            {
            }

            return ret;
        }

        public static object DeserializarObjeto(string trama, Type tipoDato)
        {
            object ret = null;

            try
            {
                if (trama != null)
                {

                    XmlSerializer serializer = new XmlSerializer(tipoDato);

                    using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new StringReader(trama)))
                    {
                        ret = serializer.Deserialize(reader);
                    }
                }
            }
            catch
            {
            }

            return ret;
        }

        public static DateTime ConvertToDate(string dateInput)
        {
            DateTime ret = DateTime.MinValue;

            if (DateTime.TryParseExact(dateInput, defaultDateFormat, null, DateTimeStyles.AssumeLocal, out ret))
            {

            }

            return ret;
        }

        public static DateTime ConvertToTime(string dateInput)
        {
            DateTime ret = DateTime.MinValue;

            if (DateTime.TryParseExact(dateInput, defaultTimeFormat, null, DateTimeStyles.AssumeLocal, out ret))
            {

            }


            return ret;
        }

        public static string FormatDateTime(string date, string time)
        {
            return string.Format("{0}/{1}/{2} {3}", date.Split('/')[1], date.Split('/')[0], date.Split('/')[2], time);
        }

        public static DateTime? ConvertToDateOrDateTime(string strDateTime)
        {
            string[] dateTime = strDateTime.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (dateTime.Count() == 1)
                return ConvertToDateOrDateTime(dateTime[0], "00:00:00");
            else if (dateTime.Count() != 2 && dateTime.Count() != 3)
                return null;

            return ConvertToDateOrDateTime(dateTime[0], dateTime[1]);
        }

        public static DateTime? ConvertToDateOrDateTime(string strDate, string strTime)
        {
            string pattern = @"^(?<dia>\d{2})\/(?<mes>\d{2})/(?<agno>\d{4})( (?<horas>\d{2}):(?<minutos>\d{2}):(?<segundos>\d{2})(\.(?<msegs>\d{3}))?)?$";

            DateTime? dateTime = null;
            string input = strDate + (!string.IsNullOrEmpty(strTime) ? " " + strTime : "");
            MatchCollection matches = Regex.Matches(input, pattern);
            if (matches.Count > 0)
            {
                Match match = matches[0];
                int agno, mes, dia;
                agno = int.Parse(match.Groups["agno"].Value);
                mes = int.Parse(match.Groups["mes"].Value);
                dia = int.Parse(match.Groups["dia"].Value);

                if (match.Groups.Count == 10)
                {
                    if (string.IsNullOrEmpty(match.Groups["horas"].Value))
                    {
                        try
                        {
                            dateTime = new DateTime(agno, mes, dia);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format("Excepción al convertir dato '{0}' en fecha con formato '{1}'.", input, defaultDateFormat), e);
                        }
                    }
                    else
                    {
                        int horas, minutos, segundos;
                        horas = int.Parse(match.Groups["horas"].Value);
                        minutos = int.Parse(match.Groups["minutos"].Value);
                        segundos = int.Parse(match.Groups["segundos"].Value);
                        if (string.IsNullOrEmpty(match.Groups["msegs"].Value))
                        {
                            try
                            {
                                dateTime = new DateTime(agno, mes, dia, horas, minutos, segundos);
                                //dateTime = new DateTime(int.Parse(matches[0].Result("$3")), int.Parse(matches[0].Result("$2")), int.Parse(matches[0].Result("$1")), int.Parse(matches[0].Result("$4")), int.Parse(matches[0].Result("$5")), 0);
                            }
                            catch (Exception e)
                            {
                                throw new Exception(string.Format("Excepción al convertir dato '{0}' en fecha con formato '{1} {2}'.", input, defaultDateFormat, defaultTimeFormat), e);
                            }
                        }
                        else
                        {
                            try
                            {
                                int milisegundos = int.Parse(match.Groups["msegs"].Value);
                                dateTime = new DateTime(agno, mes, dia, horas, minutos, segundos, milisegundos);
                                //dateTime = new DateTime(int.Parse(matches[0].Result("agno")), int.Parse(matches[0].Result("$2")), int.Parse(matches[0].Result("$1")), int.Parse(matches[0].Result("$4")), int.Parse(matches[0].Result("$5")), int.Parse(matches[0].Result("$6")));
                            }
                            catch (Exception e)
                            {
                                throw new Exception(string.Format("Excepción al convertir dato '{0}' en fecha con formato '{1} {2}'.", input, defaultDateFormat, defaultTimeFormatMS), e);
                            }
                        }
                    }
                }
            }

            return dateTime;
        }

        public static int ObtenerLongitudCampoBD(string datoLongitud, out int longitudDecimales)
        {
            longitudDecimales = 0;
            if (IsNullOrEmpty(datoLongitud))
                return 0;

            int longitud = 0;
            string patron = @"^(?<longitud>\d+)$|^\((?<longitud>\d+),(?<longitudDecimales>\d+)\)$";

            MatchCollection matches = Regex.Matches(datoLongitud, patron);
            if (matches.Count > 0)
            {
                try
                {
                    Match match = matches[0];
                    longitud = int.Parse(match.Groups["longitud"].Value);
                    if (match.Groups.Count == 3 && !string.IsNullOrEmpty(match.Groups["longitudDecimales"].Value))
                    {
                        longitudDecimales = int.Parse(match.Groups["longitudDecimales"].Value);
                        longitud = longitud - longitudDecimales;
                    }
                }
                catch
                {

                }
            }
            return longitud;
        }

        public static DateTime ConvertToDateTime(string strDateTime, string format = defaultDateFormat)
        {
            return DateTime.ParseExact(strDateTime, format, CultureInfo.InvariantCulture);
        }

        public static string ConvertoDateToString(DateTime dateInput)
        {
            string ret = string.Empty;

            try
            {
                ret = dateInput.ToString(defaultDateFormat, null);
            }
            catch
            {

            }

            return ret;
        }

        public static string ConvertTimeToString(DateTime timeInput)
        {
            string ret = string.Empty;

            try
            {
                ret = timeInput.ToString(defaultTimeFormat, null);
            }
            catch
            {

            }

            return ret;
        }

        public static string ToCapital(string stringInput)
        {
            StringBuilder sb = new StringBuilder();
            bool whiteSpaceBefore = true;

            foreach (char ch in stringInput)
            {
                char chThis = ch;

                if (Char.IsWhiteSpace(ch))
                    whiteSpaceBefore = true;
                else
                {
                    if (Char.IsLetter(ch) && whiteSpaceBefore)
                        chThis = Char.ToUpper(ch);
                    else
                        chThis = Char.ToLower(ch);

                    whiteSpaceBefore = false;
                }

                sb.Append(chThis);
            }

            return sb.ToString();
        }

        public static decimal ConvertToDecimal(string texto)
        {
            decimal ret = 0;
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;

            ret = Decimal.Parse(texto, style, new CultureInfo(cultureDecimalPoint));

            return ret;
        }

        public static string ConvertToToStringDecimal(decimal value)
        {
            string ret = "0.00";

            ret = string.Format("{0:##.##}", value);

            return ret;
        }

        public static string ImprimirSiglas(string texto)
        {
            if (IsNullOrEmpty(texto)) return "";
            string patron = @"((?<=^|\s)([A-Z]))";
            Regex expreg = new Regex(patron, RegexOptions.IgnoreCase);
            texto = texto.Trim();
            MatchCollection matchCol = expreg.Matches(texto);
            string siglas = "";
            foreach (Match m in matchCol)
            {
                siglas += m.Result("$1");
            }

            return siglas;
        }

        public static string ObtenerSiglas(string texto)
        {
            if (IsNullOrEmpty(texto)) return string.Empty;
            var patron = @"((?<=^|\s)([A-Z]))";
            texto = texto.Trim();
            string siglas = string.Join(string.Empty, Regex.Matches(texto, patron).OfType<Match>().Select(x => x.Value.ToUpper()).ToArray<string>());
            if (IsNullOrEmpty(siglas) && !char.IsNumber(texto, 0))
                return ObtenerSiglas(ToCapital(texto));
            return siglas;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> list) where T : class
        {
            try
            {
                DataTable table = CreateDataTable<T>();
                Type objType = typeof(T);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);

                foreach (T item in list)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor property in properties)
                    {
                        if (!CanUseType(property.PropertyType)) continue;
                        row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                    }

                    table.Rows.Add(row);
                }
                return table;
            }
            catch (DataException)
            {
                return null;
            }
        }

        private static DataTable CreateDataTable<T>() where T : class
        {
            Type objType = typeof(T);
            DataTable table = new DataTable(objType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);
            foreach (PropertyDescriptor property in properties)
            {
                Type propertyType = property.PropertyType;

                if (!CanUseType(propertyType)) continue;

                //nullables must use underlying types
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                //enums also need special treatment
                if (propertyType.IsEnum)
                    propertyType = Enum.GetUnderlyingType(propertyType);
                table.Columns.Add(property.Name, propertyType);
            }
            return table;
        }

        private static bool CanUseType(Type propertyType)
        {
            //only strings and value types
            if (propertyType.IsArray) return false;
            if (!propertyType.IsValueType && propertyType != typeof(string)) return false;
            return true;
        }

        public static bool IsValidXML(String xmlString)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlString))
                {
                    return false;
                }

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xmlString);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static bool EntidadValida(object entidad, ref List<string> mensajes)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext valid = new ValidationContext(entidad);

            //Validacion del contexto de la poliza
            bool isvalid = Validator.TryValidateObject(entidad, valid, validationResults, true);

            foreach (ValidationResult valResult in validationResults)
            {
                mensajes.Add(valResult.ErrorMessage);
            }

            return isvalid;
        }

        public static void BindDropDown<T>(DropDownList ddl, List<T> items, string dataTextField, string dataValueField, Boolean itemInicial = false, string noSelectionText = "")
        {
            if (items == null)
            {
                items = new List<T>();
            }

            if (itemInicial)
            {
                dynamic entidad = Activator.CreateInstance(typeof(T));
                SetEntidadPropiedadValue(entidad, -1, dataValueField);
                SetEntidadPropiedadValue(entidad, string.IsNullOrEmpty(noSelectionText) ? "[Seleccione un valor]" : noSelectionText, dataTextField);
                items.Insert(0, entidad);
            }

            ddl.DataSource = items;
            ddl.DataValueField = dataValueField;
            ddl.DataTextField = dataTextField;
            ddl.DataBind();
        }

        public static void SetEntidadPropiedadValue<T, T2>(T pEntidad, T2 pValorPropiedad, String pNombrePropiedad)
        {
            PropertyInfo propiedad = pEntidad.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x => x.Name == pNombrePropiedad);

            if (propiedad.PropertyType == pValorPropiedad.GetType())
            {
                propiedad.SetValue(pEntidad, pValorPropiedad);
            }
            else
            {
                var targetType = propiedad.PropertyType.IsGenericType && propiedad.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))
                    ? Nullable.GetUnderlyingType(propiedad.PropertyType)
                    : propiedad.PropertyType;

                var convertedValue = Convert.ChangeType(pValorPropiedad, targetType);

                propiedad.SetValue(pEntidad, convertedValue, null);
            }
        }

        public static String ObtenerNombrePropiedad<T, TReturn>(Expression<Func<T, TReturn>> exp)
        {
            var body = exp.Body as MemberExpression;
            return body.Member.Name;
        }

        public static DescriptionAttribute GetEnumDescriptionAttribute<T>(T value) where T : struct
        {
            // El tipo del enum, el cual será reutilizado.
            Type type = typeof(T);

            // Si T no es un enum, lance una excepción y salga.
            if (!type.IsEnum)
                throw new InvalidOperationException(
                    "The type parameter T must be an enum type.");

            // Si el valor no está definido en el enum lance una excepción.
            if (!Enum.IsDefined(type, value))
                throw new InvalidEnumArgumentException(
                    "value", Convert.ToInt32(value), type);

            // Obtener el campo estático para el valor.
            FieldInfo fi = type.GetField(value.ToString(),
                BindingFlags.Static | BindingFlags.Public);

            // Obtener el atributo Description, si lo hay.
            return fi.GetCustomAttributes(typeof(DescriptionAttribute), true).
                Cast<DescriptionAttribute>().SingleOrDefault();
        }

        public static string GetDescriptionFromEnum<T>(T value) where T : struct
        {
            return GetEnumDescriptionAttribute<T>(value).Description;
        }

        public static bool IsNullOrEmpty(string value)
        {
            return (value == null || value.Trim() == string.Empty);
        }

        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        public static IEnumerable<T> GetEnumValues<T>()
        {
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new ArgumentException("T debe ser de tipo System.Enum");
            }

            //Enum.GetValues(typeof(T)).OfType<T>().ToList();
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

    }
}
