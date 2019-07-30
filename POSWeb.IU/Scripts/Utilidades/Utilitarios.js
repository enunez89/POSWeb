var componente1 = "Componente74";
var componente2 = "Componente89";
var nombreUsuario = "#NombreUsuario";

var separadorDecimal = '.';

var mensajeError = "Se produjo un error en el sistema, vuelva a intentar más tarde, si el problema persiste contacte con el Administrador del Sistema.";
var mensajeCamposRequeridos = "Los campos marcados son requeridos";
var mensajeFormatoFecha = "Los campos marcados deben tener un formato de fecha válido dd/mm/aaaa";
var mensajeNumerico = "Los campos marcados deben ser numéricos";
var mensajeNumericoEntero = "Los campos marcados deben ser numéricos enteros";
var mensajeNumericoDecimal = "Los campos marcados deben ser numéricos decimales";
var mensajePalabrasRestringidas = "Se encontraron las siguientes palabras restringidas: ";

var accionGuardaGen = "G";
var accionModificaGen = "E";
var accionEliminaGen = "Delete";
var accionAutorizarGen = "A";
var accionResetearGen = "R";
var tipoMsjAlerta = "warning";
var chklist = "#chklist";



$(document).ready(function () {
    $.mask = {
        //Predefined character definitions
        definitions: {
            '#': "[0-9]",
            'a': "[A-Za-z]",
            '*': "[A-Za-z0-9]"
        },
        dataName: "rawMaskFn",
        placeholder: '_',
    };

});

