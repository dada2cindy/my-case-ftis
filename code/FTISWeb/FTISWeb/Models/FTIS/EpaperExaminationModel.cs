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
    /// 電子報問卷
    /// </summary>
    public class EpaperExaminationModel : AbstractExaminationModel
    {
        public EpaperExaminationModel()
        {
        }

        public EpaperExaminationModel(int id)
        {
            LoadEntity(id);
        }

        public void LoadEntity(int id)
        {
            EpaperExamination entity = m_FTISService.GetEpaperExaminationById(id);

            LoadEntity(entity);
        }

        public void LoadEntity(EpaperExamination entity)
        {
            if (entity != null)
            {
                EntityId = entity.EpaperExaminationId;
                Name = entity.Name;
                Email = entity.Email;
                Gender = entity.Gender;
                PostDate = entity.PostDate;
                IndustryId = entity.Industry.IndustryId;
                Question1 = entity.Question1;
                Question2 = entity.Question2;
                Question3 = entity.Question3;
                Question4 = entity.Question4;
                Question5 = entity.Question5;
                Question6 = entity.Question6;
                Question7 = entity.Question7;
                Question8 = entity.Question8;
                Question9 = entity.Question9;
            }
        }        

        public void Insert()
        {
            EpaperExamination entity = new EpaperExamination();
            Save(entity);
        }

        public void Update()
        {
            EpaperExamination entity = m_FTISService.GetEpaperExaminationById(EntityId);
            Save(entity);
        }

        private void Save(EpaperExamination entity)
        {
            entity.Name = Name;
            entity.Email = Email;
            entity.Gender = Gender;            
            entity.PostDate = PostDate;
            entity.Question1 = Question1;
            entity.Question2 = Question2;
            entity.Question3 = Question3;
            entity.Question4 = Question4;
            entity.Question5 = Question5;
            entity.Question6 = Question6;
            entity.Question7 = Question7;
            entity.Question8 = Question8;
            entity.Question9 = Question9;

            if (IndustryId > 0)
            {
                entity.Industry = m_FTISService.GetIndustryById(IndustryId);
            }

            if (entity.EpaperExaminationId == 0)
            {
                if (entity.PostDate == null)
                {
                    entity.PostDate = DateTime.Now;
                }
                m_FTISService.CreateEpaperExamination(entity);
            }
            else
            {
                m_FTISService.UpdateEpaperExamination(entity);
            }

            LoadEntity(entity.EpaperExaminationId);
        }

        public bool IsValid()
        {
            bool isValid = true;

            //if (!string.IsNullOrWhiteSpace(this.UserStatus))
            //{
            //    if ("1".Equals(this.UserStatus))
            //    {
            //        if (this.InTypeList == null)
            //        {
            //            isValid = false;
            //        }
            //    }
            //}

            return isValid;
        }
    }
}
