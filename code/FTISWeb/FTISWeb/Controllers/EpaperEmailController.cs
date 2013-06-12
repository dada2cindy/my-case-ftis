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
    public partial class EpaperEmailController : Controller
    {
        public ActionResult Order()
        {
            return View("Order", new EpaperEmailModel());
        }

        public ActionResult SendOrder(EpaperEmailModel model)
        {
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");
            }
            else
            {
                if (model.IsValid())
                {
                    model.SendOrder();                                        
                }
            }
            if (model.SendOrderOk)
            {
                ModelState.Clear();
            }
            return View("Order", model);
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
