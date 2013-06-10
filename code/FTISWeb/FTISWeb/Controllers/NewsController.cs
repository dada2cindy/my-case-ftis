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
    public partial class NewsController : Controller
    {
        [NewsClassData(OnlyOpen = true)]
        public ActionResult Index(string keyWord, string newsClassId, string newsTypeId, int? page)
        {
            SetConditions(keyWord, DecryptId(newsClassId), DecryptId(newsTypeId));
            m_Conditions.Add("Status", "1");
            int total = GetGridTotal();
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            m_Conditions.Add("PageIndex", pageIndex.ToString());
            m_Conditions.Add("PageSize", AppSettings.InSitePageSize.ToString());            

            var data = GetGridData();
            return View(data.ToPagedList(pageIndex, AppSettings.InSitePageSize, total));
        }

        [NewsClassData(OnlyOpen = true)]
        public ActionResult EngIndex(string keyWord, string newsClassId, string newsTypeId, int? page)
        {
            SetConditions(keyWord, DecryptId(newsClassId), DecryptId(newsTypeId));
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
            NewsModel entityModel = new NewsModel(id);
            if ("1".Equals(entityModel.IsOut) && !string.IsNullOrWhiteSpace(entityModel.AUrl))
            {
                Response.Redirect(entityModel.AUrl);
            }
            return View(entityModel);
        }

        public ActionResult EngDetail(string id, string cdts)
        {
            GetConditions(cdts);
            EntityCounter(id, "VisterENG");
            NewsModel entityModel = new NewsModel(id);
            if ("1".Equals(entityModel.IsOut) && !string.IsNullOrWhiteSpace(entityModel.AUrl))
            {
                Response.Redirect(entityModel.AUrl);
            }
            return View(entityModel);
        }

        public ActionResult Email(string id)
        {
            return View(new NewsModel(id));
        }

        public ActionResult SendMail(SendMailModel model, string id)
        {
            NewsModel entityModel = new NewsModel(id);
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.SendMailConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(string.Empty, "驗證碼錯誤");
            }
            else
            {
                model.SendMailTitle = string.Format("收到一封由【{0}】從產業永續發展整合資訊網的轉寄信：{1}。"
                    , model.SendMailName, entityModel.Name);
                model.SendMailContent += entityModel.Content;
                model.SendMail();
                EntityCounter(id, "Emailer");
                ViewData["SendMailOk"] = true;
            }
            return View("Email", entityModel);
        }

        public ActionResult Print(string id)
        {
            EntityCounter(id, "Printer");
            return View(new NewsModel(id));
        }

        public ActionResult CaptchaImg()
        {
            var builder = new XCaptcha.ImageBuilder(4);

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }

        private string DecryptId(string id)
        {
            return EncryptUtil.DecryptDES(id, AppSettings.EncryptKey, AppSettings.EncryptIV);
        }

        private void EntityCounter(string id, string type)
        {
            News entity = m_FTISService.GetNewsById(int.Parse(new NewsModel().DecryptId(id)));

            switch (type)
            {
                case "Vister":
                    entity.Vister += 1;
                    break;
                case "VisterENG":
                    entity.VisterENG += 1;
                    break;
                case "Emailer":
                    entity.Emailer += 1;
                    break;
                case "Printer":
                    entity.Printer += 1;
                    break;
            }
            m_FTISService.UpdateNews(entity);
        }
    }
}
