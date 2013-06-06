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
    /// 網路資源分類
    /// </summary>
    public class LinksClassModel : AbstractClassModel
    {
        public LinksClassModel()
        {
            this.LangId = "2";
        }

        public LinksClassModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 語言. 1.英文 2.中文
        /// </summary>
        public string LangId { get; set; }

        protected void LoadEntity(int id)
        {
            LinksClass entity = m_FTISService.GetLinksClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(LinksClass entity)
        {
            if (entity != null)
            {
                EntityId = entity.LinksClassId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                LangId = entity.LangId;
            }
        }

        public void Insert()
        {
            LinksClass entity = new LinksClass();
            Save(entity);
        }

        public void Update()
        {
            LinksClass entity = m_FTISService.GetLinksClassById(EntityId);
            Save(entity);
        }

        private void Save(LinksClass entity)
        {
            entity.Name = Name;            
            entity.SortId = SortId;
            entity.Status = Status;
            entity.LangId = LangId;

            if (entity.LinksClassId == 0)
            {
                m_FTISService.CreateLinksClass(entity);
            }
            else
            {
                m_FTISService.UpdateLinksClass(entity);
            }

            LoadEntity(entity.LinksClassId);
        }
    }
}
