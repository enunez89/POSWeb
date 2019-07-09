using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSWeb.Entidades;
using POSWeb.Encriptacion;

namespace POSWeb.Utilidades
{
    /// <summary>
    /// Métodos para la encripción y desencripción de Valores
    /// </summary>
    public static class AdministrarEncriptacion
    {
        /// <summary>
        /// Encripta el valor enviado usando la dll de Servicios Generales TI
        /// </summary>
        /// <param name="pValor">Valor a Encriptar</param>
        /// <returns>Valor encriptado</returns>
        public static Respuesta EncriptarValor(string pValor)
        {
            string LLAVE_ENCRIPTACION = "BM.Enterprise.2017";
            var respuesta = new Respuesta();

            try
            {
                var valorEncriptado = Encriptador.EncriptarString(pValor, LLAVE_ENCRIPTACION);
                respuesta.CodMensaje = Respuesta.CodExitoso;
                respuesta.Mensaje = valorEncriptado;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.CodMensaje = Respuesta.CodError;
                respuesta.Mensaje = string.Format("Error al encriptar valor: {0} - Se produjo el siguiente error: {1}", pValor, ex.ToString());
                return respuesta;
            }
        }

        /// <summary>
        /// Desencripta el valor enviado usando la dll de Servicios Generales TI
        /// </summary>
        /// <param name="pValor">Valor a desencriptar</param>
        /// <returns>Valor desencriptado</returns>
        public static Respuesta DesencriptarValor(string pValor)
        {
            string LLAVE_ENCRIPTACION = "BCRGSATI";
            var respuesta = new Respuesta();

            try
            {
                var valorEncriptado = Encriptador.DesencriptarString(pValor, LLAVE_ENCRIPTACION);
                respuesta.CodMensaje = Respuesta.CodExitoso;
                respuesta.Mensaje = valorEncriptado;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.CodMensaje = Respuesta.CodError;
                respuesta.Mensaje = string.Format("Error al encriptar valor: {0} - Se produjo el siguiente error: {1}", pValor, ex.ToString());
                return respuesta;
            }
        }
    }
}
