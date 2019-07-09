using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using POSWeb.Entidades;
using POSWeb.Utilidades;
using System.IO;

namespace POSWeb.LogicaNegocios
{
    /// <summary>
    /// Registra Bitácoras del Sistema tanto para eventos como para excepciones
    /// </summary>
    public static class Bitacoras
    {
        /// <summary>
        /// Registra bitácoras de auditoría
        /// </summary>
        /// <param name="pBitacora">bitácora a registrar</param>
        public static void RegistrarBitacora(BitacoraGeneral pBitacora)
        {
            try
            {
                //Se obtiene el código de aplicación
                var parametroAplicacion = AdministrarConfiguracion.ObtenerParametro("Codigo_Servicios_Generales");
                var tipoBinding = AdministrarConfiguracion.ObtenerParametro("Tipo_Binding");
                var binding = AdministrarConfiguracion.ObtenerBinding("SvAdministrarBitacoras", tipoBinding.Mensaje);

                if (parametroAplicacion.CodMensaje == Respuesta.CodExitoso)
                {
                    //using (var proxy = new SvAdministrarBitacoras.SvAdministrarBitacorasClient(binding))
                    //{
                    //    var bitacora = new SvAdministrarBitacoras.Bitacora();
                    //    //Datos Generales
                    //    bitacora.Aplicacion = parametroAplicacion.Mensaje;
                    //    bitacora.FechaAccion = DateTime.Now;
                    //    bitacora.NombreEquipo = Environment.MachineName;
                    //    bitacora.DireccionIp = AdministrarDatosEquipo.ObtenerDireccionIP();

                    //    ///Datos provenientes del parámetro
                    //    bitacora.Usuario = pBitacora.Usuario;
                    //    bitacora.PerfilUsuario = pBitacora.PerfilUsuario;
                    //    bitacora.Cliente = pBitacora.Cliente;
                    //    bitacora.Modulo = pBitacora.Modulo;
                    //    bitacora.DetalleAccion = pBitacora.DetalleAccion.ToString();
                    //    bitacora.ValorActual = pBitacora.ValorActual;
                    //    bitacora.ValorAnterior = pBitacora.ValorAnterior;

                    //    //Datos no implementados
                    //    bitacora.Accion = 0;
                    //    bitacora.CodigoAutorizacion = "N/A";
                    //    bitacora.MonedaTransaccion = "N/A";
                    //    bitacora.MontoTransaccion = 0;
                    //    bitacora.NumeroControl = "N/A";
                    //    bitacora.TramaEnviada = "</>";
                    //    bitacora.TramaRecibida = "</>";

                    //    //Se registra la bitácora
                    //    proxy.RegistrarBitacora(bitacora);
                    //}
                }
                else
                {
                    var error = new MensajeExcepcion();
                    error.Codigo = (int)CODIGOS_ERROR.ServicioBitacoras;
                    error.Descripcion = "Error al registrar bitácora";
                    error.ClaseOrigen = MethodBase.GetCurrentMethod().ReflectedType.Name;
                    error.MetodoOrigen = MethodBase.GetCurrentMethod().Name;
                    error.Detalle = string.Format("{0}: {1} - Bitacora a registrar es: {2}", "Error al obtener el parámetro: Codigo_Servicios_Generales", pBitacora.ToString());
                    Bitacoras.RegistrarExcepcion(error);
                }
            }
            catch (Exception ex)
            {
                var error = new MensajeExcepcion();
                error.Codigo = (int)CODIGOS_ERROR.ServicioBitacoras;
                error.Descripcion = "Error al registrar bitácora";
                error.ClaseOrigen = MethodBase.GetCurrentMethod().ReflectedType.Name;
                error.MetodoOrigen = MethodBase.GetCurrentMethod().Name;
                error.Detalle = string.Format("{0}: {1} - Bitacora a registrar es: {2}", error.Descripcion, ex.ToString(), pBitacora.ToString());
                Bitacoras.RegistrarExcepcion(error);
            }
        }

