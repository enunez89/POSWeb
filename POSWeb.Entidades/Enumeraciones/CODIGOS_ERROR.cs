using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    public enum CODIGOS_ERROR
    {
        //1-99 = Base de Datos
        ErrorGeneralBD = -1,
        AbrirConexionBD = 1,
        CerrarConexionBD = 2,
        EjecucionProcedimientoAlmacenado = 3,
        LeerDataReader = 4,

        //100-109 = Parámetros
        ConsultaParametro   =   100,
        ParametroNulo       =   101,
        ParametroNoValido   =   102,

        //150-199 Errores Procesos Generales
        ErrorGeneralProceso = 150,

        //200-299 = Errores Servicios Externos
        ServicioAutorizacion = 200,
        ServicioSesiones = 201,
        ServicioBitacoras = 202,

        //500-599 = Sesiones y Autorización
        SesionVencida = 501,
        AccesoNoEncontrado = 502,

        //Carga de archivos GSATI
        CargaGSATI = 600
    }
}
