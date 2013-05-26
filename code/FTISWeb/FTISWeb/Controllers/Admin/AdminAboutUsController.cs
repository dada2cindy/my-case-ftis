using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTIS.Domain;
using FTISWeb.Models;
using FTISWeb.Security;
using FTISWeb.Utility;
using WuDada.Accessibility.FreeGO;

namespace FTISWeb.Controllers
{
    public partial class AboutUsController : Controller
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
            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
                //View(model);
            }
            else if (!AccessibilityUtil.CheckFreeGO(model.ContentENG))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "ContentENG";
                //View(model);
            }

            model.Update();
            return View(model);
        }
    }
}
