using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 會員
    /// </summary>
    [Serializable]
    [DataContract]
    public class Member
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int MemberId { get; set; }

        /// <summary>
        /// 行業別
        /// </summary>
        [DataMember]
        public virtual Industry Industry { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        [DataMember]
        public virtual string LoginId { get; set; }

        /// <summary>
        /// 登入密碼
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DataMember]
        public virtual string Company { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        [DataMember]
        public virtual string CompanyNo { get; set; }

        /// <summary>
        /// 公司規模 1.大企業 2.中小企業
        /// </summary>
        [DataMember]
        public virtual string CompanyNum { get; set; }

        /// <summary>
        /// 公司屬性(複選,分隔) 1.ODM(設計製造代工廠) 2.OBM(品牌廠商) 3.OEM(設備製造代工廠) 4.其他
        /// </summary>
        [DataMember]
        public virtual string CompanyType { get; set; }

        /// <summary>
        /// 公司屬性其它(自key文字)
        /// </summary>
        [DataMember]
        public virtual string CompanyTypeOther { get; set; }

        /// <summary>
        /// 連絡人
        /// </summary>
        [DataMember]
        public virtual string Contact { get; set; }

        /// <summary>
        /// 部門
        /// </summary>
        [DataMember]
        public virtual string Dept { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        [DataMember]
        public virtual string JobTitle { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [DataMember]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        [DataMember]
        public virtual string Tel2 { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        [DataMember]
        public virtual string Fax { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 申請日期
        /// </summary>
        [DataMember]
        public virtual DateTime? RegDate { get; set; }

        /// <summary>
        /// 主要產品內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }        

        /// <summary>
        /// 狀態. 0.審核中 1.審核通過 2.審核不通過
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        public virtual string GetStr_Status
        {
            get
            {
                string result = string.Empty;
                switch (this.Status)
                {
                    case "0":
                        result = "審核中";
                        break;
                    case "1":
                        result = "審核通過";
                        break;
                    case "2":
                        result = "審核不通過";
                        break;
                }

                return result;
            }
        }

        public virtual string GetStr_RegDate
        {
            get
            {
                string result = string.Empty;

                if (RegDate != null)
                {
                    result = RegDate.Value.ToString("yyyy/MM/dd");
                }

                return result;
            }
        }

        #endregion
    }
}
