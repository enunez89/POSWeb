var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";
var ddlTipoIdentificacion = "#ddlTipoIdentificacion";
var txtIdentificacion = "#txtIdentificacion1";

$(document).ready(function () {

    //carga la mascara para la identificacion
    CargarMaskTipoID();
});

$(function () {
  //  $('#dtpFechaExpClave').datepicker();
});

function EliminarUsuarioConfirmar() {
    return confirm('Desea desactivar el Usuario ?');
}

function EliminarUsuarioConfirmar(ruta, menuPadre, id) {
    var c = confirm('Desea desactivar el Usuario ?');
    if (c == true) {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionUsuario(ruta, menuPadre) {
    //var c = confirm('Desea cancelar los cambios realizados en Usuario ?');
    var c = true;
    if (c == true) {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposUsuario() {
    //.attr("disabled", "disabled");


    $("#txtCodigoUsuario").attr("disabled", "disabled");
    $("#txtNombre").attr("disabled", "disabled");
    $("#txtClave").attr("disabled", "disabled");
    $(txtIdentificacion).attr("disabled", "disabled");
    $("#ddlEstado").attr("disabled", "disabled");
    $("#txtFechaExpiracionClave").attr("disabled", "disabled");
    $("#txtIntentosFallidos").attr("disabled", "disabled");
    $("#chkPendienteCambio").attr("disabled", "disabled");
    $("#txtCorreoElectronico").attr("disabled", "disabled");
    $("#ddlTipoIdentificacion").attr("disabled", "disabled");
    $("#txtIdPais").attr("disabled", "disabled");
    //$("#chkRol").attr("disabled", "disabled");
    $("#Roles").find("input,button,textarea,select").attr("disabled", "disabled");
}

function CargarFormatoID(txtNombreControl, ddlNombreControl) {
    var codigoTipoID = $(ddlNombreControl + ' option:selected').val();
    if (codigoTipoID == undefined)
        return;

    var arrObj = FormatosTipoID[codigoTipoID];
    var mask = arrObj;
    $(txtNombreControl).mask(mask);
}

function CargarMaskTipoID() {
      CargarFormatoID(txtIdentificacion, ddlTipoIdentificacion);
}