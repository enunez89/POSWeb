using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    [DataContract]
    public class Bitacora
    {
        [DataMember]
        /// <summary>
        /// Aplicación que registra la bitácora
        /// <summary>
        public string Aplicacion { get; set; }

        [DataMember]
        /// <summary>
        /// Módulo desde el que se llevó a cabo la acción.
        /// <summary>
        public string Modulo { get; set; }

        [DataMember]
        /// <summary>
        /// Acción realizada
        /// <summary>
        public int Accion { get; set; }

        [DataMember]
        /// <summary>
        /// Fecha en la que se llevó a cabo la acción.
        /// <summary>
        public DateTime FechaAccion { get; set; }

        [DataMember]
        /// <summary>
        ///Detalle de la acción
        /// <summary>
        public string DetalleAccion { get; set; }

        [DataMember]
        /// <summary>
        /// Nombre del equipo desde el cual se llevó a cabo la acción.
        /// <summary>
        public string NombreEquipo { get; set; }

        [DataMember]
        /// <summary>
        /// Dirección IP del equipo desde el cual se llevó a cabo la acción
        /// <summary>
        public string DireccionIp { get; set; }

        [DataMember]
        /// <summary>
        /// Usuario que realiza la acción
        /// <summary>
        public string Usuario { get; set; }

        [DataMember]
        /// <summary>
        /// Perfil del usuario que realiza la accion
        /// <summary>
        public string PerfilUsuario { get; set; }

        [DataMember]
        /// <summary>
        ///Valor anterior.
        /// <summary>
        public string ValorAnterior { get; set; }

        [DataMember]
        /// <summary>
        ///Nuevo valor.
        /// <summary>
        public string ValorActual { get; set; }

        [DataMember]
        /// <summary>
        ///Trama enviada.
        /// <summary>
        public string TramaEnviada { get; set; }

        [DataMember]
        /// <summary>
        ///Trama recibida.
        /// <summary>
        public string TramaRecibida { get; set; }

        [DataMember]
        /// <summary>
        ///Cliente sobre el cual se llevó a cabo la acción
        /// <summary>
        public string Cliente { get; set; }

        [DataMember]
        /// <summary>
        ///código de autorización recibido
        /// <summary>
        public string CodigoAutorizacion { get; set; }

        [DataMember]
        /// <summary>
        ///Monto de la transacción
        /// <summary>
        public decimal MontoTransaccion { get; set; }

        [DataMember]
        /// <summary>
        ///Moneda de la transacción
        /// <summary>
        public string MonedaTransaccion { get; set; }

        [DataMember]
        /// <summary>
        ///Número de control
        /// <summary>
        public string NumeroControl { get; set; }

        public override string ToString()
        {
            StringBuilder texto = new StringBuilder();
            texto.Append("Aplicación: " + Aplicacion);
            texto.Append(", Modulo: " + Modulo);
            texto.Append(", Acción: " + Accion);
            texto.Append(", FechaAccion: " + (FechaAccion != null ? FechaAccion.ToString("dd-MM-yyyy HH:mm:ss") : ""));
            texto.Append(", DetalleAcción: " + DetalleAccion);
            texto.Append(", NombreEquipo: " + NombreEquipo);
            texto.Append(", DireccionIp: " + DireccionIp);
            texto.Append(", Usuario: " + Usuario);
            texto.Append(", PerfilUsuario: " + PerfilUsuario);
            texto.Append(", ValorAnterior: " + ValorAnterior);
            texto.Append(", ValorActual: " + ValorActual);
            texto.Append(", TramaEnviada: " + TramaEnviada);
            texto.Append(", TramaRecibida: " + TramaRecibida);
            texto.Append(", Usuario: " + Usuario);
            texto.Append(", Cliente: " + Cliente);
            texto.Append(", CodigoAutorizacion: " + CodigoAutorizacion);
            texto.Append(", MontoTransaccion: " + MontoTransaccion);
            texto.Append(", MonedaTransaccion: " + MonedaTransaccion);
            texto.Append(", NumeroControl: " + NumeroControl);

            return texto.ToString();
        }
    }
}
