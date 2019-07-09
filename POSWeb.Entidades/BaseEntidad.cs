using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.Configuration;

namespace POSWeb.Entidades
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntidad
    {
        #region Constantes

        public const string usrCreacionProperty = "Usr_Creacion";
        public const string usrModificacionProperty = "Usr_Modificacion";
        public const string fecCreacionProperty = "Fecha_Creacion";
        public const string fecModificacionProperty = "Fecha_Modificacion";
        public const string codErrorProperty = "CodError";
        public const string mensajeProperty = "Mensaje";
        public const string codigoCanalProperty = "CodigoCanal";
        public const string usrTokenAuthenticateProperty = "UsrtokensAuthenticate";
        public const string xmlDataProperty = "XML_Data";
        public const string idEntidadProperty = "Id_Entidad";
        public const string descEstadoProperty = "Desc_Estado";
        public const string nombreControladorProperty = "Nombre_Controlador";

        #endregion

        public BaseEntidad()
        {
            try
            {
                if(HttpContext.Current != null)
                {
                    UsrtokensAuthenticate = (string)HttpContext.Current.Session["UsrtokensAuthenticate"];
                    IP = (string)HttpContext.Current.Session["IP"];
                    NombreControlador = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                    IdEntidad = Convert.ToInt64(ConfigurationManager.AppSettings["Entidad"]);
                }
            }
            catch
            { }
        }

        protected void LoadProperty(IDataReader dataReader, string alias = "")
        {
            if(HelperValues.DataReaderHasColumn(dataReader, alias + usrCreacionProperty))
                UsrModificacion = HelperValues.GetValue<string>(dataReader, alias + usrModificacionProperty);
            if(HelperValues.DataReaderHasColumn(dataReader, alias + usrModificacionProperty))
                UsrCreacion = HelperValues.GetValue<string>(dataReader, alias + usrCreacionProperty);
            if(HelperValues.DataReaderHasColumn(dataReader, alias + fecCreacionProperty))
                FecCreacion = HelperValues.GetValue<DateTime?>(dataReader, alias + fecCreacionProperty);
            if(HelperValues.DataReaderHasColumn(dataReader, alias + fecModificacionProperty))
                FecModificacion = HelperValues.GetValue<DateTime?>(dataReader, alias + fecModificacionProperty);
            if(HelperValues.DataReaderHasColumn(dataReader, alias + descEstadoProperty))
                DescEstado = HelperValues.GetValue<string>(dataReader, alias + descEstadoProperty);
        }

        protected String usrCreacion;
        protected String codigoCanal;
        protected String usrModificacion;
        protected DateTime? fecCreacion;
        protected DateTime? fecModificacion;
        protected String usrLoginConectado;
        protected String passLoginConectado;
        protected String sourceLoginConectado;
        protected String ip;
        protected String Usrtokensauthenticate;

        protected String codError;
        protected String mensaje;

       

        [DataMember]
        [DisplayName("Creado por")]
        public String UsrCreacion
        {
            get { return usrCreacion; }
            set { usrCreacion = value; }
        }

        [DataMember]
        [DisplayName("Modificado por")]
        public String UsrModificacion
        {
            get { return usrModificacion; }
            set { usrModificacion = value; }
        }

        [DataMember]
        public String UsrLoginConectado
        {
            get { return usrLoginConectado; }
            set { usrLoginConectado = value; }
        }

        [DataMember]
        public String PassLoginConectado
        {
            get { return passLoginConectado; }
            set { passLoginConectado = value; }
        }

        [DataMember]
        public String SourceLoginConectado
        {
            get { return sourceLoginConectado; }
            set { sourceLoginConectado = value; }
        }

        [DataMember]
        public string UsrtokensAuthenticate { get; set; }

        [DataMember]
        public string IP { get; set; }

        [DataMember]
        [DisplayName("Fecha creado")]
        public DateTime? FecCreacion
        {
            get { return fecCreacion; }
            set { fecCreacion = value; }
        }

        [DataMember]
        [DisplayName("Fecha modificado")]
        public DateTime? FecModificacion
        {
            get { return fecModificacion; }
            set { fecModificacion = value; }
        }     


        [DataMember]
        public String CodError
        {
            get { return codError; }
            set { codError = value; }
        }

        [DataMember]
        public String Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        [DataMember]
        public String CodigoCanal
        {
            get { return codigoCanal; }
            set { codigoCanal = value; }
        }

        [DataMember]
        public string XMLData { get; set; }

        [DataMember]
        public long IdEntidad { get; set; }

        [DataMember]
        [Display(Name = "Estado")]
        public string DescEstado { get; set; }

        [DataMember]
        public string NombreControlador { get; set; }


        public int? ConvertirConsecutivo(decimal? obj)
        {
            int? ret = null;
            if (obj != null)
            {
                ret = Convert.ToInt32(obj);
            }

            return ret;
        }


    }
}
