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
    /// Propósito:           Entidad Alerta
    /// Ultima modificacion: 01/12/2017
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class AlertaBase : BaseEntidad
    {
        #region Constantes
        
           public const string idProperty = "Id";
           public const string nombreAlertaProperty = "NombreAlerta";
           public const string codigoAlertaProperty = "CodigoAlerta";
           public const string tipoAlertaProperty = "TipoAlerta";
           public const string activaProperty = "Activa";
           public const string idControladorProperty = "Id_Controlador";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="Id")]
        [Required(ErrorMessage = "El dato Id es requerido")]
        public long Id { get; set; }

        [DataMember]
        [Display(Name ="NombreAlerta")]
        [StringLength(50, ErrorMessage = "NombreAlerta no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato NombreAlerta es requerido")]
        public string NombreAlerta { get; set; }

        [DataMember]
        [Display(Name ="CodigoAlerta")]
        [StringLength(50, ErrorMessage = "CodigoAlerta no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato CodigoAlerta es requerido")]
        public string CodigoAlerta { get; set; }

        [DataMember]
        [Display(Name ="TipoAlerta")]
        [StringLength(1, ErrorMessage = "TipoAlerta no puede contener más de 1 caracteres.")]
        [Required(ErrorMessage = "El dato TipoAlerta es requerido")]
        public string TipoAlerta { get; set; }

        [DataMember]
        [Display(Name ="Activa")]
        public bool? Activo { get; set; }

        [DataMember]
        [Display(Name ="IdControlador")]
        public long? IdControlador { get; set; }

        #endregion Propiedades


        #region Constructores
        

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AlertaBase() { }

        /// <summary>
        /// Constructor para la clase Alerta
        /// </summary>
        /// <param name="dataReader"></param>
        public AlertaBase(IDataReader dataReader, string alias = "")
        {
            
            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaBase.idProperty))
            {
                this.Id = HelperValues.GetValue<long>(dataReader, alias + AlertaBase.idProperty);
            }
            //nombreAlerta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaBase.nombreAlertaProperty))
            {
                this.NombreAlerta = HelperValues.GetValue<string>(dataReader, alias + AlertaBase.nombreAlertaProperty);
            }
            //codigoAlerta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaBase.codigoAlertaProperty))
            {
                this.CodigoAlerta = HelperValues.GetValue<string>(dataReader, alias + AlertaBase.codigoAlertaProperty);
            }
            //tipoAlerta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaBase.tipoAlertaProperty))
            {
                this.TipoAlerta = HelperValues.GetValue<string>(dataReader, alias + AlertaBase.tipoAlertaProperty);
            }
            //activa
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaBase.activaProperty))
            {
                this.Activo = HelperValues.GetValue<bool?>(dataReader, alias + AlertaBase.activaProperty);
            }
            //idControlador
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AlertaBase.idControladorProperty))
            {
                this.IdControlador = HelperValues.GetValue<long?>(dataReader, alias + AlertaBase.idControladorProperty);
            }
            //idControladorDesc
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BaseEntidad.nombreControladorProperty))
            {
                this.NombreControlador = HelperValues.GetValue<string>(dataReader, alias + BaseEntidad.nombreControladorProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasAlerta
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaAlerta : Paginacion
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<AlertaBase> ListaAlerta { get; set; }
		}
		
		[DataContract]
		public class RespuestaAlerta
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public AlertaBase Alerta { get; set; }
		}	
	}
#endregion RespuestasAlerta


