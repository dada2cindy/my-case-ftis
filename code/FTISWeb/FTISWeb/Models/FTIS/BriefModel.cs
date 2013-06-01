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
    /// 永續產業發展簡訊
    /// </summary>
    public class BriefModel : AbstractArticleModel
    {
        public BriefModel()
        {
            this.IsUrl = "0";
        }

        public BriefModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 下載格式. 0.文檔 1.網址鏈接
        /// </summary>
        [DisplayName("下載格式")]
        [Required]
        public string IsUrl { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [DisplayName("年份")]
        [Required]
        public string AYear { get; set; }

        protected void LoadEntity(int id)
        {
            Brief entity = m_FTISService.GetBriefById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Brief entity)
        {
            if (entity != null)
            {
                EntityId = entity.BriefId;                
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;                
                Content = entity.Content;
                AFile1 = entity.AFile1;
                AFile1Name = entity.AFile1Name;
                IsUrl = entity.IsUrl;
                AUrl = entity.AUrl;
                AYear = entity.AYear;                
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
            Brief entity = new Brief();
            Save(entity);
        }

        public void Update()
        {
            Brief entity = m_FTISService.GetBriefById(EntityId);
            Save(entity);
        }

        private void Save(Brief entity)
        {
            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;            
            entity.Content = Content;
            entity.AFile1 = AFile1;
            entity.AFile1Name = AFile1Name;
            entity.IsUrl = IsUrl;
            entity.AUrl = AUrl;
            entity.AYear = AYear;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;            

            if (entity.BriefId == 0)
            {
                m_FTISService.CreateBrief(entity);
            }
            else
            {
                m_FTISService.UpdateBrief(entity);
            }

            LoadEntity(entity.BriefId);
        }
    }
}
