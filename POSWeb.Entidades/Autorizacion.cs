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
    /// Propósito:           Entidad Autorizacion
    /// Ultima modificacion: 28/11/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class Autorizacion : BaseEntidad
    {
        #region Constantes

        public const string idAutorizacionProperty = "Id_Autorizacion";
        public const string recursoProperty = "Recurso";
        public const string idRecursoProperty = "Id_Recurso";
        public const string conteoAutorizacionProperty = "Conteo_Autorizacion";
        public const string descripcionProperty = "Descripcion";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IdAutorizacion")]
        [Required(ErrorMessage = "El dato IdAutorizacion es requerido")]
        public long IdAutorizacion { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Recurso")]
        [StringLength(100, ErrorMessage = "Recurso no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Recurso es requerido")]
        public string Recurso { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IdRecurso")]
        [StringLength(100, ErrorMessage = "IdRecurso no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato IdRecurso es requerido")]
        public string IdRecurso { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "ConteoAutorizacion")]
        [Required(ErrorMessage = "El dato ConteoAutorizacion es requerido")]
        public int ConteoAutorizacion { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Descripcion")]
        public string Descripcion { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Autorizacion() { }

        /// <summary>
        /// Constructor para la clase Autorizacion
        /// </summary>
        /// <param name="dataReader"></param>
        public Autorizacion(IDataReader dataReader, string alias = "")
        {

            //idAutorizacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Autorizacion.idAutorizacionProperty))
            {
                this.IdAutorizacion = HelperValues.GetValue<long>(dataReader, alias + Autorizacion.idAutorizacionProperty);
            }
            //recurso
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Autorizacion.recursoProperty))
            {
                this.Recurso = HelperValues.GetValue<string>(dataReader, alias + Autorizacion.recursoProperty);
            }
            //idRecurso
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Autorizacion.idRecursoProperty))
            {
                this.IdRecurso = HelperValues.GetValue<string>(dataReader, alias + Autorizacion.idRecursoProperty);
            }
            //conteoAutorizacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Autorizacion.conteoAutorizacionProperty))
            {
                this.ConteoAutorizacion = HelperValues.GetValue<int>(dataReader, alias + Autorizacion.conteoAutorizacionProperty);
            }
            //Descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Autorizacion.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + Autorizacion.descripcionProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasAutorizacion
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaAutorizacion : Paginacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<Autorizacion> ListaAutorizacion { get; set; }
    }

    [DataContract]
    public class RespuestaAutorizacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public Autorizacion Autorizacion { get; set; }
    }
}
#endregion RespuestasAutorizacion


