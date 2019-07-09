using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    /// <summary>
    /// Clase de productos del inventario
    /// </summary>
    [DataContract]
    [Serializable]
    public class Producto: BaseEntidad
    {

    }


    #region RespuestasProducto
    namespace POSWeb.Entidades
    {
        [DataContract]
        public class RespuestaListaProducto : Paginacion
        {
            [DataMember]
            public Respuesta Respuesta { get; set; }

            [DataMember]
            public List<Producto> ListaProducto { get; set; }
        }

        [DataContract]
        public class RespuestaProducto
        {
            [DataMember]
            public Respuesta Respuesta { get; set; }

            [DataMember]
            public Producto Producto { get; set; }
        }
    }
    #endregion RespuestasProducto
}
