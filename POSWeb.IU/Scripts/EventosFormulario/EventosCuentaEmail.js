var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";

$(document).ready(function () {


});

function EliminarCuentaEmailConfirmar() {
    return confirm('Desea desactivar la Cuenta Email ?');
}

function EliminarCuentaEmailConfirmar(ruta, menuPadre, id) {
    var c = confirm('Desea desactivar la Cuenta Email ?');
    if (c == true) {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionCuentaEmail(ruta, menuPadre) {
    //var c = confirm('Desea cancelar los cambios realizados en CuentaEmail ?');
    var c = true;
    if (c == true) {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposCuentaEmail() {
    $("#txtCorreoElectronico").attr("readonly", true);
    $("#txtAlias").attr("readonly", true);
    $("#txtServidor").attr("readonly", true);
    $("#txtPuerto").attr("readonly", true);
    $("#txtUsuario").attr("readonly", true);
    $("#txtContrasena").attr("readonly", true);
    $("#chkSsl").attr("readonly", true);
    $("#chkCredencialesDefecto").attr("readonly", true);
    $("#chkCuentaDefecto").attr("readonly", true);
}