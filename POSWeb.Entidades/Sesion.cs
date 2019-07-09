using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Propósito:           Entidad Sesion
    /// Ultima modificacion: 04/11/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class Sesion : BaseEntidad
    {
        #region Constantes
        
           public const string idSesionProperty = "Id_Sesion";
           public new const string idEntidadProperty = "Id_Entidad";
           public const string codigoUsuarioProperty = "Codigo_Usuario";
           public const string tokenProperty = "Token";
           public const string fechaConexionProperty = "Fecha_Conexion";
           public const string ipProperty = "IP";
           public const string numCelularProperty = "Num_Celular";
           public const string ubicacionProperty = "Ubicacion";
           public const string ambienteProperty = "Ambiente";
           public const string fechaDesconexionProperty = "Fecha_Desconexion";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="IdSesion")]
        [Required(ErrorMessage = "El dato IdSesion es requerido")]
        public long IdSesion { get; set; }
        [DataMember]
        [Display(Name ="IdEntidad")]
        [Required(ErrorMessage = "El dato IdEntidad es requerido")]
        public new long IdEntidad { get; set; }
        [DataMember]
        [Display(Name ="CodigoUsuario")]
        [StringLength(50, ErrorMessage = "CodigoUsuario no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato CodigoUsuario es requerido")]
        public string CodigoUsuario { get; set; }
        [DataMember]
        [Display(Name ="Token")]
        [StringLength(250, ErrorMessage = "Token no puede contener más de 250 caracteres.")]
        [Required(ErrorMessage = "El dato Token es requerido")]
        public string Token { get; set; }
        [DataMember]
        [Display(Name ="FechaConexion")]
        public DateTime? FechaConexion { get; set; }
        [DataMember]
        [Display(Name ="Ip")]
        [StringLength(100, ErrorMessage = "Ip no puede contener más de 100 caracteres.")]
        public string Ip { get; set; }
        [DataMember]
        [Display(Name ="NumCelular")]
        [StringLength(15, ErrorMessage = "NumCelular no puede contener más de 15 caracteres.")]
        public string NumCelular { get; set; }
        [DataMember]
        [Display(Name ="Ubicacion")]
        [StringLength(255, ErrorMessage = "Ubicacion no puede contener más de 255 caracteres.")]
        public string Ubicacion { get; set; }
        [DataMember]
        [Display(Name ="Ambiente")]
        [StringLength(255, ErrorMessage = "Ambiente no puede contener más de 255 caracteres.")]
        public string Ambiente { get; set; }
        [DataMember]
        [Display(Name ="FechaDesconexion")]
        public DateTime? FechaDesconexion { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Sesion() { }

        /// <summary>
        /// Constructor para la clase Sesion
        /// </summary>
        /// <param name="dataReader"></param>
        public Sesion(IDataReader dataReader, string alias = "")
        {
            
            //idSesion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.idSesionProperty))
            {
                this.IdSesion = HelperValues.GetValue<long>(dataReader, alias + Sesion.idSesionProperty);
            }
            //idEntidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.idEntidadProperty))
            {
                this.IdEntidad = HelperValues.GetValue<long>(dataReader, alias + Sesion.idEntidadProperty);
            }
            //codigoUsuario
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.codigoUsuarioProperty))
            {
                this.CodigoUsuario = HelperValues.GetValue<string>(dataReader, alias + Sesion.codigoUsuarioProperty);
            }
            //token
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.tokenProperty))
            {
                this.Token = HelperValues.GetValue<string>(dataReader, alias + Sesion.tokenProperty);
            }
            //fechaConexion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.fechaConexionProperty))
            {
                this.FechaConexion = HelperValues.GetValue<DateTime?>(dataReader, alias + Sesion.fechaConexionProperty);
            }
            //ip
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.ipProperty))
            {
                this.Ip = HelperValues.GetValue<string>(dataReader, alias + Sesion.ipProperty);
            }
            //numCelular
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.numCelularProperty))
            {
                this.NumCelular = HelperValues.GetValue<string>(dataReader, alias + Sesion.numCelularProperty);
            }
            //ubicacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.ubicacionProperty))
            {
                this.Ubicacion = HelperValues.GetValue<string>(dataReader, alias + Sesion.ubicacionProperty);
            }
            //ambiente
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.ambienteProperty))
            {
                this.Ambiente = HelperValues.GetValue<string>(dataReader, alias + Sesion.ambienteProperty);
            }
            //fechaDesconexion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Sesion.fechaDesconexionProperty))
            {
                this.FechaDesconexion = HelperValues.GetValue<DateTime?>(dataReader, alias + Sesion.fechaDesconexionProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasSesion
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaSesion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Sesion> ListaSesion { get; set; }
		}
		
		[DataContract]
		public class RespuestaSesion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Sesion Sesion { get; set; }
		}	
	}
#endregion RespuestasSesion


