using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTISWeb.Utility
{
    public class CommonUtil
    {
        /// <summary>
        /// Convert null string to empty string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The string.</returns>
        public static string ConvertString(string input)
        {
            return input ?? string.Empty;
        }

        /// <summary>
        /// request ur client ip
        /// </summary>
        /// <returns></returns>
        public static string ClientIp()
        {
            return IpBlockUtil.ClientIp();
        }

        /// <summary>
        /// IP 比對
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IPEquals(string source, string target)
        {
            return IpBlockUtil.IPEquals(source, target);
        }
    }
}