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
    /// Propósito:           Entidad VentaDetalle
    /// Ultima modificacion: 31/07/2019
    /// </summary>

    [DataContract]
	[Serializable]
    public class VentaDetalle : BaseEntidad
    {
        #region Constantes
        
           public const string idProperty = "Id";
           public const string idVentaProperty = "Id_Venta";
           public const string idProductoProperty = "Id_Producto";
           public const string cantidadProperty = "Cantidad";
           public const string montoProperty = "Monto";

        #endregion Constantes


        #region Propiedades
        
        [DataMember]
        [Display(Name ="Id")]
        [Required(ErrorMessage = "El dato Id es requerido")]
        public long Id { get; set; }
        [DataMember]
        [Display(Name ="IdVenta")]
        [Required(ErrorMessage = "El dato IdVenta es requerido")]
        public long IdVenta { get; set; }
        [DataMember]
        [Display(Name ="IdProducto")]
        [Required(ErrorMessage = "El dato IdProducto es requerido")]
        public int IdProducto { get; set; }
        [DataMember]
        [Display(Name ="Cantidad")]
        [Required(ErrorMessage = "El dato Cantidad es requerido")]
        public int Cantidad { get; set; }
        [DataMember]
        [Display(Name ="Monto")]
        [Required(ErrorMessage = "El dato Monto es requerido")]
        public decimal Monto { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public VentaDetalle() { }

        /// <summary>
        /// Constructor para la clase VentaDetalle
        /// </summary>
        /// <param name="dataReader"></param>
        public VentaDetalle(IDataReader dataReader, string alias = "")
        {
            
            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + VentaDetalle.idProperty))
            {
                this.Id = HelperValues.GetValue<long>(dataReader, alias + VentaDetalle.idProperty);
            }
            //idVenta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + VentaDetalle.idVentaProperty))
            {
                this.IdVenta = HelperValues.GetValue<long>(dataReader, alias + VentaDetalle.idVentaProperty);
            }
            //idProducto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + VentaDetalle.idProductoProperty))
            {
                this.IdProducto = HelperValues.GetValue<int>(dataReader, alias + VentaDetalle.idProductoProperty);
            }
            //cantidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + VentaDetalle.cantidadProperty))
            {
                this.Cantidad = HelperValues.GetValue<int>(dataReader, alias + VentaDetalle.cantidadProperty);
            }
            //monto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + VentaDetalle.montoProperty))
            {
                this.Monto = HelperValues.GetValue<decimal>(dataReader, alias + VentaDetalle.montoProperty);
            }
			this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasVentaDetalle
	namespace POSWeb.Entidades
	{
		[DataContract]
		public class RespuestaListaVentaDetalle
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
		
			[DataMember]
			public List<VentaDetalle> ListaVentaDetalle { get; set; }
		}
		
		[DataContract]
		public class RespuestaVentaDetalle
		{
			[DataMember]
			public Respuesta Respuesta { get; set; }
			
			[DataMember]
			public VentaDetalle VentaDetalle { get; set; }
		}	
	}
#endregion RespuestasVentaDetalle


