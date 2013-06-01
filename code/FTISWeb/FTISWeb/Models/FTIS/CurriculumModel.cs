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
    /// 課程講義
    /// </summary>
    public class CurriculumModel : AbstractArticleModel, ICheckFreeGO
    {
        public CurriculumModel()
        {            
        }

        public CurriculumModel(int id)
        {
            LoadEntity(id);
        }        

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            Curriculum entity = m_FTISService.GetCurriculumById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Curriculum entity)
        {
            if (entity != null)
            {
                EntityId = entity.CurriculumId;                
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;                
                Content = entity.Content;
                Pic1 = entity.Pic1;
                Pic1Name = entity.Pic1Name;
                Pic2 = entity.Pic2;
                Pic2Name = entity.Pic2Name;
                Pic3 = entity.Pic3;
                Pic3Name = entity.Pic3Name;
                AFile1 = entity.AFile1;
                AFile1Name = entity.AFile1Name;
                AFile2 = entity.AFile2;
                AFile2Name = entity.AFile2Name;
                AFile3 = entity.AFile3;
                AFile3Name = entity.AFile3Name;
                AUrl1 = entity.AUrl1;
                AUrl2 = entity.AUrl2;
                AUrl3 = entity.AUrl3;
                IsNew = entity.IsNew;
                Vister = entity.Vister;
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
            Curriculum entity = new Curriculum();
            Save(entity);
        }

        public void Update()
        {
            Curriculum entity = m_FTISService.GetCurriculumById(EntityId);
            Save(entity);
        }

        private void Save(Curriculum entity)
        {
            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;
            entity.Content = Content;
            entity.Pic1 = Pic1;
            entity.Pic1Name = Pic1Name;
            entity.Pic2 = Pic2;
            entity.Pic2Name = Pic2Name;
            entity.Pic3 = Pic3;
            entity.Pic3Name = Pic3Name;
            entity.AFile1 = AFile1;
            entity.AFile1Name = AFile1Name;
            entity.AFile2 = AFile2;
            entity.AFile2Name = AFile2Name;
            entity.AFile3 = AFile3;
            entity.AFile3Name = AFile3Name;
            entity.AUrl1 = AUrl1;
            entity.AUrl2 = AUrl2;
            entity.AUrl3 = AUrl3;
            entity.IsNew = IsNew;
            entity.Vister = Vister;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.CurriculumId == 0)
            {
                m_FTISService.CreateCurriculum(entity);
            }
            else
            {
                m_FTISService.UpdateCurriculum(entity);
            }

            LoadEntity(entity.CurriculumId);
        }
    }
}
