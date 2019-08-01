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
    /// Propósito:           Entidad Producto
    /// Ultima modificacion: 11-07-2019
    /// </summary>

    [DataContract]
    [Serializable]
    public class Producto : BaseEntidad
    {
        #region Constantes

        public const string idProperty = "Id";
        public new const string idEntidadProperty = "Id_Entidad";
        public const string idProveedorProperty = "Id_Proveedor";
        public const string idCategoriaProperty = "Id_Categoria";
        public const string nombreProperty = "Nombre";
        public const string precioCostoProperty = "Precio_Costo";
        public const string precioVentaProperty = "Precio_Venta";
        public const string utilidadProperty = "Utilidad";
        public const string impuestoProperty = "Impuesto";
        public const string stockProperty = "Stock";
        public const string existenciaProperty = "Existencia";
        public const string descuentoProperty = "Descuento";
        public const string codigoBarraProperty = "Codigo_Barra";
        public const string activoProperty = "Activo";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "Entidad")]
        [Required(ErrorMessage = "El dato Entidad es requerido")]
        public new long IdEntidad { get; set; }

        [DataMember]
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "El dato Proveedor es requerido")]
        public int IdProveedor { get; set; }

        [DataMember]
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El dato Categoria es requerido")]
        public int IdCategoria { get; set; }

        [DataMember]
        [Display(Name = "Producto")]
        [StringLength(250, ErrorMessage = "Nombre no puede contener más de 250 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; }

        [DataMember]
        [Display(Name = "Precio Costo")]
        [Required(ErrorMessage = "El dato Precio Costo es requerido")]
        public decimal PrecioCosto { get; set; }

        [DataMember]
        [Display(Name = "Precio Venta")]
        [Required(ErrorMessage = "El dato Precio Venta es requerido")]
        public decimal PrecioVenta { get; set; }

        [DataMember]
        [Display(Name = "Utilidad")]
        [Required(ErrorMessage = "El dato Utilidad es requerido")]
        public decimal Utilidad { get; set; }

        [DataMember]
        [Display(Name = "Impuesto")]
        [Required(ErrorMessage = "El dato Impuesto es requerido")]
        public decimal Impuesto { get; set; }

        [DataMember]
        [Display(Name = "Stock")]
        [Required(ErrorMessage = "El dato Stock es requerido")]
        public int Stock { get; set; }

        [DataMember]
        [Display(Name = "Existencia")]
        [Required(ErrorMessage = "El dato Existencia es requerido")]
        public int Existencia { get; set; }

        [DataMember]
        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "El dato Descuento es requerido")]
        public decimal Descuento { get; set; }

        [DataMember]
        [Display(Name = "Código")]
        [StringLength(250, ErrorMessage = "CodigoBarra no puede contener más de 250 caracteres.")]
        [Required(ErrorMessage = "El dato CodigoBarra es requerido")]
        public string CodigoBarra { get; set; }

        [DataMember]
        public bool Activo { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Producto() { }

        /// <summary>
        /// Constructor para la clase Producto
        /// </summary>
        /// <param name="dataReader"></param>
        public Producto(IDataReader dataReader, string alias = "")
        {

            //id
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.idProperty))
            {
                this.Id = HelperValues.GetValue<int>(dataReader, alias + Producto.idProperty);
            }
            //idEntidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.idEntidadProperty))
            {
                this.IdEntidad = HelperValues.GetValue<long>(dataReader, alias + Producto.idEntidadProperty);
            }
            //idProveedor
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.idProveedorProperty))
            {
                this.IdProveedor = HelperValues.GetValue<Int16>(dataReader, alias + Producto.idProveedorProperty);
            }
            //idCategoria
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.idCategoriaProperty))
            {
                this.IdCategoria = HelperValues.GetValue<byte>(dataReader, alias + Producto.idCategoriaProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Producto.nombreProperty);
            }
            //precioCosto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.precioCostoProperty))
            {
                this.PrecioCosto = HelperValues.GetValue<decimal>(dataReader, alias + Producto.precioCostoProperty);
            }
            //precioVenta
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.precioVentaProperty))
            {
                this.PrecioVenta = HelperValues.GetValue<decimal>(dataReader, alias + Producto.precioVentaProperty);
            }
            //utilidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.utilidadProperty))
            {
                this.Utilidad = HelperValues.GetValue<decimal>(dataReader, alias + Producto.utilidadProperty);
            }
            //impuesto
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.impuestoProperty))
            {
                this.Impuesto = HelperValues.GetValue<decimal>(dataReader, alias + Producto.impuestoProperty);
            }
            //stock
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.stockProperty))
            {
                this.Stock = HelperValues.GetValue<Int16>(dataReader, alias + Producto.stockProperty);
            }
            //existencia
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.existenciaProperty))
            {
                this.Existencia = HelperValues.GetValue<Int16>(dataReader, alias + Producto.existenciaProperty);
            }
            //descuento
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.descuentoProperty))
            {
                this.Descuento = HelperValues.GetValue<decimal>(dataReader, alias + Producto.descuentoProperty);
            }
            //codigoBarra
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.codigoBarraProperty))
            {
                this.CodigoBarra = HelperValues.GetValue<string>(dataReader, alias + Producto.codigoBarraProperty);
            }
            //Activo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Producto.activoProperty))
            {
                this.Activo = HelperValues.GetValue<bool>(dataReader, alias + Producto.activoProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasProducto
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaProducto
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<Producto> ListaProducto { get; set; }
    }

    [DataContract]
    public class RespuestaProducto
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public Producto Producto { get; set; }
    }
}
#endregion RespuestasProducto

