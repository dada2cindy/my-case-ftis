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
using System.Collections;
using WuDada.Core.Generic.Extension;

namespace FTISWeb.Controllers
{
    public partial class BriefController : Controller
    {
        public ActionResult Index()
        {
            return View(GetDataByGroupYear());
        }

        public ActionResult EngIndex()
        {
            return View(GetDataByGroupYear());
        }

        [PublicationClassData(OnlyOpen = true)]
        public ActionResult Print()
        {
            return View(GetDataByGroupYear());
        }

        private IDictionary<string, IList<Brief>> GetDataByGroupYear()
        {
            IDictionary<string, IList<Brief>> result = new Dictionary<string, IList<Brief>>();

            SetConditions(string.Empty);
            m_Conditions.Add("Status", "1");
            IList<Brief> dataList = m_FTISService.GetBriefList(m_Conditions);
            if (dataList != null && dataList.Count() > 0)
            {
                foreach (Brief data in dataList)
                {
                    string key = data.AYear.Trim();
                    IList<Brief> groupDataList = new List<Brief>();
                    if (result.ContainsKey(key))
                    {
                        groupDataList = result[key];
                        result.Remove(key);
                    }
                    groupDataList.Add(data);

                    result.Add(key, groupDataList);
                }
            }

            return result;
        }
    }
}
