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
    public class NewsTypeModel : AbstractClassModel
    {
        public NewsTypeModel()
        {
        }

        public NewsTypeModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DisplayName("名稱(英)")]
        //[Required]
        public string NameENG { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DisplayName("內容")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 英文內容
        /// </summary>
        [DisplayName("內容(英)")]
        //[Required]
        public string ContentENG { get; set; }

        protected void LoadEntity(int id)
        {
            NewsType entity = m_FTISService.GetNewsTypeById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(NewsType entity)
        {
            if (entity != null)
            {
                ClassId = entity.NewsTypeId;
                Name = entity.Name;
                NameENG = entity.NameENG;
                Content = entity.Content;
                ContentENG = entity.ContentENG;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            NewsType entity = new NewsType();
            Save(entity);
        }

        public void Update()
        {
            NewsType entity = m_FTISService.GetNewsTypeById(ClassId);
            Save(entity);
        }

        private void Save(NewsType entity)
        {
            entity.Name = Name;
            entity.NameENG = NameENG;
            entity.Content = Content;
            entity.ContentENG = ContentENG;
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.NewsTypeId == 0)
            {
                m_FTISService.CreateNewsType(entity);
            }
            else
            {
                m_FTISService.UpdateNewsType(entity);
            }

            LoadEntity(entity.NewsTypeId);
        }
    }
}
