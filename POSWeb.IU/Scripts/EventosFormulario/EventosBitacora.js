
var gridTabla = "#tabla";
var contenedor = "#ContenidoGrid";
var actualizar = "#accionActualizar";
var eliminar = "#accionEliminar";
var classDatepicker = ".datepicker";

//PARA FILTRO GRID
var dtpFechaInicio = "#dtpFechaInicio";
var dtpFechaFinal = "#dtpFechaFinal";
var hdnFechaInicio = "#FechaInicio";
var hdnFechaFinal = "#FechaFinal";
var btnBuscar = "#btnBuscar";
var frmBitacoraFiltrar = "#frmBitacoraFiltrar";


$(document).ready(function () {

    //FECHA INICIO
    $(dtpFechaInicio).datepicker({
        maxDate: "0D",
        showAnim: "drop",
        format: "dd/mm/yyyy",
        defaultDate: new Date(),
        changeMonth: true,
        changeYear: true,
        changeDay: true,
        autoclose: true,
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            $(dtpFechaInicio).datepicker('option', 'minDate', selectedDate);
            $(hdnFechaInicio).val($.datepicker.formatDate('mm-dd-yy', new Date($(this).datepicker('getDate'))));
        }
    });

    //FECHA FINAL
    $(dtpFechaFinal).datepicker({
        maxDate: "0D",
        showAnim: "drop",
        format: "DD/MM/YYYY",
        defaultDate: new Date(),
        changeMonth: true,
        changeYear: true,
        changeDay: true,
        autoclose: true,
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            //$(dtpFechaInicio).datepicker('option', 'maxDate', selectedDate);
          //  $(hdnFechaFinal).val($.datepicker.formatDate('mm-dd-yy', new Date($(this).datepicker('getDate'))));
        }
    });


  //  $(dtpFechaInicio).rules("remove");
  //  $(dtpFechaFinal).rules("remove");
});


//************** PARA FILTRO DEL GRID **********************************************

//$(frmBitacoraFiltrar).submit(function (event) {
//    $(hdnFechaInicio).val($.datepicker.formatDate('mm-dd-yy', new Date($(dtpFechaInicio).datepicker('getDate'))));
//    $(hdnFechaFinal).val($.datepicker.formatDate('mm-dd-yy', new Date($(dtpFechaFinal).datepicker('getDate'))));
//});

//$(function () {
//    $(frmBitacoraFiltrar).validate({ ignore: '.datepicker' });
//})

$(btnBuscar).click(function () {
   $(hdnFechaInicio).val($.datepicker.formatDate('mm-dd-yy', new Date($(dtpFechaInicio).datepicker('getDate'))));
    $(hdnFechaFinal).val($.datepicker.formatDate('mm-dd-yy', new Date($(dtpFechaFinal).datepicker('getDate'))));
    $(frmBitacoraFiltrar).submit();
});
//***********************************************************************************************

function EliminarBitacoraConfirmar() {
    return confirm('Desea desactivar el Bitacora ?');
}

function EliminarBitacoraConfirmar(ruta, menuPadre, id) {
    var c = confirm('Desea desactivar el Bitacora ?');
    if (c == true) {
        Sesion.RedireccionaValores1(ruta, menuPadre, id);
    }
}

function CancelarEdicionBitacora(ruta, menuPadre) {
    //var c = confirm('Desea cancelar los cambios realizados en Bitacora ?');
    var c = true;
    if (c == true) {
        Sesion.RedireccionaMenu(ruta, menuPadre);
    }
}

function BloquearCamposBitacora() {
    //.attr("disabled", "disabled");

    $("#txtFechaRegistro").attr("disabled", "disabled");
    $("#txtTipoEvento").attr("disabled", "disabled");
    $("#txtMensaje").attr("disabled", "disabled");
    $("#txtMensajeTecnico").attr("disabled", "disabled");
    $("#txtTrazador").attr("disabled", "disabled");
}

