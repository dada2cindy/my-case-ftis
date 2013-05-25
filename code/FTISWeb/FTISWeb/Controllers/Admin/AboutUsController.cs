using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Models;
using FTISWeb.Security;

namespace FTISWeb.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly int m_NodeId = 6;

        public ActionResult Index()
        {
            return Edit();
        }

        [AdminAuthorizeAttribute(AppFunction = SiteEntities.AboutUs, Operation = SiteOperations.Edit)]
        public ActionResult Edit()
        {
            return View(new NodeModel(m_NodeId, false));
        }

        [ValidateInput(false)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.AboutUs, Operation = SiteOperations.Edit)]
        [HttpPost]
        public ActionResult Edit(NodeModel model)
        {
            model.Update();
            return View(model);
        }
    }
}
