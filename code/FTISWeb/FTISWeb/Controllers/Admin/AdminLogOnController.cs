using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FTISWeb.Models;
using FTISWeb.Security;
using FTISWeb.Helper;
using FTISWeb.Utility;

namespace FTISWeb.Controllers
{
    public class LogOnController : Controller
    {
        private SessionHelper m_SessionHelper = new SessionHelper();

        public ActionResult Unauthorized()
        {
            return View(new UnauthorizedModel(User.Identity.IsAuthenticated));
        }

        public ActionResult Logon()
        {
            if (User.Identity.IsAuthenticated && m_SessionHelper.LoginUser != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        public ActionResult CaptchaImg()
        {
            var builder = new XCaptcha.ImageBuilder(CaptchaHelper.GetRandomStringOnlyNum(6));

            var result = builder.Create();
            AccountUtil.SetCaptcha(result.Solution);
            return new FileContentResult(result.Image, result.ContentType);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            string captcha = AccountUtil.GetCaptcha();
            if (!captcha.Equals(model.ConfirmationCode, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(string.Empty, "驗證碼錯誤");
            }
            else
            {
                if (model.ValidateUser())
                {
                    return !String.IsNullOrWhiteSpace(model.RtnUrl)
                                ? (ActionResult)Redirect(model.RtnUrl)
                                : RedirectToAction("Index", "Admin", new { RtnUrl = HttpContext.Request["RtnUrl"] });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼錯誤");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                //// 移除cookie
                Ticket.SignOut();
                m_SessionHelper.LoginUser = null;
            }
            catch (ArgumentException)
            {
                /* 使用者可能早已逾時或是清除cookie，因為CategoryRouteHandler裡處理logon相關的url都不再redirect logon，所以此url即使沒登入也不會被擋下，此狀況略過錯誤訊息。 */
            }

            return View("Unauthorized", new UnauthorizedModel());
        }

        /// <summary>
        /// 測試用快速登入
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult TestLogon()
        {
            LogOnModel model = new LogOnModel() { Account = "admin", Password = "01801726", ConfirmationCode = AccountUtil.GetCaptcha() };
            if (model.ValidateUser())
            {
                return !String.IsNullOrWhiteSpace(model.RtnUrl)
                            ? (ActionResult)Redirect(model.RtnUrl)
                            : RedirectToAction("Index", "Admin", new { RtnUrl = HttpContext.Request["RtnUrl"] });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "帳號密碼錯誤");
            }

            return View("LogOn");
        }
    }
}
