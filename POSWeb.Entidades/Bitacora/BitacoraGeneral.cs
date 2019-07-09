using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    /// <summary>
    /// Permite el registro de eventos en la bitácora de auditoría
    /// </summary>
    [DataContract]
    [KnownType(typeof(ACCIONES))]
    public class BitacoraGeneral
    {
        [DataMember]
        public string Usuario { get; set; }
        public string PerfilUsuario { get; set; }
        public string Cliente { get; set; }
        public string Modulo { get; set; }
        public ACCIONES DetalleAccion { get; set; }
        public string ValorActual { get; set; }
        public string ValorAnterior { get; set; }

        public override string ToString()
        {
            var texto = new StringBuilder();
            texto.Append("Usuario: ").Append(Usuario).Append(" - ");
            texto.Append("Perfil Usuario: ").Append(PerfilUsuario).Append(" - ");
            texto.Append("Cliente: ").Append(Cliente).Append(" - ");
            texto.Append("Modulo: ").Append(Modulo).Append(" - ");
            texto.Append("Detalle Accion: ").Append(DetalleAccion);
            texto.Append("Valor Actual: ").Append(ValorActual);
            texto.Append("Valor Anterior: ").Append(ValorAnterior);
            return texto.ToString();
        }
    }
}
