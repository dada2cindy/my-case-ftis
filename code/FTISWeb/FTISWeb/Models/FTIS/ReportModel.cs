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
    /// 即時訊息
    /// </summary>
    public class ReportModel : AbstractEntityModel, ICheckFreeGO
    {
        public ReportModel()
        {
            this.PostDate = DateTime.Today;
        }

        public ReportModel(string id)
        {
            LoadEntity(int.Parse(DecryptId(id)));
        }

        public ReportModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DisplayName("公司名稱")]
        [Required]
        public string Company { get; set; }

        /// <summary>
        /// 提供日期
        /// </summary>
        [DisplayName("提供日期")]
        [Required]
        public DateTime? PostDate { get; set; }

        /// <summary>
        /// 行業別
        /// </summary>
        [DisplayName("行業別")]
        [Required]
        public string CompanyTrade { get; set; }

        /// <summary>
        /// 公司網址
        /// </summary>
        [DisplayName("公司網址")]
        [Required]
        public string AUrl { get; set; }

        /// <summary>
        /// 公司國籍
        /// </summary>
        [DisplayName("公司國籍")]
        [Required]
        public string CompanyNationality { get; set; }

        /// <summary>
        /// 公司規模
        /// </summary>
        [DisplayName("公司規模")]
        [Required]
        public string CompanyScale { get; set; }

        /// <summary>
        /// 公司類型
        /// </summary>
        [DisplayName("公司類型")]
        [Required]
        public string CompanyType { get; set; }

        /// <summary>
        /// 發佈年份
        /// </summary>
        [DisplayName("發佈年份")]
        [Required]
        [Range(1900, 2100)]
        public int PostYear { get; set; }

        /// <summary>
        /// 報告書名稱
        /// </summary>
        [DisplayName("報告書名稱")]
        [Required]
        public string ReportName { get; set; }

        /// <summary>
        /// 報告年份
        /// </summary>
        [DisplayName("報告年份")]
        [Required]
        [Range(1900, 2100)]
        public int ReportYear { get; set; }

        /// <summary>
        /// 報告格式
        /// </summary>
        [DisplayName("報告格式")]
        [Required]
        public string ReportType { get; set; }

        /// <summary>
        /// 報告頁數
        /// </summary>
        [DisplayName("報告頁數")]
        [Required]
        public string ReportPage { get; set; }

        /// <summary>
        /// AA1000標準
        /// </summary>
        [DisplayName("AA1000標準")]
        [Required]
        public string AA1000 { get; set; }

        /// <summary>
        /// GRI等級
        /// </summary>
        [DisplayName("GRI等級")]
        [Required]
        public string GRI { get; set; }

        /// <summary>
        /// 報告書封面
        /// </summary>
        [DisplayName("報告書封面")]
        [Required]
        public string ReportPic { get; set; }

        /// <summary>
        /// CSR網頁1
        /// </summary>
        [DisplayName("CSR網頁1")]
        [Required]
        public string CSRPage1 { get; set; }

        /// <summary>
        /// CSR網頁2
        /// </summary>
        [DisplayName("CSR網頁2")]
        public string CSRPage2 { get; set; }

        /// <summary>
        /// 語言版本--中文
        /// </summary>
        [DisplayName("語言版本--中文")]
        [Required]
        public string CAUrl { get; set; }

        /// <summary>
        /// 中文：下載人數
        /// </summary>
        public int CVister { get; set; }

        /// <summary>
        /// 語言版本--英文：下載網址
        /// </summary>
        public string EAUrl { get; set; }

        /// <summary>
        /// 英文：下載人數
        /// </summary>
        public int EVister { get; set; }

        /// <summary>
        /// 語言版本--雙語：下載網址
        /// </summary>
        public string DAUrl { get; set; }

        /// <summary>
        /// 雙語：下載人數名稱
        /// </summary>
        public int DVister { get; set; }

        /// <summary>
        /// 報告確證
        /// </summary>
        public string ReportCert { get; set; }

        /// <summary>
        /// 意見回饋-電話
        /// </summary>
        [DisplayName("意見回饋-電話")]
        [Required]
        public string OpinionTel { get; set; }

        /// <summary>
        /// 意見回饋-E-mail
        /// </summary>
        [DisplayName("意見回饋-E-mail")]
        [Required]
        public string OpinionEmail { get; set; }

        /// <summary>
        /// 聯絡方式-姓名
        /// </summary>
        [DisplayName("聯絡方式-姓名")]
        [Required]
        public string ContactName { get; set; }

        /// <summary>
        /// 聯絡方式-職稱
        /// </summary>
        [DisplayName("聯絡方式-職稱")]
        [Required]
        public string ContactDept { get; set; }

        /// <summary>
        /// 聯絡方式-電話
        /// </summary>
        [DisplayName("聯絡方式-電話")]
        [Required]
        public string ContactTel { get; set; }

        /// <summary>
        /// 聯絡方式-E-mail
        /// </summary>
        [DisplayName("聯絡方式-E-mail")]
        [Required]
        public string ContactEmail { get; set; }

        /// <summary>
        /// 報告書介紹
        /// </summary>
        [DisplayName("報告書介紹")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 驗證碼
        /// </summary>
        [DisplayName("驗證碼")]
        [Required]
        public string ConfirmationCode { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        public string MainCode { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        public string MainName { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        public string AdminCode { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        public string ServiceName { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            Report entity = m_FTISService.GetReportById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Report entity)
        {
            if (entity != null)
            {
                EntityId = entity.ReportId;
                //Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;
                Company = entity.Company;
                CompanyTrade = entity.CompanyTrade;
                AUrl = entity.AUrl;
                CompanyNationality = entity.CompanyNationality;
                CompanyScale = entity.CompanyScale;
                CompanyType = entity.CompanyType;
                PostYear = entity.PostYear;
                ReportName = entity.ReportName;
                ReportYear = entity.ReportYear;
                ReportType = entity.ReportType;
                ReportPage = entity.ReportPage;                
                AA1000 = entity.AA1000;
                GRI = entity.GRI;
                ReportPic = entity.ReportPic;
                CSRPage1 = entity.CSRPage1;
                CSRPage2 = entity.CSRPage2;
                CAUrl = entity.CAUrl;
                CVister = entity.CVister;
                EAUrl = entity.EAUrl;
                EVister = entity.EVister;
                DAUrl = entity.DAUrl;
                DVister = entity.DVister;
                ReportCert = entity.ReportCert;
                OpinionTel = entity.OpinionTel;
                OpinionEmail = entity.OpinionEmail;
                ContactName = entity.ContactName;
                ContactDept = entity.ContactDept;
                ContactTel = entity.ContactTel;
                ContactEmail = entity.ContactEmail;
                Content = entity.Content;
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
            }
        }

        public void Insert()
        {
            Report entity = new Report();
            Save(entity);
        }

        public void Update()
        {
            Report entity = m_FTISService.GetReportById(EntityId);
            Save(entity);
        }

        private void Save(Report entity)
        {
            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;
            entity.Company = Company;
            entity.CompanyTrade = CompanyTrade;
            entity.AUrl = AUrl;
            entity.CompanyNationality = CompanyNationality;
            entity.CompanyScale = CompanyScale;
            entity.CompanyType = CompanyType;
            entity.PostYear = PostYear;
            entity.ReportName = ReportName;
            entity.ReportYear = ReportYear;
            entity.ReportType = ReportType;
            entity.ReportPage = ReportPage;
            entity.AA1000 = AA1000;
            entity.GRI = GRI;
            entity.ReportPic = ReportPic;
            entity.CSRPage1 = CSRPage1;
            entity.CSRPage2 = CSRPage2;
            entity.CAUrl = CAUrl;
            entity.CVister = CVister;
            entity.EAUrl = EAUrl;
            entity.EVister = EVister;
            entity.DAUrl = DAUrl;
            entity.DVister = DVister;
            entity.ReportCert = ReportCert;
            entity.OpinionTel = OpinionTel;
            entity.OpinionEmail = OpinionEmail;
            entity.ContactName = ContactName;
            entity.ContactDept = ContactDept;
            entity.ContactTel = ContactTel;
            entity.ContactEmail = ContactEmail;
            entity.Content = Content;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.ReportId == 0)
            {
                m_FTISService.CreateReport(entity);
            }
            else
            {
                m_FTISService.UpdateReport(entity);
            }

            LoadEntity(entity.ReportId);
        }
    }
}
