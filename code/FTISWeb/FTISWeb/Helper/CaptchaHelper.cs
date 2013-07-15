using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTISWeb.Helper
{
    public class CaptchaHelper
    {
        const string Charset = "0123456789abcdefghijklmnoprstuvyzqxw0123456789ABCDEFGHIJKLMNOPRSTUVYZQXW0123456789";
        const string CharsetNum = "0123456789";

        public static string GetRandomString(int length)
        {
            Random Rnd = new Random();

            string captchaString = string.Empty;

            for (int i = 0; i < length; i++)
            {
                captchaString += Charset[Rnd.Next(0, Charset.Length)];
            }

            return captchaString;
        }

        public static string GetRandomStringOnlyNum(int length)
        {
            Random Rnd = new Random();

            string captchaString = string.Empty;

            for (int i = 0; i < length; i++)
            {
                captchaString += CharsetNum[Rnd.Next(0, CharsetNum.Length)];
            }

            return captchaString;
        }
    }
}