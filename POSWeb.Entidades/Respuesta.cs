using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using POSWeb.Entidades.ResourceFiles;
using System.Resources;

namespace POSWeb.Entidades
{
    public enum TipoRespuesta
    {
        Exitoso,
        SessionExpirada,
        Error,
        NoEncontrado,
        Desconocido,
        Excepcion,
        RegistroRepetido,
        NoValido,
        NoPermitido,
        FondosInsuficientes,
        CuentaInactiva,
        TokenIncorrecto

    }
    [DataContract]
    [Serializable]
    public class Respuesta
    {
        private const string _mensajeExitoso = "Proceso realizado en forma exitosa";
        private const string _mensajeExcepcion = "Ha ocurrido una excepción";
        private const string _mensajeSessionExpirada = "Sesión expirada";
        private const string _mensajeRegistroRepetido = "Registro ya existe en la base de datos";
        private const string _mensajeError = "Ha ocurrido un error";
        private const string _mensajeNoEncontrado = "Registro no encontrado";
        private const string _mensajeNoPermitido = "El usuario no puede activar este registro.";
        private const string _mensajeFondosInsuficientes = "Fondos Insuficientes.";
        private const string _mensajeCuentaInactiva = "Cuenta Destino Inactiva o Bloqueada.";
        private const string _mensajeTokenIncorrecto = "El Token digitado es incorrecto.";

        //Codigos de mensajes por defecto
        private const string _codExitoso = "00";
        private const string _codFondosInsuficientes = "02";
        private const string _codCuentaInactiva = "03";
        private const string _codExcepcion = "99";
        private const string _sessionExpired = "98";
        private const string _codRegistroRepetido = "97";
        private const string _codError = "96";
        private const string _codNoEncontrado = "95";
        private const string _codNoPermitido = "94";
        private const string _codTokenIncorrecto = "93";
        private const string _codUsuarioNoExiste = "U03";
        private const string _codUsuarioBloqueado = "U02";
        private const string _codUsuarioInactivo = "U01";
        private const string _codPassVencido = "U04";
        private const string _codUsrPendCambioContra = "U07";


        private string _codMensaje = string.Empty;
        private List<string> _mensajes = new List<string>();
        private string _mensaje = string.Empty;

        /// <summary>
        /// Contruye un mensaje por defecto como desconocido
        /// </summary>
        public Respuesta()
        {
            //this._mensajes.Add(_mensajeExcepcion);
            //this._codMensaje = _codExcepcion;
            //this._mensaje = _mensajeExcepcion;
        }
        /// <summary>
        /// Contruye un mensaje por Código; 00 o "" = exitoso
        /// </summary>
        /// <param name="pCodMensaje"></param>
        public Respuesta(string pCodMensaje)
        {
            this._codMensaje = pCodMensaje;
            switch (pCodMensaje)
            {
                case _codExitoso:
                    this._mensajes.Add(_mensajeExitoso);
                    this._mensaje = _mensajeExitoso;
                    break;
                case _sessionExpired:
                    this._mensajes.Add(_mensajeSessionExpirada);
                    this._mensaje = _mensajeSessionExpirada;
                    break;
                case _codExcepcion:
                    this._mensajes.Add(_mensajeExcepcion);
                    this._mensaje = _mensajeExcepcion;
                    break;
                case _codNoEncontrado:
                    this._mensajes.Add(_mensajeNoEncontrado);
                    this._mensaje = _mensajeNoEncontrado;
                    break;
                case _codRegistroRepetido:
                    this._mensajes.Add(_mensajeError);
                    this._mensaje = _mensajeError;
                    break;
                case _codError:
                    ResourceManager rm = Respuestas.ResourceManager;
                    this._mensajes.Add(rm.GetString(_codError));
                    this._mensaje = rm.GetString(_codError);
                    break;
                case _codNoPermitido:
                    this._mensajes.Add(_mensajeNoPermitido);
                    this._mensaje = _mensajeNoPermitido;
                    break;
                case _codFondosInsuficientes:
                    this._mensajes.Add(_mensajeFondosInsuficientes);
                    this._mensaje = _mensajeFondosInsuficientes;
                    break;
                case _codCuentaInactiva:
                    this._mensajes.Add(_mensajeCuentaInactiva);
                    this._mensaje = _mensajeCuentaInactiva;
                    break;
                case _codTokenIncorrecto:
                    this._mensajes.Add(_mensajeTokenIncorrecto);
                    this._mensaje = _mensajeTokenIncorrecto;
                    break;
                default:
                    this._codMensaje = pCodMensaje;
                    ResourceManager rm1 = Respuestas.ResourceManager;
                    this._mensajes.Add(rm1.GetString(pCodMensaje));
                    this._mensaje = rm1.GetString(pCodMensaje);
                    break;
            }
        }

