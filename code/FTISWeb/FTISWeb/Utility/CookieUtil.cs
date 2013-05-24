using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTISWeb.Utility
{
    public static class CookieUtil
    {
        public static void SetCookie(string name, string value, DateTime expires, string path, string domain, bool secure)
        {
            RemoveCookie(name);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
            {
                cookie = new HttpCookie(name);
            }
            cookie.Value = value;
            cookie.Path = path;
            cookie.Domain = domain;
            cookie.Secure = secure;
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string name)
        {
            string value = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                value = cookie.Value;
            }
            return value;
        }

        public static void RemoveCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}