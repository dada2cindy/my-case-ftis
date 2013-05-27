using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace FTISWeb.Helper
{
    public static class UrlButtonHelper
    {
        /// <summary>
        /// The button submit.
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="formName">
        /// The form name.
        /// </param>
        /// <returns>
        /// The button submit.
        /// </returns>
        public static string ButtonSubmit(this UrlHelper urlHelper, string buttonText, string formName)
        {
            return ButtonSubmit(urlHelper, buttonText, formName, null);
        }

        /// <summary>
        /// The button submit.
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="formName">
        /// The form name.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The button submit.
        /// </returns>
        public static string ButtonSubmit(this UrlHelper urlHelper,
            string buttonText,
            string formName,
                                          object htmlAttributes)
        {
            string javaScript = "$('#Replace').submit();";
            javaScript = javaScript.Replace("Replace", formName);
            return InputHelper(buttonText, javaScript, "button", htmlAttributes);
        }

        /// <summary>
        /// The button link.
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <returns>
        /// The button link.
        /// </returns>
        public static string ButtonLink(this UrlHelper urlHelper, string buttonText, string actionName)
        {
            return ButtonLink(urlHelper, buttonText, actionName, null);
        }

        /// <summary>
        /// The button link.
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <returns>
        /// The button link.
        /// </returns>
        public static string ButtonLink(this UrlHelper urlHelper,
            string buttonText,
            string actionName,
                                        string controllerName)
        {
            return ButtonLink(urlHelper, buttonText, actionName, controllerName, null);
        }

        /// <summary>
        /// The button link.
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <param name="routeValues">
        /// The route values.
        /// </param>
        /// <returns>
        /// The button link.
        /// </returns>
        public static string ButtonLink(this UrlHelper urlHelper,
            string buttonText,
            string actionName,
            string controllerName,
            object routeValues)
        {
            return ButtonLink(urlHelper, buttonText, actionName, controllerName, routeValues, null);
        }

        /// <summary>
        /// The button link.
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <param name="routeValues">
        /// The route values.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The button link.
        /// </returns>
        public static string ButtonLink(this UrlHelper urlHelper,
            string buttonText,
            string actionName,
            string controllerName,
            object routeValues,
            object htmlAttributes)
        {
            return ButtonLink(urlHelper, buttonText, actionName, controllerName, true, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 根據display參數決定是否產生ButtonLink
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="buttonText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="display"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string ButtonLink(this UrlHelper urlHelper,
            string buttonText,
            string actionName,
            string controllerName,
            bool display,
            object routeValues,
            object htmlAttributes)
        {
            return ButtonLink(urlHelper, buttonText, urlHelper.Action(actionName, controllerName, routeValues), display, htmlAttributes);
        }

        public static string ButtonLink(this UrlHelper urlHelper, string buttonText, Uri url, object htmlAttributes)
        {
            return ButtonLink(urlHelper, buttonText, url.ToString(), true, htmlAttributes);
        }

        private static string ButtonLink(this UrlHelper urlHelper, string buttonText, string href, bool display, object htmlAttributes)
        {
            if (display)
            {
                string javaScript = "location.href='Replace'";
                javaScript = javaScript.Replace("Replace", href);
                return InputHelper(buttonText, javaScript, "button", htmlAttributes);
            }

            return string.Empty;
        }

        /// <summary>
        /// The input helper.
        /// </summary>
        /// <param name="buttonText">
        /// The button text.
        /// </param>
        /// <param name="javaScript">
        /// The java script.
        /// </param>
        /// <param name="inputType">
        /// The input type.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The input helper.
        /// </returns>
        private static string InputHelper(string buttonText, string javaScript, string inputType, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("type", inputType);
            tagBuilder.MergeAttribute("name", buttonText, true);
            tagBuilder.MergeAttribute("value", buttonText, true);
            tagBuilder.MergeAttribute("onclick", javaScript);

            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}