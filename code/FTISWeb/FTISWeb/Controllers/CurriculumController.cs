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
using FTISWeb.Helper;
using FTISWeb.Models;
using FTISWeb.Utility;
using MvcPaging;

namespace FTISWeb.Controllers
{
    public partial class CurriculumController : Controller
    {
        SessionHelper m_SessionHelper = new SessionHelper();

        public ActionResult Index(string keyWord, int? page)
        {
            SetConditions(keyWord);
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());

            var data = GetGridData();
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        public ActionResult Detail(string id, string cdts)
        {
            GetConditions(cdts);
            EntityCounter(id, "Vister");
            CurriculumModel entityModel = new CurriculumModel(id);
            if ("1".Equals(entityModel.IsOut) && !string.IsNullOrWhiteSpace(entityModel.AUrl))
            {
                Response.Redirect(entityModel.AUrl);
            }
            return View(entityModel);
        }

        public ActionResult DownloadFile(string id, string fileNum, string name)
        {
            Member member = m_SessionHelper.WebMember;
            if (member != null)
            {
                new MemberModel().AddDownloadRecord(name, "2", member.MemberId.ToString());
            }
            
            CurriculumModel entityModel = new CurriculumModel(id);
            switch (fileNum)
            {
                case ("1"):
                    return Redirect(new HomeShowModel().GetFileByEncrypt(entityModel.AFile1));
                case ("2"):
                    return Redirect(new HomeShowModel().GetFileByEncrypt(entityModel.AFile2));
                case ("3"):
                    return Redirect(new HomeShowModel().GetFileByEncrypt(entityModel.AFile3));
            }

            return new EmptyResult();
        }

        private void EntityCounter(string id, string type)
        {
            Curriculum entity = m_FTISService.GetCurriculumById(int.Parse(new CurriculumModel().DecryptId(id)));
            switch (type)
            {
                case "Vister":
                    entity.Vister += 1;
                    break;
            }
            m_FTISService.UpdateCurriculum(entity);
        }
    }
}
