using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTISWeb.Helper
{
    public class HttpHelper
    {
        public HttpHelper()
        {

        }

        /// <summary>
        /// 獲取使用者的真實IP位置
        /// </summary>
        /// <returns></returns>
        public static string GetUserIp()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            return result;
        }

        /// <summary>
        /// 取得來到站之前的位置
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetReferer()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            return result;
        }
    }
}