var Sesion =
{
    RedireccionaMenu: function (ruta, menuPadre) {
        Utilidades.MostrarProcesos();
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", ruta);

        var hiddenFieldComponente1 = document.createElement("input");
        hiddenFieldComponente1.setAttribute("type", "hidden");
        hiddenFieldComponente1.setAttribute("name", componente1);
        hiddenFieldComponente1.setAttribute("value", $("#" + componente1).val());
        form.appendChild(hiddenFieldComponente1);

        var hiddenFieldComponente2 = document.createElement("input");
        hiddenFieldComponente2.setAttribute("type", "hidden");
        hiddenFieldComponente2.setAttribute("name", componente2);
        hiddenFieldComponente2.setAttribute("value", $("#" + componente2).val());
        form.appendChild(hiddenFieldComponente2);

        var hiddenFieldMenu = document.createElement("input");
        hiddenFieldMenu.setAttribute("type", "hidden");
        hiddenFieldMenu.setAttribute("name", "MenuPadre");
        hiddenFieldMenu.setAttribute("value", menuPadre);
        form.appendChild(hiddenFieldMenu);

        var hiddenFieldNombreUsuario = document.createElement("input");
        hiddenFieldNombreUsuario.setAttribute("type", "hidden");
        hiddenFieldNombreUsuario.setAttribute("name", "NombreUsuario");
        hiddenFieldNombreUsuario.setAttribute("value", $(nombreUsuario).val());
        form.appendChild(hiddenFieldNombreUsuario);

        document.body.appendChild(form);
        form.submit();
    },
    RedireccionaValores1: function (ruta, menuPadre, valor1, Accion) {
        Utilidades.MostrarProcesos();
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", ruta);

        var hiddenFieldComponente1 = document.createElement("input");
        hiddenFieldComponente1.setAttribute("type", "hidden");
        hiddenFieldComponente1.setAttribute("name", componente1);
        hiddenFieldComponente1.setAttribute("value", $("#" + componente1).val());
        form.appendChild(hiddenFieldComponente1);

        var hiddenFieldComponente2 = document.createElement("input");
        hiddenFieldComponente2.setAttribute("type", "hidden");
        hiddenFieldComponente2.setAttribute("name", componente2);
        hiddenFieldComponente2.setAttribute("value", $("#" + componente2).val());
        form.appendChild(hiddenFieldComponente2);

        var hiddenFieldMenu = document.createElement("input");
        hiddenFieldMenu.setAttribute("type", "hidden");
        hiddenFieldMenu.setAttribute("name", "MenuPadre");
        hiddenFieldMenu.setAttribute("value", menuPadre);
        form.appendChild(hiddenFieldMenu);

        var hiddenFieldNombreUsuario = document.createElement("input");
        hiddenFieldNombreUsuario.setAttribute("type", "hidden");
        hiddenFieldNombreUsuario.setAttribute("name", "NombreUsuario");
        hiddenFieldNombreUsuario.setAttribute("value", $(nombreUsuario).val());
        form.appendChild(hiddenFieldNombreUsuario);

        var hiddenFieldValor1 = document.createElement("input");
        hiddenFieldValor1.setAttribute("type", "hidden");
        hiddenFieldValor1.setAttribute("name", "pValor1");
        hiddenFieldValor1.setAttribute("value", valor1);
        form.appendChild(hiddenFieldValor1);

        document.body.appendChild(form);
        form.submit();
    },
    RedireccionaValores2: function (ruta, menuPadre, valor1, valor2) {
        Utilidades.MostrarProcesos();
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", ruta);

        var hiddenFieldComponente1 = document.createElement("input");
        hiddenFieldComponente1.setAttribute("type", "hidden");
        hiddenFieldComponente1.setAttribute("name", componente1);
        hiddenFieldComponente1.setAttribute("value", $("#" + componente1).val());
        form.appendChild(hiddenFieldComponente1);

        var hiddenFieldComponente2 = document.createElement("input");
        hiddenFieldComponente2.setAttribute("type", "hidden");
        hiddenFieldComponente2.setAttribute("name", componente2);
        hiddenFieldComponente2.setAttribute("value", $("#" + componente2).val());
        form.appendChild(hiddenFieldComponente2);

        var hiddenFieldMenu = document.createElement("input");
        hiddenFieldMenu.setAttribute("type", "hidden");
        hiddenFieldMenu.setAttribute("name", "MenuPadre");
        hiddenFieldMenu.setAttribute("value", menuPadre);
        form.appendChild(hiddenFieldMenu);

        var hiddenFieldNombreUsuario = document.createElement("input");
        hiddenFieldNombreUsuario.setAttribute("type", "hidden");
        hiddenFieldNombreUsuario.setAttribute("name", "NombreUsuario");
        hiddenFieldNombreUsuario.setAttribute("value", $(nombreUsuario).val());
        form.appendChild(hiddenFieldNombreUsuario);

        var hiddenFieldValor1 = document.createElement("input");
        hiddenFieldValor1.setAttribute("type", "hidden");
        hiddenFieldValor1.setAttribute("name", "pValor1");
        hiddenFieldValor1.setAttribute("value", valor1);
        form.appendChild(hiddenFieldValor1);

        var hiddenFieldValor2 = document.createElement("input");
        hiddenFieldValor2.setAttribute("type", "hidden");
        hiddenFieldValor2.setAttribute("name", "pValor2");
        hiddenFieldValor2.setAttribute("value", valor2);
        form.appendChild(hiddenFieldValor2);


        document.body.appendChild(form);
        form.submit();
    },
    RedireccionaValores3: function (ruta, menuPadre, valor1, valor2, valor3) {
        Utilidades.MostrarProcesos();
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", ruta);

        var hiddenFieldComponente1 = document.createElement("input");
        hiddenFieldComponente1.setAttribute("type", "hidden");
        hiddenFieldComponente1.setAttribute("name", componente1);
        hiddenFieldComponente1.setAttribute("value", $("#" + componente1).val());
        form.appendChild(hiddenFieldComponente1);

        var hiddenFieldComponente2 = document.createElement("input");
        hiddenFieldComponente2.setAttribute("type", "hidden");
        hiddenFieldComponente2.setAttribute("name", componente2);
        hiddenFieldComponente2.setAttribute("value", $("#" + componente2).val());
        form.appendChild(hiddenFieldComponente2);

        var hiddenFieldMenu = document.createElement("input");
        hiddenFieldMenu.setAttribute("type", "hidden");
        hiddenFieldMenu.setAttribute("name", "MenuPadre");
        hiddenFieldMenu.setAttribute("value", menuPadre);
        form.appendChild(hiddenFieldMenu);

        var hiddenFieldNombreUsuario = document.createElement("input");
        hiddenFieldNombreUsuario.setAttribute("type", "hidden");
        hiddenFieldNombreUsuario.setAttribute("name", "NombreUsuario");
        hiddenFieldNombreUsuario.setAttribute("value", $(nombreUsuario).val());
        form.appendChild(hiddenFieldNombreUsuario);

        var hiddenFieldValor1 = document.createElement("input");
        hiddenFieldValor1.setAttribute("type", "hidden");
        hiddenFieldValor1.setAttribute("name", "pValor1");
        hiddenFieldValor1.setAttribute("value", valor1);
        form.appendChild(hiddenFieldValor1);

        var hiddenFieldValor2 = document.createElement("input");
        hiddenFieldValor2.setAttribute("type", "hidden");
        hiddenFieldValor2.setAttribute("name", "pValor2");
        hiddenFieldValor2.setAttribute("value", valor2);
        form.appendChild(hiddenFieldValor2);

        var hiddenFieldValor3 = document.createElement("input");
        hiddenFieldValor3.setAttribute("type", "hidden");
        hiddenFieldValor3.setAttribute("name", "pValor3");
        hiddenFieldValor3.setAttribute("value", valor3);
        form.appendChild(hiddenFieldValor3);

        document.body.appendChild(form);
        form.submit();
    },
    RedireccionaValores4: function (ruta, menuPadre, valor1, valor2, valor3, valor4) {
        Utilidades.MostrarProcesos();
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", ruta);

        var hiddenFieldComponente1 = document.createElement("input");
        hiddenFieldComponente1.setAttribute("type", "hidden");
        hiddenFieldComponente1.setAttribute("name", componente1);
        hiddenFieldComponente1.setAttribute("value", $("#" + componente1).val());
        form.appendChild(hiddenFieldComponente1);

        var hiddenFieldComponente2 = document.createElement("input");
        hiddenFieldComponente2.setAttribute("type", "hidden");
        hiddenFieldComponente2.setAttribute("name", componente2);
        hiddenFieldComponente2.setAttribute("value", $("#" + componente2).val());
        form.appendChild(hiddenFieldComponente2);

        var hiddenFieldMenu = document.createElement("input");
        hiddenFieldMenu.setAttribute("type", "hidden");
        hiddenFieldMenu.setAttribute("name", "MenuPadre");
        hiddenFieldMenu.setAttribute("value", menuPadre);
        form.appendChild(hiddenFieldMenu);

        var hiddenFieldNombreUsuario = document.createElement("input");
        hiddenFieldNombreUsuario.setAttribute("type", "hidden");
        hiddenFieldNombreUsuario.setAttribute("name", "NombreUsuario");
        hiddenFieldNombreUsuario.setAttribute("value", $(nombreUsuario).val());
        form.appendChild(hiddenFieldNombreUsuario);

        var hiddenFieldValor1 = document.createElement("input");
        hiddenFieldValor1.setAttribute("type", "hidden");
        hiddenFieldValor1.setAttribute("name", "pValor1");
        hiddenFieldValor1.setAttribute("value", valor1);
        form.appendChild(hiddenFieldValor1);

        var hiddenFieldValor2 = document.createElement("input");
        hiddenFieldValor2.setAttribute("type", "hidden");
        hiddenFieldValor2.setAttribute("name", "pValor2");
        hiddenFieldValor2.setAttribute("value", valor2);
        form.appendChild(hiddenFieldValor2);

        var hiddenFieldValor3 = document.createElement("input");
        hiddenFieldValor3.setAttribute("type", "hidden");
        hiddenFieldValor3.setAttribute("name", "pValor3");
        hiddenFieldValor3.setAttribute("value", valor3);
        form.appendChild(hiddenFieldValor3);

        var hiddenFieldValor4 = document.createElement("input");
        hiddenFieldValor4.setAttribute("type", "hidden");
        hiddenFieldValor4.setAttribute("name", "pValor4");
        hiddenFieldValor4.setAttribute("value", valor4);
        form.appendChild(hiddenFieldValor4);

        document.body.appendChild(form);
        form.submit();
    }
};

