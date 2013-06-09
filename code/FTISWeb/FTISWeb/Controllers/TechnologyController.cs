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
using MvcPaging;

namespace FTISWeb.Controllers
{
    public partial class TechnologyController : Controller
    {
        [NodeSubData(OnlyOpen = true, ParentNodeId = SiteParentNode.TechnologyParent)]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = ((IList<Node>)ViewData["NodeList"])[0].NodeId.ToString();
            }

            return View(new NodeModel(id));
        }

        [NodeSubData(OnlyOpen = true, ParentNodeId = SiteParentNode.TechnologyParent)]
        public ActionResult Index2(string keyWord, string nodeId, int? page)
        {
            nodeId = DecryptId(nodeId);
            Node node = m_FTISService.GetNodeById(int.Parse(nodeId));
            ViewData["Node"] = node;

            SetConditions(string.Empty, nodeId);
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());

            var data = GetGridData();
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}
