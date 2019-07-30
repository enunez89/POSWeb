var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";

$(document).ready(function () {


});

function EliminarAlertaEntidadConfirmar()
{
    return confirm('Desea desactivar el AlertaEntidad ?');
}

function EliminarAlertaEntidadConfirmar(ruta, menuPadre, id)
{
    var c = confirm('Desea desactivar el AlertaEntidad ?');
    if (c == true)
    {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionAlertaEntidad(ruta, menuPadre)
{
    //var c = confirm('Desea cancelar los cambios realizados en AlertaEntidad ?');
    var c = true;
    if (c == true)
    {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposAlertaEntidad()
{
	//.attr("disabled", "disabled");

	
           $("#txtId").attr("disabled", "disabled");
           $("#ddlIdAlerta").attr("disabled", "disabled");
           $("#txtTitulo").attr("disabled", "disabled");
           $("#HtmlContent").attr("disabled", "disabled");
           $("#ddlIdCuenta").attr("disabled", "disabled");
           $("#chkActiva").attr("disabled", "disabled");
}