using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    public class ErrorType
    {

        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [DisplayName("Mensaje")]
        public string Mensaje { get; set; }

        [DisplayName("Código Excepción")]
        public string CodigoExcepcion { get; set; }

        [DisplayName("Excepción")]
        public string Excepcion { get; set; }
    }
}
