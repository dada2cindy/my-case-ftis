using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTISWeb.Utility
{
    public class AccountUtil
    {
        public static string GetKeyOfCaptcha()
        {
            string keyPattern = "Captcha_{0}";
            return string.Format(keyPattern, CommonUtil.ClientIp().Replace('.', '_'));
        }

        public static void ClearCaptcha()
        {
            CookieUtil.RemoveCookie(GetKeyOfCaptcha());
        }

        public static void SetCaptcha(string solution)
        {
            ClearCaptcha();
            CookieUtil.SetCookie(GetKeyOfCaptcha(), solution, DateTime.Now.AddMinutes(10), string.Empty, string.Empty, false);
        }

        public static string GetCaptcha()
        {
            return CookieUtil.GetCookie(GetKeyOfCaptcha()) ?? string.Empty;
        }
    }
}