var Utilidades =
{
    MostrarModal: function (idModal) {
        $("#" + idModal).removeClass("fade");
        $("#" + idModal).css({ display: "block" });
    },
    OcultarModal: function (idModal) {
        $("#" + idModal).addClass("fade");
        $("#" + idModal).css({ display: "none" });
    },
    ValidarRespuestaAJAX: function (data, modal) {
        if (data === null || typeof data === 'undefined') {
            if (modal === null) {
                Utilidades.MostrarMensajeFormulario(mensajeError);
            } else {
                Utilidades.MostrarMensajeModal(modal, mensajeError);
            }
            return false;
        }
        else if (data.Codigo !== 0) {
            if (modal === null) {
                Utilidades.MostrarMensajeFormulario(data.Descripcion);
            } else {
                Utilidades.MostrarMensajeModal(modal, data.Descripcion);
            }
            return false;
        }
        return true;
    },
    FormatoBooleano: function (cellvalue, options, rowObject) {
        if (cellvalue === true) {
            return "Si";
        }
        return "No";
    },
    OcultarMensajeFormulario: function () {
        $("#divMensajeFormulario").removeClass("alert alert-danger");
        $("#iconTipoMensaje").removeClass("fa fa-exclamation-triangle fa-2x fa-li fa-lg");
        $("#divMensajeFormulario").hide();
    },
    MostrarMensajeFormulario: function (descripcion, tipo) {

        if (tipo === null || typeof tipo === "undefined" || tipo === 'Error') {
            $("#divMensajeFormulario").addClass("alert alert-danger");
            $("#iconTipoMensaje").addClass("fa fa-exclamation-triangle fa-2x fa-li fa-lg");
        }
        else if (tipo === 'Exito') {
            $("#divMensajeFormulario").addClass("alert alert-success");
            $("#iconTipoMensaje").addClass("fa fa-check fa-2x fa-li fa-lg");
        }
        $("#lblMensajeFormulario").text(descripcion);
        $("#divMensajeFormulario").show();
    },
    MostrarProcesos: function () {
        var element = document.getElementById("procesando")
        if (typeof element != null) {
            element.style.visibility = "visible";
        }
    },
    OcultarProcesos: function () {
        var element = document.getElementById("procesando")
        if (typeof element != null) {
            element.style.visibility = "hidden";
        }
    },
    MostrarMensajeModal: function (idModal, mensaje, tipo) {
        var mensajeModal = "#mensajeModal";
        $('#' + idModal + ' ' + mensajeModal).text(mensaje);
        if (tipo === "exito") {
            $('#' + idModal + ' ' + mensajeModal).attr("class", "text-success");
        } else if (tipo === "atencion") {
            $('#' + idModal + ' ' + mensajeModal).attr("class", "text-warning");
        } else if (tipo === "ocultar") {
            $('#' + idModal + ' ' + mensajeModal).attr("class", "");
        } else {
            $('#' + idModal + ' ' + mensajeModal).attr("class", "text-danger");
        }
    },
    QuitarAlertaCamposRequeridos: function (idModal) {
        var campos = $("#" + idModal + " :input");
        campos.each(function () {
            $(chklist).remove();
            $(this).removeClass('input-validation-error');
        });
    },
    LimpiarCampos: function (idModal) {
        var campos = $("#" + idModal + " :input");
        campos.each(function () {
            (this).value = "";
        });
    },
    ValidarCamposRequeridos: function (idModal, esModal) {
        Utilidades.QuitarAlertaCamposRequeridos(idModal);
        var campos = $("#" + idModal + " :input.requerido");
        var valido = true;
        campos.each(function () {
            var campo = (this).value;
            if (!campo) {
                valido = false;
                $(this).addClass('input-validation-error');
            }
        });

        //Validar Combos
        $("#" + idModal + " :input.requeridoCombo").each(function () {
            if (jQuery.trim($(this).val()) == "0" || jQuery.trim($(this).val()) == '') {
                var nombreAtributo = $(this).parent().parent().find('.control-label').text();
                if (nombreAtributo == "") {
                    nombreAtributo = $(this).parent().find('label').text();
                }
                //$(this)
                //    .after('<span class="text-danger validationMessajeFor"> El dato ' +
                //    nombreAtributo +
                //    ' es requerido</span>');
                //this.style.border = "1px solid #b94a48";
                $(this).addClass('input-validation-error');
                valido = false;
            }
        });

        //Validar checkBox
        var validarChk = false;
        var chkRequerido = true;
        var controlChek = "";
        $("#" + idModal + " :input.ChkListRequerido").each(function () {
            validarChk = true;
            controlChek = $(this);
            if ($(this).is(':checked')) {
                chkRequerido = false;
            }
        });
        if (validarChk) {
            if (chkRequerido) {
                //var nombreEtiqueta = controlChek.parent().parent().parent().parent().parent().parent().find('.control-label').text();
                //controlChek.parent().parent().parent().parent().after('<span class="text-danger validationMessajeFor">' + window.mensajeRecursos.ChckListRequerido + '</span>');
                controlChek.parent().parent().parent().parent().addClass('input-validation-error');
                valido = false;
            }
        }


        if (!valido) {
            if (esModal) {
                Utilidades.MostrarMensajeModal(idModal, mensajeCamposRequeridos, "atencion");
            } else {
                Utilidades.MostrarMensajeFormulario(mensajeCamposRequeridos);
            }
        }
        return valido;
    },
    ValidarCamposNumericos: function (idModal, esModal) {
        Utilidades.QuitarAlertaCamposRequeridos(idModal);
        var campos = $("#" + idModal + " :input.numerico");
        var valido = true;
        campos.each(function () {
            var campo = (this).value;
            if (campo) {
                if (!$.isNumeric(campo)) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
            }
        });
        if (!valido) {
            if (esModal) {
                Utilidades.MostrarMensajeModal(idModal, mensajeNumerico, "atencion");
            } else {
                Utilidades.MostrarMensajeFormulario(mensajeNumerico);
            }
        }
        return valido;
    },
    ValidarPresicionCampo: function (campo, tamanno, presicion, esModal, idModal) {
        var valor = $(campo).val();
        var esValido = true;
        var formatoRequerido = "";
        var mensaje = "";
        var arregloNumero = [];
        $(campo).removeClass('input-validation-error');
        if (presicion === 0) {
            arregloNumero = valor.split(separadorDecimal);

            if (arregloNumero.length > 1) {
                esValido = false;
            }

            if (!esValido) {
                $(campo).addClass('input-validation-error');
                var i = 0;
                while (i < tamanno) {
                    formatoRequerido += "9";
                    i++;
                }
                mensaje = "El formato requerido es " + formatoRequerido;
                if (esModal) {
                    Utilidades.MostrarMensajeModal(idModal, mensaje, "atencion");
                } else {
                    Utilidades.MostrarMensajeFormulario(mensaje);
                }
            }
        } else {

            if (valor.length > (tamanno + 1)) {
                esValido = false;
            } else {
                arregloNumero = valor.split(separadorDecimal);
                if (arregloNumero.length !== 2) {
                    esValido = false;
                } else {
                    var cantidadEnteros = arregloNumero[0].length;
                    var cantidadDecimales = arregloNumero[1].length;
                    if (cantidadEnteros > (tamanno - presicion)) {
                        esValido = false;
                    }
                    if (cantidadDecimales > presicion) {
                        esValido = false;
                    }
                }
            }

            if (!esValido) {
                $(campo).addClass('input-validation-error');
                var i = 0;
                while (i < (tamanno - presicion)) {
                    formatoRequerido += "9";
                    i++;
                }
                formatoRequerido += separadorDecimal;
                i = 0;
                while (i < presicion) {
                    formatoRequerido += "9";
                    i++;
                }
                mensaje = "El formato requerido es " + formatoRequerido;
                if (esModal) {
                    Utilidades.MostrarMensajeModal(idModal, mensaje, "atencion");
                } else {
                    Utilidades.MostrarMensajeFormulario(mensaje);
                }
            }
        }
        return esValido;
    },
    ValidarEnteros: function (idModal, esModal) {
        var campos = $("#" + idModal + " :input.campo_entero");
        var expresion = '^[0-9]+$';
        var valido = true;
        campos.each(function () {
            var campo = (this).value;
            if (campo) {
                var expCorrecta = campo.match(expresion);
                if (!expCorrecta) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
            }
        });
        if (!valido) {
            if (esModal) {
                Utilidades.MostrarMensajeModal(idModal, mensajeFormatoFecha, "atencion");
            } else {
                Utilidades.MostrarMensajeFormulario(mensajeCamposRequeridos);
            }
        }
        return valido;
    },
    ValidarDecimales: function (idModal, esModal) {
        var campos = $("#" + idModal + " :input.campo_decimal");
        var expresion = /^-?\d+(?:\.\d+)?$/;
        var valido = true;
        campos.each(function () {
            var campo = (this).value;
            if (campo) {
                var expCorrecta = campo.match(expresion);
                if (!expCorrecta) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
            }
        });

        if (!valido) {
            if (esModal) {
                Utilidades.MostrarMensajeModal(idModal, mensajeNumericoDecimal, "atencion");
            } else {
                Utilidades.MostrarMensajeFormulario(mensajeCamposRequeridos);
            }
        }
        return valido;
    },
    ValidarFechas: function (idModal, esModal) {
        var campos = $("#" + idModal + " :input.campo_fecha");
        var valido = true;
        campos.each(function () {
            var campo = (this).value;
            if (campo) {
                var date = campo.split("/");
                var d = parseInt(date[0], 10);
                var m = parseInt(date[1], 10);
                var y = parseInt(date[2], 10);

                if (isNaN(d) || isNaN(m) || isNaN(y)) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
                else if (d < 1 || d > 31 || date[0].length !== 2) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
                else if (m < 1 || m > 12 || date[1].length !== 2) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
                else if (y < 1900 || y > 2100 || date[2].length !== 4) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                }
            }
        });
        if (!valido) {
            if (esModal) {
                Utilidades.MostrarMensajeModal(idModal, mensajeFormatoFecha, "atencion");
            } else {
                Utilidades.MostrarMensajeFormulario(mensajeCamposRequeridos);
            }
        }
        return valido;
    },
    RevisarTextoCampos: function (modal, esModal) {
        Utilidades.QuitarAlertaCamposRequeridos(modal);
        var campos = $("#" + modal + " :input.campo"), encontradas = [], valido = true;
        campos.each(function () {
            var campo = (this).value;
            if (campo) {
                var resultado = Utilidades.ValidarPalabrasRestringidas(campo);
                if (resultado.length > 0) {
                    valido = false;
                    $(this).addClass('input-validation-error');
                    encontradas= $.merge(encontradas, resultado);
                }
            }
        });
        if (!valido) {
            encontradas = $.unique(encontradas);
            var p = "";

            $.each(encontradas, function (index, value) {
                if (index === 0) {
                    p= p.concat(value);
                } else {
                    p= p.concat(", "+value);
                }
            });

            if (esModal) {
                Utilidades.MostrarMensajeModal(modal, mensajePalabrasRestringidas + p, "atencion");
            } else {
                Utilidades.MostrarMensajeFormulario(mensajePalabrasRestringidas + p);
            }
        }
        return valido;
    },
    ValidarPalabrasRestringidas: function (valor) {
        var lista = ["update","from", "insert", "delete", "truncate"], noValidasEcontradas = [];
        var p = valor.split(" ");
        $.each(p, function (n, i) {
            if ($.inArray(i.toLowerCase(), lista) !== -1) {
                noValidasEcontradas.push(i);
            }
        });
        return noValidasEcontradas;
    },
    MostrarConfirmacion: function (registro, accion, mensaje) {

        var titulo, tipoBtnConfirmar, metodo;
        if (accion == accionEliminaGen) {
            tipoBtnConfirmar = window.mensajeRecursos.ConfBtnEliminar;
            metodo = window.mensajeRecursos.EliminaRegistro;
        }
        if (accion == accionAutorizarGen) {
            tipoBtnConfirmar = window.mensajeRecursos.ConfBtnAutorizar;
            metodo = window.mensajeRecursos.AutorizarRegistro;
        }
        if (accion == accionResetearGen) {
            tipoBtnConfirmar = window.mensajeRecursos.BotonReset;
            metodo = window.mensajeRecursos.ResetPass;
            MsjAccion = window.mensajeRecursos.MsjConfirmacionReset;
        }
        var mensajeMostrar = Msj;
        if (mensaje != "" && mensaje != undefined && mensaje != null) {
            mensajeMostrar = mensaje;
        }

        swal({
            title: window.mensajeRecursos.MsjConfirmacion,
            text: mensajeMostrar,
            type: tipoMsjAlerta,
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            cancelButtonText: window.mensajeRecursos.botonSalir,
            confirmButtonText: tipoBtnConfirmar
        }).then(function (isConfirm) {
            if (isConfirm === true) {
                window.location.href = nombreControlador + "/" + metodo + registro;
            }
        })
        return false;
    },

    ValidarCheckList: function (idModal, esModal) {
        Utilidades.QuitarAlertaCamposRequeridos(idModal);
        var valido = true;
        //Validar checkBox
        var validarChk = false;
        var chkRequerido = true;
        var controlChek = "";
        $("#" + idModal + " :input.ChkListRequerido").each(function () {
            validarChk = true;
            controlChek = $(this);
            if ($(this).is(':checked')) {
                chkRequerido = false;
            }
        });
        if (validarChk) {
            if (chkRequerido) {
                var nombreEtiqueta = controlChek.parent().parent().parent().parent().parent().parent().find('.control-label').text();
                controlChek.parent().parent().parent().parent().after('<span id="chklist" class="text-danger validationMessajeFor">' + "Eliga almenos un elemento de la lista" + '</span>');
                valido = false;
            }
        }
        //if (!valido) {
        //    if (esModal) {
        //        Utilidades.MostrarMensajeModal(idModal, mensajeCamposRequeridos, "atencion");
        //    } else {
        //        Utilidades.MostrarMensajeFormulario(mensajeCamposRequeridos);
        //    }
        //}
        return valido;
    },

    CargarMascaraFecha(restFechaMinima, restFechaMax) {
        //minDate: 0,
        //maxDate: "+1Y"
        $(".datepicker").datepicker(
            {
                minDate: restFechaMinima,
                maxDate: restFechaMax,
                dateFormat: "dd/mm/yy",
                showAnim: "fadeIn",
                changeMonth: true,
                changeYear: true,
            });
        //$('.datepicker').mask("##/##/####");
    }

};


function convertirFechaJson(fecha) {

    var now = new Date(parseInt(fecha.replace('/Date(', '')));

    return ((now.getDate()) + '/' + (now.getMonth() + 1) + '/' + now.getFullYear() + " " + now.getHours() + ':'
                  + ((now.getMinutes() < 10) ? ("0" + now.getMinutes()) : (now.getMinutes())) + ':' + ((now.getSeconds() < 10) ? ("0" + now
                  .getSeconds()) : (now.getSeconds())));        

}
