using System;
using System.Collections.Generic;
using System.IO;
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
using WuDada.Core.Generic.Util;

namespace FTISWeb.Controllers
{
    public partial class ReportController : Controller
    {
        SessionHelper m_SessionHelper = new SessionHelper();

        [ApplicationClassData(OnlyOpen = true)]
        [ReportItemData]
        public ActionResult Index(string keyWord, string company, string companyTrade, string companyNationality, string companyScale,
            string companyType, string isAA1000, string isGRI, string postYearFrom, string postYearTo,
            string reportYearFrom, string reportYearTo, int? page)
        {
            SetConditions(keyWord, company, companyTrade, companyNationality, companyScale, companyType, isAA1000,
                isGRI, postYearFrom, postYearTo, reportYearFrom, reportYearTo);
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
            ReportModel entityModel = new ReportModel(id);
            return View(entityModel);
        }

        public ActionResult ReportFile(string id, string type)
        {
            EntityCounter(id, type);

            ReportModel entityModel = new ReportModel(id);
            switch (type)
            {
                case ("CVister"):
                    return Redirect(entityModel.CAUrl);
                case ("EVister"):
                    return Redirect(entityModel.EAUrl);
                case ("DVister"):
                    return Redirect(entityModel.DAUrl);
            }

            return new EmptyResult();
        }

        [ApplicationClassData(OnlyOpen = true)]
        public ActionResult Index2()
        {
            return View();
        }

        [ApplicationClassData(OnlyOpen = true)]
        public ActionResult Provide()
        {
            return View();
        }

        [ApplicationClassData(OnlyOpen = true)]
        [ReportItemData]
        public ActionResult Provide2()
        {
            return View(new ReportModel());
        }

        [HttpPost]
        [ReportItemData]
        [ApplicationClassData(OnlyOpen = true)]
        public ActionResult SendOrder(ReportModel model)
        {
            HttpPostedFileBase picFile = null;
            foreach (string file in Request.Files)
            {

                picFile = Request.Files[file] as HttpPostedFileBase;
            }

            ModelState.Remove("Name");
            string captcha = AccountUtil.GetCaptcha();
            if (picFile == null || picFile.ContentLength <= 0)
            {
                ModelState.AddModelError("ReportPic", "請選擇報告書封面圖片");
            }
            else if (picFile.ContentLength > (1024 * 1024))
            {
                ModelState.AddModelError("ReportPic", "請選擇小於 1MB 的報告書封面圖片");
            }
            else if (!IsImage(picFile))
            {
                ModelState.AddModelError("ReportPic", "請上傳圖片格式的報告書封面圖片");
            }
            else if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");
            }
            else
            {
                if (picFile.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picFile.FileName);
                    var path = Path.Combine(AppSettings.CKFinderBaseDir, "webuser/images/", fileName);
                    picFile.SaveAs(path);
                    model.ReportPic = "webuser/images/" + fileName;
                }

                if (model.IsValid(ModelState))
                {
                    model.Status = "0";
                    model.Insert();
                }
            }
            if (model.SendOrderOk)
            {
                ModelState.Clear();
            }
            return View("Provide2", model);
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

            // linq from Henrik Stenbæk
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public ActionResult CaptchaImg()
        {
            var builder = new XCaptcha.ImageBuilder(CaptchaHelper.GetRandomStringOnlyNum(6));

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }

        private void EntityCounter(string id, string type)
        {
            Report entity = m_FTISService.GetReportById(int.Parse(DecryptId(id)));
            switch (type)
            {
                case "CVister":
                    entity.CVister += 1;
                    break;
                case "EVister":
                    entity.EVister += 1;
                    break;
                case "DVister":
                    entity.DVister += 1;
                    break;
            }
            m_FTISService.UpdateReport(entity);
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }
    }
}
