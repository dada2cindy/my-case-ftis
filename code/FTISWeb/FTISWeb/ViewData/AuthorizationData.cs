using System.Web.Mvc;
using FTIS.ACUtility;
using FTIS.Domain;
using FTISWeb.Helper;

namespace FTISWeb.Controllers
{
    public class AuthorizationData : ActionFilterAttribute
    {
        /// <summary>
        /// 功能
        /// </summary>
        public SiteEntities AppFunction { get; set; }

        public string ControllerName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SessionHelper sessionHelper = new SessionHelper();
            if (sessionHelper.LoginUser != null)
            {
                context.Controller.ViewData["AllowRead"] = ACUtility.CheckAuthorization(sessionHelper.LoginUser, AppFunction, SiteOperations.Read);
                context.Controller.ViewData["AllowCreate"] = ACUtility.CheckAuthorization(sessionHelper.LoginUser, AppFunction, SiteOperations.Create);
                context.Controller.ViewData["AllowEdit"] = ACUtility.CheckAuthorization(sessionHelper.LoginUser, AppFunction, SiteOperations.Edit);
                context.Controller.ViewData["AllowDelete"] = ACUtility.CheckAuthorization(sessionHelper.LoginUser, AppFunction, SiteOperations.Delete);
            }

            context.Controller.ViewData["ControllerName"] = ControllerName;
        }
    }
}
