using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using FTIS;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Security;
using FTISWeb.Helper;
using WuDada.Core.Generic.Util;
using System.ComponentModel;

namespace FTISWeb.Models
{
    public class LogOnModel 
    {
        private FTISFactory m_FTISFactory = new FTISFactory();
        private IFTISService m_FTISService;

        private SessionHelper m_SessionHelper = new SessionHelper();

        public LogOnModel()
        {
            m_FTISService = m_FTISFactory.GetFTISService();
        }

        //protected MasterMember modelUser = null;

        /// <summary>
        /// 登入帳號
        /// </summary>
        [DisplayName("帳號")]
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 登入密碼
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 記住我
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 確認碼
        /// </summary>
        [DisplayName("確認碼")]
        [Required]
        public string ConfirmationCode { get; set; }

        public string RtnUrl { get; set; }

        /// <summary>
        /// 驗證使用者，若通過驗證則完成簽入動作
        /// </summary>
        /// <returns></returns>
        public bool ValidateUser()
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Account", this.Account);
            IList<MasterMember> adminList = m_FTISService.GetMasterMemberListNoLazy(conditions);
            if (adminList != null && adminList.Count() > 0)
            {
                MasterMember admin = adminList[0];
                string passwordDecrypt = EncryptUtil.GetMD5(Password);
                if (passwordDecrypt.Equals(admin.Password, StringComparison.OrdinalIgnoreCase))
                {
                    //modelUser = admin;
                    FormsAuthentication.SetAuthCookie(this.Account, this.RememberMe);
                    Ticket.SignIn(this.Account, this.RememberMe, 1);
                    m_SessionHelper.LoginUser = admin;
                    return true;
                }
            }

            return false;
        }
    }
}
