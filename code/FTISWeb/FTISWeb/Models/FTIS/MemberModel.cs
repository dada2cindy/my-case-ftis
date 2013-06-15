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
    /// 電子報訂閱
    /// </summary>
    public class MemberModel : AbstractEntityModel
    {
        SessionHelper m_SessionHelper = new SessionHelper();

        public MemberModel()
        {
        }

        public MemberModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            Member entity = m_FTISService.GetMemberById(id);

            LoadEntity(entity);
        }

        /// <summary>
        /// 行業別
        /// </summary>
        [DisplayName("行業別")]
        [Required(ErrorMessage = "請選擇行業別")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇行業別")]
        public int IndustryId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [Required]
        public override string Name { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        [DisplayName("登入帳號")]
        [Required]
        public string LoginId { get; set; }

        /// <summary>
        /// 登入密碼
        /// </summary>
        [DisplayName("登入密碼")]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        [DisplayName("確認密碼")]
        [Required]
        public string CheckPassword { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DisplayName("公司名稱")]
        [Required]
        public string Company { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        [DisplayName("統一編號")]
        public string CompanyNo { get; set; }

        /// <summary>
        /// 公司規模 1.大企業 2.中小企業
        /// </summary>
        [DisplayName("公司規模")]
        [Required]
        public string CompanyNum { get; set; }

        /// <summary>
        /// 公司屬性(複選,分隔) 1.ODM(設計製造代工廠) 2.OBM(品牌廠商) 3.OEM(設備製造代工廠) 4.其他
        /// </summary>
        public string CompanyType { get; set; }

        /// <summary>
        /// 公司屬性(複選,分隔) 1.ODM(設計製造代工廠) 2.OBM(品牌廠商) 3.OEM(設備製造代工廠) 4.其他
        /// </summary>
        [DisplayName("公司屬性")]
        [Required]
        public string[] CompanyTypeList { get; set; }

        /// <summary>
        /// 公司屬性其它(自key文字)
        /// </summary>
        public string CompanyTypeOther { get; set; }

        /// <summary>
        /// 連絡人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 部門
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string Tel2 { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        public virtual string Fax { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DisplayName("電子郵件")]
        [Required]
        public virtual string Email { get; set; }

        /// <summary>
        /// 申請日期
        /// </summary>
        [DisplayName("申請日期")]
        [Required]
        public DateTime? RegDate { get; set; }

        /// <summary>
        /// 主要產品內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 驗證碼
        /// </summary>
        [DisplayName("驗證碼")]
        [Required]
        public string ConfirmationCode { get; set; }

        public bool SendOrderOk { get; set; }

        public string ErrorMsg { get; set; }

        public string Msg { get; set; }

        /// <summary>
        /// 是否同意本會提供免費電子報資訊
        /// </summary>
        [DisplayName("是否同意本會提供免費電子報資訊")]
        [Required]
        public virtual string ReceiveEpaperInfo { get; set; }

        protected void LoadEntity(Member entity)
        {
            if (entity != null)
            {
                EntityId = entity.MemberId;
                Name = entity.Name;
                LoginId = entity.LoginId;
                Password = entity.Password;
                Company = entity.Company;
                Status = entity.Status;
                RegDate = entity.RegDate;
                CompanyNo = entity.CompanyNo;
                CompanyNum = entity.CompanyNum;
                CompanyType = entity.CompanyType;
                CompanyTypeOther = entity.CompanyTypeOther;
                Email = entity.Email;
                Contact = entity.Contact;
                Dept = entity.Dept;
                JobTitle = entity.JobTitle;
                Tel = entity.Tel;
                Tel2 = entity.Tel2;
                Fax = entity.Fax;
                Content = entity.Content;
                IndustryId = entity.Industry.IndustryId;
                ReceiveEpaperInfo = entity.ReceiveEpaperInfo;
                CompanyTypeList = entity.CompanyType.Replace(", ", ",").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public void SendOrder()
        {
            ////查是否有舊有的
            Member existMember = m_FTISService.GetMemberByLoginId(this.LoginId);
            if (existMember != null)
            {
                this.SendOrderOk = false;
                this.ErrorMsg = "此帳號已經有人使用!";
                return;
            }
            this.Status = "0";
            Insert();
            Member member = m_FTISService.GetMemberById(this.EntityId);
            m_SessionHelper.WebMember = member;
            this.SendOrderOk = true;

            ////電子報
            if ("1".Equals(this.ReceiveEpaperInfo))
            {
                EpaperEmailModel epaperEmailModel = new EpaperEmailModel()
                {
                    Company = this.Company,
                    Dept = this.Dept,
                    Email = this.Email,
                    Name = this.Name,
                    Tel = this.Tel,
                    UserStatus = "1"
                };
                epaperEmailModel.SendOrder();
            }
        }

        public void Insert()
        {
            Member entity = new Member();
            Save(entity);
        }

        public void Update()
        {
            Member entity = m_FTISService.GetMemberById(EntityId);
            Save(entity);
        }

        private void Save(Member entity)
        {
            entity.MemberId = EntityId;
            entity.Name = Name;
            entity.LoginId = LoginId;
            entity.Password = Password;
            entity.Company = Company;
            entity.Status = Status;
            entity.RegDate = RegDate;
            entity.CompanyNo = CompanyNo;
            entity.CompanyNum = CompanyNum;
            entity.CompanyType = CompanyType;
            entity.CompanyTypeOther = CompanyTypeOther;
            entity.Email = Email;
            entity.Contact = Contact;
            entity.Dept = Dept;
            entity.JobTitle = JobTitle;
            entity.Tel = Tel;
            entity.Tel2 = Tel2;
            entity.Fax = Fax;
            entity.Content = Content;
            entity.ReceiveEpaperInfo = ReceiveEpaperInfo;
            if (IndustryId > 0)
            {
                entity.Industry = m_FTISService.GetIndustryById(IndustryId);
            }

            if (CompanyTypeList != null && CompanyTypeList.Count() > 0)
            {
                entity.CompanyType = String.Join(", ", CompanyTypeList);
            }
            else
            {
                entity.CompanyType = string.Empty;
            }

            if (entity.MemberId == 0)
            {
                entity.RegDate = DateTime.Now;
                m_FTISService.CreateMember(entity);
            }
            else
            {
                m_FTISService.UpdateMember(entity);
            }

            LoadEntity(entity.MemberId);
        }

        internal bool IsValid()
        {
            bool isValid = true;

            if (this.CompanyTypeList == null)
            {
                isValid = false;
            }

            return isValid;
        }

        public void AddDownloadRecord(string name, string classId, string memberId)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(classId) || string.IsNullOrWhiteSpace(memberId))
            {
                return;
            }

            string postDate = DateTime.Today.ToString("yyyy/M/d");
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Name", name);
            conditions.Add("ClassId", classId);
            conditions.Add("MemberId", memberId);
            conditions.Add("PostDate", postDate);

            ////今天下載過就不再紀錄
            if (m_FTISService.GetDownloadRecordCount(conditions) == 0)
            {
                DownloadRecord downloadRecord = new DownloadRecord()
                {
                    Name = name,
                    ClassId = classId,
                    Member = m_FTISService.GetMemberById(int.Parse(memberId)),
                    PostDate = postDate
                };
                m_FTISService.CreateDownloadRecord(downloadRecord);
            }
        }

        public bool DoLogon()
        {
            Member member = m_FTISService.GetMemberByLoginId(this.LoginId);
            if (member != null && member.Password.Equals(this.Password) && member.Status == "1")
            {
                m_SessionHelper.WebMember = member;
                this.SendOrderOk = true;
                return true;
            }

            this.ErrorMsg = "帳號錯誤";
            this.SendOrderOk = false;
            return false;
        }

        public bool CheckLoginId(string loginId)
        {
            bool result = true;
            Member member = m_FTISService.GetMemberByLoginId(loginId);
            if (member == null)
            {
                result = false;
            }

            return result;
        }


        public void ForgetPass()
        {
            Member member = m_FTISService.GetMemberByLoginId(this.LoginId);
            if (member == null || member.Email != this.Email || member.Status == "0")
            {
                this.SendOrderOk = false;
                this.ErrorMsg = "帳號錯誤";
                return;
            }

            SendMailModel sendMailModel = new SendMailModel();
            sendMailModel.SendMailTitle = "收到一封從產業永續發展整合資訊網的忘記密碼信。";
            sendMailModel.SendMailContent = string.Format("{0}您好，您的密碼為{1}", member.LoginId, member.Password);
            sendMailModel.SendMailTo = member.Email;
            sendMailModel.SendMail();

            this.SendOrderOk = true;
            this.ErrorMsg = string.Empty;
            this.Msg = "您的密碼已寄發至註冊信箱中";
        }
    }
}
