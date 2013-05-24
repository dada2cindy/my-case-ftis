using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FTIS.ACUtility;
using FTIS.Domain;
using FTIS.Domain.Container;
using FTISWeb.Helper;
using FTISWeb.Utility;

namespace FTISWeb.Security
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 功能
        /// </summary>
        public SiteEntities AppFunction { get; set; }

        /// <summary>
        /// The operation.
        /// </summary>
        public SiteOperations Operation { get; set; }

        /// <summary>
        /// 覆寫AuthorizeAttribute類別的AuthorizeCore方法
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            SessionHelper sessionHelper = new SessionHelper();

            if (!httpContext.Request.IsAuthenticated)
            {
                return false;
            }

            if (sessionHelper.LoginUser == null)
            {
                return false;
            }

            //// 必須通過功能權限檢核                      
            //// 從Sigleton機制取LoginUser，防止後台更新權限後，已登入使用的人不會異動到權限
            return ACUtility.CheckAuthorization(LoginUserContainer.GetInstance().GetUser(sessionHelper.LoginUser.Account), (int)AppFunction, (int)Operation);
        }

        /// <summary>
        /// 無權限時，轉至LogOn.mvc/Unauthorized
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary 
                                   {
                                       { "action", "Unauthorized" },
                                       { "controller", "LogOn" }
                                   });
        }
    }
}