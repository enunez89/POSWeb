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
    /// Propósito:           Entidad Modulo
    /// Ultima modificacion: 25/10/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class Modulo : BaseEntidad
    {
        #region Constantes
        
           public const string idModuloProperty = "Id_Modulo";
           public const string nombreProperty = "Nombre";
           public const string descripcionProperty = "Descripcion";
           public const string cssClassProperty = "Css_Class";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="IdModulo")]
        [Required(ErrorMessage = "El dato IdModulo es requerido")]
        public long IdModulo { get; set; }
        [DataMember]
        [Display(Name ="Nombre")]
        [StringLength(100, ErrorMessage = "Nombre no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; }
        [DataMember]
        [Display(Name ="Descripcion")]
        [StringLength(250, ErrorMessage = "Descripcion no puede contener más de 250 caracteres.")]
        public string Descripcion { get; set; }
        [DataMember]
        [Display(Name ="CssClass")]
        [StringLength(50, ErrorMessage = "CssClass no puede contener más de 50 caracteres.")]
        public string CssClass { get; set; }

        [DataMember]
        public List<Controlador> Controladores { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Modulo() { }

        /// <summary>
        /// Constructor para la clase Modulo
        /// </summary>
        /// <param name="dataReader"></param>
        public Modulo(IDataReader dataReader, string alias = "")
        {
            
            //idModulo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Modulo.idModuloProperty))
            {
                this.IdModulo = HelperValues.GetValue<long>(dataReader, alias + Modulo.idModuloProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Modulo.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Modulo.nombreProperty);
            }
            //descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Modulo.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + Modulo.descripcionProperty);
            }
            //cssClass
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Modulo.cssClassProperty))
            {
                this.CssClass = HelperValues.GetValue<string>(dataReader, alias + Modulo.cssClassProperty);
            }
			this.LoadProperty(dataReader, alias);

            this.Controladores = new List<Controlador>();
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasModulo
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaModulo
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Modulo> ListaModulo { get; set; }
		}
		
		[DataContract]
		public class RespuestaModulo
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Modulo Modulo { get; set; }
		}	
	}
#endregion RespuestasModulo


