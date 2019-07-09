using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Encriptacion
{
    public class Encriptador
    {
        const bool UsarHashParaLlaveEncriptacion = true;
        static byte[] DEFAULT_HASH_SALT = { 0xcd, 0xd4, 0xe1, 0x9e, 0x08, 0x83, 0xaf, 0x15, 
                                           0xc0, 0xe4, 0xe9, 0x0e, 0xa7, 0xe0, 0x06, 0xef,
                                           0xc3, 0x87, 0xc5, 0x0c, 0xad, 0xb6, 0xfc, 0xba,
                                           0xb4, 0xba, 0x9b, 0x97, 0x17, 0xf0, 0x87, 0xef };

        #region Métodos para manejo String

        /// <summary>
        /// Método Para Encriptar
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Encriptar(string text)
        {
            string key = Config.stringKey;
            string crypto = EncriptarString(text, key);
            return crypto;
        }

        /// <summary>
        /// Método para Descriptar
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string DesEncriptar(string text)
        {

            string key = Config.stringKey;
            string secret = DesencriptarString(text, key);
            return secret;
        }

        /// <summary>
        /// Método que genera una Salt a partir de un string
        /// Este método se recomienda siempre que se pueda almacenar la salt junto con el valor hashed
        /// </summary>
        /// <returns>Random string con Hash generado</returns>
        public static byte[] GenerarSalt(int largo = 32)
        {
            byte[] bytes = new byte[largo];
            new RNGCryptoServiceProvider().GetBytes(bytes);

            return bytes;
        }

        /// <summary>
        /// Método que genera una Salt a partir de un string
        /// Este método se recomienda siempre que se pueda almacenar la sal junto con el valor hashed
        /// </summary>
        /// <returns>string con Hash generado</returns>
        public static byte[] GenerarSalt(String pTexto)
        {
            using (var stream = new MemoryStream())
            {
                byte[] bytes = UTF8Encoding.UTF8.GetBytes(pTexto);

                stream.Write(bytes, 0, bytes.Length);
                stream.Write(DEFAULT_HASH_SALT, 0, DEFAULT_HASH_SALT.Length);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Método que genera un Hash a partir de un string
        /// </summary>
        /// <param name="pTexto">Texto para generar Hash</param>
        /// <param name="pSalt">Salt para aplicar al hash, puede venir en null pero no se recomienda</param>
        /// <returns>string con Hash generado</returns>
        public static String HashString(String pTexto, byte[] pSalt)
        {
            var contenidoHash = HashContenido(UTF8Encoding.UTF8.GetBytes(pTexto), pSalt);

            return Base64String(contenidoHash);
        }

        /// <summary>
        /// Método que genera un Hash a partir de un string
        /// </summary>
        /// <param name="pTexto">Texto para generar Hash</param>
        /// <param name="pSalt">Salt para aplicar al hash, puede venir en null pero no se recomienda</param>
        /// <returns>string con Hash generado</returns>
        public static String HashString(String pTexto, String pTextoSalt)
        {
            var contenidoHash = HashContenido(UTF8Encoding.UTF8.GetBytes(pTexto), GenerarSalt(pTextoSalt));

            return Base64String(contenidoHash);
        }

        /// <summary>
        /// Método para encriptar texto a partir de una llave privada
        /// </summary>
        /// <param name="pContenidoParaEncriptar">Texto a encriptar</param>
        /// <param name="pLlaveEncriptacion">Llave privada para la encripción</param>
        /// <returns>Texto encriptado</returns>
        public static String EncriptarString(String pContenidoParaEncriptar, String pLlaveEncriptacion)
        {
            byte[] contenidoEncriptado = Encriptar(UTF8Encoding.UTF8.GetBytes(pContenidoParaEncriptar), pLlaveEncriptacion);

            return Base64String(contenidoEncriptado);
        }

        /// <summary>
        /// Desencripta texto mediante llave privada enviada
        /// </summary>
        /// <param name="pTextoParaDesencriptar">Texto encriptado</param>
        /// <param name="pLlaveEncriptacion">Llave privada utilizada durante la encripcion</param>
        /// <returns>Texto desencriptado</returns>
        public static String DesencriptarString(String pTextoParaDesencriptar, String pLlaveEncriptacion)
        {
            byte[] contenidoDesencriptado = Desencriptar(Base64ByteArray(pTextoParaDesencriptar), pLlaveEncriptacion);

            return UTF8Encoding.UTF8.GetString(contenidoDesencriptado);
        }

        #endregion

        #region Métodos para manejo byte[]

        /// <summary>
        /// Genera un Hash a partir de un arreglo de bytes
        /// </summary>
        /// <param name="pContenido">Arreglo de bytes para generar Hash</param>
        /// <param name="pSalt">Salt utilizada para hacer el cracking del Hash dificil</param>
        /// <returns>Arreglo de bytes con el Hash generado</returns>
        public static byte[] HashContenido(byte[] pContenido, byte[] pSalt)
        {
            var hashmd5 = new MD5CryptoServiceProvider();

            try
            {
                using (var stream = new MemoryStream())
                {
                    byte[] bytes = (pSalt != null && pSalt.Length > 0) ? (pSalt) : (DEFAULT_HASH_SALT);

                    stream.Write(bytes, 0, bytes.Length);
                    stream.Write(pContenido, 0, pContenido.Length);
                    stream.Seek(0, SeekOrigin.Begin);

                    return hashmd5.ComputeHash(stream);
                }
            }
            finally
            {
                // Libera recursos
                hashmd5.Clear();
            }
        }

        /// <summary>
        /// Encripta un arreglo de bytes mediante una llave privada proporcionada
        /// </summary>
        /// <param name="pContenidoParaEncriptar">Arreglo de bytes a encriptar</param>
        /// <param name="pLlaveEncriptacion">Llave privada de encripción</param>
        /// <returns>Retorna un arreglo de bytes encriptado</returns>
        public static byte[] Encriptar(byte[] pContenidoParaEncriptar, String pLlaveEncriptacion)
        {
            var tdes = new TripleDESCryptoServiceProvider();

            try
            {
                var llaveEncriptacionBytes = UTF8Encoding.UTF8.GetBytes(pLlaveEncriptacion);

                tdes.Key = (UsarHashParaLlaveEncriptacion) ? (HashContenido(llaveEncriptacionBytes, DEFAULT_HASH_SALT)) : (llaveEncriptacionBytes);

                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                //transform the specified region of bytes array to resultArray
                return cTransform.TransformFinalBlock(pContenidoParaEncriptar, 0, pContenidoParaEncriptar.Length);
            }
            finally
            {
                // Libera recursos
                tdes.Clear();
            }
        }

        /// <summary>
        /// Desencripta un arreglo de bytes
        /// </summary>
        /// <param name="pTextoParaDesencriptar">Arreglo de bytes para desencriptar</param>
        /// <param name="pLlaveEncriptacion">Llave privada utilizada en la encripcion</param>
        /// <returns>Arreglo de bytes desencriptado</returns>
        public static byte[] Desencriptar(byte[] pContenidoParaDesencriptar, String pLlaveEncriptacion)
        {
            var tdes = new TripleDESCryptoServiceProvider();

            try
            {
                var llaveEncriptacionBytes = UTF8Encoding.UTF8.GetBytes(pLlaveEncriptacion);

                //set the secret key for the tripleDES algorithm
                tdes.Key = (UsarHashParaLlaveEncriptacion) ? (HashContenido(llaveEncriptacionBytes, DEFAULT_HASH_SALT)) : (llaveEncriptacionBytes);

                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                return cTransform.TransformFinalBlock(pContenidoParaDesencriptar, 0, pContenidoParaDesencriptar.Length);
            }
            finally
            {
                // Libera los recursos
                tdes.Clear();
            }
        }

        #endregion

        #region Métodos Privados
        private static byte[] Base64ByteArray(String pTexto)
        {
            return Convert.FromBase64String(pTexto);
        }

        private static String Base64String(byte[] pContenido)
        {
            return Convert.ToBase64String(pContenido, 0, pContenido.Length);
        }
        #endregion
    }
}
