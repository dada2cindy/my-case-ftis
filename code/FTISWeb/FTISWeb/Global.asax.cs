using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Diagnostics;

namespace FTISWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            ////Todo 寫入登出的時間
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{

        //    String Errormsg = String.Empty;

        //    Exception unhandledException = Server.GetLastError();

        //    Response.Clear();

        //    HttpException httpException = unhandledException as HttpException;

        //    Errormsg = "發生列外網頁:{0}錯誤訊息:{1}堆疊內容:{2}";

        //    if (httpException != null)
        //    {

        //        RouteData routeData = new RouteData();

        //        routeData.Values.Add("controller", "Error");

        //        Errormsg = String.Format(Errormsg, Request.Path + Environment.NewLine,

        //            unhandledException.GetBaseException().Message + Environment.NewLine,
        //            unhandledException.StackTrace + Environment.NewLine);

        //        //寫入事件撿視器  
        //        EventLog.WriteEntry("WebError", Errormsg, EventLogEntryType.Error);

        //        //寫入文字檔
        //        //System.IO.File.AppendAllText(Server.MapPath(String.Format("WebLog\\{0}.txt",
        //        //   DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss"))), Errormsg);
        //        switch (httpException.GetHttpCode())
        //        {
        //            //case 404:
        //            //    // page not found                        
        //            //    routeData.Values.Add("action", "StatusCode404");
        //            //    break;
        //            //case 500:
        //            //    // server error                     
        //            //    routeData.Values.Add("action", "StatusCode500");
        //            //    break;
        //            default:
        //                routeData.Values.Add("action", "General");
        //                break;
        //        }

        //        // Pass exception details to the target error View.
        //        routeData.Values.Add("Error", unhandledException);

        //        // clear error on server 
        //        Server.ClearError();

        //        // Call target Controller and pass the routeData.

        //        IController errorController = new FTISWeb.Controllers.ErrorController();

        //        errorController.Execute(new RequestContext(
        //            new HttpContextWrapper(Context), routeData));
        //    }

        //}
    }
}