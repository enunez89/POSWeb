using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    [DataContract]
    public class MensajeExcepcion
    {
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Detalle { get; set; }

        [DataMember]
        public string ClaseOrigen { get; set; }

        [DataMember]
        public string MetodoOrigen { get; set; }

        [DataMember]
        public string IdentificacionUsuario { get; set; }

        [DataMember]
        public string TramaEntrada { get; set; }

        [DataMember]
        public string TramaSalida { get; set; }

        public override string ToString()
        {
            var texto = new StringBuilder();
            texto.Append("Codigo: ").Append(Codigo).Append(" - ");
            texto.Append("Descripcion: ").Append(Descripcion).Append(" - ");
            texto.Append("Detalle: ").Append(Detalle).Append(" - ");
            texto.Append("ClaseOrigen: ").Append(ClaseOrigen).Append(" - ");
            texto.Append("Método Oginen: ").Append(MetodoOrigen).Append(" - ");
            texto.Append("Usuario: ").Append(IdentificacionUsuario);
            return texto.ToString();
        }
    }
}
