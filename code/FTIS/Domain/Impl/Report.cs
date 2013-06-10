using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 報告書
    /// </summary>
    [Serializable]
    [DataContract]
    public class Report : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int ReportId { get; set; }        

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DataMember]
        public virtual string Company { get; set; }

        /// <summary>
        /// 提供日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// 行業別
        /// </summary>
        [DataMember]
        public virtual string CompanyTrade { get; set; }

        /// <summary>
        /// 公司網址
        /// </summary>
        [DataMember]
        public virtual string AUrl { get; set; }

        /// <summary>
        /// 公司國籍 0.國外 1.國內
        /// </summary>
        [DataMember]
        public virtual string CompanyNationality { get; set; }

        /// <summary>
        /// 公司規模 0.中小企業 1.大企業
        /// </summary>
        [DataMember]
        public virtual string CompanyScale { get; set; }

        /// <summary>
        /// 公司類型 1.上市公司 2.上櫃公司 3.興櫃公司 4.其它
        /// </summary>
        [DataMember]
        public virtual string CompanyType { get; set; }

        /// <summary>
        /// 發佈年份(西元年)
        /// </summary>
        [DataMember]
        public virtual int PostYear { get; set; }

        /// <summary>
        /// 報告書名稱
        /// </summary>
        [DataMember]
        public virtual string ReportName { get; set; }

        /// <summary>
        /// 報告年份(西元年)
        /// </summary>
        [DataMember]
        public virtual int ReportYear { get; set; }

        /// <summary>
        /// 報告格式 (如PDF、線上閱讀)
        /// </summary>
        [DataMember]
        public virtual string ReportType { get; set; }

        /// <summary>
        /// 報告頁數
        /// </summary>
        [DataMember]
        public virtual string ReportPage { get; set; }

        /// <summary>
        /// AA1000標準 1.通過 2.無
        /// </summary>
        [DataMember]
        public virtual string AA1000 { get; set; }

        /// <summary>
        /// GRI等級 1.A+ 2.A 3.B+ 4.B 5.C+ 6.C 7.無
        /// </summary>
        [DataMember]
        public virtual string GRI { get; set; }

        /// <summary>
        /// 報告書封面 (檔案格式為JPEG，檔案大小1MB為限)
        /// </summary>
        [DataMember]
        public virtual string ReportPic { get; set; }

        /// <summary>
        /// CSR網頁1
        /// </summary>
        [DataMember]
        public virtual string CSRPage1 { get; set; }

        /// <summary>
        /// CSR網頁2
        /// </summary>
        [DataMember]
        public virtual string CSRPage2 { get; set; }

        /// <summary>
        /// 語言版本--中文：下載網址
        /// </summary>
        [DataMember]
        public virtual string CAUrl { get; set; }

        /// <summary>
        /// 中文：下載人數
        /// </summary>
        [DataMember]
        public virtual int CVister { get; set; }

        /// <summary>
        /// 語言版本--英文：下載網址
        /// </summary>
        [DataMember]
        public virtual string EAUrl { get; set; }

        /// <summary>
        /// 英文：下載人數
        /// </summary>
        [DataMember]
        public virtual int EVister { get; set; }

        /// <summary>
        /// 語言版本--雙語：下載網址
        /// </summary>
        [DataMember]
        public virtual string DAUrl { get; set; }

        /// <summary>
        /// 雙語：下載人數名稱
        /// </summary>
        [DataMember]
        public virtual int DVister { get; set; }

        /// <summary>
        /// 報告確證
        /// </summary>
        [DataMember]
        public virtual string ReportCert { get; set; }

        /// <summary>
        /// 意見回饋-電話
        /// </summary>
        [DataMember]
        public virtual string OpinionTel { get; set; }

        /// <summary>
        /// 意見回饋-E-mail
        /// </summary>
        [DataMember]
        public virtual string OpinionEmail { get; set; }

        /// <summary>
        /// 聯絡方式-姓名
        /// </summary>
        [DataMember]
        public virtual string ContactName { get; set; }

        /// <summary>
        /// 聯絡方式-職稱
        /// </summary>
        [DataMember]
        public virtual string ContactDept { get; set; }

        /// <summary>
        /// 聯絡方式-電話
        /// </summary>
        [DataMember]
        public virtual string ContactTel { get; set; }

        /// <summary>
        /// 聯絡方式-E-mail
        /// </summary>
        [DataMember]
        public virtual string ContactEmail { get; set; }

        /// <summary>
        /// 報告書介紹
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        [DataMember]
        public virtual string MainCode { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        [DataMember]
        public virtual string MainName { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        [DataMember]
        public virtual string AdminCode { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        [DataMember]
        public virtual string AdminName { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        [DataMember]
        public virtual string ServiceCode { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        [DataMember]
        public virtual string ServiceName { get; set; }

        public virtual string GetStr_PostDate
        {
            get
            {
                string result = string.Empty;

                if (PostDate != null)
                {
                    result = PostDate.Value.ToString("yyyy/MM/dd");
                }

                return result;
            }
        }

        public virtual string GetStr_CompanyNationality
        {
            get
            {
                string result = string.Empty;

                switch (this.CompanyTrade)
                {
                    case "0":
                        result = "國外";
                        break;
                    case "1":
                        result = "國內";
                        break;
                }

                return result;
            }
        }

        public virtual string GetStr_CompanyScale
        {
            get
            {
                string result = string.Empty;

                switch (this.CompanyTrade)
                {
                    case "0":
                        result = "中小企業";
                        break;
                    case "1":
                        result = "大企業";
                        break;
                }

                return result;
            }
        }
        
        public virtual string GetStr_CompanyTrade
        {
            get
            {
                string result = string.Empty;

                switch (this.CompanyTrade)
                {
                    case "1":
                        result = "一般服務業";
                        break;
                    case "2":
                        result = "技術服務業";
                        break;
                    case "3":
                        result = "旅遊餐飲業";
                        break;
                    case "4":
                        result = "批發零售業";
                        break;
                    case "5":
                        result = "金融保險業";
                        break;
                    case "6":
                        result = "電子資訊業";
                        break;
                    case "7":
                        result = "倉儲物流業";
                        break;
                    case "8":
                        result = "傳統製造業";
                        break;
                    case "9":
                        result = "傳播出版業";
                        break;
                    case "10":
                        result = "營建不動產";
                        break;
                    case "11":
                        result = "公行社福業";
                        break;
                    case "12":
                        result = "教育學術業";
                        break;
                    case "13":
                        result = "醫療保健業";
                        break;
                    case "14":
                        result = "其他行業";
                        break;
                }

                return result;
            }
        }

        #endregion
    }
}
