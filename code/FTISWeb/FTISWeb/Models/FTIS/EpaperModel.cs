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
    /// 電子報
    /// </summary>
    public class EpaperModel : AbstractArticleModel
    {
        public EpaperModel()
        {            
        }

        /// <summary>
        /// 期數
        /// </summary>
        [Required]
        [DisplayName("期數")]
        public string ENo { get; set; }

        /// <summary>
        /// 連結網址
        /// </summary>
        [Required]
        [DisplayName("連結網址")]
        public string AUrl { get; set; }

        public EpaperModel(int id)
        {
            LoadEntity(id);
        }    

        protected void LoadEntity(int id)
        {
            Epaper entity = m_FTISService.GetEpaperById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Epaper entity)
        {
            if (entity != null)
            {
                ENo = entity.ENo;
                EntityId = entity.EpaperId;                
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;                
                AUrl = entity.AUrl;
                Vister = entity.Vister;                
            }
        }

        public void Insert()
        {
            Epaper entity = new Epaper();
            Save(entity);
        }

        public void Update()
        {
            Epaper entity = m_FTISService.GetEpaperById(EntityId);
            Save(entity);
        }

        private void Save(Epaper entity)
        {
            entity.ENo = ENo;
            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;                        
            entity.AUrl = AUrl;
            entity.Vister = Vister;          

            if (entity.EpaperId == 0)
            {
                m_FTISService.CreateEpaper(entity);
            }
            else
            {
                m_FTISService.UpdateEpaper(entity);
            }

            LoadEntity(entity.EpaperId);
        }
    }
}
