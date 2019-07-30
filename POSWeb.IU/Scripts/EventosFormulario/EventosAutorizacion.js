var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";

$(document).ready(function () {


});

function EliminarAutorizacionConfirmar() {
    return confirm('Desea desactivar el Autorizacion ?');
}

function EliminarAutorizacionConfirmar(ruta, menuPadre, id) {
    var c = confirm('Desea desactivar el Autorizacion ?');
    if (c == true) {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionAutorizacion(ruta, menuPadre) {
    //var c = confirm('Desea cancelar los cambios realizados en Autorizacion ?');
    var c = true;
    if (c == true) {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposAutorizacion() {
    //.attr("disabled", "disabled");


    $("#txtIdAutorizacion").attr("disabled", "disabled");
    $("#txtRecurso").attr("disabled", "disabled");
    $("#txtIdRecurso").attr("disabled", "disabled");
    $("#txtConteoAutorizacion").attr("disabled", "disabled");
}