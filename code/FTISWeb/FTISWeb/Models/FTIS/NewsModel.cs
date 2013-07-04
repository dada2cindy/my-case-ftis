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
    /// 即時訊息
    /// </summary>
    public class NewsModel : AbstractArticleENGModel, ICheckFreeGO
    {
        public NewsModel()
        {
        }

        public NewsModel(string id)
        {            
            LoadEntity(int.Parse(DecryptId(id)));
        }

        public NewsModel(int id)
        {
            LoadEntity(id);
        }

        /// <summary>
        /// 新聞分類(議題)
        /// </summary>
        public NewsClass NewsClass { get; set; }

        /// <summary>
        /// 新聞種類
        /// </summary>
        public NewsType NewsType { get; set; }

        /// <summary>
        /// 議題
        /// </summary>
        [DisplayName("議題")]
        [Required(ErrorMessage = "請選擇議題")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇議題")]
        public int NewsClassId { get; set; }

        /// <summary>
        /// 種類
        /// </summary>
        [DisplayName("種類")]
        [Required(ErrorMessage = "請選擇種類")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇種類")]
        public int NewsTypeId { get; set; }

        /// <summary>
        /// 上一筆
        /// </summary>
        public News PrevEntity { get; set; }

        /// <summary>
        /// 下一筆
        /// </summary>
        public News NextEntity { get; set; }

        #region ICheckFreeGO 成員

        public bool ShowFreeGOMsg { get; set; }

        public string FreeGOColumnName { get; set; }

        #endregion

        protected void LoadEntity(int id)
        {
            News entity = m_FTISService.GetNewsById(id);

            LoadEntity(entity);
        }

        protected void LoadEntity(News entity)
        {
            if (entity != null)
            {
                #region 中文欄位

                EntityId = entity.NewsId;
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
                AUrl = entity.AUrl;
                AUrl1 = entity.AUrl1;
                AUrl1Link = entity.AUrl1Link;
                AUrl2 = entity.AUrl2;
                AUrl2Link = entity.AUrl2Link;
                AUrl3 = entity.AUrl3;
                AUrl3Link = entity.AUrl3Link;
                IsNew = entity.IsNew;
                IsHome = entity.IsHome;
                IsOut = entity.IsOut;
                Vister = entity.Vister;
                Emailer = entity.Emailer;
                Printer = entity.Printer;
                MainCode = entity.MainCode;
                MainName = entity.MainName;
                AdminCode = entity.AdminCode;
                AdminName = entity.AdminName;
                ServiceCode = entity.ServiceCode;
                ServiceName = entity.ServiceName;
                Tag = entity.Tag;

                #endregion

                #region 英文欄位

                NameENG = entity.NameENG;
                ContentENG = entity.ContentENG;
                Pic1ENG = entity.Pic1ENG;
                Pic1NameENG = entity.Pic1NameENG;
                Pic2ENG = entity.Pic2ENG;
                Pic2NameENG = entity.Pic2NameENG;
                Pic3ENG = entity.Pic3ENG;
                Pic3NameENG = entity.Pic3NameENG;
                AFile1ENG = entity.AFile1ENG;
                AFile1NameENG = entity.AFile1NameENG;
                AFile2ENG = entity.AFile2ENG;
                AFile2NameENG = entity.AFile2NameENG;
                AFile3ENG = entity.AFile3ENG;
                AFile3NameENG = entity.AFile3NameENG;
                AUrlENG = entity.AUrlENG;
                AUrl1ENG = entity.AUrl1ENG;
                AUrl1LinkENG = entity.AUrl1LinkENG;
                AUrl2ENG = entity.AUrl2ENG;
                AUrl2LinkENG = entity.AUrl2LinkENG;
                AUrl3ENG = entity.AUrl3ENG;
                AUrl3LinkENG = entity.AUrl3LinkENG;
                VisterENG = entity.VisterENG;
                EmailerENG = entity.EmailerENG;
                PrinterENG = entity.PrinterENG;                

                #endregion

                if (entity.NewsClass != null)
                {
                    NewsClass = entity.NewsClass;
                    NewsClassId = entity.NewsClass.NewsClassId;
                }

                if (entity.NewsType != null)
                {
                    NewsType = entity.NewsType;
                    NewsTypeId = entity.NewsType.NewsTypeId;
                }
            }
        }

        public void Insert()
        {
            News entity = new News();
            Save(entity);
        }

        public void Update()
        {
            News entity = m_FTISService.GetNewsById(EntityId);
            Save(entity);
        }

        private void Save(News entity)
        {
            if (NewsClassId > 0)
            {
                entity.NewsClass = m_FTISService.GetNewsClassById(NewsClassId);
            }
            else
            {
                entity.NewsClass = null;
            }

            if (NewsTypeId > 0)
            {
                entity.NewsType = m_FTISService.GetNewsTypeById(NewsTypeId);
            }
            else
            {
                entity.NewsType = null;
            }

            #region 中文欄位

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
            entity.AUrl = AUrl;
            entity.AUrl1 = AUrl1;
            entity.AUrl1Link = AUrl1Link;
            entity.AUrl2 = AUrl2;
            entity.AUrl2Link = AUrl2Link;
            entity.AUrl3 = AUrl3;
            entity.AUrl3Link = AUrl3Link;
            entity.IsNew = IsNew;
            entity.IsHome = IsHome;
            entity.IsOut = IsOut;
            entity.Vister = Vister;
            entity.Emailer = Emailer;
            entity.Printer = Printer;
            entity.MainCode = MainCode;
            entity.MainName = MainName;
            entity.AdminCode = AdminCode;
            entity.AdminName = AdminName;
            entity.ServiceCode = ServiceCode;
            entity.ServiceName = ServiceName;
            entity.Tag = Tag;

            #endregion

            #region 英文欄位

            entity.NameENG = NameENG;
            entity.ContentENG = ContentENG;
            entity.Pic1ENG = Pic1ENG;
            entity.Pic1NameENG = Pic1NameENG;
            entity.Pic2ENG = Pic2ENG;
            entity.Pic2NameENG = Pic2NameENG;
            entity.Pic3ENG = Pic3ENG;
            entity.Pic3NameENG = Pic3NameENG;
            entity.AFile1ENG = AFile1ENG;
            entity.AFile1NameENG = AFile1NameENG;
            entity.AFile2ENG = AFile2ENG;
            entity.AFile2NameENG = AFile2NameENG;
            entity.AFile3ENG = AFile3ENG;
            entity.AFile3NameENG = AFile3NameENG;
            entity.AUrlENG = AUrlENG;
            entity.AUrl1ENG = AUrl1ENG;
            entity.AUrl1LinkENG =AUrl1LinkENG;
            entity.AUrl2ENG = AUrl2ENG;
            entity.AUrl2LinkENG = AUrl2LinkENG;
            entity.AUrl3ENG = AUrl3ENG;
            entity.AUrl3LinkENG = AUrl3LinkENG;
            entity.VisterENG = VisterENG;
            entity.EmailerENG = EmailerENG;
            entity.PrinterENG = PrinterENG;

            #endregion

            if (entity.NewsId == 0)
            {
                m_FTISService.CreateNews(entity);
            }
            else
            {
                m_FTISService.UpdateNews(entity);
            }

            LoadEntity(entity.NewsId);
        }

        /// <summary>
        /// 取得上一筆, 下一筆
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <param name="keyWord"></param>
        /// <param name="newsClassId"></param>
        /// <param name="newsTypeId"></param>
        public void GetPrevNextEntity(int dataIndex, string keyWord, string newsClassId, string newsTypeId)
        {
            
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("KeyWord", keyWord);
            conditions.Add("NewsClassId", DecryptId(newsClassId));
            conditions.Add("NewsTypeId", DecryptId(newsTypeId));
            conditions.Add("PageSize", "1");
            
            ////上一筆
            if (dataIndex > 0)
            {
                conditions.Add("PageIndex", (dataIndex - 1).ToString());

                IList<News> list = m_FTISService.GetNewsListNoLazy(conditions);
                if (list != null && list.Count > 0)
                {
                    PrevEntity = list[0];
                }
            }

            ////下一筆
            if (dataIndex >= 0)
            {
                if (conditions.ContainsKey("PageIndex"))
                {
                    conditions.Remove("PageIndex");
                }
                conditions.Add("PageIndex", (dataIndex + 1).ToString());

                IList<News> list = m_FTISService.GetNewsListNoLazy(conditions);
                if (list != null && list.Count > 0)
                {
                    NextEntity = list[0];
                }
            }
        }
    }
}
