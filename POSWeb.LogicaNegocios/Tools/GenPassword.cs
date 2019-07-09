using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using POSWeb.Utilidades;

namespace POSWeb.LogicaNegocios.Tools
{
    public class LnGenPassword
    {
        private enum TipoCaracterEnum { Minuscula, Mayuscula, Simbolo, Numero }

        #region Campos
        public int cantidadMayusculas;
        public int cantidadSimbolos;
        public int cantidadNumeros;
        public int longitudPassword;
        public int longitudCaracteres;
        Random semilla;

        // Caracteres que pueden emplearse en la contraseña
        string caracteres = "abcdefghijklmnopqrstuvwxyz";
        string numeros = "0123456789";
        string simbolos = "%$#@+-=&*.";

        // Cadena que contiene el password generado
        private StringBuilder password;
        #endregion

        #region Constructores
        public LnGenPassword()
        { }

        public LnGenPassword(int longitudPassword, int cantidadMayusculas, int cantidadSimbolos, int cantidadNumeros)
        {
            if (cantidadMayusculas + cantidadSimbolos + cantidadNumeros > longitudPassword)
                throw new ArgumentOutOfRangeException(
                "La suma de los caracteres especiales" +
                "no puede ser superior a la longitud de la contraseña");
            semilla = new Random(DateTime.Now.Millisecond);
        }
        #endregion
       
        #region Métodos públicos

        public string GetNewPassword()
        {
            var catalogoLn = new CatalogoLogicaNegocio();

            /*************  Obtener Longitud *******************/
            longitudPassword = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Entidades.Catalogo() { CodigoParametro = Constantes.Catalogo.LongitudPassword}).Catalogo.Identificador);
            /*************  Obtener Numeros *******************/
            cantidadNumeros = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Entidades.Catalogo() { CodigoParametro = Constantes.Catalogo.CntMinNumPassword }).Catalogo.Identificador);
            /*************  Obtener Mayusculas *******************/
            cantidadMayusculas = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Entidades.Catalogo() { CodigoParametro = Constantes.Catalogo.CntMinMayusculasPassword }).Catalogo.Identificador);
            /*************  Obtener Simbolos *******************/
            cantidadSimbolos = Convert.ToInt32(catalogoLn.ObtenerCatalogo(new Entidades.Catalogo() { CodigoParametro = Constantes.Catalogo.CntMinSimbolosPassword }).Catalogo.Identificador);

            semilla = new Random(DateTime.Now.Millisecond);

            GeneraPassword();
            return password.ToString();
        }



        #endregion

        #region Métodos de cálculo

        private void GeneraPassword()
        {
            password = new StringBuilder(longitudPassword);
            for (int i = 0; i < longitudPassword; i++)
            {
                password.Append(GetCaracterAleatorio(TipoCaracterEnum.Minuscula));
            }

            // Se obtienen las posiciones en las que irán los caracteres especiales
            int[] caracteresEspeciales =
                    GetPosicionesCaracteresEspeciales(cantidadMayusculas + cantidadSimbolos + cantidadNumeros);
            int posicionInicial = 0;
            int posicionFinal = 0;

            // Se reemplazan las mayúsculas
            posicionFinal += cantidadMayusculas;
            ReemplazaCaracteresEspeciales(caracteresEspeciales,
                 posicionInicial, posicionFinal, TipoCaracterEnum.Mayuscula);

            // Se reemplazan los símbolos
            posicionInicial = posicionFinal;
            posicionFinal += cantidadSimbolos;
            ReemplazaCaracteresEspeciales(caracteresEspeciales,
                 posicionInicial, posicionFinal, TipoCaracterEnum.Simbolo);

            // Se reemplazan los Números
            posicionInicial = posicionFinal;
            posicionFinal += cantidadNumeros;
            ReemplazaCaracteresEspeciales(caracteresEspeciales,
                 posicionInicial, posicionFinal, TipoCaracterEnum.Numero);
        }



        /// <summary>
        /// Reemplaza un caracter especial en la cadena Password
        /// </summary>
        private void ReemplazaCaracteresEspeciales(
                                        int[] posiciones
                                        , int posicionInicial
                                        , int posicionFinal
                                        , TipoCaracterEnum tipoCaracter)
        {
            for (int i = posicionInicial; i < posicionFinal; i++)
            {
                password[posiciones[i]] = GetCaracterAleatorio(tipoCaracter);
            }
        }



        private int[] GetPosicionesCaracteresEspeciales(int numeroPosiciones)
        {
            List<int> lista = new List<int>();
            while (lista.Count < numeroPosiciones)
            {
                int posicion = semilla.Next(0, longitudPassword);
                if (!lista.Contains(posicion))
                {
                    lista.Add(posicion);
                }
            }
            return lista.ToArray();
        }


        /// <summary>
        /// Obtiene un carácter aleatorio en base a la "matriz" del tipo de caracteres
        /// </summary>
        private char GetCaracterAleatorio(TipoCaracterEnum tipoCaracter)
        {
            string juegoCaracteres;
            switch (tipoCaracter)
            {
                case TipoCaracterEnum.Mayuscula:
                    juegoCaracteres = caracteres.ToUpper();
                    break;
                case TipoCaracterEnum.Minuscula:
                    juegoCaracteres = caracteres.ToLower();
                    break;
                case TipoCaracterEnum.Numero:
                    juegoCaracteres = numeros;
                    break;
                default:
                    juegoCaracteres = simbolos;
                    break;
            }

            // índice máximo de la matriz char de caracteres
            int longitudJuegoCaracteres = juegoCaracteres.Length;

            // Obtención de un número aletorio para obtener la posición del carácter
            int numeroAleatorio = semilla.Next(0, longitudJuegoCaracteres);

            // Se devuelve una posición obtenida aleatoriamente
            return juegoCaracteres[numeroAleatorio];
        }


        public string Encriptar(string text)
        {
            string pwd = "Novabnk2015;";
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }
        byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        byte[] GetRandomBytes()
        {
            int _saltSize = 4;
            byte[] ba = new byte[_saltSize];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        #endregion
    }
}
