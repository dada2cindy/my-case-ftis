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
    public partial class LinksController : Controller
    {
        [LinksClassData(OnlyOpen = true)]
        public ActionResult Index(string keyWord, string linksClassId, int? page)
        {
            linksClassId = DecryptId(linksClassId);
            if (string.IsNullOrWhiteSpace(linksClassId))
            {
                linksClassId = ((IList<LinksClass>)ViewData["LinksClassList"])[0].LinksClassId.ToString();
            }
            LinksClass linksClass = m_FTISService.GetLinksClassById(int.Parse(linksClassId));
            ViewData["LinksClass"] = linksClass;

            SetConditions(string.Empty, linksClassId, "2");
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
