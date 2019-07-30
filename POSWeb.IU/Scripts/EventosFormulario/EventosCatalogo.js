var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";

$(document).ready(function () {


});

function EliminarCatalogoConfirmar() {
    return confirm('Desea desactivar el Catalogo ?');
}

function EliminarCatalogoConfirmar(ruta, menuPadre, id) {
    var c = confirm('Desea desactivar el Catalogo ?');
    if (c == true) {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionCatalogo(ruta, menuPadre) {
    //var c = confirm('Desea cancelar los cambios realizados en Catalogo ?');
    var c = true;
    if (c == true) {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposCatalogo() {
    //.attr("disabled", "disabled");


    $("#txtIdCatalogo").attr("disabled", "disabled");
    $("#txtIdentificador").attr("disabled", "disabled");
    $("#txtCodigoParametro").attr("disabled", "disabled");
    $("#txtDescripcion").attr("disabled", "disabled");
    $("#chkPublico").attr("disabled", "disabled");
}