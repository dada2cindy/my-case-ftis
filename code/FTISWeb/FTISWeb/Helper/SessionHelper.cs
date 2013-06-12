using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Common.Logging;
using FTIS.Domain.Impl;

namespace FTISWeb.Helper
{
    public class SessionHelper
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string LOGIN_USER = "LOGIN_USER";
        private readonly string WEB_USER = "WEB_USER";

        /// <summary>
        /// 使用者的資訊
        /// </summary>
        public MasterMember LoginUser
        {
            get
            {
                try
                {
                    return (GetMySession()[LOGIN_USER] == null ? null : (MasterMember)GetMySession()[LOGIN_USER]);
                }
                catch
                {
                    // log.Error(ex);
                    return null;
                }
            }

            set { GetMySession()[LOGIN_USER] = value; }
        }

        /// <summary>
        /// 使用者的資訊
        /// </summary>
        public Member WebMember
        {
            get
            {
                try
                {
                    return (GetMySession()[WEB_USER] == null ? null : (Member)GetMySession()[WEB_USER]);
                }
                catch
                {
                    // log.Error(ex);
                    return null;
                }
            }

            set { GetMySession()[WEB_USER] = value; }
        }

        private HttpSessionState GetMySession()
        {
            HttpContext ctx = HttpContext.Current;
            return ctx.Session;
        }
    }    
}