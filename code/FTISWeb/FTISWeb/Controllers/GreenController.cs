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
    public partial class GreenController : Controller
    {
        [NodeSubData(OnlyOpen = true, ParentNodeId = SiteParentNode.Green)]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = ((IList<Node>)ViewData["NodeList"])[0].NodeId.ToString();
                id = new NodeModel().EncryptId(id);
            }

            return View(new NodeModel(id));
        }

        [NodeSubData(OnlyOpen = true, ParentNodeId = SiteParentNode.Green)]
        public ActionResult Index2(string id)
        {
            return View(new PostModel(id, true));
        }
    }
}
