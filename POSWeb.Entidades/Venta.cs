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
    /// Propósito:           Entidad Venta
    /// Ultima modificacion: 31/07/2019
    /// </summary>
	
    [DataContract]
	[Serializable]
    public class Venta : BaseEntidad
    {
        #region Constantes
        
           public const string idProperty = "Id";
           public const string idClienteProperty = "Id_Cliente";
           public const string tipoProperty = "Tipo";
           public const string totalVentaProperty = "Total_Venta";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="Id")]
        [Required(ErrorMessage = "El dato Id es requerido")]
        public long Id { get; set; }
        [DataMember]
        [Display(Name ="IdCliente")]
        public int? IdCliente { get; set; }
        [DataMember]
        [Display(Name ="Tipo")]
        [StringLength(5, ErrorMessage = "Tipo no puede contener más de 5 caracteres.")]
        [Required(ErrorMessage = "El dato Tipo es requerido")]
        public string Tipo { get; set; }
        [DataMember]
        [Display(Name ="TotalVenta")]
        [Required(ErrorMessage = "El dato TotalVenta es requerido")]
        public decimal TotalVenta { get; set; }

        [DataMember]
        public List<VentaDetalle> Items { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Venta() { }

        /// <summary>
        /// Constructor para la clase Venta
        /// </summary>
        /// <param name="dataReader"></param>
        public Venta(IDataReader dataReader, string alias = "")
        {
            
            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Venta.idProperty))
            {
                this.Id = HelperValues.GetValue<long>(dataReader, alias + Venta.idProperty);
            }
            //idCliente
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Venta.idClienteProperty))
            {
                this.IdCliente = HelperValues.GetValue<int?>(dataReader, alias + Venta.idClienteProperty);
            }
            //tipo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Venta.tipoProperty))
            {
                this.Tipo = HelperValues.GetValue<string>(dataReader, alias + Venta.tipoProperty);
            }
            //totalVenta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Venta.totalVentaProperty))
            {
                this.TotalVenta = HelperValues.GetValue<decimal>(dataReader, alias + Venta.totalVentaProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasVenta
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaVenta
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<Venta> ListaVenta { get; set; }
		}
		
		[DataContract]
		public class RespuestaVenta
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public Venta Venta { get; set; }
		}	
	}
#endregion RespuestasVenta


