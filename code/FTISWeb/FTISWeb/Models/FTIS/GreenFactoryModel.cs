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
    /// 綠色工廠
    /// </summary>
    public class GreenFactoryModel : AbstractArticleModel, ICheckFreeGO
    {
        public GreenFactoryModel()
        {

        }

        public GreenFactoryModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 綠色工廠分類
        /// </summary>
        public GreenFactoryClass GreenFactoryClass { get; set; }

        /// <summary>
        /// 綠色工廠分類
        /// </summary>
        [DisplayName("分類")]
        [Required(ErrorMessage = "請選擇分類")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇分類")]
        public int GreenFactoryClassId { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            GreenFactory entity = m_FTISService.GetGreenFactoryById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(GreenFactory entity)
        {
            if (entity != null)
            {
                EntityId = entity.GreenFactoryId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;                
                Content = entity.Content;                
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
                if (entity.GreenFactoryClass != null)
                {
                    GreenFactoryClass = entity.GreenFactoryClass;
                    GreenFactoryClassId = entity.GreenFactoryClass.GreenFactoryClassId;
                }
            }
        }

        public void Insert()
        {
            GreenFactory entity = new GreenFactory();
            Save(entity);
        }

        public void Update()
        {
            GreenFactory entity = m_FTISService.GetGreenFactoryById(EntityId);
            Save(entity);
        }

        private void Save(GreenFactory entity)
        {
            if (GreenFactoryClassId > 0)
            {
                entity.GreenFactoryClass = m_FTISService.GetGreenFactoryClassById(GreenFactoryClassId);
            }
            else
            {
                entity.GreenFactoryClass = null;
            }

            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;
            entity.Content = Content;            
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.GreenFactoryId == 0)
            {
                m_FTISService.CreateGreenFactory(entity);
            }
            else
            {
                m_FTISService.UpdateGreenFactory(entity);
            }

            LoadEntity(entity.GreenFactoryId);
        }
    }
}
