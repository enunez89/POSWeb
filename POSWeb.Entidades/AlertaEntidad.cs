using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace POSWeb.Entidades
{
    /// <summary>
    /// Requerimiento:       POSWeb
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Entidad AlertaEntidad
    /// Ultima modificacion: 01/12/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class AlertaEntidad : BaseEntidad
    {
        #region Constantes
        
           public const string idProperty = "Id";
           public const string idAlertaProperty = "Id_Alerta";
           public const string nombreAlertaProperty = "NombreAlerta";
           public const string codigoAlertaProperty = "CodigoAlerta";
           public const string tituloProperty = "Titulo";
           public const string bodyProperty = "Body";
           public const string idCuentaProperty = "Id_Cuenta";
           public const string nombreCuentaProperty = "NombreCuenta";
           public const string activaProperty = "Activa";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="Id")]
        [Required(ErrorMessage = "El dato Id es requerido")]
        public long Id { get; set; }

        [DataMember]
        [Display(Name ="IdAlerta")]
        [Required(ErrorMessage = "El dato IdAlerta es requerido")]
        public long IdAlerta { get; set; }

        [DataMember]
        [Display(Name = "Alerta")]
        public string NombreAlerta { get; set; }

        [DataMember]
        [Display(Name ="Titulo")]
        [StringLength(200, ErrorMessage = "Titulo no puede contener más de 200 caracteres.")]
        [Required(ErrorMessage = "El dato Titulo es requerido")]
        public string Titulo { get; set; }

        [AllowHtml]
        [DataMember]
        [Display(Name ="Contenido")]
        [UIHint("tinymce_jquery_full")]
        public string HtmlContent { get; set; }

        [DataMember]
        [Display(Name ="IdCuenta")]
        [Required(ErrorMessage = "El dato IdCuenta es requerido")]
        public long IdCuenta { get; set; }

        [DataMember]
        [Display(Name = "Cuenta Correo")]
        public string NombreCuenta { get; set; }

        [DataMember]
        [Display(Name ="Activa")]
        public bool Activa { get; set; }

        [DataMember]
        [Display(Name = "Código Alerta")]
        public string CodigoAlerta { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AlertaEntidad() { }

        /// <summary>
        /// Constructor para la clase AlertaEntidad
        /// </summary>
        /// <param name="dataReader"></param>
        public AlertaEntidad(IDataReader dataReader, string alias = "")
        {
            
            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.idProperty))
            {
                this.Id = HelperValues.GetValue<long>(dataReader, alias + AlertaEntidad.idProperty);
            }
            //idAlerta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.idAlertaProperty))
            {
                this.IdAlerta = HelperValues.GetValue<long>(dataReader, alias + AlertaEntidad.idAlertaProperty);
            }
            //NombreAlerta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.nombreAlertaProperty))
            {
                this.NombreAlerta = HelperValues.GetValue<string>(dataReader, alias + AlertaEntidad.nombreAlertaProperty);
            }
            //titulo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.tituloProperty))
            {
                this.Titulo = HelperValues.GetValue<string>(dataReader, alias + AlertaEntidad.tituloProperty);
            }
            //body
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.bodyProperty))
            {
                this.HtmlContent = HelperValues.GetValue<string>(dataReader, alias + AlertaEntidad.bodyProperty);
            }
            //idCuenta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.idCuentaProperty))
            {
                this.IdCuenta = HelperValues.GetValue<long>(dataReader, alias + AlertaEntidad.idCuentaProperty);
            }
            //NombreCuenta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.nombreCuentaProperty))
            {
                this.NombreCuenta = HelperValues.GetValue<string>(dataReader, alias + AlertaEntidad.nombreCuentaProperty);
            }
            //activa
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaEntidad.activaProperty))
            {
                this.Activa = HelperValues.GetValue<bool>(dataReader, alias + AlertaEntidad.activaProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasAlertaEntidad
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaAlertaEntidad : Paginacion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<AlertaEntidad> ListaAlertaEntidad { get; set; }
		}
		
		[DataContract]
		public class RespuestaAlertaEntidad
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public AlertaEntidad AlertaEntidad { get; set; }
		}	
	}
#endregion RespuestasAlertaEntidad


