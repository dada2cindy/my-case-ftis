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

        public string[] InTypeList { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
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
            }
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
    }
}
