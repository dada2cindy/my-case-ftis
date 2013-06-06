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
    /// 期刊
    /// </summary>
    public class LinksModel : AbstractArticleModel
    {
        public LinksModel()
        {
            this.LangId = "2";
        }

        public LinksModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 網路資源分類
        /// </summary>
        public LinksClass LinksClass { get; set; }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        public string LangId { get; set; }

        /// <summary>
        /// 網路資源分類
        /// </summary>
        [DisplayName("分類")]
        [Required(ErrorMessage = "請選擇分類")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇分類")]
        public int LinksClassId { get; set; }        

        protected void LoadEntity(int id)
        {
            Links entity = m_FTISService.GetLinksById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Links entity)
        {
            if (entity != null)
            {
                EntityId = entity.LinksId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                AUrl = entity.AUrl;
                LangId = entity.LangId;
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
                if (entity.LinksClass != null)
                {
                    LinksClass = entity.LinksClass;
                    LinksClassId = entity.LinksClass.LinksClassId;
                }
            }
        }

        public void Insert()
        {
            Links entity = new Links();
            Save(entity);
        }

        public void Update()
        {
            Links entity = m_FTISService.GetLinksById(EntityId);
            Save(entity);
        }

        private void Save(Links entity)
        {
            if (LinksClassId > 0)
            {
                entity.LinksClass = m_FTISService.GetLinksClassById(LinksClassId);
            }
            else
            {
                entity.LinksClass = null;
            }

            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.AUrl = AUrl;
            entity.LangId = LangId;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.LinksId == 0)
            {
                m_FTISService.CreateLinks(entity);
            }
            else
            {
                m_FTISService.UpdateLinks(entity);
            }

            LoadEntity(entity.LinksId);
        }
    }
}
