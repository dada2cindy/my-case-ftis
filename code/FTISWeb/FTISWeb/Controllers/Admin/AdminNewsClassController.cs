using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Security;

namespace FTISWeb.Controllers.Admin
{
    public partial class NewsClassController : Controller
    {
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.News, Operation = SiteOperations.Read)]
        public ActionResult AdminIndex()
        {
            return View();
        }

    }
}
