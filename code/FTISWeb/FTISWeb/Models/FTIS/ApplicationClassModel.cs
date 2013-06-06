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
    /// 社會責任分類
    /// </summary>
    public class ApplicationClassModel : AbstractClassModel
    {
        public ApplicationClassModel()
        {
        }

        public ApplicationClassModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            ApplicationClass entity = m_FTISService.GetApplicationClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(ApplicationClass entity)
        {
            if (entity != null)
            {
                EntityId = entity.ApplicationClassId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            ApplicationClass entity = new ApplicationClass();
            Save(entity);
        }

        public void Update()
        {
            ApplicationClass entity = m_FTISService.GetApplicationClassById(EntityId);
            Save(entity);
        }

        private void Save(ApplicationClass entity)
        {
            entity.Name = Name;            
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.ApplicationClassId == 0)
            {
                m_FTISService.CreateApplicationClass(entity);
            }
            else
            {
                m_FTISService.UpdateApplicationClass(entity);
            }

            LoadEntity(entity.ApplicationClassId);
        }
    }
}
