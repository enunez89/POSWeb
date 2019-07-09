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
    /// Propósito:           Entidad Catalogo
    /// Ultima modificacion: 18/11/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class Catalogo : BaseEntidad
    {
        #region Constantes
        
           public const string idCatalogoProperty = "Id_Catalogo";
           public const string identificadorProperty = "Identificador";
           public const string codigoParametroProperty = "CodigoParametro";
           public const string descripcionProperty = "Descripcion";
        public const string publicoProperty = "Publico";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(Name ="IdCatalogo")]
        [Required(ErrorMessage = "El dato IdCatalogo es requerido")]
        public long IdCatalogo { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Valor")]
        [StringLength(20, ErrorMessage = "Identificador no puede contener más de 20 caracteres.")]
        [Required(ErrorMessage = "El dato Identificador es requerido")]
        public string Identificador { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "CodigoParametro")]
        [StringLength(50, ErrorMessage = "CodigoParametro no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato CodigoParametro es requerido")]
        public string CodigoParametro { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Descripcion")]
        [StringLength(250, ErrorMessage = "Descripcion no puede contener más de 250 caracteres.")]
        public string Descripcion { get; set; }

        [DataMember]
        [Display(Name = "Publico")]
        public bool Publico { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Catalogo() { }

        /// <summary>
        /// Constructor para la clase Catalogo
        /// </summary>
        /// <param name="dataReader"></param>
        public Catalogo(IDataReader dataReader, string alias = "")
        {
            
            //idCatalogo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Catalogo.idCatalogoProperty))
            {
                this.IdCatalogo = HelperValues.GetValue<long>(dataReader, alias + Catalogo.idCatalogoProperty);
            }
            //identificador
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Catalogo.identificadorProperty))
            {
                this.Identificador = HelperValues.GetValue<string>(dataReader, alias + Catalogo.identificadorProperty);
            }
            //codigoParametro
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Catalogo.codigoParametroProperty))
            {
                this.CodigoParametro = HelperValues.GetValue<string>(dataReader, alias + Catalogo.codigoParametroProperty);
            }
            //descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Catalogo.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + Catalogo.descripcionProperty);
            }
            //publico
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Catalogo.publicoProperty))
            {
                this.Publico = HelperValues.GetValue<bool>(dataReader, alias + Catalogo.publicoProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasCatalogo
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaCatalogo: Paginacion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Catalogo> ListaCatalogo { get; set; }
		}
		
		[DataContract]
		public class RespuestaCatalogo
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Catalogo Catalogo { get; set; }
		}	
	}
#endregion RespuestasCatalogo


