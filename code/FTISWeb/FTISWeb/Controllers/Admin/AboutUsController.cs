using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Security;

namespace FTISWeb.Controllers.Admin
{
    public class AboutUsController : Controller
    {
        public ActionResult Index()
        {
            return Edit();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.AboutUs, Operation = SiteOperations.Edit)]
        public ActionResult Edit()
        {
            return View();
        }
    }
}
