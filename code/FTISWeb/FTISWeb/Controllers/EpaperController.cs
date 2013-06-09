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
    public partial class EpaperController : Controller
    {
        [EpaperYearData(OnlyOpen = true)]
        public ActionResult Index(string keyWord, string year, int? page)
        {
            if (string.IsNullOrWhiteSpace(year))
            {
                year = ((IList<string>)ViewData["EpaperYearList"])[0];
            }

            SetConditions(keyWord, year);
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());

            var data = GetGridData();
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        public ActionResult Detail(string id, string url)
        {
            EntityCounter(id, "Vister");
            Response.Redirect(url);

            return new EmptyResult();
        }

        private void EntityCounter(string id, string type)
        {
            Epaper entity = m_FTISService.GetEpaperById(int.Parse(DecryptId(id)));

            switch (type)
            {
                case "Vister":
                    entity.Vister += 1;
                    break;
            }
            m_FTISService.UpdateEpaper(entity);
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}
