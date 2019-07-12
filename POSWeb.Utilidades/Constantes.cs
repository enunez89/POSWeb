using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Utilidades
{
    public static class Constantes
    {
        public const string Seleccione = "Seleccione";
        public struct prefijoTipoDato
        {
            public const string prefijoParametro = "@p";
            public const string Int32 = "i";
            public const string Float = "f";
            public const string Decimal = "d";
            public const string DateTime = "dt";
            public const string String = "s";
            public const string Boolean = "i";
        
        }
        public enum Tipos
        {
            Menu = 9
        }

        public enum Codigos
        {
            Error_Regla_Invalida = 99,
            Registrar_Bitacora = 2,
            Transaccion_Exitosa = 0,
            LOG_IN = 0,
            Error_Sin_Permisos_Usuario = 20,
            Error_Registro_Bitacora = 1,
            Error_Registro_Sesion = 2,
            Error_Registro_Variable = 3,
            Error_Parametros = 1,
            Error_Abrir_Conexion = 10,
            Error_Cerrar_Conexion = 11,
            Error_Procedimiento_Almacenado = 12,
            Error_Respuesta_Procedimiento_Almacenado = 13,
            Error_Obtener_Conexion = 14,
            Error_Obtener_Comando_BD = 15,
            Error_Obtener_Parametro_BD = 16,
            Error_Obtener_DataReader = 17,
            Error_Parametro_No_Valido = 20,
            Error_Durante_Consulta = 104,
            Error_Serializacion = 201,
            Error_General_Proceso = 50,
            Error_Login = 15,
            Error_Dependencia_Registro = 2,
            Error_Sesion_Caducada = 102,
            Error_Acceso_Denegado = 738,
            Error_Usuario_Aprobar = 813,

        }


        public enum TiposRecursos
        {
            Menu = 9,
            Acción = 11,
            Control = 10
        }

        public enum TiposReglas
        {

            ValoresFijos = 1,
            ValoresSql = 2,
            ExpresionRegular = 3,
            RangoNumerico = 4,
            Fecha = 5
        }

        public struct Session
        {
            public const string sCodigoUsuario = "CodigoUsuario";
            public const string sUsrtokensAuthenticate = "UsrtokensAuthenticate";
            public const string sIP = "IP";
            public const string sAcciones = "Acciones";

            public const string sesTipoMsj = "sesTipoMsj";
            public const string sesMsj = "sesMsj";
            public const string sesTitulo = "sesTitulo";
        }

        public struct LlavesConfig
        {
            public const string Entidad = "Entidad";
        }

        public struct EstadoGeneral
        {
            public const string Activo = "A";
            public const string Inactivo = "I";
        }

        public struct BaseDatos
        {
            //constantes para los tamaños de los out parameters
            public const int codErrorTamano = 5;
            public const int mensajeTamano = int.MaxValue;
        }

        public struct Catalogo
        {
            public const string EstadoGeneral = "EstadoGeneral";
            public const string EstadoUsuario = "EstadoUsuario";
            public const string LongitudPassword = "LongitudMinPassword";
            public const string CntMinNumPassword = "CntMinNumPassword";
            public const string CntMinMayusculasPassword = "CntMinMayusculasPassword";
            public const string CntMinSimbolosPassword = "CntMinSimbolosPassword";
            public const string TipoEvento = "TipoEvento";
        }

        public struct HttpMethod
        {
            public const string HTTPOST = "POST";
            public const string HTTGET = "GET";
        }

        public struct EstadoUsuario
        {
            public const string Inactivo = "I";
            public const string Activo = "A";
            public const string Bloqueado = "B";
            public const string PendienteAutorizar = "P";
        }

        public struct CodigoPais
        {
            public const string CR = "CR";
        }

        public struct RecursoAlerta
        {
            public const string Usuario = "Usuario";
            public const string Rol = "Rol";
            public const string CuentaEmail = "CuentaEmail";
        }
    }
}
