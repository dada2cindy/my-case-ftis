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
    public partial class EpaperExaminationController : Controller
    {
        [IndustryData(OnlyOpen = true)]
        public ActionResult Index()
        {
            return View(new EpaperExaminationModel());
        }

        [IndustryData(OnlyOpen = true)]
        public ActionResult SendOrder(EpaperExaminationModel model)
        {            
            ModelState.Remove("PostDate");
            ModelState.Remove("Question10");
            ModelState.Remove("Question11");
            ModelState.Remove("Question12");
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");
            }
            else
            {
                if (model.IsValid())
                {
                    model.Insert();
                    model.SendOrderOk = true;
                }
            }
            if (model.SendOrderOk)
            {
                return View("SendOrderOK");
            }
            else
            {
                return View("Index", model);
            }
        }

        public ActionResult CaptchaImg()
        {
            var builder = new XCaptcha.ImageBuilder(4);

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }
    }
}
