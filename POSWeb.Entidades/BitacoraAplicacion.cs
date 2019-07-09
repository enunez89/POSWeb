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
    /// Propósito:           Entidad Bitacora
    /// Ultima modificacion: 25/11/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class BitacoraAplicacion : BaseEntidad
    {
        #region Constantes

        public const string idBitacoraProperty = "Id_Bitacora";
        public const string fechaRegistroProperty = "Fecha_Registro";
        public const string tipoEventoProperty = "Tipo_Evento";
        public const string mensajeRegistroProperty = "Mensaje_Registro";
        public const string mensajeTecnicoProperty = "Mensaje_Tecnico";
        public const string trazadorProperty = "Trazador";
        public const string fechaInicioProperty = "Fecha_Inicio";
        public const string fechaFinalProperty = "Fecha_Final";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        public decimal IdBitacora { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "FechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "TipoEvento")]
        public string TipoEvento { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "MensajeRegistro")]
        public string MensajeRegistro { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "MensajeTecnico")]
        public string MensajeTecnico { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Trazador")]
        public string Trazador { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "FechaInicio")]
       // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { set; get; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "FechaFinal")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { set; get; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public BitacoraAplicacion() { }

        /// <summary>
        /// Constructor para la clase Bitacora
        /// </summary>
        /// <param name="dataReader"></param>
        public BitacoraAplicacion(IDataReader dataReader, string alias = "")
        {

            //idBitacora
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BitacoraAplicacion.idBitacoraProperty))
            {
                this.IdBitacora = HelperValues.GetValue<decimal>(dataReader, alias + BitacoraAplicacion.idBitacoraProperty);
            }
            //fechaRegistro
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BitacoraAplicacion.fechaRegistroProperty))
            {
                this.FechaRegistro = HelperValues.GetValue<DateTime>(dataReader, alias + BitacoraAplicacion.fechaRegistroProperty);
            }
            //tipoEvento
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BitacoraAplicacion.tipoEventoProperty))
            {
                this.TipoEvento = HelperValues.GetValue<string>(dataReader, alias + BitacoraAplicacion.tipoEventoProperty);
            }
            //mensajeRegistro
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BitacoraAplicacion.mensajeRegistroProperty))
            {
                this.MensajeRegistro = HelperValues.GetValue<string>(dataReader, alias + BitacoraAplicacion.mensajeRegistroProperty);
            }
            //mensajeTecnico
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BitacoraAplicacion.mensajeTecnicoProperty))
            {
                this.MensajeTecnico = HelperValues.GetValue<string>(dataReader, alias + BitacoraAplicacion.mensajeTecnicoProperty);
            }
            //trazador
            if (HelperValues.DataReaderHasColumn(dataReader, alias + BitacoraAplicacion.trazadorProperty))
            {
                this.Trazador = HelperValues.GetValue<string>(dataReader, alias + BitacoraAplicacion.trazadorProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasBitacora
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaBitacoraAplicacion : Paginacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<BitacoraAplicacion> ListaBitacora { get; set; }
    }

    [DataContract]
    public class RespuestaBitacoraAplicacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public BitacoraAplicacion Bitacora { get; set; }
    }
}
#endregion RespuestasBitacora


