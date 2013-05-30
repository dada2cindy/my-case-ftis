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
    public class NewsClassModel : AbstractClassModel
    {
        public NewsClassModel()
        {
        }

        public NewsClassModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 英文名稱
        /// </summary>
        //[DisplayName("名稱(英)")]
        [Required]
        public string NameENG { get; set; }

        protected void LoadEntity(int id)
        {
            NewsClass entity = m_FTISService.GetNewsClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(NewsClass entity)
        {
            if (entity != null)
            {
                ClassId = entity.NewsClassId;
                Name = entity.Name;
                NameENG = entity.NameENG;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            NewsClass entity = new NewsClass();
            Save(entity);
        }

        public void Update()
        {
            NewsClass entity = m_FTISService.GetNewsClassById(ClassId);
            Save(entity);
        }

        private void Save(NewsClass entity)
        {
            entity.Name = Name;
            entity.NameENG = NameENG;
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.NewsClassId == 0)
            {
                m_FTISService.CreateNewsClass(entity);
            }
            else
            {
                m_FTISService.UpdateNewsClass(entity);
            }

            LoadEntity(entity.NewsClassId);
        }
    }
}
