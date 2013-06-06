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
    /// Q&A
    /// </summary>
    public class QuestionModel : AbstractArticleModel, ICheckFreeGO
    {
        public QuestionModel()
        {

        }

        public QuestionModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// Q&A分類
        /// </summary>
        public QuestionClass QuestionClass { get; set; }

        /// <summary>
        /// Q&A分類
        /// </summary>
        [DisplayName("分類")]
        [Required(ErrorMessage = "請選擇分類")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇分類")]
        public int QuestionClassId { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            Question entity = m_FTISService.GetQuestionById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Question entity)
        {
            if (entity != null)
            {
                EntityId = entity.QuestionId;
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
                if (entity.QuestionClass != null)
                {
                    QuestionClass = entity.QuestionClass;
                    QuestionClassId = entity.QuestionClass.QuestionClassId;
                }
            }
        }

        public void Insert()
        {
            Question entity = new Question();
            Save(entity);
        }

        public void Update()
        {
            Question entity = m_FTISService.GetQuestionById(EntityId);
            Save(entity);
        }

        private void Save(Question entity)
        {
            if (QuestionClassId > 0)
            {
                entity.QuestionClass = m_FTISService.GetQuestionClassById(QuestionClassId);
            }
            else
            {
                entity.QuestionClass = null;
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

            if (entity.QuestionId == 0)
            {
                m_FTISService.CreateQuestion(entity);
            }
            else
            {
                m_FTISService.UpdateQuestion(entity);
            }

            LoadEntity(entity.QuestionId);
        }
    }
}
