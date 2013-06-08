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
using FTISWeb.Utility;
using MvcPaging;

namespace FTISWeb.Controllers
{
    public partial class ActivityController : Controller
    {
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
            return View(new ActivityModel(id));
        }

        public ActionResult Email(string id)
        {            
            return View(new ActivityModel(id));
        }

        public ActionResult SendMail(SendMailModel model, string id)
        {
            ActivityModel entityModel = new ActivityModel(id);
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
            return View(new ActivityModel(id));
        }

        public ActionResult CaptchaImg()
        {
            var builder = new XCaptcha.ImageBuilder(4);

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }

        private void EntityCounter(string id, string type)
        {
            Activity entity = m_FTISService.GetActivityById(int.Parse(new ActivityModel().DecryptId(id)));

            switch (type)
            {
                case "Vister":
                    entity.Vister += 1;
                    break;
                case "Emailer":
                    entity.Emailer += 1;
                    break;
                case "Printer":
                    entity.Printer += 1;
                    break;
            }
            m_FTISService.UpdateActivity(entity);
        }
    }
}
