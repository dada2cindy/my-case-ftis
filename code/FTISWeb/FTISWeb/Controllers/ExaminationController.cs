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
    public partial class ExaminationController : Controller
    {
        [IndustryData(OnlyOpen = true)]
        public ActionResult Index()
        {
            return View(new ExaminationModel());
        }

        [IndustryData(OnlyOpen = true)]
        public ActionResult SendOrder(ExaminationModel model)
        {            
            ModelState.Remove("PostDate");
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");

                if (model.QuestionSelects2 != null && model.QuestionSelects2.Count() > 0)
                {
                    ModelState.Remove("Question2");
                }

                if (model.QuestionSelects3 != null && model.QuestionSelects3.Count() > 0)
                {
                    ModelState.Remove("Question3");
                }

                if (model.QuestionSelects9 != null && model.QuestionSelects9.Count() > 0)
                {
                    ModelState.Remove("Question9");
                }

                if (model.QuestionSelects10 != null && model.QuestionSelects10.Count() > 0)
                {
                    ModelState.Remove("Question10");
                }
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
            var builder = new XCaptcha.ImageBuilder(CaptchaHelper.GetRandomStringOnlyNum(6));

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }
    }
}
