using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Helper;
using FTISWeb.Security;
using FTISWeb.Utility;

namespace FTISWeb.Controllers
{
    public partial class AdminController : Controller
    {
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.None, Operation = SiteOperations.None)]
        public ActionResult Top()
        {
            ViewBag.LoginUserMsg = m_SessionHelper.LoginUser.Name + "@" + IpBlockUtil.ClientIp() + " > 歡迎您再次登入！今天是民國" + (DateTime.Today.Year - 1911).ToString() + "年" + DateTime.Today.Month.ToString("00") + "月" + DateTime.Today.Day.ToString("00") + "日";
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.None, Operation = SiteOperations.None)]
        public ActionResult LeftMenu()
        {
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.None, Operation = SiteOperations.None)]
        public ActionResult Toggle()
        {
            return View();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.None, Operation = SiteOperations.None)]
        public ActionResult Welcome()
        {
            return View();
        }
    }
}
