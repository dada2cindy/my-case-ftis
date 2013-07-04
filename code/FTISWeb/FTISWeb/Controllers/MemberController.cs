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
    public partial class MemberController : Controller
    {
        SessionHelper m_SessionHelper = new SessionHelper();

        public ActionResult Index()
        {
            if (m_SessionHelper.WebMember == null)
            {
                return View("Logon", new MemberModel());
            }
            else
            {
                return View("LogOff", new MemberModel(m_SessionHelper.WebMember.MemberId));
            }
        }

        [IndustryData(OnlyOpen = true)]
        public ActionResult Join()
        {
            return View(new MemberModel());
        }

        [IndustryData(OnlyOpen = true)]
        [HttpPost]
        public ActionResult Join(MemberModel model)
        {            
            ModelState.Remove("RegDate");
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {                
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");
            }
            else if (model.Password != model.CheckPassword)
            {
                ModelState.AddModelError("Password", "");
                ModelState.AddModelError("CheckPassword", "登入密碼與確認密碼不一樣");
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
                return View("LogOff", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Logon(MemberModel model)
        {
            ModelState.Remove("IndustryId");
            ModelState.Remove("Name");
            ModelState.Remove("Company");
            ModelState.Remove("CompanyNo");
            ModelState.Remove("CompanyNum");
            ModelState.Remove("CompanyTypeList");
            ModelState.Remove("Email");
            ModelState.Remove("RegDate");
            ModelState.Remove("CheckPassword");
            ModelState.Remove("ReceiveEpaperInfo");
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");
            }
            else
            {
                model.DoLogon();
            }

            if (model.SendOrderOk)
            {
                return View("LogOff", model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DoLogoff()
        {
            m_SessionHelper.WebMember = null;
            return View("Logon", new MemberModel());
        }

        public ActionResult CaptchaImg()
        {
            var builder = new XCaptcha.ImageBuilder(CaptchaHelper.GetRandomStringOnlyNum(6));

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }

        [HttpPost]
        public ActionResult CheckLoginId(string loginId)
        {
            bool isExistMember = new MemberModel().CheckLoginId(loginId);

            return Json(isExistMember);
        }

        public ActionResult ForgetPass()
        {
            return View(new MemberModel());
        }

        [HttpPost]
        public ActionResult ForgetPass(MemberModel model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("IndustryId");
            ModelState.Remove("Name");
            ModelState.Remove("Company");
            ModelState.Remove("CompanyNo");
            ModelState.Remove("CompanyNum");
            ModelState.Remove("CompanyTypeList");
            ModelState.Remove("RegDate");
            ModelState.Remove("CheckPassword");
            ModelState.Remove("ReceiveEpaperInfo");
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ConfirmationCode", "驗證碼錯誤");
            }
            else
            {
                model.ForgetPass();                
            }

            return View("ForgetPass", model);
        }
    }
}
