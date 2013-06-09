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
    /// 綠色工廠分類
    /// </summary>
    public class GreenFactoryClassModel : AbstractClassModel
    {
        public GreenFactoryClassModel()
        {
        }

        public GreenFactoryClassModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            GreenFactoryClass entity = m_FTISService.GetGreenFactoryClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(GreenFactoryClass entity)
        {
            if (entity != null)
            {
                EntityId = entity.GreenFactoryClassId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            GreenFactoryClass entity = new GreenFactoryClass();
            Save(entity);
        }

        public void Update()
        {
            GreenFactoryClass entity = m_FTISService.GetGreenFactoryClassById(EntityId);
            Save(entity);
        }

        private void Save(GreenFactoryClass entity)
        {
            entity.Name = Name;            
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.GreenFactoryClassId == 0)
            {
                m_FTISService.CreateGreenFactoryClass(entity);
            }
            else
            {
                m_FTISService.UpdateGreenFactoryClass(entity);
            }

            LoadEntity(entity.GreenFactoryClassId);
        }

        public IList<GreenFactory> GetGreenFactoryList(bool onlyOpen, int getGreenFactoryClassId)
        {
            if (getGreenFactoryClassId > 0)
            {
                EntityId = getGreenFactoryClassId;
            }

            IList<GreenFactory> result = new List<GreenFactory>();

            IDictionary<string, string> conditions = new Dictionary<string, string>();
            if (onlyOpen)
            {
                conditions.Add("Status", "1");
            }
            if (EntityId > 0)
            {
                conditions.Add("GreenFactoryClassId", EntityId.ToString());
                result = m_FTISService.GetGreenFactoryListNoLazy(conditions);
            }

            if (result == null)
            {
                result = new List<GreenFactory>();
            }

            return result;
        }
    }
}
