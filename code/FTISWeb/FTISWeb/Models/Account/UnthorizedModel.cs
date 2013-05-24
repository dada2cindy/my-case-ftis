using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using FTISWeb.Helper;

namespace FTISWeb.Models
{
    public class UnauthorizedModel
    {
        public string Title { get; set; }
        public string Msg { get; set; }
        public string ReturnUrl { get; set; }
        public int Timeout { get; set; }

        public UnauthorizedModel()
        {
            string url = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, new RouteValueDictionary(new { controller = "LogOn", action = "LogOn" })).VirtualPath;
            this.ReturnUrl = url;
        }

        public UnauthorizedModel(bool isAuthenticated)
        {
            SessionHelper sessionHelper = new SessionHelper();
            this.Timeout = 5;
            this.Title = "未獲授權";
            if (isAuthenticated && sessionHelper.LoginUser != null)
            {
                Msg = "您未被授權查看該頁，即將返回系統首頁。";
                string url = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, new RouteValueDictionary(new { controller = "Admin", action = "Index" })).VirtualPath;
                ReturnUrl = url;
            }
            else
            {
                Msg = "您尚未登入或登入已逾期，即將前往登入頁。";
                string url = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, new RouteValueDictionary(new { controller = "LogOn", action = "LogOn" })).VirtualPath;
                ReturnUrl = url;
            }
        }
    }
}