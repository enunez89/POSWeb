using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    [DataContract]
    [Serializable]
    public class Paginacion
    {
        #region Constantes

        public const string numPaginaProperty = "NumPagina";
        public const string tamanoPaginaProperty = "TamanoPagina";
        public const string totalRegistrosProperty = "TotalRegistros";

        #endregion Constantes

        #region Propiedades
        [DataMember]
        public int NumPagina { get; set; }

        [DataMember]
        public int TamanoPagina { get; set; }

        [DataMember]
        public int TotalRegistros { get; set; }

        #endregion Propiedades


        public Paginacion()
        {
            try
            {
                NumPagina = NumPagina - 1;
            }
            catch
            { }
        }


    }
}
