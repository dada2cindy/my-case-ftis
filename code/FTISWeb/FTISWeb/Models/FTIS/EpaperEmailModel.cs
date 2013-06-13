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
    public class EpaperEmailModel : AbstractEntityModel
    {
        public EpaperEmailModel()
        {
            this.UserStatus = "1";
        }

        public EpaperEmailModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            EpaperEmail entity = m_FTISService.GetEpaperEmailById(id);

            LoadEntity(entity);
        }

        /// <summary>
        /// 申請/退定日期
        /// </summary>
        public DateTime? RegDate { get; set; }

        /// <summary>
        /// 產業別 1.製造業 2.服務業 3.政府機關 4.學術/研究單位 5.其他
        /// </summary>
        public string InType { get; set; }

        /// <summary>
        /// 產業別 1.製造業 2.服務業 3.政府機關 4.學術/研究單位 5.其他
        /// </summary>
        [DisplayName("產業別")]
        [Required]
        public string[] InTypeList { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DisplayName("公司名稱")]
        [Required]
        public string Company { get; set; }

        /// <summary>
        /// 部門
        /// </summary>
        public string Dept { get; set; }

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
        public virtual string Sex { get; set; }

        /// <summary>
        /// 是否同意獲得本會其他免費資訊
        /// </summary>
        [DisplayName("是否同意獲得本會其他免費資訊")]
        [Required]
        public virtual string ReceiveOtherFreeInfo { get; set; }

        /// <summary>
        /// 驗證碼
        /// </summary>
        [DisplayName("驗證碼")]
        [Required]
        public string ConfirmationCode { get; set; }

        /// <summary>
        /// 訂/退閱
        /// </summary>
        [DisplayName("訂/退閱")]
        [Required]
        public string UserStatus { get; set; }

        public bool SendOrderOk { get; set; }

        protected void LoadEntity(EpaperEmail entity)
        {
            if (entity != null)
            {
                EntityId = entity.EpaperEmailId;
                Name = entity.Name;
                Status = entity.Status;
                RegDate = entity.RegDate;
                InType = entity.InType;
                Company = entity.Company;
                Email = entity.Email;
                Sex = entity.Sex;
                ReceiveOtherFreeInfo = entity.ReceiveOtherFreeInfo;
            }
        }

        public void SendOrder()
        {
            ////查是否有舊有的
            EpaperEmail existEpaperEmail = m_FTISService.GetEpaperEmailByEmail(this.Email);
            this.Status = this.UserStatus;
            if ("1".Equals(this.UserStatus))
            {
                ////訂閱
                if (existEpaperEmail == null)
                {
                    Insert();
                }
                else
                {
                    EntityId = existEpaperEmail.EpaperEmailId;
                    Save(existEpaperEmail);
                }
            }
            else if ("0".Equals(this.UserStatus))
            {
                ////退閱
                if (existEpaperEmail != null)
                {
                    existEpaperEmail.Status = this.Status;
                    existEpaperEmail.RegDate = DateTime.Now;
                    m_FTISService.UpdateEpaperEmail(existEpaperEmail);
                }
            }
            this.SendOrderOk = true;
        }

        public void Insert()
        {
            EpaperEmail entity = new EpaperEmail();
            Save(entity);
        }

        public void Update()
        {
            EpaperEmail entity = m_FTISService.GetEpaperEmailById(EntityId);
            Save(entity);
        }

        private void Save(EpaperEmail entity)
        {
            entity.Name = Name;
            entity.Status = Status;
            entity.RegDate = DateTime.Now;

            if (InTypeList != null && InTypeList.Count() > 0)
            {
                entity.InType = String.Join(", ", InTypeList);
            }
            else
            {
                entity.InType = string.Empty;
            }
            entity.Company = Company;
            entity.Email = Email;
            entity.Sex = Sex;
            entity.ReceiveOtherFreeInfo = ReceiveOtherFreeInfo;

            if (entity.EpaperEmailId == 0)
            {
                m_FTISService.CreateEpaperEmail(entity);
            }
            else
            {
                m_FTISService.UpdateEpaperEmail(entity);
            }

            LoadEntity(entity.EpaperEmailId);
        }        

        public bool IsValid()
        {
            bool isValid = true;

            if (!string.IsNullOrWhiteSpace(this.UserStatus))
            {
                if ("1".Equals(this.UserStatus))
                {
                    if (this.InTypeList == null)
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
    }
}
