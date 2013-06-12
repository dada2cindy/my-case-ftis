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
    /// 國際組織標準/系統動態
    /// </summary>
    public class MNormModel : AbstractArticleModel, ICheckFreeGO
    {
        public MNormModel()
        {            
        }

        public MNormModel(string id)
        {            
            LoadEntity(int.Parse(DecryptId(id)));
        }

        public MNormModel(int id)
        {
            LoadEntity(id);
        }        

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            MNorm entity = m_FTISService.GetMNormById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(MNorm entity)
        {
            if (entity != null)
            {
                EntityId = entity.MNormId;                
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
            }
        }

        public void Insert()
        {
            MNorm entity = new MNorm();
            Save(entity);
        }

        public void Update()
        {
            MNorm entity = m_FTISService.GetMNormById(EntityId);
            Save(entity);
        }

        private void Save(MNorm entity)
        {
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

            if (entity.MNormId == 0)
            {
                m_FTISService.CreateMNorm(entity);
            }
            else
            {
                m_FTISService.UpdateMNorm(entity);
            }

            LoadEntity(entity.MNormId);
        }
    }
}
