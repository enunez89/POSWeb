var txtCodigoBarra = "#CodigoBarra";

var oFacturacion = {
    /**
     * Obtiene la información de un producto.
     */
    BuscarProducto: function () {
        //obtenemos el código digitado
        var codigo = $(txtCodigoBarra).val();

        var parametro = {
            codigoBarra: codigo
        };

        //se hace el llamado del método BuscarProducto en el controller
        Utilidades.EjecutarAjax(urlBuscarProducto, parametro, true, oFacturacion.AgregarFilaGrid);

    },

    /**
     * Agrega una fila al grid de items de la compra.
     * @param {any} producto Datos del producto a agregar
     */
    AgregarFilaGrid: function (producto) {
        alert(producto.Data.Nombre);
    }
};