        /// <summary>
        /// Contruye una respuesta con el mensaje y el codigo
        /// </summary>
        /// <param name="pMensaje"></param>
        /// <param name="pCodMensaje"></param>
        public Respuesta(string pMensaje, string pCodMensaje)
        {
            switch (pCodMensaje)
            {
                case _codExitoso:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    break;
                case _sessionExpired:
                    this._codMensaje = pCodMensaje;
                    this._mensaje = _mensajeSessionExpirada;
                    this._mensajes.Add(_mensajeSessionExpirada);
                    break;
                case _codExcepcion:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    break;
                case _codNoEncontrado:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    break;
                case _codNoPermitido:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    break;
                case _codRegistroRepetido:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    break;
                case _codError:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    break;
                case _codCuentaInactiva:
                    this._codMensaje = pCodMensaje;
                    this._mensaje = _mensajeCuentaInactiva;
                    this._mensajes.Add(_mensajeCuentaInactiva);
                    break;
                case _codFondosInsuficientes:
                    this._codMensaje = pCodMensaje;
                    this._mensaje = _mensajeFondosInsuficientes;
                    this._mensajes.Add(_mensajeFondosInsuficientes);
                    break;
                case _codTokenIncorrecto:
                    this._codMensaje = pCodMensaje;
                    this._mensaje = _mensajeTokenIncorrecto;
                    this._mensajes.Add(_mensajeTokenIncorrecto);
                    break;
                default:
                    this._codMensaje = pCodMensaje;
                    this._mensajes.Add(pMensaje);
                    //this._estado = TipoRespuesta.Desconocido;
                    break;
            }
        }


        /// <summary>
        /// Contruye una respuesta con el mensaje y el codigo
        /// </summary>
        /// <param name="pMensaje"></param>
        /// <param name="pCodMensaje"></param>
        public Respuesta(string pMensaje, string pCodMensaje, string pReferencia)
            : this(pMensaje, pCodMensaje)
        {
            this.Referencia = pReferencia;
        }

        public Respuesta(TipoRespuesta pTipoRespuesta)
        {
            switch (pTipoRespuesta)
            {
                case TipoRespuesta.Exitoso:
                    this._codMensaje = _codExitoso;
                    this._mensajes.Add(_mensajeExitoso);
                    break;
                case TipoRespuesta.SessionExpirada:
                    this._codMensaje = _sessionExpired;
                    this._mensajes.Add(_mensajeSessionExpirada);
                    break;
                case TipoRespuesta.Error:
                    this._codMensaje = _codError;
                    this._mensajes.Add(_mensajeError);
                    break;
                case TipoRespuesta.NoEncontrado:
                    this._codMensaje = _codNoEncontrado;
                    this._mensajes.Add(_mensajeNoEncontrado);
                    break;
                case TipoRespuesta.NoPermitido:
                    this._codMensaje = _codNoPermitido;
                    this._mensajes.Add(_mensajeNoPermitido);
                    break;
                case TipoRespuesta.RegistroRepetido:
                    this._codMensaje = _codRegistroRepetido;
                    this._mensajes.Add(_mensajeNoEncontrado);
                    break;
                case TipoRespuesta.CuentaInactiva:
                    this._codMensaje = _codCuentaInactiva;
                    this._mensajes.Add(_mensajeCuentaInactiva);
                    break;
                case TipoRespuesta.FondosInsuficientes:
                    this._codMensaje = _codFondosInsuficientes;
                    this._mensajes.Add(_mensajeFondosInsuficientes);
                    break;
                case TipoRespuesta.TokenIncorrecto:
                    this._codMensaje = _codTokenIncorrecto;
                    this._mensajes.Add(_mensajeTokenIncorrecto);
                    break;
                default:
                    this._codMensaje = _mensajeExcepcion;
                    //this._mensajes.Add(_mensajeDesconcido);
                    break;
            }
        }

