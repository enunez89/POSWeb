using POSWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace POSWeb.IU
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string IsSelected(this HtmlHelper html, List<Controlador> controladores, string modulo, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            //si el controlador y la accion actual existen en la lista de controladores
            //es activo el modulo
            if (controladores.Any(a => a.Codigo == currentController && a.AccionDefault == currentAction))
            {
                return cssClass;
            }
            else
            {
                return String.Empty;
            }
        }



        /// <summary>
        ///     Compares the requested route with the given <paramref name="value" /> value, if a match is found the
        ///     <paramref name="attribute" /> value is returned.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">The action value to compare to the requested route action.</param>
        /// <param name="attribute">The attribute value to return in the current action matches the given action value.</param>
        /// <returns>A HtmlString containing the given attribute value; otherwise an empty string.</returns>
        public static IHtmlString RouteIf(this HtmlHelper helper, string value, string attribute)
        {
            var currentController =
                (helper.ViewContext.RequestContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();
            var currentAction =
                (helper.ViewContext.RequestContext.RouteData.Values["action"] ?? string.Empty).ToString().UnDash();

            var hasController = value.Equals(currentController, StringComparison.InvariantCultureIgnoreCase);
            var hasAction = value.Equals(currentAction, StringComparison.InvariantCultureIgnoreCase);

            return hasAction || hasController ? new HtmlString(attribute) : new HtmlString(string.Empty);
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        /// <summary>
        ///     Renders the specified partial view with the parent's view data and model if the given setting entry is found and
        ///     represents the equivalent of true.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName">The name of the partial view.</param>
        /// <param name="appSetting">The key value of the entry point to look for.</param>
        public static void RenderPartialIf(this HtmlHelper htmlHelper, string partialViewName, string appSetting)
        {
            var setting = Settings.GetValue<bool>(appSetting);

            htmlHelper.RenderPartialIf(partialViewName, setting);
        }

        /// <summary>
        ///     Renders the specified partial view with the parent's view data and model if the given setting entry is found and
        ///     represents the equivalent of true.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName">The name of the partial view.</param>
        /// <param name="condition">The boolean value that determines if the partial view should be rendered.</param>
        public static void RenderPartialIf(this HtmlHelper htmlHelper, string partialViewName, bool condition)
        {
            if (!condition)
                return;

            htmlHelper.RenderPartial(partialViewName);
        }

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo, IDictionary<string, object> htmlAttributes = null,
            string nombre = "", string clase = "")
        {
            int split = 2;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Parametro requerido", "name");
            if (listInfo == null)
                throw new ArgumentNullException("listInfo");
            if (listInfo.Count < 1)
                return string.Empty;
            //throw new ArgumentException("The list must contain at least one value", "listInfo");

            StringBuilder sb = new StringBuilder();
            int colCount = 0;
            int count = 0;
            int maxItems = listInfo.Count;

            if (split > 1)

            {
                sb.AppendLine("<table id='" + name + "' title='" + listInfo[0].id + "' class=' " + "" + " table table - striped table - bordered table - hover' width='100%'> <caption>" + listInfo[0].Titulo + "</caption> <tr><td>");
            }

            foreach (CheckBoxListInfo info in listInfo)

            {

                TagBuilder builder = new TagBuilder("input");

                if (info.IsChecked) builder.MergeAttribute("checked", "checked");

                builder.MergeAttributes<string, object>(htmlAttributes);

                builder.MergeAttribute("type", "checkbox");

                builder.MergeAttribute("value", info.Value);

                builder.MergeAttribute("name", string.IsNullOrEmpty(nombre) ? info.DisplayText : nombre);

                builder.MergeAttribute("class", clase);

                builder.MergeAttribute("id", info.id);

                //    builder.MergeAttribute("text", "prueba");

                builder.InnerHtml = " " + info.DisplayText;

                sb.Append(builder.ToString(TagRenderMode.Normal));

                if (split <= 1)

                {
                    // Skip Table all together….

                }
                else
                {
                    count++;

                    colCount++;

                    if (split == colCount)

                    {

                        colCount = 0;

                        sb.Append("</td></tr>");

                        if (count != maxItems)

                        {
                            // Need another row
                            sb.Append("<tr><td>");
                        }
                    }
                    else

                    {
                        sb.Append("</td><td>");
                    }

                }

            }

            if (split > 1)
                sb.Append("</table>");

            return sb.ToString();
        }

        public static MvcHtmlString UserActionLink(this HtmlHelper htmlHelper, List<Accion> userActions, string controllerName, string linkText, string actionName,
            object routeValues, object htmlAttributes)
        {
            if (userActions == null || userActions.Count == 0)
                return new MvcHtmlString(string.Empty);

            controllerName = controllerName.Replace("Controller", string.Empty).Trim();

            if (!userActions.Any(a => a.Controlador == controllerName && a.Nombre == actionName))
                return new MvcHtmlString(string.Empty);
            else
                return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Hyperlink(this HtmlHelper htmlHelper, string clase, string accionClick, string actionName,
             List<Accion> userActions, string controllerName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<a class='" + clase + "' onclick='" + accionClick + "' , title=" + actionName + "> </a>");

            if (userActions == null || userActions.Count == 0)
                return new MvcHtmlString(string.Empty);

            controllerName = controllerName.Replace("Controller", string.Empty).Trim();

            if (!userActions.Any(a => a.Controlador == controllerName && a.Nombre == actionName))
                return new MvcHtmlString(string.Empty);
            else
                return MvcHtmlString.Create(sb.ToString());
        }

        public static List<WebGridColumn> ObtenerColumna(this HtmlHelper htmlHelper, List<WebGridColumn> Columnas, WebGridColumn columna, 
            bool validarColumna = false, string controlador = "", string accion = "", List<Accion> Acciones = null)
        {
            //Se verifica si se debe de validar los permisos
            if (validarColumna)
            {
                if (Acciones != null || Acciones.Count != 0)
                {
                    //se obtiene el cont
                    controlador = controlador.Replace("Controller", string.Empty).Trim();

                    if (Acciones.Any(a => a.Controlador == controlador && a.Nombre == accion))
                    {
                        Columnas.Add(columna);
                    }
                }
            }
            else
                Columnas.Add(columna);

            return Columnas;
        }

    }
}
