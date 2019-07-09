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
    /// Propósito:           Entidad AutorizacionDetalle
    /// Ultima modificacion: 28/11/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class AutorizacionDetalle : BaseEntidad
    {
        #region Constantes

        public const string idAutorizacionDetalleProperty = "Id_Autorizacion_Detalle";
        public const string idAutorizacionProperty = "Id_Autorizacion";
        public const string usrAutorizadorProperty = "Usr_Autorizador";
        public const string fechaAutorizacionProperty = "Fecha_Autorizacion";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IdAutorizacionDetalle")]
        public long IdAutorizacionDetalle { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IdAutorizacion")]
        [Required(ErrorMessage = "El dato IdAutorizacion es requerido")]
        public long IdAutorizacion { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "UsrAutorizador")]
        [StringLength(50, ErrorMessage = "UsrAutorizador no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato UsrAutorizador es requerido")]
        public string UsrAutorizador { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "FechaAutorizacion")]
        [Required(ErrorMessage = "El dato FechaAutorizacion es requerido")]
        public DateTime FechaAutorizacion { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AutorizacionDetalle() { }

        /// <summary>
        /// Constructor para la clase AutorizacionDetalle
        /// </summary>
        /// <param name="dataReader"></param>
        public AutorizacionDetalle(IDataReader dataReader, string alias = "")
        {

            //idAutorizacionDetalle
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AutorizacionDetalle.idAutorizacionDetalleProperty))
            {
                this.IdAutorizacionDetalle = HelperValues.GetValue<long>(dataReader, alias + AutorizacionDetalle.idAutorizacionDetalleProperty);
            }
            //idAutorizacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AutorizacionDetalle.idAutorizacionProperty))
            {
                this.IdAutorizacion = HelperValues.GetValue<long>(dataReader, alias + AutorizacionDetalle.idAutorizacionProperty);
            }
            //usrAutorizador
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AutorizacionDetalle.usrAutorizadorProperty))
            {
                this.UsrAutorizador = HelperValues.GetValue<string>(dataReader, alias + AutorizacionDetalle.usrAutorizadorProperty);
            }
            //fechaAutorizacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + AutorizacionDetalle.fechaAutorizacionProperty))
            {
                this.FechaAutorizacion = HelperValues.GetValue<DateTime>(dataReader, alias + AutorizacionDetalle.fechaAutorizacionProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasAutorizacionDetalle
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaAutorizacionDetalle : Paginacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<AutorizacionDetalle> ListaAutorizacionDetalle { get; set; }
    }

    [DataContract]
    public class RespuestaAutorizacionDetalle
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public AutorizacionDetalle AutorizacionDetalle { get; set; }
    }
}
#endregion RespuestasAutorizacionDetalle


