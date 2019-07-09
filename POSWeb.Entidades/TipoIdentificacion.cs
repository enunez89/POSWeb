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
    /// Propósito:           Entidad TipoIdentificacion
    /// Ultima modificacion: 06/12/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class TipoIdentificacion : BaseEntidad
    {
        #region Constantes

        public const string idTipoProperty = "Id_Tipo";
        public const string idPaisProperty = "Id_Pais";
        public const string descripcionProperty = "Descripcion";
        public const string formatoProperty = "Formato";
        public const string codigoPaisProperty = "Codigo_Pais";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(Name = "IdTipo")]
        [Required(ErrorMessage = "El dato IdTipo es requerido")]
        public long IdTipo { get; set; }
        [DataMember]
        [Display(Name = "IdPais")]
        [Required(ErrorMessage = "El dato IdPais es requerido")]
        public long IdPais { get; set; }
        [DataMember]
        [Display(Name = "Descripcion")]
        [StringLength(100, ErrorMessage = "Descripcion no puede contener más de 100 caracteres.")]
        public string Descripcion { get; set; }
        [DataMember]
        [Display(Name = "Formato")]
        [StringLength(60, ErrorMessage = "Formato no puede contener más de 60 caracteres.")]
        [Required(ErrorMessage = "El dato Formato es requerido")]
        public string Formato { get; set; }

        [DataMember]
        public string CodigoPais { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TipoIdentificacion() { }

        /// <summary>
        /// Constructor para la clase TipoIdentificacion
        /// </summary>
        /// <param name="dataReader"></param>
        public TipoIdentificacion(IDataReader dataReader, string alias = "")
        {

            //idTipo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + TipoIdentificacion.idTipoProperty))
            {
                this.IdTipo = HelperValues.GetValue<long>(dataReader, alias + TipoIdentificacion.idTipoProperty);
            }
            //idPais
            if (HelperValues.DataReaderHasColumn(dataReader, alias + TipoIdentificacion.idPaisProperty))
            {
                this.IdPais = HelperValues.GetValue<long>(dataReader, alias + TipoIdentificacion.idPaisProperty);
            }
            //descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + TipoIdentificacion.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + TipoIdentificacion.descripcionProperty);
            }
            //formato
            if (HelperValues.DataReaderHasColumn(dataReader, alias + TipoIdentificacion.formatoProperty))
            {
                this.Formato = HelperValues.GetValue<string>(dataReader, alias + TipoIdentificacion.formatoProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasTipoIdentificacion
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaTipoIdentificacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<TipoIdentificacion> ListaTipoIdentificacion { get; set; }
    }

    [DataContract]
    public class RespuestaTipoIdentificacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public TipoIdentificacion TipoIdentificacion { get; set; }
    }
}
#endregion RespuestasTipoIdentificacion


