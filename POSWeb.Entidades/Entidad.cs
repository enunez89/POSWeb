using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;

namespace POSWeb.Entidades
{
    public partial class Entidad : BaseEntidad
    {
        #region Constantes
        public const string codigoEntidadProperty = "CodigoEntidad";
        public const string nombreEntidadProperty = "NombreEntidad";
        public const string urlConectividadProperty = "UrlConectividad";
        public const string activoProperty = "Activo";
        #endregion Constantes

        [Key]
        [DisplayName("Código Entidad")]
        [StringLength(10)]
        [Required(ErrorMessage = "Código Entidad es requerido")]
        public string CodigoEntidad { get; set; }

        [DisplayName("Nombre Entidad")]
        [StringLength(255)]
        [Required(ErrorMessage = "Nombre Entidad es requerido")]
        public string NombreEntidad { get; set; }

        [DisplayName("Url Conectividad")]
        [StringLength(255)]
        [Required(ErrorMessage = "Url Conectivdad es requerido")]
        public string UrlConectividad { get; set; }

        public bool? Activo { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AccionEvento> AccionEvento { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Alerta> Alerta { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Boletin> Boletin { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Canal> Canal { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Categoria> Categoria { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Cliente> Cliente { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Cuenta> Cuenta { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CuentaFavorita> CuentaFavorita { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CuentaMancomunada> CuentaMancomunada { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Horario> Horario { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Limite> Limite { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MovimientoFondos> MovimientoFondos { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<NotificacionPush> NotificacionPush { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Moneda> Moneda { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Objeto> Objeto { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PlanillaEmpleado> PlanillaEmpleado { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Sugerencia> Sugerencia { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TipoDispositivo> TipoDispositivo { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Transferencia> Transferencia { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<UsuarioDispositivo> UsuarioDispositivo { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<NotificacionCuenta> CuentaNotificacion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Notificacion> Notificacion { get; set; }

        #region Constructores

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Entidad()
        //{
        //    AccionEvento = new HashSet<AccionEvento>();
        //    Alerta = new HashSet<Alerta>();
        //    Boletin = new HashSet<Boletin>();
        //    Canal = new HashSet<Canal>();
        //    Categoria = new HashSet<Categoria>();
        //    Cliente = new HashSet<Cliente>();
        //    Cuenta = new HashSet<Cuenta>();
        //    CuentaFavorita = new HashSet<CuentaFavorita>();
        //    CuentaMancomunada = new HashSet<CuentaMancomunada>();
        //    //Horario = new HashSet<Horario>();
        //    MovimientoFondos = new HashSet<MovimientoFondos>();
        //    NotificacionPush = new HashSet<NotificacionPush>();
        //    Limite = new HashSet<Limite>();
        //    Moneda = new HashSet<Moneda>();
        //    Objeto = new HashSet<Objeto>();
        //    PlanillaEmpleado = new HashSet<PlanillaEmpleado>();
        //    Sugerencia = new HashSet<Sugerencia>();
        //    TipoDispositivo = new HashSet<TipoDispositivo>();
        //    Transferencia = new HashSet<Transferencia>();
        //    UsuarioDispositivo = new HashSet<UsuarioDispositivo>();
        //    CuentaNotificacion = new HashSet<NotificacionCuenta>();
        //    Notificacion = new HashSet<Notificacion>();
        //}

        /// <summary>
        /// Constructor para la clase Entidad
        /// </summary>
        /// <param name="dataReader"></param>
        public Entidad(IDataReader dataReader, string alias = "")
        {

            //codigoEntidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Entidad.codigoEntidadProperty))
            {
                this.CodigoEntidad = HelperValues.GetValue<string>(dataReader, alias + Entidad.codigoEntidadProperty);
            }
            //nombreEntidad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Entidad.nombreEntidadProperty))
            {
                this.NombreEntidad = HelperValues.GetValue<string>(dataReader, alias + Entidad.nombreEntidadProperty);
            }
            //urlConectividad
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Entidad.urlConectividadProperty))
            {
                this.UrlConectividad = HelperValues.GetValue<string>(dataReader, alias + Entidad.urlConectividadProperty);
            }
            //activo
            if (HelperValues.DataReaderHasColumn(dataReader, alias + Entidad.activoProperty))
            {
                this.Activo = HelperValues.GetValue<bool>(dataReader, alias + Entidad.activoProperty);
            }
        }

        #endregion Constructores
    }

    public class EntidadRespuesta
    {

        public Entidad Entidad { get; set; }

        public Respuesta Respuesta { get; set; }
    }

    public class EntidadesRespuesta
    {

        public List<Entidad> Entidades { get; set; }

        public Respuesta Respuesta { get; set; }
    }
}
