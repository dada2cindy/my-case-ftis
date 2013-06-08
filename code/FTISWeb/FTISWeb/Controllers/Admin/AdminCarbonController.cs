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
    public partial class CarbonController : Controller
    {
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Carbon, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Carbon, ControllerName = "Carbon")]
        public ActionResult NodeSingleEdit(int id)
        {
            return View("NodeSingleEdit", new NodeModel(id));
        }

        [ValidateInput(false)]
        [AdminAuthorizeAttribute(AppFunction = SiteEntities.Carbon, Operation = SiteOperations.Edit)]
        [AuthorizationData(AppFunction = SiteEntities.Carbon, ControllerName = "Carbon")]
        [HttpPost]
        public ActionResult NodeSingleEdit(NodeModel model)
        {
            ////檢查內容無障礙是否通過
            if (!AccessibilityUtil.CheckFreeGO(model.Content))
            {
                model.ShowFreeGOMsg = true;
                model.FreeGOColumnName = "Content";
            }

            model.Update();
            return View(model);
        }
    }
}