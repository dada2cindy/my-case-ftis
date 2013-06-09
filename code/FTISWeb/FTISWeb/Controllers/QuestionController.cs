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
    public partial class QuestionController : Controller
    {
        [QuestionClassData(OnlyOpen = true)]
        public ActionResult Index(string keyWord, string questionClassId, int? page)
        {
            questionClassId = DecryptId(questionClassId);
            if (!string.IsNullOrWhiteSpace(questionClassId))
            {
                QuestionClass questionClass = m_FTISService.GetQuestionClassById(int.Parse(questionClassId));
                ViewData["QuestionClass"] = questionClass;
            }
            else
            {
                QuestionClass questionClass = new QuestionClass() { Name = "全部" };
                ViewData["QuestionClass"] = questionClass;
            }

            SetConditions(string.Empty, questionClassId);
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());   

            var data = GetGridData();            
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        [QuestionClassData(OnlyOpen = true)]
        public ActionResult Detail(string id, string cdts)
        {
            GetConditions(cdts);
            return View(new QuestionModel(id, true));
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}
