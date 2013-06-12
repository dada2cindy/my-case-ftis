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

        /// <summary>
        /// 地區
        /// </summary>
        [DisplayName("地區")]
        [Required(ErrorMessage = "請選擇地區")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇地區")]
        public int ParentId { get; set; }

        /// <summary>
        /// 圖片1
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

        public IList<NormClass> GetNormClassList(bool onlyOpen, int parentId)
        {
            IList<NormClass> result = new List<NormClass>();

            IDictionary<string, string> conditions = new Dictionary<string, string>();
            if (onlyOpen)
            {
                conditions.Add("Status", "1");
            }
            if (parentId > 0)
            {
                conditions.Add("OnlySub", "OnlySub");
                conditions.Add("ParentNormClassId", parentId.ToString());

                result = m_FTISService.GetNormClassListNoLazy(conditions);
            }
            else
            {
                result = m_FTISService.GetNormClassListNoLazy(conditions);
            }

            if (result == null)
            {
                result = new List<NormClass>();
            }

            return result;
        }
    }
}
