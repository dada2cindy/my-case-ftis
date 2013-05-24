using System;
using System.Linq;
using System.Net;
using System.Web;

namespace FTISWeb.Utility
{
    public static class IpBlockUtil
    {
        public static string ClientIp()
        {
            var variables = new[] { "HTTP_X_FORWARDED_FOR", "HTTP_X_REAL_IP", "X-REAL-IP", "REMOTE_ADDR" };
            foreach (string variable in
                variables.Where(variable => HttpContext.Current.Request.ServerVariables[variable] != null))
            {
                string clientIp = HttpContext.Current.Request.ServerVariables[variable];

                ////無痛版用url reWrite的時候，ip後面會多 :xxx，考量IPV6的格式，過濾第8個字後最後出現:後面的字
                if (!string.IsNullOrWhiteSpace(clientIp) && clientIp.LastIndexOf(':') >= 7)
                {
                    clientIp = clientIp.Substring(0, clientIp.LastIndexOf(':'));
                }
                return clientIp;
            }
            return string.Empty;
        }

        /// <summary>
        /// IP 比對
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IPEquals(string source, string target)
        {
            string[] allIp = source.Split(',');
            bool resultStatus = false;
            foreach (string sourceIp in allIp)
            {
                string sIp = sourceIp.Trim();
                string subNetMask = SubnetMask(sIp);
                IPAddress nIpAddress = IPAddress.Parse(sIp.Replace('*', '1'));
                IPAddress nSubnetMask = IPAddress.Parse(subNetMask);
                IPAddress nNetworkAddress = new IPAddress(IPAddress.NetworkToHostOrder(IPAddress.HostToNetworkOrder(nIpAddress.Address) & IPAddress.HostToNetworkOrder(nSubnetMask.Address)));

                IPAddress nIpAddress1 = IPAddress.Parse(target);
                IPAddress nSubnetMask1 = IPAddress.Parse(subNetMask);
                IPAddress nNetworkAddress1 = new IPAddress(IPAddress.NetworkToHostOrder(IPAddress.HostToNetworkOrder(nIpAddress1.Address) & IPAddress.HostToNetworkOrder(nSubnetMask1.Address)));

                resultStatus = nNetworkAddress.Equals(nNetworkAddress1);
                if (resultStatus)
                {
                    break;
                }
            }
            return resultStatus;
        }

        /// <summary>
        /// 處理網路遮罩
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SubnetMask(string source)
        {
            ///// 第一個0後面0
            string[] rtnIp = "255.255.255.255".Split('.');
            string[] s = source.Trim().Split('.');

            for (int i = 0; i < rtnIp.Length; i++)
            {
                bool g = s[i].Trim().Equals("*");
                rtnIp[i] = g ? "0" : "255";
                if (g)
                {
                    for (int j = i; j < rtnIp.Length; j++)
                    {
                        rtnIp[j] = "0";
                    }
                    break;
                }
            }
            return string.Join(".", rtnIp);
        }
    }
}