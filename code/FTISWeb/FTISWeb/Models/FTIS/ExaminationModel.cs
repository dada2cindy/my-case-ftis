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
    /// 電子報訂閱
    /// </summary>
    public class ExaminationModel : AbstractExaminationModel
    {
        public ExaminationModel()
        {
            QuestionSelects2 = new string[] { };
            QuestionSelects3 = new string[] { };
            QuestionSelects9 = new string[] { };
            QuestionSelects10 = new string[] { };
        }

        public ExaminationModel(int id)
        {
            LoadEntity(id);
        }

        public void LoadEntity(int id)
        {
            Examination entity = m_FTISService.GetExaminationById(id);

            LoadEntity(entity);
        }

        /// <summary>
        /// 問題2 Checkbox
        /// </summary>
        [DisplayName("問題2")]
        [Required]
        public string[] QuestionSelects2 { get; set; }

        /// <summary>
        /// 問題3 Checkbox
        /// </summary>
        [DisplayName("問題3")]
        [Required]
        public string[] QuestionSelects3 { get; set; }

        /// <summary>
        /// 問題9 Checkbox
        /// </summary>
        [DisplayName("問題9")]
        [Required]
        public string[] QuestionSelects9 { get; set; }

        /// <summary>
        /// 問題10 Checkbox
        /// </summary>
        [DisplayName("問題10")]
        [Required]
        public string[] QuestionSelects10 { get; set; }

        public void LoadEntity(Examination entity)
        {
            if (entity != null)
            {
                EntityId = entity.ExaminationId;
                Name = entity.Name;
                Email = entity.Email;
                Gender = entity.Gender;
                PostDate = entity.PostDate;
                IndustryId = entity.Industry.IndustryId;
                Question1 = entity.Question1;
                Question2 = entity.Question2;
                QuestionSelects2 = entity.Question2.Replace(", ", ",").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                QOther2 = entity.QOther2;
                Question3 = entity.Question3;
                QuestionSelects3 = entity.Question3.Replace(", ", ",").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                QOther3 = entity.QOther3;
                Question4 = entity.Question4;
                Question5 = entity.Question5;
                Question6 = entity.Question6;
                Question7 = entity.Question7;
                Question8 = entity.Question8;
                Question9 = entity.Question9;
                QuestionSelects9 = entity.Question9.Replace(", ", ",").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                QOther9 = entity.QOther9;
                Question10 = entity.Question10;
                QuestionSelects10 = entity.Question10.Replace(", ", ",").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                QOther10 = entity.QOther10;
                Question11 = entity.Question11;
                Question12 = entity.Question12;
            }
        }

        public void Insert()
        {
            Examination entity = new Examination();
            Save(entity);
        }

        public void Update()
        {
            Examination entity = m_FTISService.GetExaminationById(EntityId);
            Save(entity);
        }

        private void Save(Examination entity)
        {
            entity.Name = Name;
            entity.Email = Email;
            entity.Gender = Gender;
            entity.PostDate = PostDate;
            if (IndustryId > 0)
            {
                entity.Industry = m_FTISService.GetIndustryById(IndustryId);
            }
            entity.Question1 = Question1;            
            if (QuestionSelects2 != null && QuestionSelects2.Count() > 0)
            {
                entity.Question2 = String.Join(", ", QuestionSelects2);
            }
            else
            {
                entity.Question2 = string.Empty;
            }
            entity.QOther2 = QOther2;
            if (QuestionSelects3 != null && QuestionSelects3.Count() > 0)
            {
                entity.Question3 = String.Join(", ", QuestionSelects3);
            }
            else
            {
                entity.Question3 = string.Empty;
            }
            entity.QOther3 = QOther3;
            entity.Question4 = Question4;
            entity.Question5 = Question5;
            entity.Question6 = Question6;
            entity.Question7 = Question7;
            entity.Question8 = Question8;
            if (QuestionSelects9 != null && QuestionSelects9.Count() > 0)
            {
                entity.Question9 = String.Join(", ", QuestionSelects9);
            }
            else
            {
                entity.Question9 = string.Empty;
            }
            entity.QOther9 = QOther9;
            if (QuestionSelects10 != null && QuestionSelects10.Count() > 0)
            {
                entity.Question10 = String.Join(", ", QuestionSelects10);
            }
            else
            {
                entity.Question10 = string.Empty;
            }
            entity.QOther10 = QOther10;
            entity.Question11 = Question11;
            entity.Question12 = Question12;

            if (entity.ExaminationId == 0)
            {
                if (entity.PostDate == null)
                {
                    entity.PostDate = DateTime.Now;
                }
                m_FTISService.CreateExamination(entity);
            }
            else
            {
                m_FTISService.UpdateExamination(entity);
            }

            LoadEntity(entity.ExaminationId);
        }

        public void SendOrder()
        {
            Insert();
            this.SendOrderOk = true;
        }

        public bool IsValid()
        {
            bool isValid = true;

            if (this.QuestionSelects2 == null || this.QuestionSelects3 == null || this.QuestionSelects9 == null || this.QuestionSelects10 == null)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
