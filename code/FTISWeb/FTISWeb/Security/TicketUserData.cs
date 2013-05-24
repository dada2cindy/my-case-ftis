using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FTISWeb.Security
{
    /// <summary>
    /// 協助 Ticket.cs 處理 FormsAuthenticationTicket UserData 屬性值
    /// </summary>
    public class TicketUserData
    {
        #region Properties

        private bool isAuth = false;
        public bool IsAuthenticated
        {
            get
            {
                return (splitedData.Length > 1 && splitedData[1] == "1");
            }
        }

        private int versionNo = 1;
        public int RELogOnVersionNo
        {
            get
            {
                if (splitedData.Length > 2 && !string.IsNullOrWhiteSpace(splitedData[2]))
                {
                    return int.Parse(splitedData[2], System.Globalization.NumberStyles.Integer);
                }

                return 1;
            }
        }

        private string userData = null;
        private string[] splitedData = { };
        public string UserData
        {
            get
            {
                return string.Format("{0},{1}", (isAuth ? 1 : 0), versionNo.ToString());
            }
        }

        bool hasValues = false;
        public bool HasValues
        {
            get
            {
                return hasValues;
            }
        }
        #endregion

        public TicketUserData()
        {
        }

        public TicketUserData(FormsAuthenticationTicket authTicket)
        {
            if (authTicket.UserData.Length > 0)
            {
                userData = authTicket.UserData;
                splitedData = userData.Split(new[] { "," }, StringSplitOptions.None);
                hasValues = true;
            }
        }

        public TicketUserData(bool isAuthenticated, int reLogOnVersionNo)
        {
            isAuth = isAuthenticated;
            versionNo = reLogOnVersionNo;
            hasValues = true;
        }
    }
}