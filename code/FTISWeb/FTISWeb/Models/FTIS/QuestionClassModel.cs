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
    /// Q&A分類
    /// </summary>
    public class QuestionClassModel : AbstractClassModel
    {
        public QuestionClassModel()
        {
        }

        public QuestionClassModel(int id)
        {
            LoadEntity(id);
        }

        protected void LoadEntity(int id)
        {
            QuestionClass entity = m_FTISService.GetQuestionClassById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(QuestionClass entity)
        {
            if (entity != null)
            {
                EntityId = entity.QuestionClassId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
            }
        }

        public void Insert()
        {
            QuestionClass entity = new QuestionClass();
            Save(entity);
        }

        public void Update()
        {
            QuestionClass entity = m_FTISService.GetQuestionClassById(EntityId);
            Save(entity);
        }

        private void Save(QuestionClass entity)
        {
            entity.Name = Name;            
            entity.SortId = SortId;
            entity.Status = Status;

            if (entity.QuestionClassId == 0)
            {
                m_FTISService.CreateQuestionClass(entity);
            }
            else
            {
                m_FTISService.UpdateQuestionClass(entity);
            }

            LoadEntity(entity.QuestionClassId);
        }
    }
}
