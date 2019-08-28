using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSWeb.IU.Servicio;
using POSWeb.ControlExcepciones;
using POSWeb.Entidades;

namespace POSWeb.IU.Controllers
{
    public class FacturacionController : Controller
    {
        CnnWebApi servicio = new CnnWebApi();

        // GET: Facturacion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BuscarProducto(string codigoBarra)
        {
            var respuesta = new RespuestaProducto();
            try
            {
                //****envíamos a buscar el producto
                respuesta = servicio.ObtenerProductoPorCodigo(codigoBarra);
                //****

                //respuesta exitosa
                return Json(new
                {
                    CodError = respuesta.Respuesta.CodMensaje,
                    Data = respuesta.Producto
                });
            }
            catch (Exception ex)
            {
                //registra error
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, codigoBarra);

                //respuesta error
                return Json(new
                {
                    CodError = Respuesta.CodNoValido
                });
            }
            //prueba
        }
    }
}