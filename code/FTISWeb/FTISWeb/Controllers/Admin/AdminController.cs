using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Helper;
using FTISWeb.Security;

namespace FTISWeb.Controllers
{
    public partial class AdminController : Controller
    {
        SessionHelper m_SessionHelper = new SessionHelper();

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.None, Operation = SiteOperations.None)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
