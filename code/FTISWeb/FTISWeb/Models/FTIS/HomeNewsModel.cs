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
    public class HomeNewsModel : AbstractEntityModel
    {
        public HomeNewsModel()
        {
        }

        public HomeNewsModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 連結網址
        /// </summary>
        [DisplayName("連結網址")]
        [Required]
        public string AUrl { get; set; }

        protected void LoadEntity(int id)
        {
            HomeNews entity = m_FTISService.GetHomeNewsById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(HomeNews entity)
        {
            if (entity != null)
            {
                EntityId = entity.HomeNewsId;
                Name = entity.Name;
                AUrl = entity.AUrl;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            HomeNews entity = new HomeNews();
            Save(entity);
        }

        public void Update()
        {
            HomeNews entity = m_FTISService.GetHomeNewsById(EntityId);
            Save(entity);
        }

        private void Save(HomeNews entity)
        {
            entity.Name = Name;
            entity.AUrl = AUrl;
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.HomeNewsId == 0)
            {
                m_FTISService.CreateHomeNews(entity);
            }
            else
            {
                m_FTISService.UpdateHomeNews(entity);
            }

            LoadEntity(entity.HomeNewsId);
        }
    }
}
