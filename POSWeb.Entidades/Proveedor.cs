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
    /// Empresa:             Salazar & Asociados S.A.
    /// Autor:               Eddy 
    /// Propósito:           Entidad Proveedor
    /// Ultima modificacion: 23-07-2019
    /// </summary>

    [DataContract]
	[Serializable]
    public class Proveedor : BaseEntidad
    {
        #region Constantes
        
           public const string idProperty = "Id";
           public const string idEntidadProperty = "Id_Entidad";
           public const string nombreProperty = "Nombre";
           public const string descripcionProperty = "Descripcion";
           public const string nomContactoProperty = "Nom_Contacto";
           public const string telContactoProperty = "Tel_Contacto";
           public const string correoContactoProperty = "Correo_Contacto";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="Id")]
        [Required(ErrorMessage = "El dato Id es requerido")]
        public int Id { get; set; }
        [DataMember]
        [Display(Name ="IdEntidad")]
        [Required(ErrorMessage = "El dato IdEntidad es requerido")]
        public long IdEntidad { get; set; }
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
        [Display(Name ="NomContacto")]
        [StringLength(150, ErrorMessage = "NomContacto no puede contener más de 150 caracteres.")]
        public string NomContacto { get; set; }
        [DataMember]
        [Display(Name ="TelContacto")]
        [StringLength(200, ErrorMessage = "TelContacto no puede contener más de 200 caracteres.")]
        public string TelContacto { get; set; }
        [DataMember]
        [Display(Name ="CorreoContacto")]
        [StringLength(100, ErrorMessage = "CorreoContacto no puede contener más de 100 caracteres.")]
        public string CorreoContacto { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Proveedor() { }

        /// <summary>
        /// Constructor para la clase Proveedor
        /// </summary>
        /// <param name="dataReader"></param>
        public Proveedor(IDataReader dataReader, string alias = "")
        {
            
            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.idProperty))
            {
                this.Id = HelperValues.GetValue<short>(dataReader, alias + Proveedor.idProperty);
            }
            //idEntidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.idEntidadProperty))
            {
                this.IdEntidad = HelperValues.GetValue<long>(dataReader, alias + Proveedor.idEntidadProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Proveedor.nombreProperty);
            }
            //descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + Proveedor.descripcionProperty);
            }
            //nomContacto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.nomContactoProperty))
            {
                this.NomContacto = HelperValues.GetValue<string>(dataReader, alias + Proveedor.nomContactoProperty);
            }
            //telContacto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.telContactoProperty))
            {
                this.TelContacto = HelperValues.GetValue<string>(dataReader, alias + Proveedor.telContactoProperty);
            }
            //correoContacto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Proveedor.correoContactoProperty))
            {
                this.CorreoContacto = HelperValues.GetValue<string>(dataReader, alias + Proveedor.correoContactoProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasProveedor
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaProveedor
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Proveedor> ListaProveedor { get; set; }
		}
		
		[DataContract]
		public class RespuestaProveedor
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Proveedor Proveedor { get; set; }
		}	
	}
#endregion RespuestasProveedor


