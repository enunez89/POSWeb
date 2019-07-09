using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace POSWeb.ControlExcepciones
{
    public class ControlExcepciones
    {
        public const string _systemName = "OMN";

        private static bool _initialized = false;

        static ControlExcepciones()
        {
            ControlExcepciones.InitializeLogger();
        }

        public static void InitializeLogger()
        {
            try
            {
                if (!_initialized)
                {
                    /*Enterprise Library 6.0*/
                    /*IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
                    LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
                    Logger.SetLogWriter(logWriterFactory.Create());*/
                    Logger.SetLogWriter(new LogWriterFactory().Create());
                    _initialized = true;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static string ManejoExcepciones(Exception exc, object trama, string idPantalla, string categoria = null)
        {
            try
            {
                if (exc is System.Threading.ThreadAbortException)
                {
                    return string.Empty;
                }

                Dictionary<string, object> DatosAgregados = new Dictionary<string, object>();
                if (idPantalla != null)
                    DatosAgregados.Add("Clase:", idPantalla);
                if (trama != null)
                    DatosAgregados.Add("Trama:", SerializarObjeto(trama));

                if (idPantalla == null)
                {
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();

                    int idx = 0;

                    string strClase = st.GetFrame(idx).GetMethod().ReflectedType.FullName;

                    while (idx < st.FrameCount)
                    {
                        if (strClase.Contains("ControlExcepciones"))
                        {
                            idx++;
                            strClase = st.GetFrame(idx).GetMethod().ReflectedType.FullName;
                        }
                        else
                        {
                            break;
                        }
                    }


                    string strNombreMetodo = st.GetFrame(idx).GetMethod().Name;

                    DatosAgregados.Add("Clase:", (strClase == null ? "" : strClase));
                    DatosAgregados.Add("Metodo:", (strNombreMetodo == null ? "" : strNombreMetodo));

                }
                if (HttpContext.Current != null)
                {
                    DatosAgregados.Add("Usuario:", HttpContext.Current.User.Identity.Name);
                    DatosAgregados.Add("IP:", HttpContext.Current.Request.UserHostName);
                    DatosAgregados.Add("URL:", HttpContext.Current.Request.Url.ToString());
                }

                string codError = (idPantalla != null ? idPantalla : _systemName) + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

                DatosAgregados.Add("Codigo Error:", codError);

                if (string.IsNullOrEmpty(categoria))
                {
                    Logger.Write(exc, DatosAgregados);
                }
                else
                {
                    Logger.Write(exc, categoria, DatosAgregados);
                }

                return codError;
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string Debug(string msg, object trama = null)
        {
            return Log(msg, trama, null, "Debug");
        }

        public static string Log(string exc, object trama = null, string idPantalla = null, string categoria = null)
        {
            try
            {

                bool escribirEnLog = true;

                try
                {
                    bool ret = true;

                    string claveLog = "Log" + categoria;

                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[claveLog]))
                    {
                        if (bool.TryParse(ConfigurationManager.AppSettings[claveLog], out ret))
                        {
                            escribirEnLog = ret;
                        }
                    }
                }
                catch
                {
                }

                if (escribirEnLog)
                {

                    Dictionary<string, object> DatosAgregados = new Dictionary<string, object>();
                    if (idPantalla != null)
                        DatosAgregados.Add("Clase:", idPantalla);
                    if (trama != null)
                        DatosAgregados.Add("Trama:", SerializarObjeto(trama));

                    if (idPantalla == null)
                    {
                        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();

                        int idx = 0;

                        string strClase = st.GetFrame(idx).GetMethod().ReflectedType.FullName;

                        while (idx < st.FrameCount)
                        {
                            if (strClase.Contains("ControlExcepciones"))
                            {
                                idx++;
                                strClase = st.GetFrame(idx).GetMethod().ReflectedType.FullName;
                            }
                            else
                            {
                                break;
                            }
                        }


                        string strNombreMetodo = st.GetFrame(idx).GetMethod().Name;

                        DatosAgregados.Add("Clase:", (strClase == null ? "" : strClase));
                        DatosAgregados.Add("Metodo:", (strNombreMetodo == null ? "" : strNombreMetodo));

                    }
                    if (HttpContext.Current != null)
                    {
                        DatosAgregados.Add("Usuario:", HttpContext.Current.User.Identity.Name);
                        DatosAgregados.Add("IP:", HttpContext.Current.Request.UserHostName);
                        DatosAgregados.Add("URL:", HttpContext.Current.Request.Url.ToString());
                    }


                    string codError = (idPantalla != null ? idPantalla : _systemName) + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

                    DatosAgregados.Add("Codigo Error:", codError);

                    if (string.IsNullOrEmpty(categoria))
                    {
                        Logger.Write(exc, DatosAgregados);
                    }
                    else
                    {
                        Logger.Write(exc, categoria, DatosAgregados);
                    }

                    return codError;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void ManejoExcepciones(Exception exc)
        {
            ManejoExcepciones(exc, null, null);
        }

        public static void ManejoExcepcionesConThrow(Exception exc, object trama, string idPantalla)
        {
            try
            {
                string codError = ManejoExcepciones(exc, trama, idPantalla);

                throw (new Exception(exc.Message + "-Error anterior: " + codError));
            }
            catch { throw; }

        }

        public static void ManejoExcepcionesThrowSVC(Exception exc)
        {
            try
            {
                string codError = ManejoExcepciones(exc, null, null);

                throw (new FaultException(exc.Message + "-Error anterior: " + codError));
            }
            catch { throw; }

        }

        public static void ManejoExcepcionesThrowSVC(Exception exc, object trama)
        {
            try
            {
                string codError = ManejoExcepciones(exc, trama, null);

                throw (new FaultException(exc.Message + "-Error anterior: " + codError));
            }
            catch { throw; }

        }

        public static void ManejoExcepcionesThrowSVC(Exception exc, object trama, string idPantalla)
        {
            try
            {
                string codError = ManejoExcepciones(exc, trama, idPantalla);

                throw (new FaultException(exc.Message + "-Error anterior: " + codError));
            }
            catch { throw; }

        }

        public static void ManejoExcepcionesConThrow(Exception exc)
        {
            try
            {
                ManejoExcepcionesConThrow(exc, null, null);
            }
            catch { throw; }
        }

        public static void ManejoExcepciones(string exc)
        {
            ManejoExcepciones(new Exception(exc), null, null);
        }

        public static void ManejoExcepciones(string exc, string categoria)
        {
            ManejoExcepciones(new Exception(exc), null, null, categoria);
        }

        public static void ManejoExcepciones(string exc, object trama, string categoria)
        {
            ManejoExcepciones(new Exception(exc), trama, null, categoria);
        }

        public static void ManejoExcepcionesConThrow(string exc)
        {
            try
            {
                ManejoExcepcionesConThrow(new Exception(exc), null, null);
            }
            catch { throw; }
        }

        public static void ManejoExcepciones(string exc, object trama)
        {
            ManejoExcepciones(new Exception(exc), trama, null);
        }

        public static void ManejoExcepcionesConThrow(string exc, object trama)
        {
            try
            {
                ManejoExcepcionesConThrow(new Exception(exc), trama, null);
            }
            catch { throw; }
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

                    if (ret.Length > (15 * 1024)) // 15 MB, no serializar objetos mas grandes que.
                        ret = string.Empty;
                }
            }
            catch
            {
            }

            return ret;
        }

        public static string ManejoExcepciones(Exception exc, object trama)
        {

            return ManejoExcepciones(exc, trama, null);
        }

        public static void ManejoExcepcionesConThrow(Exception exc, object trama)
        {
            try
            {
                ManejoExcepcionesConThrow(exc, trama, null);
            }
            catch { throw; }
        }

        private static string ObtenerCodigoErrorAnterior(string codExcepcion, Exception exc)
        {
            try
            {
                string lblErrorAnterior = "Error anterior:";
                // Para asignar el codigo de error, que proviene de la capa de servicios, error anterior
                if (exc != null && exc.Message != null)
                {
                    if (exc.Message.Contains(lblErrorAnterior))
                    {
                        string tmpCodError = exc.Message;
                        int idx = tmpCodError.IndexOf(lblErrorAnterior, 0);
                        if (idx > 0)
                        {
                            int startposition = idx + lblErrorAnterior.Length + 1;
                            int idx2 = tmpCodError.IndexOf(" ", startposition);
                            if (idx2 > startposition)
                                tmpCodError = tmpCodError.Substring(startposition, idx2 - startposition);
                        }

                        return (string.IsNullOrEmpty(tmpCodError) ? codExcepcion : tmpCodError.Replace("\n", ""));
                    }
                }
                // Para asignar el codigo de error, que proviene de la capa de servicios, error anterior
            }
            catch
            {
            }
            return codExcepcion;
        }

        /// <summary>
        /// Metodo que muestra el  cuadro modal del mensaje de error, son en todos los metodos de la aplicacion 
        /// cuando se cae en el metodo de la capa de presentación
        /// </summary>
        /// <param name="exc">Es el error de la pagina</param>
        /// /// <param name="trama"></param>
        /// <param name="idUsuario">Codigo del usuario que esta utilizando la aplicacion</param>
        /// <param name="idPantalla">Identificador de la pantalla</param>
        /// <param name="page">Pagina en la cual se produjo el error</param>
        /// 
        public static string ManejoExcepcionesWeb(Exception exc, string trama, string idUsuario, string idPantalla, Page page)
        {
            try
            {

                if (exc is System.Threading.ThreadAbortException)
                {
                    return string.Empty;
                }

                Dictionary<string, object> DatosAgregados = new Dictionary<string, object>();
                if (idUsuario != null)
                {
                    DatosAgregados.Add("Cliente:", idUsuario);
                }
                else
                {
                    if (page != null)
                    {
                        DatosAgregados.Add("Cliente:", page.User.Identity.Name);
                    }
                }

                if (trama != null)
                    DatosAgregados.Add("Trama:", trama);

                if (idPantalla == null && page != null)
                    idPantalla = _systemName + "-" + page.ToString();

                if (idPantalla == null)
                {
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();

                    int idx = 0;

                    string strClase = st.GetFrame(idx).GetMethod().ReflectedType.FullName;

                    while (idx < st.FrameCount)
                    {
                        if (strClase.Contains("ControlExcepciones"))
                        {
                            idx++;
                            strClase = st.GetFrame(idx).GetMethod().ReflectedType.FullName;
                        }
                        else
                        {
                            break;
                        }
                    }


                    string strNombreMetodo = st.GetFrame(idx).GetMethod().Name;

                    DatosAgregados.Add("Clase:", (strClase == null ? "" : strClase));
                    DatosAgregados.Add("Metodo:", (strNombreMetodo == null ? "" : strNombreMetodo));

                }

                if (HttpContext.Current != null)
                {
                    DatosAgregados.Add("Usuario:", HttpContext.Current.User.Identity.Name);
                    DatosAgregados.Add("IP:", HttpContext.Current.Request.UserHostName);
                    DatosAgregados.Add("URL:", HttpContext.Current.Request.Url.ToString());
                }

                string codExcepcion = (idPantalla != null ? idPantalla : _systemName) + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

                // Obtiene el codigo de error provocado desde la capa de servicios, en caso de existir
                codExcepcion = ObtenerCodigoErrorAnterior(codExcepcion, exc);

                DatosAgregados.Add("Codigo Error:", codExcepcion);

                if (DatosAgregados.FirstOrDefault(i => i.Key == "URL").ToString().Trim().Length == 0)
                    DatosAgregados.Add("URL:", HttpContext.Current.Request.Url.ToString());



                Logger.Write(exc, DatosAgregados);



                HttpContext context = HttpContext.Current;
                if (context.Session != null)
                {
                    context.Session["DatosAgregados"] = DatosAgregados;
                    context.Session["exception"] = exc;
                    context.Session["CodError"] = codExcepcion;
                }
                string vmensaje = "Estimad@ usuari@: <br><br>En este momento contamos con problemas técnicos en nuestros sistemas, <br> agradecemos realizar el reporte respectivo mediante la opción de “Reportar”.<br><br>Rogamos nos disculpe la molestia ocasionada.<br><br><div style=\"text-align:center\"><b>Instituto Nacional de Seguros (INS)</b></div><br><br>";

                if (page != null)
                {
                    Label lblMensajeCon = (Label)page.Master.FindControl("lblMensajeCon");
                    if (exc is TimeoutException)
                    {
                        lblMensajeCon.Text = vmensaje + "El tiempo de la solicitud se excedió";
                    }
                    else if (exc is NullReferenceException)
                    {
                        lblMensajeCon.Text = vmensaje + "Se produjo un error en nuestro sistema";
                    }
                    else if (exc is System.Web.Services.Protocols.SoapException)
                    {
                        lblMensajeCon.Text = vmensaje + "Error en telecomunicaciones";
                    }
                    else if (exc is System.ServiceModel.CommunicationException)
                    {
                        lblMensajeCon.Text = vmensaje + "Error en telecomunicaciones";
                    }
                    else if (exc is System.Data.Common.DbException)
                    {
                        lblMensajeCon.Text = vmensaje + "Nos encontramos respaldando nuestra informacion";
                    }
                    else
                    {
                        lblMensajeCon.Text = vmensaje;
                    }
                    Label lblCodError = (Label)page.Master.FindControl("lblCodError");
                    if (lblCodError != null)
                    {
                        lblCodError.Text = "C&oacute;digo Error:" + codExcepcion;
                    }


                    //
                    if (!page.IsPostBack)
                    {
                        page.Response.Redirect("~/frmMensajeError.aspx?CodError=" + codExcepcion.Replace("\n", ""), false); return string.Empty;
                    }
                    else
                    {
                        //string myScript = string.Format("launchModal();");
                        ////page.Master.Page.ClientScript.RegisterStartupScript(page.GetType(), "key", myScript, true);


                        //ScriptManager sManager = ScriptManager.GetCurrent(page);
                        //if (sManager != null && sManager.IsInAsyncPostBack)
                        //{
                        //    //if a MS AJAX request, use the Scriptmanager class
                        //    ScriptManager.RegisterStartupScript(page, page.GetType(), "key", myScript, true);
                        //}
                        //else
                        //{
                        //    page.Master.Page.ClientScript.RegisterStartupScript(page.GetType(), "key", myScript, true);
                        //}
                    }

                }
            }
            catch
            {

            }

            return string.Empty;
        }

        public static string ManejoExcepcionesWeb(Exception exc, Page page)
        {
            return ManejoExcepcionesWeb(exc, null, null, null, page);
        }

        /// <summary>
        /// Metodo llamado desde el global.asax, te muestra la forma de error.
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public static string ManejoExcepcionesWebGlobalASAX(Exception exc)
        {
            try
            {
                Dictionary<string, object> DatosAgregados = new Dictionary<string, object>();
                string codError = _systemName + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                // Obtiene el codigo de error provocado desde la capa de servicios, en caso de existir
                codError = ObtenerCodigoErrorAnterior(codError, exc);

                DatosAgregados.Add("Codigo Error:", codError);
                DatosAgregados.Add("URL:", HttpContext.Current.Request.Url.ToString());

                Logger.Write(exc, DatosAgregados);
                //Logger.Write(exc, "EnvioCorreo", DatosAgregados);
                return codError;
            }
            catch
            {
                return null;
            }
        }
    }
}
