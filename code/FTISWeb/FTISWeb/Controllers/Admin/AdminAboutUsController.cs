﻿using System;
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
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.AboutUs, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.AboutUs)]
        public ActionResult Edit(int id)
        {
            return View(new NodeModel(id, false));
        }

        [ValidateInput(false)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.AboutUs, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.AboutUs)]
        [HttpPost]
        public ActionResult Edit(NodeModel model)
        {
            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";                
            }
            else if (!AccessibilityUtil.CheckFreeGO(model.ContentENG))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "ContentENG";                
            }

            model.Update();
            return View(model);
        }
    }
}
