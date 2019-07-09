using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace POSWeb.Entidades
{
    /// <summary>
    /// Requerimiento:       POSWeb
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Entidad Accion
    /// Ultima modificacion: 16/11/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class Accion : BaseEntidad
    {
        #region Constantes

        public const string idAccionProperty = "Id_Accion";
        public const string nombreProperty = "Nombre";
        public const string descripcionProperty = "Descripcion";
        public const string cssClassProperty = "Css_Class";
        public const string controladorProperty = "Controlador";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(Name = "IdAccion")]
        [Required(ErrorMessage = "El dato IdAccion es requerido")]
        public long IdAccion { get; set; }
        [DataMember]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Nombre no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; }
        [DataMember]
        [Display(Name = "Descripcion")]
        [StringLength(200, ErrorMessage = "Descripcion no puede contener más de 200 caracteres.")]
        public string Descripcion { get; set; }
        [DataMember]
        [Display(Name = "CssClass")]
        [StringLength(50, ErrorMessage = "CssClass no puede contener más de 50 caracteres.")]
        public string CssClass { get; set; }

        [DataMember]
        public long IdPerfil { get; set; }

        [DataMember]
        public string Controlador { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Accion() { }

        /// <summary>
        /// Constructor para la clase Accion
        /// </summary>
        /// <param name="dataReader"></param>
        public Accion(IDataReader dataReader, string alias = "")
        {

            //idAccion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Accion.idAccionProperty))
            {
                this.IdAccion = HelperValues.GetValue<long>(dataReader, alias + Accion.idAccionProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Accion.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Accion.nombreProperty);
            }
            //descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Accion.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + Accion.descripcionProperty);
            }
            //cssClass
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Accion.cssClassProperty))
            {
                this.CssClass = HelperValues.GetValue<string>(dataReader, alias + Accion.cssClassProperty);
            }
            //Controlador
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Accion.controladorProperty))
            {
                this.Controlador = HelperValues.GetValue<string>(dataReader, alias + Accion.controladorProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasAccion
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaAccion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<Accion> ListaAccion { get; set; }
    }

    [DataContract]
    public class RespuestaAccion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public Accion Accion { get; set; }
    }
}
#endregion RespuestasAccion


