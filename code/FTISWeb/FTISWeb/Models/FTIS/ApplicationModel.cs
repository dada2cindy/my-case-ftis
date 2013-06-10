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
    /// 企業社會責任
    /// </summary>
    public class ApplicationModel : AbstractArticleModel, ICheckFreeGO
    {
        public ApplicationModel()
        {

        }

        public ApplicationModel(string id, bool noLazy = false)
        {
            LoadEntity(int.Parse(DecryptId(id)), noLazy);
        }

        public ApplicationModel(int id, bool noLazy = false)
        {
            LoadEntity(id, noLazy);
        }

        /// <summary>
        /// 期刊年度
        /// </summary>
        public ApplicationClass ApplicationClass { get; set; }

        /// <summary>
        /// 分類
        /// </summary>
        [DisplayName("分類")]
        [Required(ErrorMessage = "請選擇分類")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇分類")]
        public int ApplicationClassId { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id, bool noLazy = false)
        {
            Application entity;
            if (noLazy)
            {
                entity = m_FTISService.GetApplicationByIdNoLazy(id);
            }
            else
            {
                entity = m_FTISService.GetApplicationById(id);
            }

            LoadEntity(entity);
        }

        protected void LoadEntity(Application entity)
        {
            if (entity != null)
            {
                EntityId = entity.ApplicationId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                Tag = entity.Tag;
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
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
                if (entity.ApplicationClass != null)
                {
                    ApplicationClass = entity.ApplicationClass;
                    ApplicationClassId = entity.ApplicationClass.ApplicationClassId;
                }
            }
        }

        public void Insert()
        {
            Application entity = new Application();
            Save(entity);
        }

        public void Update()
        {
            Application entity = m_FTISService.GetApplicationById(EntityId);
            Save(entity);
        }

        public IList<News> GetNewsByTags()
        {
            IList<News> result = new List<News>();

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                //查詢
                IDictionary<string, string> conditions = new Dictionary<string, string>();
                conditions.Add("Status", "1");
                conditions.Add("Tags", this.Tag);

                result = m_FTISService.GetNewsList(conditions);
            }
            if (result == null)
            {
                result = new List<News>();
            }

            return result;
        }

        private void Save(Application entity)
        {
            if (ApplicationClassId > 0)
            {
                entity.ApplicationClass = m_FTISService.GetApplicationClassById(ApplicationClassId);
            }
            else
            {
                entity.ApplicationClass = null;
            }

            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.Tag = Tag;
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
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.ApplicationId == 0)
            {
                m_FTISService.CreateApplication(entity);
            }
            else
            {
                m_FTISService.UpdateApplication(entity);
            }

            LoadEntity(entity.ApplicationId);
        }
    }
}
