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
    public partial class ApplicationController : Controller
    {
        [ApplicationClassData(OnlyOpen = true)]
        public ActionResult Index(string keyWord, string applicationClassId, int? page)
        {
            applicationClassId = DecryptId(applicationClassId);
            if (string.IsNullOrWhiteSpace(applicationClassId))
            {
                applicationClassId = ((IList<ApplicationClass>)ViewData["ApplicationClassList"])[0].ApplicationClassId.ToString();
            }
            ApplicationClass applicationClass = m_FTISService.GetApplicationClassById(int.Parse(applicationClassId));
            ViewData["ApplicationClass"] = applicationClass;

            SetConditions(string.Empty, applicationClassId);
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());   

            var data = GetGridData();            
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        [ApplicationClassData(OnlyOpen = true)]
        public ActionResult Detail(string id, string cdts)
        {
            GetConditions(cdts);
            return View(new ApplicationModel(id, true));
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}
