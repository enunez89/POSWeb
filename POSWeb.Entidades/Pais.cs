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
    /// Propósito:           Entidad Pais
    /// Ultima modificacion: 06/12/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class Pais : BaseEntidad
    {
        #region Constantes

        public const string idPaisProperty = "Id_Pais";
        public const string codigoProperty = "Codigo";
        public const string nombreProperty = "Nombre";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(Name = "IdPais")]
        [Required(ErrorMessage = "El dato IdPais es requerido")]
        public long IdPais { get; set; }
        [DataMember]
        [Display(Name = "Codigo")]
        [StringLength(50, ErrorMessage = "Codigo no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato Codigo es requerido")]
        public string Codigo { get; set; }
        [DataMember]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Nombre no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Pais() { }

        /// <summary>
        /// Constructor para la clase Pais
        /// </summary>
        /// <param name="dataReader"></param>
        public Pais(IDataReader dataReader, string alias = "")
        {

            //idPais
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Pais.idPaisProperty))
            {
                this.IdPais = HelperValues.GetValue<long>(dataReader, alias + Pais.idPaisProperty);
            }
            //codigo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Pais.codigoProperty))
            {
                this.Codigo = HelperValues.GetValue<string>(dataReader, alias + Pais.codigoProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Pais.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Pais.nombreProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasPais
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaPais
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<Pais> ListaPais { get; set; }
    }

    [DataContract]
    public class RespuestaPais
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public Pais Pais { get; set; }
    }
}
#endregion RespuestasPais


