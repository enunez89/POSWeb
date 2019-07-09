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
    /// Propósito:           Entidad Usuario
    /// Ultima modificacion: 04/11/2017
    /// </summary>

    [DataContract]
    [Serializable]
    public class Usuario : BaseEntidad
    {
        #region Constantes

        public const string codigoUsuarioProperty = "Codigo_Usuario";
        public const string nombreProperty = "Nombre";
        public const string claveProperty = "Clave";
        public const string identificacionProperty = "Identificacion";
        public const string estadoProperty = "Estado";
        public const string fechaExpiracionClaveProperty = "Fecha_Expiracion_Clave";
        public const string intentosFallidosProperty = "Intentos_Fallidos";
        public const string pendienteCambioProperty = "Pendiente_Cambio";
        public const string correoElectronicoProperty = "Correo_Electronico";
        public const string idTipoIdentificacionProperty = "Id_Tipo_Identificacion";
        public const string idPaisProperty = "Id_Pais";

        #endregion Constantes


        #region Propiedades

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "CodigoUsuario")]
        [StringLength(50, ErrorMessage = "CodigoUsuario no puede contener más de 50 caracteres.")]
        [Required(ErrorMessage = "El dato CodigoUsuario es requerido")]
        public string CodigoUsuario { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Nombre no puede contener más de 100 caracteres.")]
        [Required(ErrorMessage = "El dato Nombre es requerido")]
        public string Nombre { get; set; } 

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Contrasena")]
        [StringLength(200, ErrorMessage = "Contraseña no puede contener más de 200 caracteres.")]
        [Required(ErrorMessage = "El dato Contraseña es requerido")]
        public string Clave { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Identificacion")]
        [StringLength(20, ErrorMessage = "Identificacion no puede contener más de 20 caracteres.")]
        [Required(ErrorMessage = "El dato Identificacion es requerido")]
        public string Identificacion { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Estado")]
        [StringLength(10, ErrorMessage = "Estado no puede contener más de 10 caracteres.")]
        [Required(ErrorMessage = "El dato Estado es requerido")]
        public string Estado { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "FechaExpiracionClave")]
        public DateTime? FechaExpiracionClave { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IntentosFallidos")]
        [Required(ErrorMessage = "El dato IntentosFallidos es requerido")]
        public int IntentosFallidos { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "PendienteCambio")]
        [Required(ErrorMessage = "El dato PendienteCambio es requerido")]
        public bool PendienteCambio { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "CorreoElectronico")]
        [StringLength(50, ErrorMessage = "CorreoElectronico no puede contener más de 50 caracteres.")]
        public string CorreoElectronico { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IdTipoIdentificacion")]
        [Required(ErrorMessage = "El dato IdTipoIdentificacion es requerido")]
        public long IdTipoIdentificacion { get; set; }

        [DataMember]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "IdPais")]
        [Required(ErrorMessage = "El dato IdPais es requerido")]
        public long IdPais { get; set; }

        [DataMember]
        public List<Rol> Roles { get; set; }

        #endregion Propiedades


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Usuario() { }

        /// <summary>
        /// Constructor para la clase Usuario
        /// </summary>
        /// <param name="dataReader"></param>
        public Usuario(IDataReader dataReader, string alias = "")
        {

            //codigoUsuario
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.codigoUsuarioProperty))
            {
                this.CodigoUsuario = HelperValues.GetValue<string>(dataReader, alias + Usuario.codigoUsuarioProperty);
            }
            //nombre
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.nombreProperty))
            {
                this.Nombre = HelperValues.GetValue<string>(dataReader, alias + Usuario.nombreProperty);
            }
            //clave
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.claveProperty))
            {
                this.Clave = HelperValues.GetValue<string>(dataReader, alias + Usuario.claveProperty);
            }
            //identificacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.identificacionProperty))
            {
                this.Identificacion = HelperValues.GetValue<string>(dataReader, alias + Usuario.identificacionProperty);
            }
            //estado
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.estadoProperty))
            {
                this.Estado = HelperValues.GetValue<string>(dataReader, alias + Usuario.estadoProperty);
            }
            //fechaExpiracionClave
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.fechaExpiracionClaveProperty))
            {
                this.FechaExpiracionClave = HelperValues.GetValue<DateTime?>(dataReader, alias + Usuario.fechaExpiracionClaveProperty);
            }
            //intentosFallidos
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.intentosFallidosProperty))
            {
                this.IntentosFallidos = HelperValues.GetValue<Int16>(dataReader, alias + Usuario.intentosFallidosProperty);
            }
            //pendienteCambio
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.pendienteCambioProperty))
            {
                this.PendienteCambio = HelperValues.GetValue<bool>(dataReader, alias + Usuario.pendienteCambioProperty);
            }
            //correoElectronico
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.correoElectronicoProperty))
            {
                this.CorreoElectronico = HelperValues.GetValue<string>(dataReader, alias + Usuario.correoElectronicoProperty);
            }
            //idTipoIdentificacion
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.idTipoIdentificacionProperty))
            {
                this.IdTipoIdentificacion = HelperValues.GetValue<long>(dataReader, alias + Usuario.idTipoIdentificacionProperty);
            }
            //idPais
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Usuario.idPaisProperty))
            {
                this.IdPais = HelperValues.GetValue<long>(dataReader, alias + Usuario.idPaisProperty);
            }
            this.LoadProperty(dataReader, alias);
        }

        #endregion Constructores
    }//fin de clase
}

#region RespuestasUsuario
namespace POSWeb.Entidades
{
    [DataContract]
    public class RespuestaListaUsuario : Paginacion
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public List<Usuario> ListaUsuario { get; set; }
    }

    [DataContract]
    public class RespuestaUsuario
    {
        [DataMember]
        public Respuesta Respuesta { get; set; }

        [DataMember]
        public Usuario Usuario { get; set; }
    }
}
#endregion RespuestasUsuario


