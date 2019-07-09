using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    public class BitacoraObjeto
    {
        public string NombreObjeto { get; set; }
        public TIPOS_OBJETO TipoObjeto { get; set; }
        public string UsuarioRegistro { get; set; }
        public TIPOS_MODIFICACION_OBJETO TipoRegistro { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorActual { get; set; }
    }
}
