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
            //string keyPattern = "Captcha_{0}";
            //return string.Format(keyPattern, CommonUtil.ClientIp().Replace('.', '_'));
            return "Captcha_FTIS";
        }

        public static void ClearCaptcha()
        {
            CookieUtil.RemoveCookie(GetKeyOfCaptcha());
        }

        public static void SetCaptcha(string solution)
        {
            ClearCaptcha();
            string key = GetKeyOfCaptcha();
            CookieUtil.SetCookie(key, solution, DateTime.Now.AddMinutes(10), string.Empty, string.Empty, false);
        }

        public static string GetCaptcha()
        {
            string key = GetKeyOfCaptcha();
            return CookieUtil.GetCookie(key) ?? string.Empty;
        }
    }
}