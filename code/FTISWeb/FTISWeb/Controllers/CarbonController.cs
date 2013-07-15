using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FTIS;
using FTIS.Domain;
using FTIS.Domain.Dto;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Models;
using FTISWeb.Security;
using FTISWeb.Utility;
using KendoGridBinder;
using KendoGridBinder.Containers;
using WuDada.Core.Generic.Util;

namespace FTISWeb.Controllers
{
    public partial class CarbonController : Controller
    {
        [NodeSubData(OnlyOpen = true, ParentNodeId = SiteParentNode.Carbon)]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = ((IList<Node>)ViewData["NodeList"])[0].NodeId.ToString();
                id = new NodeModel().EncryptId(id);
            }

            return View(new NodeModel(id));
        }
    }
}
