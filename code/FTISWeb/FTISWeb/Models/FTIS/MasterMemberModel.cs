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
using FTIS.Domain;
using FTIS.ACUtility;
using FTIS.Domain.Container;

namespace FTISWeb.Models
{
    /// <summary>
    /// 網站管理員
    /// </summary>
    public class MasterMemberModel : AbstractEntityModel
    {
        SessionHelper m_SessionHelper = new SessionHelper();

        private readonly string m_DefaultPass = "zse4xdr5";

        public MasterMemberModel()
        {
            RolesBarList1 = new string[] { };
            RolesBarList2 = new string[] { };
            RolesBarList3 = new string[] { };
            RolesBarList4 = new string[] { };
            RolesBarList5 = new string[] { };
            RolesBarList6 = new string[] { };
            RolesBarList7 = new string[] { };
            RolesBarList8 = new string[] { };
            RolesBarList9 = new string[] { };
            RolesBarList10 = new string[] { };
            RolesBarList11 = new string[] { };
            RolesBarList12 = new string[] { };
            RolesBarList13 = new string[] { };
            RolesBarList14 = new string[] { };
            RolesBarList15 = new string[] { };
            RolesBarList16 = new string[] { };
            RolesBarList17 = new string[] { };
        }

        public MasterMemberModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            MasterMember entity = m_FTISService.GetMasterMemberByIdNoLazy(id);

            LoadEntity(entity);
        }

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
        public string Account { get; set; }

