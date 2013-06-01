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
    /// 技術文件/工具
    /// </summary>
    public class DownloadModel : AbstractArticleModel, ICheckFreeGO
    {
        public DownloadModel()
        {            
        }

        public DownloadModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 下載數
        /// </summary>
        public int Downer { get; set; }        

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            Download entity = m_FTISService.GetDownloadById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(Download entity)
        {
            if (entity != null)
            {
                EntityId = entity.DownloadId;                
                Name = entity.Name;
                SortId = entity.SortId;
                Status = entity.Status;
                PostDate = entity.PostDate;                
                Content = entity.Content;
                AFile1 = entity.AFile1;
                AFile1Name = entity.AFile1Name;
                IsOut = entity.IsOut;
                AUrl = entity.AUrl;
                Vister = entity.Vister;
                Downer = entity.Downer;
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
            Download entity = new Download();
            Save(entity);
        }

        public void Update()
        {
            Download entity = m_FTISService.GetDownloadById(EntityId);
            Save(entity);
        }

        private void Save(Download entity)
        {
            entity.Name = Name;
            entity.SortId = SortId;
            entity.Status = Status;
            entity.PostDate = PostDate;            
            entity.Content = Content;
            entity.AFile1 = AFile1;
            entity.AFile1Name = AFile1Name;
            entity.IsOut = IsOut;
            entity.AUrl = AUrl;
            entity.Vister = Vister;
            entity.Downer = Downer;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;            

            if (entity.DownloadId == 0)
            {
                m_FTISService.CreateDownload(entity);
            }
            else
            {
                m_FTISService.UpdateDownload(entity);
            }

            LoadEntity(entity.DownloadId);
        }
    }
}
