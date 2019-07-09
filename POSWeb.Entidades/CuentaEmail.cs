using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    /// <summary>
    /// Requerimiento:       POSWeb
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Entidad CuentaEmail
    /// Ultima modificacion: 25/11/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class CuentaEmail : BaseEntidad
    {
        #region Constantes
        
           public const string idProperty = "Id";
           public const string correoElectronicoProperty = "CorreoElectronico";
           public const string aliasProperty = "Alias";
           public const string servidorProperty = "Servidor";
           public const string puertoProperty = "Puerto";
           public const string usuarioProperty = "Usuario";
           public const string contrasenaProperty = "Contrasena";
           public const string sslProperty = "SSL";
           public const string credencialesDefectoProperty = "CredencialesDefecto";
           public const string cuentaDefectoProperty = "CuentaDefecto";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="Id")]
        [Required(ErrorMessage = "El dato Id es requerido")]
        public Int64 Id { get; set; }

        [DataMember]
        [Display(Name ="CorreoElectronico")]
        [StringLength(50, ErrorMessage = "CorreoElectronico no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        public string CorreoElectronico { get; set; }

        [DataMember]
        [Display(Name ="Alias")]
        [StringLength(100, ErrorMessage = "Alias no puede contener más de 100 caracteres.")]
        public string Alias { get; set; }
        [DataMember]
        [Display(Name ="Servidor")]
        [StringLength(50, ErrorMessage = "Servidor no puede contener más de 50 caracteres.")]
        public string Servidor { get; set; }
        [DataMember]
        [Display(Name ="Puerto")]
        public int? Puerto { get; set; }
        [DataMember]
        [Display(Name ="Usuario")]
        [StringLength(100, ErrorMessage = "Usuario no puede contener más de 100 caracteres.")]
        public string Usuario { get; set; }
        [DataMember]
        [Display(Name ="Contrasena")]
        [StringLength(100, ErrorMessage = "Contrasena no puede contener más de 100 caracteres.")]
        public string Contrasena { get; set; }
        [DataMember]
        [Display(Name ="Ssl")]
        public bool Ssl { get; set; }
        [DataMember]
        [Display(Name ="CredencialesDefecto")]
        public bool CredencialesDefecto { get; set; }
        [DataMember]
        [Display(Name ="CuentaDefecto")]
        public bool CuentaDefecto { get; set; }
        [NotMapped]
        [Display(Name = "Enviar Correo Prueba")]
        public string SendTestEmailTo { get; set; }
        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CuentaEmail() { }

        /// <summary>
        /// Constructor para la clase CuentaEmail
        /// </summary>
        /// <param name="dataReader"></param>
        public CuentaEmail(IDataReader dataReader, string alias = "")
        {
            
            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.idProperty))
            {
                this.Id = HelperValues.GetValue<Int64>(dataReader, alias + CuentaEmail.idProperty);
            }
            //IdEntidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BaseEntidad.idEntidadProperty))
            {
                this.IdEntidad = HelperValues.GetValue<Int64>(dataReader, alias + BaseEntidad.idEntidadProperty);
            }
            //correoElectronico
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.correoElectronicoProperty))
            {
                this.CorreoElectronico = HelperValues.GetValue<string>(dataReader, alias + CuentaEmail.correoElectronicoProperty);
            }
            //alias
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.aliasProperty))
            {
                this.Alias = HelperValues.GetValue<string>(dataReader, alias + CuentaEmail.aliasProperty);
            }
            //servidor
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.servidorProperty))
            {
                this.Servidor = HelperValues.GetValue<string>(dataReader, alias + CuentaEmail.servidorProperty);
            }
            //puerto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.puertoProperty))
            {
                this.Puerto = HelperValues.GetValue<int?>(dataReader, alias + CuentaEmail.puertoProperty);
            }
            //usuario
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.usuarioProperty))
            {
                this.Usuario = HelperValues.GetValue<string>(dataReader, alias + CuentaEmail.usuarioProperty);
            }
            //contrasena
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.contrasenaProperty))
            {
                this.Contrasena = HelperValues.GetValue<string>(dataReader, alias + CuentaEmail.contrasenaProperty);
            }
            //ssl
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.sslProperty))
            {
                this.Ssl = HelperValues.GetValue<bool>(dataReader, alias + CuentaEmail.sslProperty);
            }
            //credencialesDefecto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.credencialesDefectoProperty))
            {
                this.CredencialesDefecto = HelperValues.GetValue<bool>(dataReader, alias + CuentaEmail.credencialesDefectoProperty);
            }
            //cuentaDefecto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + CuentaEmail.cuentaDefectoProperty))
            {
                this.CuentaDefecto = HelperValues.GetValue<bool>(dataReader, alias + CuentaEmail.cuentaDefectoProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasCuentaEmail
	namespace POSWeb.Entidades
	{
		[DataContract]
    public class RespuestaListaCuentaEmail : Paginacion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<CuentaEmail> ListaCuentaEmail { get; set; }
		}
		
		[DataContract]
		public class RespuestaCuentaEmail
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public CuentaEmail CuentaEmail { get; set; }
		}	
	}
#endregion RespuestasCuentaEmail


