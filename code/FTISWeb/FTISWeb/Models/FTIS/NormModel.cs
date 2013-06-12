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
    public class NormModel : AbstractArticleModel, ICheckFreeGO
    {
        public NormModel()
        {
        }

        public NormModel(string id)
        {            
            LoadEntity(int.Parse(DecryptId(id)));
        }

        public NormModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 地區
        /// </summary>
        public NormClass NormClass { get; set; }

        /// <summary>
        /// 區域
        /// </summary>
        public NormClass NormClassParent { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        [DisplayName("地區")]
        [Required(ErrorMessage = "請選擇地區")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇地區")]
        public int NormClassId { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        [DisplayName("國家")]
        [Required(ErrorMessage = "請選擇國家")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇國家")]
        public int NormClassParentId { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            Norm entity = m_FTISService.GetNormById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Norm entity)
        {
            if (entity != null)
            {
                #region 中文欄位

                EntityId = entity.NormId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                Content = entity.Content;
                Pic1 = entity.Pic1;                
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
                Tag = entity.Tag;

                if (entity.NormClass != null)
                {
                    NormClass = entity.NormClass;
                    NormClassId = entity.NormClass.NormClassId;
                }

                if (entity.NormClassParent != null)
                {
                    NormClassParent = entity.NormClassParent;
                    NormClassParentId = entity.NormClassParent.NormClassId;
                }

                #endregion
            }
        }

        public void Insert()
        {
            Norm entity = new Norm();
            Save(entity);
        }

        public void Update()
        {
            Norm entity = m_FTISService.GetNormById(EntityId);
            Save(entity);
        }

        public IList<News> GetNewsByTags()
        {
            IList<News> result = new List<News>();

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                //查詢
                IDictionary<string, string> conditions = new Dictionary<string, string>();
                conditions.Add("Status", "1");
                conditions.Add("Tags", this.Tag);

                result = m_FTISService.GetNewsList(conditions);
            }
            if (result == null)
            {
                result = new List<News>();
            }

            return result;
        }

        private void Save(Norm entity)
        {
            if (NormClassId > 0)
            {
                entity.NormClass = m_FTISService.GetNormClassById(NormClassId);
            }
            else
            {
                entity.NormClass = null;
            }

            if (NormClassParentId > 0)
            {
                entity.NormClassParent = m_FTISService.GetNormClassById(NormClassParentId);
            }
            else
            {
                entity.NormClassParent = null;
            }

            #region 中文欄位

            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.Content = Content;
            entity.Pic1 = Pic1;            
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;
            entity.Tag = Tag;

            #endregion

            if (entity.NormId == 0)
            {
                m_FTISService.CreateNorm(entity);
            }
            else
            {
                m_FTISService.UpdateNorm(entity);
            }

            LoadEntity(entity.NormId);
        }
    }
}
