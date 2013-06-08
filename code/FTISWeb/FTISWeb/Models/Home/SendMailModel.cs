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
using Microsoft.Security.Application;
using FTISWeb.Utility;
using FTISWeb.Service;

namespace FTISWeb.Models
{
    public class SendMailModel : AbstractShowModel
    {
        public SendMailModel()
        {

        }

        /// <summary>
        /// 發信人
        /// </summary>
        [DisplayName("你的暱稱")]
        [Required]
        public string SendMailName { get; set; }

        /// <summary>
        /// 你的Email
        /// </summary>
        public string SendMailFrom { get; set; }

        /// <summary>
        /// 電子信箱
        /// </summary>
        [DisplayName("電子信箱")]
        [Required]
        public string SendMailTo { get; set; }

        /// <summary>
        /// 主旨
        /// </summary>
        public string SendMailTitle { get; set; }

        /// <summary>
        /// 留言/內容
        /// </summary>
        public string SendMailContent { get; set; }

        /// <summary>
        /// 驗證碼
        /// </summary>
        [DisplayName("驗證碼")]
        [Required]
        public string SendMailConfirmationCode { get; set; }

        public bool SendMailOk { get; set; }

        public void SendMail()
        {
            WebMailService mailService = new WebMailService();
            mailService.SendMail(this);
            this.SendMailOk = true;
        }
    }
}
