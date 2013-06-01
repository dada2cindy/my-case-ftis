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
    /// 行業分類
    /// </summary>
    public class IndustryModel : AbstractEntityModel
    {
        public IndustryModel()
        {
        }

        public IndustryModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            Industry entity = m_FTISService.GetIndustryById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Industry entity)
        {
            if (entity != null)
            {
                EntityId = entity.IndustryId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            Industry entity = new Industry();
            Save(entity);
        }

        public void Update()
        {
            Industry entity = m_FTISService.GetIndustryById(EntityId);
            Save(entity);
        }

        private void Save(Industry entity)
        {
            entity.Name = Name;            
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.IndustryId == 0)
            {
                m_FTISService.CreateIndustry(entity);
            }
            else
            {
                m_FTISService.UpdateIndustry(entity);
            }

            LoadEntity(entity.IndustryId);
        }
    }
}
