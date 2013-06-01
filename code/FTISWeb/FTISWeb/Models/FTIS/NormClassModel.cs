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
    /// 標準/規範分類
    /// </summary>
    public class NormClassModel : AbstractClassModel
    {
        public NormClass ParentEntity { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [DisplayName("圖片1")]
        public string Pic1 { get; set; }

        public NormClassModel()
        {
        }

        public NormClassModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            NormClass entity = m_FTISService.GetNormClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(NormClass entity)
        {
            if (entity != null)
            {
                EntityId = entity.NormClassId;
                Name = entity.Name;
                NameENG = entity.NameENG;
                Pic1 = entity.Pic1;
                SortId = entity.SortId;
                Status = entity.Status;
                if (entity.ParentNormClass != null)
                {
                    ParentEntity = entity.ParentNormClass;
                    ParentId = entity.ParentNormClass.NormClassId;
                }
            }
        }

        public void Insert()
        {
            NormClass entity = new NormClass();
            Save(entity);
        }

        public void Update()
        {
            NormClass entity = m_FTISService.GetNormClassById(EntityId);
            Save(entity);
        }

        private void Save(NormClass entity)
        {
            if (ParentId > 0)
            {
                entity.ParentNormClass = m_FTISService.GetNormClassById(ParentId);
            }
            entity.Name = Name;
            entity.NameENG = NameENG;
            entity.Pic1 = Pic1;
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.NormClassId == 0)
            {
                m_FTISService.CreateNormClass(entity);
            }
            else
            {
                m_FTISService.UpdateNormClass(entity);
            }

            LoadEntity(entity.NormClassId);
        }
    }
}
