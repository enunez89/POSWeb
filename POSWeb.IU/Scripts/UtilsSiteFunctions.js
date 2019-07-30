/*Esta funcion establece el evento change a un checkbox y obtiene el valor
  al realizar el evento en el hidden establecido*/
function SetChangeEventAndGetHiddenVal(idHidden, idChkBox)
{
    //evento change
    $(idChkBox).change(function () {
        //Se obtiene el valor del checkbox
        var valueChk = $(idChkBox).is(":checked");
        $(idHidden).val(valueChk);
    });
}

//-------------/gamerule doMobSpawning true---------------------------------------------------------------------------------------------------

if (Msj != "null" | TipoMensaje != "null" | Titulo != "null") {
    swal(Titulo, Msj, TipoMensaje);
}

function nobackbutton() {
    window.location.hash = "no-back-button";
    window.location.hash = "Again-No-back-button" //chrome
    window.onhashchange = function () { window.location.hash = "no-back-button"; }
}
//----------------------------------------------------------------------------------------------------------------
$(window).on('mousedown', function (e) {
    if (e.which == 2) {
        e.preventDefault();
        swal("Acción no permitida", "Por medidas de seguridad el click central está bloqueado", "warning");
    }
});

//-----------------------------------------------------------------------------------------------------------------
/// <summary>
/// Metodo que se encarga de eliminar un elemento. Debe de estar ViewBag.ControllerName
/// </summary>
/// <param name="id"></param><param name=""></param>
/// <returns></returns>
function DeleteElement(id) {
    if (validaMsj != null) {
        var MsjEliminar = msjEliminarDato;
    }
    swal({
        title: '¿Está seguro?',
        text: MsjEliminar,
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Si, eliminar!',
        cancelButtonText: 'Cancelar'
    }).then(function () {
        window.location.href = "DeleteConfirmed?id=" + id;
    })
}
//--------------------------------------------------------------------------------------------------------------------
function Alerta(Titulo, Mensaje, type) {
    swal({
        title: Titulo,
        text: Mensaje,
        type: type,
    })

}

//-------------