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
    /// <summary>
    /// 問卷
    /// </summary>
    public class AbstractExaminationModel : AbstractShowModel
    {
        public AbstractExaminationModel()
        {
        }

        public int EntityId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 調查日期
        /// </summary>
        [DisplayName("調查日期")]
        [Required]
        public DateTime? PostDate { get; set; }

        /// <summary>
        /// 行業別
        /// </summary>
        [DisplayName("行業別")]
        [Required]
        public virtual int IndustryId { get; set; }

        /// <summary>
        /// 問題1
        /// </summary>
        [DisplayName("問題1")]
        [Required]
        public virtual string Question1 { get; set; }

        public virtual string QOther1 { get; set; }

        /// <summary>
        /// 問題2
        /// </summary>
        [DisplayName("問題2")]
        [Required]
        public virtual string Question2 { get; set; }

        public virtual string QOther2 { get; set; }

        /// <summary>
        /// 問題3
        /// </summary>
        [DisplayName("問題3")]
        [Required]
        public virtual string Question3 { get; set; }
        
        public virtual string QOther3 { get; set; }

        /// <summary>
        /// 問題4
        /// </summary>
        [DisplayName("問題4")]
        [Required]
        public virtual string Question4 { get; set; }

        public virtual string QOther4 { get; set; }

        /// <summary>
        /// 問題5
        /// </summary>
        [DisplayName("問題5")]
        [Required]
        public virtual string Question5 { get; set; }

        public virtual string QOther5 { get; set; }

        /// <summary>
        /// 問題6
        /// </summary>
        [DisplayName("問題6")]
        [Required]
        public virtual string Question6 { get; set; }

        public virtual string QOther6 { get; set; }

        /// <summary>
        /// 問題7
        /// </summary>
        [DisplayName("問題7")]
        [Required]
        public virtual string Question7 { get; set; }

        public virtual string QOther7 { get; set; }

        /// <summary>
        /// 問題8
        /// </summary>
        [DisplayName("問題8")]
        [Required]
        public virtual string Question8 { get; set; }

        public virtual string QOther8 { get; set; }

        /// <summary>
        /// 問題9
        /// </summary>
        [DisplayName("問題9")]
        [Required]
        public virtual string Question9 { get; set; }
       
        public virtual string QOther9 { get; set; }

        /// <summary>
        /// 問題10
        /// </summary>
        [DisplayName("問題10")]
        [Required]
        public virtual string Question10 { get; set; }

        public virtual string QOther10 { get; set; }

        /// <summary>
        /// 問題11
        /// </summary>
        [DisplayName("問題11")]
        [Required]
        public virtual string Question11 { get; set; }

        public virtual string QOther11 { get; set; }

        /// <summary>
        /// 問題12
        /// </summary>
        [DisplayName("問題12")]
        [Required]
        public virtual string Question12 { get; set; }

        public virtual string QOther12 { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DisplayName("電子郵件")]
        [Required]
        public virtual string Email { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [DisplayName("性別")]
        [Required]
        public virtual string Gender { get; set; }

        /// <summary>
        /// 驗證碼
        /// </summary>
        [DisplayName("驗證碼")]
        [Required]
        public string ConfirmationCode { get; set; }

        public bool SendOrderOk { get; set; }

        public string Msg { get; set; }

        public string ErrorMsg { get; set; }
    }
}
