using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    public static class MENSAJES_SISTEMA
    {
        public static string ErrorAutenticacion = "Se produjo un error al autenticar el usuario, si el error persiste contacte a un administrador";
        public static string ErrorSesion = "Se produjo un error al crear la sesión del usuario, si el error persiste contacte a un administrador";
        public static string ErrorPermisos = "El método solicitado no fue encontrado entre los accesos del usuario";
        public static string ErrorConsultaDatos = "Se produjo un error al consutar los datos solicitados";
        public static string ErrorExcepcion = "Se ha producido una excepción en el sistema.";
        public static string RespuestaInsertar = "Respuesta de insertar registro.";
        public static string RespuestaModificar = "Respuesta de modificar registro.";
        public static string RespuestaEliminar = "Respuesta de eliminar registro.";
        public static string RespuestaCambioContrasena = "Respuesta cambio contraseña usuario";
        public static string RespuestaValidacion = "Respuesta validacion";
    }
}
