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
    /// Propósito:           Entidad Rol
    /// Ultima modificacion: 12/11/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class Rol : BaseEntidad
    {
        #region Constantes
        
           public const string idRolProperty = "Id_Rol";
           public const string nombreProperty = "Nombre";
           public const string estadoProperty = "Estado";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Required(ErrorMessage = "El dato IdRol es requerido")]
        public long IdRol { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Nombre no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Estado")]
        [StringLength(10, ErrorMessage = "Estado no puede contener más de 10 caracteres.")]
        [Required(ErrorMessage = "El dato Estado es requerido")]
        public string Estado { get; set; }

        [DataMember]
        public List<Perfil> Perfiles { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Rol() { }

        /// <summary>
        /// Constructor para la clase Rol
        /// </summary>
        /// <param name="dataReader"></param>
        public Rol(IDataReader dataReader, string alias = "")
        {
            
            //idRol
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Rol.idRolProperty))
            {
                this.IdRol = HelperValues.GetValue<long>(dataReader, alias + Rol.idRolProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Rol.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Rol.nombreProperty);
            }
            //estado
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Rol.estadoProperty))
            {
                this.Estado = HelperValues.GetValue<string>(dataReader, alias + Rol.estadoProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasRol
	namespace POSWeb.Entidades
	{
		[DataContract]
        public class RespuestaListaRol : Paginacion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Rol> ListaRol { get; set; }
		}
		
		[DataContract]
		public class RespuestaRol
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Rol Rol { get; set; }
		}	
	}
#endregion RespuestasRol


