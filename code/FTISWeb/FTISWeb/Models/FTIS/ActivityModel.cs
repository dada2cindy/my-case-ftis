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
    public class ActivityModel : AbstractArticleModel, ICheckFreeGO
    {
        public ActivityModel()
        {
        }

        public ActivityModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 主辦單位
        /// </summary>
        public string MCompany { get; set; }

        /// <summary>
        /// 連絡人姓名
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 舉辦日期
        /// </summary>
        public string ActDate { get; set; }

        /// <summary>
        /// 連絡人電話
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 活動地點
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 連絡人傳真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 費用
        /// </summary>
        public string ActFee { get; set; }

        /// <summary>
        /// 報名方式
        /// </summary>
        public string ActType { get; set; }

        /// <summary>
        /// 報名狀態
        /// </summary>
        public string JoinStatus { get; set; }

        /// <summary>
        /// 活動狀態
        /// </summary>
        public string ActStatus { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            Activity entity = m_FTISService.GetActivityById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Activity entity)
        {
            if (entity != null)
            {
                EntityId = entity.ActivityId;                
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;
                MCompany = entity.MCompany;
                Contact = entity.Contact;
                ActDate = entity.ActDate;
                Tel = entity.Tel;
                Address = entity.Address;
                Fax = entity.Fax;
                ActFee = entity.ActFee;
                ActType = entity.ActType;
                JoinStatus = entity.JoinStatus;
                ActStatus = entity.ActStatus;
                Content = entity.Content;
                Pic1 = entity.Pic1;
                Pic1Name = entity.Pic1Name;
                Pic2 = entity.Pic2;
                Pic2Name = entity.Pic2Name;
                Pic3 = entity.Pic3;
                Pic3Name = entity.Pic3Name;
                AFile1 = entity.AFile1;
                AFile1Name = entity.AFile1Name;
                AFile2 = entity.AFile2;
                AFile2Name = entity.AFile2Name;
                AFile3 = entity.AFile3;
                AFile3Name = entity.AFile3Name;
                AUrl = entity.AUrl;
                AUrl1 = entity.AUrl1;
                AUrl2 = entity.AUrl2;
                AUrl3 = entity.AUrl3;
                IsNew = entity.IsNew;
                IsHome = entity.IsHome;
                Vister = entity.Vister;
                Emailer = entity.Emailer;
                Printer = entity.Printer;
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
            Activity entity = new Activity();
            Save(entity);
        }

        public void Update()
        {
            Activity entity = m_FTISService.GetActivityById(EntityId);
            Save(entity);
        }

        private void Save(Activity entity)
        {
            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;
            entity.MCompany = MCompany;
            entity.Contact = Contact;
            entity.ActDate = ActDate;
            entity.Tel = Tel;
            entity.Address = Address;
            entity.Fax = Fax;
            entity.ActFee = ActFee;
            entity.ActType = ActType;
            entity.JoinStatus = JoinStatus;
            entity.ActStatus = ActStatus;
            entity.Content = Content;
            entity.Pic1 = Pic1;
            entity.Pic1Name = Pic1Name;
            entity.Pic2 = Pic2;
            entity.Pic2Name = Pic2Name;
            entity.Pic3 = Pic3;
            entity.Pic3Name = Pic3Name;
            entity.AFile1 = AFile1;
            entity.AFile1Name = AFile1Name;
            entity.AFile2 = AFile2;
            entity.AFile2Name = AFile2Name;
            entity.AFile3 = AFile3;
            entity.AFile3Name = AFile3Name;
            entity.AUrl = AUrl;
            entity.AUrl1 = AUrl1;
            entity.AUrl2 = AUrl2;
            entity.AUrl3 = AUrl3;
            entity.IsNew = IsNew;
            entity.IsHome = IsHome;
            entity.Vister = Vister;
            entity.Emailer = Emailer;
            entity.Printer = Printer;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.ActivityId == 0)
            {
                m_FTISService.CreateActivity(entity);
            }
            else
            {
                m_FTISService.UpdateActivity(entity);
            }

            LoadEntity(entity.ActivityId);
        }
    }
}