        public Respuesta(TipoRespuesta pTipoRespuesta, String mensaje)
        {
            switch (pTipoRespuesta)
            {
                case TipoRespuesta.Exitoso:
                    this._codMensaje = _codExitoso;
                    break;
                case TipoRespuesta.SessionExpirada:
                    this._codMensaje = _sessionExpired;
                    break;
                case TipoRespuesta.Error:
                    this._codMensaje = _codError;
                    break;
                case TipoRespuesta.NoEncontrado:
                    this._codMensaje = _codNoEncontrado;
                    break;
                case TipoRespuesta.RegistroRepetido:
                    this._codMensaje = _codRegistroRepetido;
                    break;
                case TipoRespuesta.NoPermitido:
                    this._codMensaje = _codNoPermitido;
                    break;
                case TipoRespuesta.CuentaInactiva:
                    this._codMensaje = _codCuentaInactiva;
                    break;
                case TipoRespuesta.FondosInsuficientes:
                    this._codMensaje = _codFondosInsuficientes;
                    break;
                case TipoRespuesta.TokenIncorrecto:
                    this._codMensaje = _codTokenIncorrecto;
                    break;
                default:
                    this._codMensaje = _codExcepcion;
                    break;
            }
            this._mensajes.Add(mensaje);
        }

        public Respuesta(TipoRespuesta pTipoRespuesta, string pMensaje, string pCodMensaje)
        {
            this._codMensaje = pCodMensaje;
            this._mensajes.Add(pMensaje);
        }

        public Respuesta(TipoRespuesta pTipoRespuesta, List<string> pMensaje, string pCodMensaje)
        {
            this._codMensaje = pCodMensaje;
            this._mensajes = pMensaje;
        }

        #region Propiedaes

        //[DataMember]
        public static string CodExitoso { get { return _codExitoso; } }

        //[DataMember]
        public static string CodError { get { return _codError; } }

        //[DataMember]
        public static string CodNoValido { get { return _codExcepcion; } }

        //[DataMember]
        public static string CodNoEncontrado { get { return _codNoEncontrado; } }

        //[DataMember]
        public static string CodUsuarioNoExiste { get { return _codUsuarioNoExiste; } }

        //[DataMember]
        public static string CodUsuarioBloqueado { get { return _codUsuarioBloqueado; } }

        //[DataMember]
        public static string CodUsuarioInactivo { get { return _codUsuarioInactivo; } }

        //[DataMember]
        public static string CodUsrPndntCmboCntrsn { get { return _codUsrPendCambioContra; } }

        //[DataMember]
        public static string CodPassVencido { get { return _codPassVencido; } }

        //[DataMember]
        public static string CodSesionExpira { get { return _sessionExpired; } }

        //[DataMember]
        public static string CodNoPermitido { get { return _codNoPermitido; } }

        //[DataMember]
        public static string CodTokenIncorrecto { get { return _codTokenIncorrecto; } }

        [DataMember]
        public string CodMensaje
        {
            get { return _codMensaje; }
            set { _codMensaje = value; }
        }

        [DataMember]
        public string Mensaje
        {
            get
            {
                if (Mensajes == null)
                {
                    return string.Empty;
                }
                return _mensajes.FirstOrDefault();
            }
            set
            {
                if (Mensajes != null)
                {
                    Mensajes.Add(value);
                }
            }
        }

        [DataMember]
        public List<string> Mensajes
        {
            get { return _mensajes; }
            set { _mensajes = value; }
        }

        [DataMember]
        public string Referencia { get; set; }

        #endregion
    }
}
