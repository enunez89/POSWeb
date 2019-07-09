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
    /// Propósito:           Entidad Perfil
    /// Ultima modificacion: 15/11/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class Perfil : BaseEntidad
    {
        #region Constantes
        
           public const string idPerfilProperty = "Id_Perfil";
           public const string nombreProperty = "Nombre";
           public const string estadoProperty = "Estado";
           public const string indSuperUsuarioProperty = "Ind_Super_Usuario";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="IdPerfil")]
        [Required(ErrorMessage = "El dato IdPerfil es requerido")]
        public long IdPerfil { get; set; }
        [DataMember]
        [Display(Name ="Nombre")]
        [StringLength(50, ErrorMessage = "Nombre no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; }
        [DataMember]
        [Display(Name ="Estado")]
        [StringLength(10, ErrorMessage = "Estado no puede contener más de 10 caracteres.")]
        [Required(ErrorMessage = "El dato Estado es requerido")]
        public string Estado { get; set; }
        [DataMember]
        [Display(Name ="IndSuperUsuario")]
        [Required(ErrorMessage = "El dato IndSuperUsuario es requerido")]
        public bool IndSuperUsuario { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Perfil() { }

        /// <summary>
        /// Constructor para la clase Perfil
        /// </summary>
        /// <param name="dataReader"></param>
        public Perfil(IDataReader dataReader, string alias = "")
        {
            
            //idPerfil
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Perfil.idPerfilProperty))
            {
                this.IdPerfil = HelperValues.GetValue<long>(dataReader, alias + Perfil.idPerfilProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Perfil.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Perfil.nombreProperty);
            }
            //estado
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Perfil.estadoProperty))
            {
                this.Estado = HelperValues.GetValue<string>(dataReader, alias + Perfil.estadoProperty);
            }
            //indSuperUsuario
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Perfil.indSuperUsuarioProperty))
            {
                this.IndSuperUsuario = HelperValues.GetValue<bool>(dataReader, alias + Perfil.indSuperUsuarioProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasPerfil
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaPerfil
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Perfil> ListaPerfil { get; set; }
		}
		
		[DataContract]
		public class RespuestaPerfil
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Perfil Perfil { get; set; }
		}	
	}
#endregion RespuestasPerfil


