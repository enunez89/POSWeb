using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace POSWeb.Utilidades
{
    public static class SerializarObjeto
    {
        /// <summary>
        ///     Método que devuelve un string en formato XML que representa el objeto enviado
        /// </summary>
        /// <typeparam name="T">Objeto heredado de la clase Object</typeparam>
        /// <param name="pObjeto">Objeto que se desea representar como XML</param>
        /// <returns>Retorna un string en formato XML que representa el objeto enviado</returns>
        public static string Serializar<T>(T pObjeto)
        {
            var xmlSerializer = new XmlSerializer(pObjeto.GetType());
            var textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, pObjeto);

            return limpiarXML(textWriter.ToString());
        }

        /// <summary>
        ///     Método que devuelve un string en formato XML que representa el objeto enviado
        /// </summary>
        /// <typeparam name="T">Objeto heredado de la clase Object</typeparam>
        /// <param name="pObjeto">Objeto que se desea representar como XML</param>
        /// <returns>Retorna un string en formato XML que representa el objeto enviado</returns>
        public static string SerializarEntidad<T>(T pObjeto, string nombre)
        {
            var xmlSerializer = new XmlSerializer(pObjeto.GetType());
            var textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, pObjeto);

            return limpiarXML(textWriter.ToString());
        }

        /// <summary>
        ///     Método que devuelve un string en formato XML de cada objeto enviado en la lista
        /// </summary>
        /// <param name="pLista">Lista de objetos que se desean representar como XML</param>
        /// <returns>Retorna un string en formato XML que representa todos los objetos enviados en la lista</returns>
        public static string Serializar(List<Object> pLista)
        {
            var texto = new StringBuilder();

            foreach (Object objeto in pLista)
            {
                texto.Append(Serializar(objeto));
            }

            return texto.ToString();
        }

        /// <summary>
        ///     Método que devuelve un string en formato XML de tipo Item para los objetos enviados en la Lista
        /// </summary>
        /// <param name="pLista">Lista de objetos a incluir en el Item</param>
        /// <param name="pNombreEntidad">Nombre de la entidad en Base de Datos</param>
        /// <param name="pConsecutivo">Consecutivo con el que se desea que se genere el item</param>
        /// <returns></returns>
        public static string CrearItems(List<Object> pLista, string pNombreEntidad, int pConsecutivo)
        {
            var texto = new StringBuilder();
            texto.Append("<ITEM Id=\"" + pConsecutivo + "\" Entidad=\"" + pNombreEntidad + "\">");
            texto.Append("\n\r");
            texto.Append(Serializar(pLista));
            texto.Append("\n\r");
            texto.Append("</ITEM>");

            return texto.ToString();
        }

        /// <summary>
        ///     Método para eliminar del string en formato XML la infomación no deseada
        /// </summary>
        /// <param name="pTexto">Texto en formato XML</param>
        /// <returns>String en formato XML sin la información indeseada</returns>
        private static string limpiarXML(string pTexto)
        {
            pTexto = pTexto.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            pTexto =
                pTexto.Replace(
                    " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"",
                    "");
            pTexto = pTexto.Replace(" xsi:nil=\"true\" ", "");

            pTexto = pTexto.Replace("\"", "'");
            pTexto =
                pTexto.Replace(
                    " xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'",
                    "");
            pTexto = pTexto.Replace("\r\n ", "");
            pTexto = pTexto.Replace("\r\n", "");

            return pTexto;
        }
    }
}
