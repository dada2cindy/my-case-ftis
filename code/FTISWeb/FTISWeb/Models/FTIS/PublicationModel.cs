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
    /// 期刊
    /// </summary>
    public class PublicationModel : AbstractArticleModel
    {
        public PublicationModel()
        {

        }

        public PublicationModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 期刊年度
        /// </summary>
        public PublicationClass PublicationClass { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        [DisplayName("年度")]
        [Required(ErrorMessage = "請選擇年度")]
        [Range(1,int.MaxValue,ErrorMessage= "請選擇年度")]
        public int PublicationClassId { get; set; }

        /// <summary>
        /// 期別
        /// </summary>
        public string PubNo { get; set; }

        /// <summary>
        /// 特別企劃項目1
        /// </summary>
        public string Spec1 { get; set; }

        /// <summary>
        /// 特別企劃項目2
        /// </summary>
        public string Spec2 { get; set; }

        /// <summary>
        /// 特別企劃項目3
        /// </summary>
        public string Spec3 { get; set; }

        /// <summary>
        /// 特別企劃項目4
        /// </summary>
        public string Spec4 { get; set; }

        /// <summary>
        /// 特別企劃項目5
        /// </summary>        
        public string Spec5 { get; set; }

        /// <summary>
        /// 特別企劃項目6
        /// </summary>        
        public string Spec6 { get; set; }

        /// <summary>
        /// B.本期刊物電子檔案遠端連結網址
        /// </summary>
        public string LinkFile { get; set; }

        protected void LoadEntity(int id)
        {
            Publication entity = m_FTISService.GetPublicationById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Publication entity)
        {
            if (entity != null)
            {
                EntityId = entity.PublicationId;
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;                
                Content = entity.Content;
                PubNo = entity.PubNo;
                Pic1 = entity.Pic1;
                AFile1 = entity.AFile1;
                LinkFile = entity.LinkFile;
                Spec1 = entity.Spec1;
                Spec2 = entity.Spec2;
                Spec3 = entity.Spec3;
                Spec4 = entity.Spec4;
                Spec5 = entity.Spec5;
                Spec6 = entity.Spec6;
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
                if (entity.PublicationClass != null)
                {
                    PublicationClass = entity.PublicationClass;
                    PublicationClassId = entity.PublicationClass.PublicationClassId;
                }
            }
        }

        public void Insert()
        {
            Publication entity = new Publication();
            Save(entity);
        }

        public void Update()
        {
            Publication entity = m_FTISService.GetPublicationById(EntityId);
            Save(entity);
        }

        private void Save(Publication entity)
        {
            if (PublicationClassId > 0)
            {
                entity.PublicationClass = m_FTISService.GetPublicationClassById(PublicationClassId);
            }
            else
            {
                entity.PublicationClass = null;
            }

            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.Content = Content;
            entity.PubNo = PubNo;
            entity.Pic1 = Pic1;
            entity.AFile1 = AFile1;
            entity.LinkFile = LinkFile;
            entity.Spec1 = Spec1;
            entity.Spec2 = Spec2;
            entity.Spec3 = Spec3;
            entity.Spec4 = Spec4;
            entity.Spec5 = Spec5;
            entity.Spec6 = Spec6;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;

            if (entity.PublicationId == 0)
            {
                m_FTISService.CreatePublication(entity);
            }
            else
            {
                m_FTISService.UpdatePublication(entity);
            }

            LoadEntity(entity.PublicationId);
        }
    }
}
