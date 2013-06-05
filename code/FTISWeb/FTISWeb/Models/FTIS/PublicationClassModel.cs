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
    /// 刊物年度
    /// </summary>
    public class PublicationClassModel : AbstractClassModel
    {
        public PublicationClassModel()
        {
        }

        public PublicationClassModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            PublicationClass entity = m_FTISService.GetPublicationClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(PublicationClass entity)
        {
            if (entity != null)
            {
                EntityId = entity.PublicationClassId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            PublicationClass entity = new PublicationClass();
            Save(entity);
        }

        public void Update()
        {
            PublicationClass entity = m_FTISService.GetPublicationClassById(EntityId);
            Save(entity);
        }

        private void Save(PublicationClass entity)
        {
            entity.Name = Name;            
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.PublicationClassId == 0)
            {
                m_FTISService.CreatePublicationClass(entity);
            }
            else
            {
                m_FTISService.UpdatePublicationClass(entity);
            }

            LoadEntity(entity.PublicationClassId);
        }
    }
}
