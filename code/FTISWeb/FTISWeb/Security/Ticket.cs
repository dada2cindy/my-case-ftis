using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FTISWeb.Utility;

namespace FTISWeb.Security
{
    public class Ticket
    {
        /// <summary>
        /// 產生驗證票証並寫入cookie
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="loginUser"></param>
        /// <param name="sucessLogOn"></param>
        /// <param name="rememberMe"></param>
        /// <param name="versionNo">重新登入版本號，rememberMe為True時，此數值會一併被記錄下來</param>
        /// <returns></returns>
        private static FormsAuthenticationTicket WriteCookie(string loginUser, bool sucessLogOn, bool rememberMe, int versionNo)
        {
            string cookiename = GetCookieName();
            string key = cookiename;
            string resultdata = (new TicketUserData(sucessLogOn, versionNo)).UserData; //string.Format("{0},{1}", (sucessLogOn ? 1 : 0), versionNo.ToString());

            //// 建立一個新的FormsAuthenticationTicket
            var newAuthTicket = new FormsAuthenticationTicket(1, loginUser, DateTime.Now, DateTime.Now.AddHours(8), false, resultdata);
            var value = FormsAuthentication.Encrypt(newAuthTicket);

            //// *** 寫入cookie開始 ***
            HttpContext.Current.Response.Cookies.Remove(cookiename);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];

            if (cookie == null)
            {
                cookie = new HttpCookie(cookiename, value);
            }
            else
            {
                if (CookieValueExists(cookiename, key))
                {
                    cookie.Values.Set(key, value);
                }
                else
                {
                    cookie.Value = value;
                }
            }

            if (rememberMe)
            {
                cookie.Expires = DateTime.Now.AddMonths(1);
            }

            cookie.Domain = "FTIS";  /*若改成finalStoreName會再跑回登入頁，不可改*/
            cookie.HttpOnly = true;
            HttpContext.Current.Response.SetCookie(cookie);
            //// *** 寫入cookie結束 ***

            return newAuthTicket;
        }

        private static bool CookieValueExists(string cookiename, string key)
        {
            return HttpContext.Current.Request != null
                   && HttpContext.Current.Request.Cookies != null
                   && HttpContext.Current.Request.Cookies[cookiename] != null
                   && HttpContext.Current.Request.Cookies[cookiename].Values[key] != null;
        }

        private static string GetCookieName()
        {
            return ".FTIS_TICKET";
        }

        /// <summary>
        /// 登出時，將request cookie設為過期，並重新設定
        /// </summary>
        public static void SignOut()
        {
            CookieUtil.RemoveCookie(GetCookieName());
        }

        /// <summary>
        /// 登入時，產生驗證票証並寫入cookie
        /// </summary>
        /// <param name="account"></param>
        /// <param name="rememberMe"></param>
        /// <param name="versionNo">重新登入版本號，rememberMe為True時，此數值會一併被記錄下來</param>
        public static void SignIn(string account, bool rememberMe, int versionNo)
        {
            WriteCookie(account, true, rememberMe, versionNo);
        }
    }
}