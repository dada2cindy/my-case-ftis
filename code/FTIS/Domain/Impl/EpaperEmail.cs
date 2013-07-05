using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 電子報訂閱
    /// </summary>
    [Serializable]
    [DataContract]
    public class EpaperEmail
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int EpaperEmailId { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 申請/退定日期
        /// </summary>
        [DataMember]
        public virtual DateTime? RegDate { get; set; }

        /// <summary>
        /// 產業別 1.製造業 2.服務業 3.政府機關 4.學術/研究單位 5.其他
        /// </summary>
        [DataMember]
        public virtual string InType { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [DataMember]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DataMember]
        public virtual string Company { get; set; }

        /// <summary>
        /// 部門
        /// </summary>
        [DataMember]
        public virtual string Dept { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 主要產品內容
        /// </summary>
        [DataMember]
        public virtual string Address { get; set; }

        /// <summary>
        /// 狀態. 0.退定 1.訂閱
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        /// <summary>
        /// 性別 男.女
        /// </summary>
        [DataMember]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 是否同意獲得本會其他免費資訊. 0.否 1.是
        /// </summary>
        [DataMember]
        public virtual string ReceiveOtherFreeInfo { get; set; }

        public virtual string GetStr_InType
        {
            get
            {
                string result = string.Empty;

                if (!string.IsNullOrEmpty(this.InType.Trim()))
                {
                    string[] list = this.InType.Split(',');
                    if (list != null && list.Length > 0)
                    {
                        foreach (string s in list)
                        {
                            switch (s.Trim())
                            {
                                case "1":
                                    result += string.Format("{0} ", "製造業");
                                    break;
                                case "2":
                                    result += string.Format("{0} ", "服務業");
                                    break;
                                case "3":
                                    result += string.Format("{0} ", "政府機關");
                                    break;
                                case "4":
                                    result += string.Format("{0} ", "學術/研究單位");
                                    break;
                                case "5":
                                    result += string.Format("{0} ", "其他");
                                    break;
                            }
                        }
                    }
                }

                return result;
            }
        }

        public virtual string GetStr_ReceiveOtherFreeInfo
        {
            get
            {
                string result = "否";
                switch (this.ReceiveOtherFreeInfo)
                {
                    case "0":
                        result = "否";
                        break;
                    case "1":
                        result = "是";
                        break;
                }

                return result;
            }
        }

        public virtual string GetStr_Status
        {
            get
            {
                string result = string.Empty;
                switch (this.Status)
                {
                    case "0":
                        result = "退閱";
                        break;
                    case "1":
                        result = "訂閱";
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
