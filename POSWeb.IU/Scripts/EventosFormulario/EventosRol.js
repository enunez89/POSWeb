var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";

$(document).ready(function () {


});

function EliminarRolConfirmar() {
    return confirm('Desea desactivar el Rol ?');
}

function EliminarRolConfirmar(ruta, menuPadre, id) {
    var c = confirm('Desea Eliminar el Rol ?');
    if (c == true) {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionRol(ruta, menuPadre) {
    //var c = confirm('Desea cancelar los cambios realizados en Rol ?');
    var c = true;
    if (c == true) {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposRol() {
    //.attr("disabled", "disabled");


    $("#txtIdRol").attr("disabled", "disabled");
    $("#txtIdEntidad").attr("disabled", "disabled");
    $("#txtNombre").attr("disabled", "disabled");
    $("#txtEstado").attr("disabled", "disabled");
}