        /// <summary>
        /// 登入密碼
        /// </summary>
        [DisplayName("登入密碼")]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DisplayName("電子郵件")]
        public virtual string Email { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DisplayName("註冊日期")]
        [Required]
        public DateTime? RegDate { get; set; }

        /// <summary>
        /// 主要產品內容
        /// </summary>
        public string Memo { get; set; }

        ///// <summary>
        ///// 驗證碼
        ///// </summary>
        //[DisplayName("驗證碼")]
        //[Required]
        //public string ConfirmationCode { get; set; }

        public bool SendOrderOk { get; set; }

        public string ErrorMsg { get; set; }

        public string Msg { get; set; }

        #region 功能權限相關

        /// <summary>
        /// 網站管理
        /// </summary>
        public string[] RolesBarList1 { get; set; }

        /// <summary>
        /// 關於我們
        /// </summary>
        public string[] RolesBarList2 { get; set; }

        /// <summary>
        /// 新聞動態
        /// </summary>
        public string[] RolesBarList3 { get; set; }

        /// <summary>
        /// 即時訊息
        /// </summary>
        public string[] RolesBarList4 { get; set; }

        /// <summary>
        /// 活動訊息
        /// </summary>
        public string[] RolesBarList5 { get; set; }

        /// <summary>
        /// 會員專區
        /// </summary>
        public string[] RolesBarList6 { get; set; }

        /// <summary>
        /// 下載專區
        /// </summary>
        public string[] RolesBarList7 { get; set; }

        /// <summary>
        /// 標準/規範管理
        /// </summary>
        public string[] RolesBarList8 { get; set; }

        /// <summary>
        /// 產品環境資訊揭露管理
        /// </summary>
        public string[] RolesBarList9 { get; set; }

        /// <summary>
        /// 企業永續發展應用/企業社會責任管理
        /// </summary>
        public string[] RolesBarList10 { get; set; }

        /// <summary>
        /// 評比介紹管理 沒用到
        /// </summary>
        public string[] RolesBarList11 { get; set; }

        /// <summary>
        /// 產業服務專區設置
        /// </summary>
        public string[] RolesBarList12 { get; set; }

        /// <summary>
        /// 網路資源管理
        /// </summary>
        public string[] RolesBarList13 { get; set; }

        /// <summary>
        /// 電子報管理
        /// </summary>
        public string[] RolesBarList14 { get; set; }

        /// <summary>
        /// 首頁四季節慶設置
        /// </summary>
        public string[] RolesBarList15 { get; set; }

        /// <summary>
        /// 滿意度問卷管理
        /// </summary>
        public string[] RolesBarList16 { get; set; }

        /// <summary>
        /// 綠色工廠設置
        /// </summary>
        public string[] RolesBarList17 { get; set; }

        #endregion

        protected void LoadEntity(MasterMember entity)
        {
            if (entity != null)
            {
                EntityId = entity.MasterMemberId;
                Name = entity.Name;
                Account = entity.Account;
                Password = m_DefaultPass;
                Status = entity.Status;
                RegDate = entity.RegDate;
                Email = entity.Email;
                Tel = entity.Tel;
                Memo = entity.Memo;
                
                ////權限
                RolesBarList1 = GetRolesBarList(entity, SiteEntities.Master);
                RolesBarList2 = GetRolesBarList(entity, SiteEntities.AboutUs);
                RolesBarList3 = GetRolesBarList(entity, SiteEntities.News);
                RolesBarList4 = GetRolesBarList(entity, SiteEntities.HomeNews);
                RolesBarList5 = GetRolesBarList(entity, SiteEntities.Activity);
                RolesBarList6 = GetRolesBarList(entity, SiteEntities.Member);
                RolesBarList7 = GetRolesBarList(entity, SiteEntities.Download);
                RolesBarList8 = GetRolesBarList(entity, SiteEntities.Norm);
                RolesBarList9 = GetRolesBarList(entity, SiteEntities.Carbon);
                RolesBarList10 = GetRolesBarList(entity, SiteEntities.Application);
                RolesBarList11 = GetRolesBarList(entity, SiteEntities.Grade);
                RolesBarList12 = GetRolesBarList(entity, SiteEntities.Question);
                RolesBarList13 = GetRolesBarList(entity, SiteEntities.Links);
                RolesBarList14 = GetRolesBarList(entity, SiteEntities.Epaper);
                RolesBarList15 = GetRolesBarList(entity, SiteEntities.Season);
                RolesBarList16 = GetRolesBarList(entity, SiteEntities.Examination);
                RolesBarList17 = GetRolesBarList(entity, SiteEntities.GreenFactory);
            }
        }

        private string[] GetRolesBarList(MasterMember entity , SiteEntities siteEntities)
        {
            IList<string> list = new List<string>();

            foreach (SiteOperations operation in Enum.GetValues(typeof(SiteOperations)))
            {
                if (operation == SiteOperations.None)
                {
                    continue;
                }

                if (ACUtility.CheckAuthorization(entity, (int)siteEntities, (int)operation))
                {
                    list.Add(((int)operation).ToString());
                }
            }

            if (list.Count > 0)
            {
                return list.ToArray();
            }
            else
            {
                return new string[] { };
            }
        }        

        public void Insert()
        {
            MasterMember entity = m_FTISService.MakeMasterMember();
            Save(entity);
        }

        public void Update()
        {
            MasterMember entity = m_FTISService.GetMasterMemberByIdNoLazy(EntityId);
            Save(entity);
        }

        private void Save(MasterMember entity)
        {
            entity.MasterMemberId = EntityId;
            entity.Name = Name;
            entity.Account = Account;
            ////因為密碼加密，Load時會給一個m_DefaultPass，存檔時要不同才會去修改密碼
            if (!m_DefaultPass.Equals(Password))
            {
                entity.Password = EncryptUtil.GetMD5(Password);
            }
            entity.Status = Status;
            entity.Email = Email;
            entity.Tel = Tel;
            entity.Memo = Memo;

            ////權限
            foreach (AdminRole role in entity.AdminRoles)
            {
                if (role.AdminBar.AdminBarId.Equals((int)SiteEntities.Grade))
                {
                    continue;
                }

                role.AdminValue = SetRolesAdminValue(role.AdminBar.AdminBarId);
            }

            if (entity.MasterMemberId == 0)
            {
                entity.RegDate = DateTime.Now;
                m_FTISService.CreateMasterMember(entity);
            }
            else
            {
                m_FTISService.UpdateMasterMember(entity);
            }

            ////重新取得user
            LoginUserContainer.GetInstance().InitMember(this.Account);

            LoadEntity(entity.MasterMemberId);
        }

        private int SetRolesAdminValue(int adminBarId)
        {
            int result = 0;

            switch (adminBarId)
            {
                case ((int)SiteEntities.Master):
                    result = GetRolesAdminValue(RolesBarList1);
                    break;
                case ((int)SiteEntities.AboutUs):
                    result = GetRolesAdminValue(RolesBarList2);
                    break;
                case ((int)SiteEntities.News):
                    result = GetRolesAdminValue(RolesBarList3);
                    break;
                case ((int)SiteEntities.HomeNews):
                    result = GetRolesAdminValue(RolesBarList4);
                    break;
                case ((int)SiteEntities.Activity):
                    result = GetRolesAdminValue(RolesBarList5);
                    break;
                case ((int)SiteEntities.Member):
                    result = GetRolesAdminValue(RolesBarList6);
                    break;
                case ((int)SiteEntities.Download):
                    result = GetRolesAdminValue(RolesBarList7);
                    break;
                case ((int)SiteEntities.Norm):
                    result = GetRolesAdminValue(RolesBarList8);
                    break;
                case ((int)SiteEntities.Carbon):
                    result = GetRolesAdminValue(RolesBarList9);
                    break;
                case ((int)SiteEntities.Application):
                    result = GetRolesAdminValue(RolesBarList10);
                    break;
                case ((int)SiteEntities.Grade):
                    result = GetRolesAdminValue(RolesBarList11);
                    break;
                case ((int)SiteEntities.Question):
                    result = GetRolesAdminValue(RolesBarList12);
                    break;
                case ((int)SiteEntities.Links):
                    result = GetRolesAdminValue(RolesBarList13);
                    break;
                case ((int)SiteEntities.Epaper):
                    result = GetRolesAdminValue(RolesBarList14);
                    break;
                case ((int)SiteEntities.Season):
                    result = GetRolesAdminValue(RolesBarList15);
                    break;
                case ((int)SiteEntities.Examination):
                    result = GetRolesAdminValue(RolesBarList16);
                    break;
                case ((int)SiteEntities.GreenFactory):
                    result = GetRolesAdminValue(RolesBarList17);
                    break;
            }

            return result;
        }

        private int GetRolesAdminValue(string[] barList)
        {
            int result = 0;

            if (barList != null && barList.Count() > 0)
            {
                foreach (string s in barList)
                {
                    result += (int.Parse(s));
                }
            }

            return result;
        }

        public bool IsValid()
        {
            bool isValid = true;

            //if (this.CompanyTypeList == null)
            //{
            //    isValid = false;
            //}

            return isValid;
        }

        public bool CheckLoginId(string account)
        {
            bool result = true;
            MasterMember masterMember = m_FTISService.GetMasterMemberByAccount(account);
            if (masterMember == null)
            {
                result = false;
            }

            return result;
        }


        public void ForgetPass()
        {
            //Member member = m_FTISService.GetMemberByLoginId(this.LoginId);
            //if (member == null || member.Email != this.Email || member.Status == "0")
            //{
            //    this.SendOrderOk = false;
            //    this.ErrorMsg = "帳號錯誤";
            //    return;
            //}

            //SendMailModel sendMailModel = new SendMailModel();
            //sendMailModel.SendMailTitle = "收到一封從產業永續發展整合資訊網的忘記密碼信。";
            //sendMailModel.SendMailContent = string.Format("{0}您好，您的密碼為{1}", member.LoginId, member.Password);
            //sendMailModel.SendMailTo = member.Email;
            //sendMailModel.SendMail();

            //this.SendOrderOk = true;
            //this.ErrorMsg = string.Empty;
            //this.Msg = "您的密碼已寄發至註冊信箱中";
        }
    }
}