        /// <summary>
        /// Registra excepciones
        /// </summary>
        /// <param name="pExcepcion">Excepción a registrar</param>
        public static void RegistrarExcepcion(MensajeExcepcion pExcepcion)
        {
            try
            {
                //Se obtiene el código de aplicación
                var parametroAplicacion = AdministrarConfiguracion.ObtenerParametro("Codigo_Servicios_Generales");
                var tipoBinding = AdministrarConfiguracion.ObtenerParametro("Tipo_Binding");
                var binding = AdministrarConfiguracion.ObtenerBinding("SvAdministrarExcepciones", tipoBinding.Mensaje);

                if (parametroAplicacion.CodMensaje == Respuesta.CodExitoso)
                {
                    //using (var proxy = new SvAdministrarExcepciones.SvAdministrarExcepcionesClient(binding))
                    //{
                    //    var excepcion = new SvAdministrarExcepciones.Excepcion();
                    //    //Datos Generales
                    //    excepcion.Aplicacion = parametroAplicacion.Mensaje;
                    //    excepcion.Equipo = Environment.MachineName;
                    //    excepcion.DireccionIp = AdministrarDatosEquipo.ObtenerDireccionIP();

                    //    ///Datos provenientes del parámetro
                    //    excepcion.CodigoError = pExcepcion.Codigo;
                    //    excepcion.DetalleTecnico = pExcepcion.Detalle;
                    //    excepcion.Modulo = pExcepcion.ClaseOrigen;
                    //    excepcion.Operacion = pExcepcion.MetodoOrigen;
                    //    excepcion.Usuario = pExcepcion.IdentificacionUsuario;

                    //    //Datos no implementados
                    //    excepcion.Oficina = "N/A";
                    //    excepcion.ObjetoExcepcion = "N/A";
                    //    excepcion.TramaEntrada = "</>";
                    //    excepcion.TramaSalida = "</>";

                    //    //Se registra la bitácora
                    //    proxy.RegistrarExcepcion(excepcion);
                    //}
                }
                else
                {
                    GuardarLog(pExcepcion.ToString(), new Exception("No se pudo obtener el parametro: Codigo_Servicios_Generales").ToString());
                }
            }
            catch (Exception ex)
            {
                GuardarLog(pExcepcion.ToString(), ex.ToString());
            }
        }

        /// <summary>
        /// Crea un log en caso de que el servicio de excepciones genere un error
        /// </summary>
        /// <param name="pExcepcionOriginal"></param>
        /// <param name="ExcepcionServicio"></param>
        static void GuardarLog(string pExcepcionOriginal, string ExcepcionServicio)
        {
            try
            {
                //Variables para grabar el log
                int tamanoMaximo = 3000 * 1024;     //Tamano del archivo en Kb
                int indice = 1;                     //Indice que indica la cantidad de logs creados
                bool continuarBuscando = true;      //Indica si se debe continuar buscando el nombre del archivo

                //Se obtiene la ruta y el nombre para crear el log
                string rutaLog = AdministrarConfiguracion.ObtenerParametro("Ruta_Log").Mensaje;

                //Se verifica si se encontró la ruta, de lo contrario se colocará en la ruta donde corre el servicio o aplicación
                if (string.IsNullOrEmpty(rutaLog))
                {
                    rutaLog = @"\";
                }
                //Si la ruta es válida se guarda el Log
                if (rutaLog != string.Empty)
                {
                    //Construcción del texto que será escrito en el Log
                    var textoLog = new StringBuilder();
                    textoLog.AppendFormat("{0}- ", DateTime.Now.ToString());
                    textoLog.Append("Ocurrio un error al registrar el error: ");
                    textoLog.Append(ExcepcionServicio);
                    textoLog.Append(" Los datos enviados al Servicio fueron: ");
                    textoLog.Append(pExcepcionOriginal);
                    textoLog.Append("\r\n"); //agrega una línea en blanco para separar los mensajes

                    //Verifica si el archivo existe y el tamaño actual
                    while (continuarBuscando)
                    {
                        //Construcción del nombre del archivo
                        var nombreArchivo = new StringBuilder();
                        nombreArchivo.Append(rutaLog + "/");
                        nombreArchivo.Append(DateTime.Now.ToString("yyyyMMdd"));
                        nombreArchivo.Append("-").Append("Log_GSATI").Append("_");
                        nombreArchivo.Append(indice.ToString());
                        nombreArchivo.Append(".log");

                        //Se verifica ya existe un log con el mismo nombre
                        if (File.Exists(nombreArchivo.ToString()))
                        {
                            var propiedades = new FileInfo(nombreArchivo.ToString()); //Obtiene las propiedades del archivo de log

                            //Se verifica si el archivo ya alcanzó el tamaño máximo
                            if (propiedades.Length < tamanoMaximo)
                            {
                                //Se crea el log
                                var fichero = new StreamWriter(nombreArchivo.ToString(), true);
                                fichero.WriteLine(textoLog.ToString());
                                fichero.Flush();
                                fichero.Close();

                                continuarBuscando = false;
                            }
                            else //Archivo existe y alcanzó el límite
                            {
                                indice++;
                            }
                        }
                        else  //Log no existe por lo que se crea
                        {
                            var fichero = new StreamWriter(nombreArchivo.ToString(), true);
                            fichero.WriteLine(textoLog.ToString());
                            fichero.Flush();
                            fichero.Close();

                            continuarBuscando = false;
                        }
                    }
                }
            }
            catch
            {
                //No implementado ya que el error no puede ser enviado al servicio de Errores
            }
        }
    }
}
