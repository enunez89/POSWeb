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
    /// Propósito:           Entidad Controlador
    /// Ultima modificacion: 25/10/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class Controlador : BaseEntidad
    {
        #region Constantes

        public const string idControladorProperty = "Id_Controlador";
        public const string idModuloProperty = "Id_Modulo";
        public const string codigoProperty = "Codigo";
        public const string descripcionProperty = "Descripcion";
        public const string accionDefaultProperty = "Accion_Default";
        public const string cssClassProperty = "Css_Class";
        public const string autorizarProperty = "Autorizar";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(Name = "IdControlador")]
        [Required(ErrorMessage = "El dato IdControlador es requerido")]
        public long IdControlador { get; set; }

        [DataMember]
        public long IdModulo { get; set; }

        [DataMember]
        [Display(Name = "Codigo")]
        [StringLength(100, ErrorMessage = "Codigo no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Codigo es requerido")]
        public string Codigo { get; set; }
        [DataMember]
        [Display(Name = "Descripcion")]
        [StringLength(100, ErrorMessage = "Descripcion no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Descripcion es requerido")]
        public string Descripcion { get; set; }
        [DataMember]
        [Display(Name = "AccionDefault")]
        [StringLength(100, ErrorMessage = "AccionDefault no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato AccionDefault es requerido")]
        public string AccionDefault { get; set; }
        [DataMember]
        [Display(Name = "CssClass")]
        [StringLength(50, ErrorMessage = "CssClass no puede contener más de 50 caracteres.")]
        public string CssClass { get; set; }

        [DataMember]
        public bool Autorizar { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Controlador() { }

        /// <summary>
        /// Constructor para la clase Controlador
        /// </summary>
        /// <param name="dataReader"></param>
        public Controlador(IDataReader dataReader, string alias = "")
        {

            //idControlador
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.idControladorProperty))
            {
                this.IdControlador = HelperValues.GetValue<long>(dataReader, alias + Controlador.idControladorProperty);
            }
            //idModulo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.idModuloProperty))
            {
                this.IdModulo = HelperValues.GetValue<long>(dataReader, alias + Controlador.idModuloProperty);
            }
            //codigo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.codigoProperty))
            {
                this.Codigo = HelperValues.GetValue<string>(dataReader, alias + Controlador.codigoProperty);
            }
            //descripcion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.descripcionProperty))
            {
                this.Descripcion = HelperValues.GetValue<string>(dataReader, alias + Controlador.descripcionProperty);
            }
            //accionDefault
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.accionDefaultProperty))
            {
                this.AccionDefault = HelperValues.GetValue<string>(dataReader, alias + Controlador.accionDefaultProperty);
            }
            //cssClass
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.cssClassProperty))
            {
                this.CssClass = HelperValues.GetValue<string>(dataReader, alias + Controlador.cssClassProperty);
            }
            //autorizar
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Controlador.autorizarProperty))
            {
                this.Autorizar = HelperValues.GetValue<bool>(dataReader, alias + Controlador.autorizarProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasControlador
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaControlador
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<Controlador> ListaControlador { get; set; }
    }

    [DataContract]
    public class RespuestaControlador
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public Controlador Controlador { get; set; }
    }
}
#endregion RespuestasControlador


