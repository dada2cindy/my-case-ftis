using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTIS.Domain.Impl;
using FTIS.Persistence;
using NHibernate;
using WuDada.Core.Generic.Extension;

namespace FTIS.Service.Impl
{
    public class FTISService : IFTISService
    {
        #region IFTISService 成員

        public IFTISDao FTISDao { get; set; }

        #region NewsClass
        /// <summary>
        /// 新增新聞分類
        /// </summary>
        /// <param name="newsClass">被新增的新聞分類</param>
        /// <returns>新增後的新聞分類</returns>
        public NewsClass CreateNewsClass(NewsClass newsClass)
        {
            return FTISDao.CreateNewsClass(newsClass);
        }

        /// <summary>
        /// 更新新聞分類
        /// </summary>
        /// <param name="newsClass">被更新的新聞分類</param>
        /// <returns>更新後的新聞分類</returns>
        public NewsClass UpdateNewsClass(NewsClass newsClass)
        {
            return FTISDao.UpdateNewsClass(newsClass);
        }

        /// <summary>
        /// 刪除新聞分類
        /// </summary>
        /// <param name="newsClass">被刪除的新聞分類</param>
        public void DeleteNewsClass(NewsClass newsClass)
        {
            FTISDao.DeleteNewsClass(newsClass);
        }

        /// <summary>
        /// 取得新聞分類 By 識別碼
        /// </summary>
        /// <param name="newsClassId">識別碼</param>
        /// <returns>新聞分類</returns>
        public NewsClass GetNewsClassById(int newsClassId)
        {
            return FTISDao.GetNewsClassById(newsClassId);
        }

        /// <summary>
        /// 取得新聞分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞分類清單</returns>
        public IList<NewsClass> GetNewsClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsClassList(conditions);
        }

        /// <summary>
        /// 取得新聞分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNewsClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsClassCount(conditions);
        }
        #endregion

        #region News
        /// <summary>
        /// 新增新聞
        /// </summary>
        /// <param name="news">被新增的新聞</param>
        /// <returns>新增後的新聞</returns>
        public News CreateNews(News news)
        {
            return FTISDao.CreateNews(news);
        }

        /// <summary>
        /// 更新新聞
        /// </summary>
        /// <param name="news">被更新的新聞</param>
        /// <returns>更新後的新聞</returns>
        public News UpdateNews(News news)
        {
            return FTISDao.UpdateNews(news);
        }

        /// <summary>
        /// 刪除新聞
        /// </summary>
        /// <param name="news">被刪除的新聞</param>
        public void DeleteNews(News news)
        {
            FTISDao.DeleteNews(news);
        }

        /// <summary>
        /// 取得新聞 By 識別碼
        /// </summary>
        /// <param name="newsId">識別碼</param>
        /// <returns>新聞</returns>
        public News GetNewsById(int newsId)
        {
            return FTISDao.GetNewsById(newsId);
        }

        /// <summary>
        /// 取得新聞分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞分類清單</returns>
        public IList<News> GetNewsList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsList(conditions);
        }

        /// <summary>
        /// 取得新聞清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞清單</returns>
        public IList<News> GetNewsListNoLazy(IDictionary<string, string> conditions)
        {
            IList<News> list = FTISDao.GetNewsList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (News news in list)
                {
                    if (news != null && news.NewsClass != null)
                    {
                        NHibernateUtil.Initialize(news.NewsClass);
                    }
                    else
                    {
                        news.NewsClass = new NewsClass();
                    }

                    if (news != null && news.NewsType != null)
                    {
                        NHibernateUtil.Initialize(news.NewsType);
                    }
                    else
                    {
                        news.NewsType = new NewsType();
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得新聞分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNewsCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsCount(conditions);
        }

        /// <summary>
        /// 取得新聞 By 識別碼
        /// </summary>
        /// <param name="newsId">識別碼</param>
        /// <returns>新聞</returns>
        public News GetNewsByIdNoLazy(int newsId)
        {
            News news = FTISDao.GetNewsById(newsId);

            if (news != null && news.NewsClass != null)
            {
                NHibernateUtil.Initialize(news.NewsClass);
            }

            if (news != null && news.NewsType != null)
            {
                NHibernateUtil.Initialize(news.NewsType);
            }

            return news;
        }
        #endregion

        #region AdminBar
        /// <summary>
        /// 取得全部後台功能清單
        /// </summary>
        /// <returns>後台功能清單</returns>
        public IList<AdminBar> GetAllAdminBar()
        {
            return FTISDao.GetAllAdminBar();
        }
        #endregion

        #region MasterMember
        /// <summary>
        /// 取得一個新的管理者帳號空殼
        /// </summary>
        /// <returns></returns>
        public MasterMember MakeMasterMember()
        {
            MasterMember masterMember = new MasterMember();
            masterMember.AdminRoles = new List<AdminRole>();
            IList<AdminBar> allAdminBar = GetAllAdminBar();
            foreach (AdminBar adminBar in allAdminBar)
            {
                AdminRole adminRole = new AdminRole();
                adminRole.MasterMember = masterMember;
                adminRole.AdminBar = adminBar;
                masterMember.AdminRoles.Add(adminRole);
            }

            return masterMember;
        }

        /// <summary>
        /// 管理者帳號
        /// </summary>
        /// <param name="masterMember">被新增的管理者帳號</param>
        /// <returns>新增後的管理者帳號</returns>
        public MasterMember CreateMasterMember(MasterMember masterMember)
        {
            return FTISDao.CreateMasterMember(masterMember);
        }

        /// <summary>
        /// 更新管理者帳號
        /// </summary>
        /// <param name="masterMember">被更新的管理者帳號</param>
        /// <returns>更新後的管理者帳號</returns>
        public MasterMember UpdateMasterMember(MasterMember masterMember)
        {
            return FTISDao.UpdateMasterMember(masterMember);
        }

        /// <summary>
        /// 刪除管理者帳號
        /// </summary>
        /// <param name="masterMember">被刪除的管理者帳號</param>
        public void DeleteMasterMember(MasterMember masterMember)
        {
            FTISDao.DeleteMasterMember(masterMember);
        }

        /// <summary>
        /// 取得管理者帳號 By 識別碼
        /// </summary>
        /// <param name="masterMemberId">識別碼</param>
        /// <returns>管理者帳號</returns>
        public MasterMember GetMasterMemberById(int masterMemberId)
        {
            return FTISDao.GetMasterMemberById(masterMemberId);
        }

        /// <summary>
        /// 取得管理者帳號清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>管理者帳號清單</returns>
        public IList<MasterMember> GetMasterMemberList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMasterMemberList(conditions);
        }

        /// <summary>
        /// 取得管理者帳號數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMasterMemberCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMasterMemberCount(conditions);
        }

        /// <summary>
        /// 取得管理者帳號清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>管理者帳號清單</returns>
        public IList<MasterMember> GetMasterMemberListNoLazy(IDictionary<string, string> conditions)
        {
            IList<MasterMember> list = FTISDao.GetMasterMemberList(conditions);
            if (list != null && list.Count > 0)
            {
                foreach (MasterMember admin in list)
                {
                    NHibernateUtil.Initialize(admin.AdminRoles);
                    foreach (AdminRole role in admin.AdminRoles)
                    {
                        NHibernateUtil.Initialize(role.AdminBar);
                    }
                }
            }
            return list;
        }
        #endregion

        #region NewsType
        /// <summary>
        /// 新增新聞種類
        /// </summary>
        /// <param name="newsType">被新增的新聞種類</param>
        /// <returns>新增後的新聞種類</returns>
        public NewsType CreateNewsType(NewsType newsType)
        {
            return FTISDao.CreateNewsType(newsType);
        }

        /// <summary>
        /// 更新新聞種類
        /// </summary>
        /// <param name="newsClass">被更新的新聞種類</param>
        /// <returns>更新後的新聞種類</returns>
        public NewsType UpdateNewsType(NewsType newsType)
        {
            return FTISDao.UpdateNewsType(newsType);
        }

        /// <summary>
        /// 刪除新聞種類
        /// </summary>
        /// <param name="newsType">被刪除的新聞種類</param>
        public void DeleteNewsType(NewsType newsType)
        {
            FTISDao.DeleteNewsType(newsType);
        }

        /// <summary>
        /// 取得新聞種類 By 識別碼
        /// </summary>
        /// <param name="newsTypeId">識別碼</param>
        /// <returns>新聞種類</returns>
        public NewsType GetNewsTypeById(int newsTypeId)
        {
            return FTISDao.GetNewsTypeById(newsTypeId);
        }

        /// <summary>
        /// 取得新聞種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞種類清單</returns>
        public IList<NewsType> GetNewsTypeList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsTypeList(conditions);
        }

        /// <summary>
        /// 取得新聞種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNewsTypeCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsTypeCount(conditions);
        }
        #endregion

        #region MasterLog
        /// <summary>
        /// 新增後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被新增的後台帳號登入紀錄</param>
        /// <returns>新增後的後台帳號登入紀錄</returns>
        public MasterLog CreateMasterLog(MasterLog masterLog)
        {
            return FTISDao.CreateMasterLog(masterLog);
        }

        /// <summary>
        /// 更新後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被更新的後台帳號登入紀錄</param>
        /// <returns>更新後的後台帳號登入紀錄</returns>
        public MasterLog UpdateMasterLog(MasterLog masterLog)
        {
            return FTISDao.UpdateMasterLog(masterLog);
        }

        /// <summary>
        /// 刪除後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被刪除的後台帳號登入紀錄</param>
        public void DeleteMasterLog(MasterLog masterLog)
        {
            FTISDao.DeleteMasterLog(masterLog);
        }

        /// <summary>
        /// 取得後台帳號登入紀錄 By 識別碼
        /// </summary>
        /// <param name="masterLogId">識別碼</param>
        /// <returns>後台帳號登入紀錄</returns>
        public MasterLog GetMasterLogById(int masterLogId)
        {
            return FTISDao.GetMasterLogById(masterLogId);
        }

        /// <summary>
        /// 取得後台帳號登入紀錄清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>後台帳號登入紀錄清單</returns>
        public IList<MasterLog> GetMasterLogList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMasterLogList(conditions);
        }

        /// <summary>
        /// 取得後台帳號登入紀錄數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMasterLogCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMasterLogCount(conditions);
        }
        #endregion

        #region Node
        /// <summary>
        /// 新增種類
        /// </summary>
        /// <param name="node">被新增的種類</param>
        /// <returns>新增後的種類</returns>
        public Node CreateNode(Node node)
        {
            return FTISDao.CreateNode(node);
        }

        /// <summary>
        /// 更新種類
        /// </summary>
        /// <param name="node">被更新的種類</param>
        /// <returns>更新後的種類</returns>
        public Node UpdateNode(Node node)
        {
            return FTISDao.UpdateNode(node);
        }

        /// <summary>
        /// 刪除種類
        /// </summary>
        /// <param name="node">被刪除的種類</param>
        public void DeleteNode(Node node)
        {
            FTISDao.DeleteNode(node);
        }

        /// <summary>
        /// 取得種類 By 識別碼
        /// </summary>
        /// <param name="nodeId">識別碼</param>
        /// <returns>種類</returns>
        public Node GetNodeById(int nodeId)
        {
            return FTISDao.GetNodeById(nodeId);
        }

        /// <summary>
        /// 取得種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>種類清單</returns>
        public IList<Node> GetNodeList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNodeList(conditions);
        }

        /// <summary>
        /// 取得種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>種類清單</returns>
        public IList<Node> GetNodeListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Node> list = FTISDao.GetNodeList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Node node in list)
                {
                    if (node != null && node.ParentNode != null)
                    {
                        NHibernateUtil.Initialize(node.ParentNode);
                        node.ParentNode.ParentNode = null;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNodeCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNodeCount(conditions);
        }

        /// <summary>
        /// 取得種類 By 識別碼
        /// </summary>
        /// <param name="nodeId">識別碼</param>
        /// <returns>種類</returns>
        public Node GetNodeByIdNoLazy(int nodeId)
        {
            Node node = FTISDao.GetNodeById(nodeId);

            if (node != null && node.ParentNode != null)
            {
                NHibernateUtil.Initialize(node.ParentNode);
            }

            return node;
        }
        #endregion

        #region Activity
        /// <summary>
        /// 新增活動訊息
        /// </summary>
        /// <param name="activity">被新增的活動訊息</param>
        /// <returns>新增後的活動訊息</returns>
        public Activity CreateActivity(Activity activity)
        {
            return FTISDao.CreateActivity(activity);
        }

        /// <summary>
        /// 更新活動訊息
        /// </summary>
        /// <param name="activity">被更新的活動訊息</param>
        /// <returns>更新後的活動訊息</returns>
        public Activity UpdateActivity(Activity activity)
        {
            return FTISDao.UpdateActivity(activity);
        }

        /// <summary>
        /// 刪除活動訊息
        /// </summary>
        /// <param name="activity">被刪除的活動訊息</param>
        public void DeleteActivity(Activity activity)
        {
            FTISDao.DeleteActivity(activity);
        }

        /// <summary>
        /// 取得活動訊息 By 識別碼
        /// </summary>
        /// <param name="activityId">識別碼</param>
        /// <returns>活動訊息</returns>
        public Activity GetActivityById(int activityId)
        {
            return FTISDao.GetActivityById(activityId);
        }

        /// <summary>
        /// 取得活動訊息清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>活動訊息清單</returns>
        public IList<Activity> GetActivityList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetActivityList(conditions);
        }

        /// <summary>
        /// 取得活動訊息數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetActivityCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetActivityCount(conditions);
        }

        #endregion

        #region HomeNews
        /// <summary>
        /// 新增即時訊息
        /// </summary>
        /// <param name="homeNews">被新增的即時訊息</param>
        /// <returns>新增後的即時訊息</returns>
        public HomeNews CreateHomeNews(HomeNews homeNews)
        {
            return FTISDao.CreateHomeNews(homeNews);
        }

        /// <summary>
        /// 更新即時訊息
        /// </summary>
        /// <param name="homeNews">被更新的即時訊息</param>
        /// <returns>更新後的即時訊息</returns>
        public HomeNews UpdateHomeNews(HomeNews homeNews)
        {
            return FTISDao.UpdateHomeNews(homeNews);
        }

        /// <summary>
        /// 刪除即時訊息
        /// </summary>
        /// <param name="homeNews">被刪除的即時訊息</param>
        public void DeleteHomeNews(HomeNews homeNews)
        {
            FTISDao.DeleteHomeNews(homeNews);
        }

        /// <summary>
        /// 取得即時訊息 By 識別碼
        /// </summary>
        /// <param name="homeNewsId">識別碼</param>
        /// <returns>即時訊息</returns>
        public HomeNews GetHomeNewsById(int homeNewsId)
        {
            return FTISDao.GetHomeNewsById(homeNewsId);
        }

        /// <summary>
        /// 取得即時訊息清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>即時訊息清單</returns>
        public IList<HomeNews> GetHomeNewsList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetHomeNewsList(conditions);
        }

        /// <summary>
        /// 取得即時訊息數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetHomeNewsCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetHomeNewsCount(conditions);
        }
        #endregion

        #region LinksClass
        /// <summary>
        /// 新增網路資源分類
        /// </summary>
        /// <param name="linksClass">被新增的網路資源分類</param>
        /// <returns>新增後的網路資源分類</returns>
        public LinksClass CreateLinksClass(LinksClass linksClass)
        {
            return FTISDao.CreateLinksClass(linksClass);
        }

        /// <summary>
        /// 更新網路資源分類
        /// </summary>
        /// <param name="linksClass">被更新的網路資源分類</param>
        /// <returns>更新後的網路資源分類</returns>
        public LinksClass UpdateLinksClass(LinksClass linksClass)
        {
            return FTISDao.UpdateLinksClass(linksClass);
        }

        /// <summary>
        /// 刪除網路資源分類
        /// </summary>
        /// <param name="linksClass">被刪除的網路資源分類</param>
        public void DeleteLinksClass(LinksClass linksClass)
        {
            FTISDao.DeleteLinksClass(linksClass);
        }

        /// <summary>
        /// 取得網路資源分類 By 識別碼
        /// </summary>
        /// <param name="linksClassId">識別碼</param>
        /// <returns>網路資源分類</returns>
        public LinksClass GetLinksClassById(int linksClassId)
        {
            return FTISDao.GetLinksClassById(linksClassId);
        }

        /// <summary>
        /// 取得網路資源分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源分類清單</returns>
        public IList<LinksClass> GetLinksClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetLinksClassList(conditions);
        }

        /// <summary>
        /// 取得網路資源分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetLinksClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetLinksClassCount(conditions);
        }
        #endregion

        #region Links
        /// <summary>
        /// 網路資源
        /// </summary>
        /// <param name="links">被新增的網路資源</param>
        /// <returns>新增後的網路資源</returns>
        public Links CreateLinks(Links links)
        {
            return FTISDao.CreateLinks(links);
        }

        /// <summary>
        /// 更新網路資源
        /// </summary>
        /// <param name="links">被更新的網路資源</param>
        /// <returns>更新後的網路資源</returns>
        public Links UpdateLinks(Links links)
        {
            return FTISDao.UpdateLinks(links);
        }

        /// <summary>
        /// 刪除網路資源
        /// </summary>
        /// <param name="links">被刪除的網路資源</param>
        public void DeleteLinks(Links links)
        {
            FTISDao.DeleteLinks(links);
        }

        /// <summary>
        /// 取得網路資源 By 識別碼
        /// </summary>
        /// <param name="linksId">識別碼</param>
        /// <returns>網路資源</returns>
        public Links GetLinksById(int linksId)
        {
            return FTISDao.GetLinksById(linksId);
        }

        /// <summary>
        /// 取得網路資源清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源清單</returns>
        public IList<Links> GetLinksList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetLinksList(conditions);
        }

        /// <summary>
        /// 取得網路資源清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源清單</returns>
        public IList<Links> GetLinksListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Links> list = FTISDao.GetLinksList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Links links in list)
                {
                    if (links != null && links.LinksClass != null)
                    {
                        NHibernateUtil.Initialize(links.LinksClass);
                    }
                    else
                    {
                        links.LinksClass = new LinksClass();
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得網路資源數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetLinksCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetLinksCount(conditions);
        }

        /// <summary>
        /// 取得網路資源 By 識別碼
        /// </summary>
        /// <param name="linksId">識別碼</param>
        /// <returns>網路資源</returns>
        public Links GetLinksByIdNoLazy(int linksId)
        {
            Links links = FTISDao.GetLinksById(linksId);

            if (links != null && links.LinksClass != null)
            {
                NHibernateUtil.Initialize(links.LinksClass);
            }

            return links;
        }
        #endregion

        #region GreenFactoryClass
        /// <summary>
        /// 新增綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被新增的綠色工廠分類</param>
        /// <returns>新增後的綠色工廠分類</returns>
        public GreenFactoryClass CreateGreenFactoryClass(GreenFactoryClass greenFactoryClass)
        {
            return FTISDao.CreateGreenFactoryClass(greenFactoryClass);
        }

        /// <summary>
        /// 更新綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被更新的綠色工廠分類</param>
        /// <returns>更新後的綠色工廠分類</returns>
        public GreenFactoryClass UpdateGreenFactoryClass(GreenFactoryClass greenFactoryClass)
        {
            return FTISDao.UpdateGreenFactoryClass(greenFactoryClass);
        }

        /// <summary>
        /// 刪除綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被刪除的綠色工廠分類</param>
        public void DeleteGreenFactoryClass(GreenFactoryClass greenFactoryClass)
        {
            FTISDao.DeleteGreenFactoryClass(greenFactoryClass);
        }

        /// <summary>
        /// 取得綠色工廠分類 By 識別碼
        /// </summary>
        /// <param name="greenFactoryClassId">識別碼</param>
        /// <returns>綠色工廠分類</returns>
        public GreenFactoryClass GetGreenFactoryClassById(int greenFactoryClassId)
        {
            return FTISDao.GetGreenFactoryClassById(greenFactoryClassId);
        }

        /// <summary>
        /// 取得綠色工廠分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠分類清單</returns>
        public IList<GreenFactoryClass> GetGreenFactoryClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetGreenFactoryClassList(conditions);
        }

        /// <summary>
        /// 取得綠色工廠分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetGreenFactoryClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetGreenFactoryClassCount(conditions);
        }
        #endregion

        #region GreenFactory
        /// <summary>
        /// 綠色工廠
        /// </summary>
        /// <param name="greenFactory">被新增的綠色工廠</param>
        /// <returns>新增後的綠色工廠</returns>
        public GreenFactory CreateGreenFactory(GreenFactory greenFactory)
        {
            return FTISDao.CreateGreenFactory(greenFactory);
        }

        /// <summary>
        /// 更新綠色工廠
        /// </summary>
        /// <param name="greenFactory">被更新的綠色工廠</param>
        /// <returns>更新後的綠色工廠</returns>
        public GreenFactory UpdateGreenFactory(GreenFactory greenFactory)
        {
            return FTISDao.UpdateGreenFactory(greenFactory);
        }

        /// <summary>
        /// 刪除綠色工廠
        /// </summary>
        /// <param name="greenFactory">被刪除的綠色工廠</param>
        public void DeleteGreenFactory(GreenFactory greenFactory)
        {
            FTISDao.DeleteGreenFactory(greenFactory);
        }

        /// <summary>
        /// 取得綠色工廠 By 識別碼
        /// </summary>
        /// <param name="greenFactoryId">識別碼</param>
        /// <returns>綠色工廠</returns>
        public GreenFactory GetGreenFactoryById(int greenFactoryId)
        {
            return FTISDao.GetGreenFactoryById(greenFactoryId);
        }

        /// <summary>
        /// 取得綠色工廠清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠清單</returns>
        public IList<GreenFactory> GetGreenFactoryList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetGreenFactoryList(conditions);
        }

        /// <summary>
        /// 取得綠色工廠清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠清單</returns>
        public IList<GreenFactory> GetGreenFactoryListNoLazy(IDictionary<string, string> conditions)
        {
            IList<GreenFactory> list = FTISDao.GetGreenFactoryList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (GreenFactory greenFactory in list)
                {
                    if (greenFactory != null && greenFactory.GreenFactoryClass != null)
                    {
                        NHibernateUtil.Initialize(greenFactory.GreenFactoryClass);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得綠色工廠數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetGreenFactoryCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetGreenFactoryCount(conditions);
        }

        /// <summary>
        /// 取得綠色工廠 By 識別碼
        /// </summary>
        /// <param name="greenFactoryId">識別碼</param>
        /// <returns>綠色工廠</returns>
        public GreenFactory GetGreenFactoryByIdNoLazy(int greenFactoryId)
        {
            GreenFactory greenFactory = FTISDao.GetGreenFactoryById(greenFactoryId);

            if (greenFactory != null && greenFactory.GreenFactoryClass != null)
            {
                NHibernateUtil.Initialize(greenFactory.GreenFactoryClass);
            }

            return greenFactory;
        }
        #endregion

        #region ApplicationClass
        /// <summary>
        /// 新增企業社會責任分類
        /// </summary>
        /// <param name="applicationClass">被新增的企業社會責任分類</param>
        /// <returns>新增後的企業社會責任分類</returns>
        public ApplicationClass CreateApplicationClass(ApplicationClass applicationClass)
        {
            return FTISDao.CreateApplicationClass(applicationClass);
        }

        /// <summary>
        /// 更新社會責任分類
        /// </summary>
        /// <param name="applicationClass">被更新的社會責任分類</param>
        /// <returns>更新後的社會責任分類</returns>
        public ApplicationClass UpdateApplicationClass(ApplicationClass applicationClass)
        {
            return FTISDao.UpdateApplicationClass(applicationClass);
        }

        /// <summary>
        /// 刪除社會責任分類
        /// </summary>
        /// <param name="applicationClass">被刪除的社會責任分類</param>
        public void DeleteApplicationClass(ApplicationClass applicationClass)
        {
            FTISDao.DeleteApplicationClass(applicationClass);
        }

        /// <summary>
        /// 取得社會責任分類 By 識別碼
        /// </summary>
        /// <param name="applicationClassId">識別碼</param>
        /// <returns>社會責任分類</returns>
        public ApplicationClass GetApplicationClassById(int applicationClassId)
        {
            return FTISDao.GetApplicationClassById(applicationClassId);
        }

        /// <summary>
        /// 取得社會責任分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>社會責任分類清單</returns>
        public IList<ApplicationClass> GetApplicationClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetApplicationClassList(conditions);
        }

        /// <summary>
        /// 取得社會責任分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetApplicationClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetApplicationClassCount(conditions);
        }
        #endregion

        #region Application
        /// <summary>
        /// 企業社會責任
        /// </summary>
        /// <param name="application">被新增的企業社會責任</param>
        /// <returns>新增後的企業社會責任</returns>
        public Application CreateApplication(Application application)
        {
            return FTISDao.CreateApplication(application);
        }

        /// <summary>
        /// 更新企業社會責任
        /// </summary>
        /// <param name="application">被更新的企業社會責任</param>
        /// <returns>更新後的企業社會責任</returns>
        public Application UpdateApplication(Application application)
        {
            return FTISDao.UpdateApplication(application);
        }

        /// <summary>
        /// 刪除企業社會責任
        /// </summary>
        /// <param name="application">被刪除的企業社會責任</param>
        public void DeleteApplication(Application application)
        {
            FTISDao.DeleteApplication(application);
        }

        /// <summary>
        /// 取得企業社會責任 By 識別碼
        /// </summary>
        /// <param name="applicationId">識別碼</param>
        /// <returns>企業社會責任</returns>
        public Application GetApplicationById(int applicationId)
        {
            return FTISDao.GetApplicationById(applicationId);
        }

        /// <summary>
        /// 取得企業社會責任清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>企業社會責任清單</returns>
        public IList<Application> GetApplicationList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetApplicationList(conditions);
        }

        /// <summary>
        /// 取得企業社會責任清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>企業社會責任清單</returns>
        public IList<Application> GetApplicationListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Application> list = FTISDao.GetApplicationList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Application application in list)
                {
                    if (application != null && application.ApplicationClass != null)
                    {
                        NHibernateUtil.Initialize(application.ApplicationClass);
                    }
                    else
                    {
                        application.ApplicationClass = new ApplicationClass();
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得企業社會責任數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetApplicationCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetApplicationCount(conditions);
        }

        /// <summary>
        /// 取得企業社會責任 By 識別碼
        /// </summary>
        /// <param name="applicationId">識別碼</param>
        /// <returns>企業社會責任</returns>
        public Application GetApplicationByIdNoLazy(int applicationId)
        {
            Application application = FTISDao.GetApplicationById(applicationId);

            if (application != null && application.ApplicationClass != null)
            {
                NHibernateUtil.Initialize(application.ApplicationClass);
            }

            return application;
        }
        #endregion

        #region PublicationClass
        /// <summary>
        /// 新增期刊分類
        /// </summary>
        /// <param name="publicationClass">被新增的期刊分類</param>
        /// <returns>新增後的期刊分類</returns>
        public PublicationClass CreatePublicationClass(PublicationClass publicationClass)
        {
            return FTISDao.CreatePublicationClass(publicationClass);
        }

        /// <summary>
        /// 更新期刊分類
        /// </summary>
        /// <param name="publicationClass">被更新的期刊分類</param>
        /// <returns>更新後的期刊分類</returns>
        public PublicationClass UpdatePublicationClass(PublicationClass publicationClass)
        {
            return FTISDao.UpdatePublicationClass(publicationClass);
        }

        /// <summary>
        /// 刪除期刊分類
        /// </summary>
        /// <param name="publicationClass">被刪除的期刊分類</param>
        public void DeletePublicationClass(PublicationClass publicationClass)
        {
            FTISDao.DeletePublicationClass(publicationClass);
        }

        /// <summary>
        /// 取得期刊分類 By 識別碼
        /// </summary>
        /// <param name="publicationClassId">識別碼</param>
        /// <returns>期刊分類</returns>
        public PublicationClass GetPublicationClassById(int publicationClassId)
        {
            return FTISDao.GetPublicationClassById(publicationClassId);
        }

        /// <summary>
        /// 取得期刊分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊分類清單</returns>
        public IList<PublicationClass> GetPublicationClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetPublicationClassList(conditions);
        }

        /// <summary>
        /// 取得期刊分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPublicationClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetPublicationClassCount(conditions);
        }
        #endregion

        #region Publication
        /// <summary>
        /// 期刊
        /// </summary>
        /// <param name="publication">被新增的期刊</param>
        /// <returns>新增後的期刊</returns>
        public Publication CreatePublication(Publication publication)
        {
            return FTISDao.CreatePublication(publication);
        }

        /// <summary>
        /// 更新期刊
        /// </summary>
        /// <param name="publication">被更新的期刊</param>
        /// <returns>更新後的期刊</returns>
        public Publication UpdatePublication(Publication publication)
        {
            return FTISDao.UpdatePublication(publication);
        }

        /// <summary>
        /// 刪除期刊
        /// </summary>
        /// <param name="publication">被刪除的期刊</param>
        public void DeletePublication(Publication publication)
        {
            FTISDao.DeletePublication(publication);
        }

        /// <summary>
        /// 取得期刊 By 識別碼
        /// </summary>
        /// <param name="publicationId">識別碼</param>
        /// <returns>期刊</returns>
        public Publication GetPublicationById(int publicationId)
        {
            return FTISDao.GetPublicationById(publicationId);
        }

        /// <summary>
        /// 取得期刊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊清單</returns>
        public IList<Publication> GetPublicationList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetPublicationList(conditions);
        }

        /// <summary>
        /// 取得期刊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊清單</returns>
        public IList<Publication> GetPublicationListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Publication> list = FTISDao.GetPublicationList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Publication publication in list)
                {
                    if (publication != null && publication.PublicationClass != null)
                    {
                        NHibernateUtil.Initialize(publication.PublicationClass);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得期刊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPublicationCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetPublicationCount(conditions);
        }

        /// <summary>
        /// 取得期刊 By 識別碼
        /// </summary>
        /// <param name="publicationId">識別碼</param>
        /// <returns>期刊</returns>
        public Publication GetPublicationByIdNoLazy(int publicationId)
        {
            Publication publication = FTISDao.GetPublicationById(publicationId);

            if (publication != null && publication.PublicationClass != null)
            {
                NHibernateUtil.Initialize(publication.PublicationClass);
            }

            return publication;
        }
        #endregion

        #region QuestionClass
        /// <summary>
        /// 新增Q&A分類
        /// </summary>
        /// <param name="questionClass">被新增的Q&A分類</param>
        /// <returns>新增後的Q&A分類</returns>
        public QuestionClass CreateQuestionClass(QuestionClass questionClass)
        {
            return FTISDao.CreateQuestionClass(questionClass);
        }

        /// <summary>
        /// 更新Q&A分類
        /// </summary>
        /// <param name="questionClass">被更新的Q&A分類</param>
        /// <returns>更新後的Q&A分類</returns>
        public QuestionClass UpdateQuestionClass(QuestionClass questionClass)
        {
            return FTISDao.UpdateQuestionClass(questionClass);
        }

        /// <summary>
        /// 刪除Q&A分類
        /// </summary>
        /// <param name="questionClass">被刪除的Q&A分類</param>
        public void DeleteQuestionClass(QuestionClass questionClass)
        {
            FTISDao.DeleteQuestionClass(questionClass);
        }

        /// <summary>
        /// 取得Q&A分類 By 識別碼
        /// </summary>
        /// <param name="questionClassId">識別碼</param>
        /// <returns>Q&A分類</returns>
        public QuestionClass GetQuestionClassById(int questionClassId)
        {
            return FTISDao.GetQuestionClassById(questionClassId);
        }

        /// <summary>
        /// 取得Q&A分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A分類清單</returns>
        public IList<QuestionClass> GetQuestionClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetQuestionClassList(conditions);
        }

        /// <summary>
        /// 取得Q&A分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetQuestionClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetQuestionClassCount(conditions);
        }
        #endregion

        #region Question
        /// <summary>
        /// Q&A
        /// </summary>
        /// <param name="question">被新增的Q&A</param>
        /// <returns>新增後的Q&A</returns>
        public Question CreateQuestion(Question question)
        {
            return FTISDao.CreateQuestion(question);
        }

        /// <summary>
        /// 更新Q&A
        /// </summary>
        /// <param name="question">被更新的Q&A</param>
        /// <returns>更新後的Q&A</returns>
        public Question UpdateQuestion(Question question)
        {
            return FTISDao.UpdateQuestion(question);
        }

        /// <summary>
        /// 刪除Q&A
        /// </summary>
        /// <param name="question">被刪除的Q&A</param>
        public void DeleteQuestion(Question question)
        {
            FTISDao.DeleteQuestion(question);
        }

        /// <summary>
        /// 取得Q&A By 識別碼
        /// </summary>
        /// <param name="questionId">識別碼</param>
        /// <returns>Q&A</returns>
        public Question GetQuestionById(int questionId)
        {
            return FTISDao.GetQuestionById(questionId);
        }

        /// <summary>
        /// 取得Q&A清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A清單</returns>
        public IList<Question> GetQuestionList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetQuestionList(conditions);
        }

        /// <summary>
        /// 取得Q&A清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A清單</returns>
        public IList<Question> GetQuestionListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Question> list = FTISDao.GetQuestionList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Question question in list)
                {
                    if (question != null && question.QuestionClass != null)
                    {
                        NHibernateUtil.Initialize(question.QuestionClass);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得Q&A數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetQuestionCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetQuestionCount(conditions);
        }

        /// <summary>
        /// 取得Q&A By 識別碼
        /// </summary>
        /// <param name="questionId">識別碼</param>
        /// <returns>Q&A</returns>
        public Question GetQuestionByIdNoLazy(int questionId)
        {
            Question question = FTISDao.GetQuestionById(questionId);

            if (question != null && question.QuestionClass != null)
            {
                NHibernateUtil.Initialize(question.QuestionClass);
            }

            return question;
        }
        #endregion

        #region Brief
        /// <summary>
        /// 新增永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被新增的永續產業發展簡訊</param>
        /// <returns>新增後的永續產業發展簡訊</returns>
        public Brief CreateBrief(Brief brief)
        {
            return FTISDao.CreateBrief(brief);
        }

        /// <summary>
        /// 更新永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被更新的永續產業發展簡訊</param>
        /// <returns>更新後的永續產業發展簡訊</returns>
        public Brief UpdateBrief(Brief brief)
        {
            return FTISDao.UpdateBrief(brief);
        }

        /// <summary>
        /// 刪除永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被刪除的永續產業發展簡訊</param>
        public void DeleteBrief(Brief brief)
        {
            FTISDao.DeleteBrief(brief);
        }

        /// <summary>
        /// 取得永續產業發展簡訊 By 識別碼
        /// </summary>
        /// <param name="briefId">識別碼</param>
        /// <returns>永續產業發展簡訊</returns>
        public Brief GetBriefById(int briefId)
        {
            return FTISDao.GetBriefById(briefId);
        }

        /// <summary>
        /// 取得永續產業發展簡訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>永續產業發展簡訊清單</returns>
        public IList<Brief> GetBriefList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetBriefList(conditions);
        }

        /// <summary>
        /// 取得永續產業發展簡訊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetBriefCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetBriefCount(conditions);
        }
        #endregion

        #region Curriculum
        /// <summary>
        /// 新增課程講義
        /// </summary>
        /// <param name="curriculum">被新增的課程講義</param>
        /// <returns>新增後的課程講義</returns>
        public Curriculum CreateCurriculum(Curriculum curriculum)
        {
            return FTISDao.CreateCurriculum(curriculum);
        }

        /// <summary>
        /// 更新課程講義
        /// </summary>
        /// <param name="curriculum">被更新的課程講義</param>
        /// <returns>更新後的課程講義</returns>
        public Curriculum UpdateCurriculum(Curriculum curriculum)
        {
            return FTISDao.UpdateCurriculum(curriculum);
        }

        /// <summary>
        /// 刪除課程講義
        /// </summary>
        /// <param name="curriculum">被刪除的課程講義</param>
        public void DeleteCurriculum(Curriculum curriculum)
        {
            FTISDao.DeleteCurriculum(curriculum);
        }

        /// <summary>
        /// 取得課程講義 By 識別碼
        /// </summary>
        /// <param name="curriculumId">識別碼</param>
        /// <returns>課程講義</returns>
        public Curriculum GetCurriculumById(int curriculumId)
        {
            return FTISDao.GetCurriculumById(curriculumId);
        }

        /// <summary>
        /// 取得課程講義清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>課程講義清單</returns>
        public IList<Curriculum> GetCurriculumList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetCurriculumList(conditions);
        }

        /// <summary>
        /// 取得課程講義數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetCurriculumCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetCurriculumCount(conditions);
        }
        #endregion

        #region Download
        /// <summary>
        /// 技術工具/文件下載
        /// </summary>
        /// <param name="download">被新增的技術工具/文件下載</param>
        /// <returns>新增後的技術工具/文件下載</returns>
        public Download CreateDownload(Download download)
        {
            return FTISDao.CreateDownload(download);
        }

        /// <summary>
        /// 更新技術工具/文件下載
        /// </summary>
        /// <param name="download">被更新的技術工具/文件下載</param>
        /// <returns>更新後的技術工具/文件下載</returns>
        public Download UpdateDownload(Download download)
        {
            return FTISDao.UpdateDownload(download);
        }

        /// <summary>
        /// 刪除技術工具/文件下載
        /// </summary>
        /// <param name="download">被刪除的技術工具/文件下載</param>
        public void DeleteDownload(Download download)
        {
            FTISDao.DeleteDownload(download);
        }

        /// <summary>
        /// 取得技術工具/文件下載 By 識別碼
        /// </summary>
        /// <param name="downloadId">識別碼</param>
        /// <returns>技術工具/文件下載</returns>
        public Download GetDownloadById(int downloadId)
        {
            return FTISDao.GetDownloadById(downloadId);
        }

        /// <summary>
        /// 取得技術工具/文件下載清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>技術工具/文件下載清單</returns>
        public IList<Download> GetDownloadList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetDownloadList(conditions);
        }

        /// <summary>
        /// 取得技術工具/文件下載數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetDownloadCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetDownloadCount(conditions);
        }
        #endregion

        #region Industry
        /// <summary>
        /// 行業分類
        /// </summary>
        /// <param name="industry">被新增的行業分類</param>
        /// <returns>新增後的行業分類</returns>
        public Industry CreateIndustry(Industry industry)
        {
            return FTISDao.CreateIndustry(industry);
        }

        /// <summary>
        /// 更新行業分類
        /// </summary>
        /// <param name="industry">被更新的行業分類</param>
        /// <returns>更新後的行業分類</returns>
        public Industry UpdateIndustry(Industry industry)
        {
            return FTISDao.UpdateIndustry(industry);
        }

        /// <summary>
        /// 刪除行業分類
        /// </summary>
        /// <param name="industry">被刪除的行業分類</param>
        public void DeleteIndustry(Industry industry)
        {
            FTISDao.DeleteIndustry(industry);
        }

        /// <summary>
        /// 取得行業分類 By 識別碼
        /// </summary>
        /// <param name="industryId">識別碼</param>
        /// <returns>行業分類</returns>
        public Industry GetIndustryById(int industryId)
        {
            return FTISDao.GetIndustryById(industryId);
        }

        /// <summary>
        /// 取得行業分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>行業分類清單</returns>
        public IList<Industry> GetIndustryList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetIndustryList(conditions);
        }

        /// <summary>
        /// 取得行業分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetIndustryCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetIndustryCount(conditions);
        }
        #endregion

        #region Member
        /// <summary>
        /// 會員
        /// </summary>
        /// <param name="member">被新增的會員</param>
        /// <returns>新增後的會員</returns>
        public Member CreateMember(Member member)
        {
            return FTISDao.CreateMember(member);
        }

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="member">被更新的會員</param>
        /// <returns>更新後的會員</returns>
        public Member UpdateMember(Member member)
        {
            return FTISDao.UpdateMember(member);
        }

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="member">被刪除的會員</param>
        public void DeleteMember(Member member)
        {
            FTISDao.DeleteMember(member);
        }

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        public Member GetMemberById(int memberId)
        {
            return FTISDao.GetMemberById(memberId);
        }

        /// <summary>
        /// 取得會員清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員清單</returns>
        public IList<Member> GetMemberList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMemberList(conditions);
        }

        /// <summary>
        /// 取得會員數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMemberCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMemberCount(conditions);
        }

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        public Member GetMemberByIdNoLazy(int memberId)
        {
            Member member = FTISDao.GetMemberById(memberId);

            if (member != null && member.Industry != null)
            {
                NHibernateUtil.Initialize(member.Industry);
            }

            return member;
        }

        /// <summary>
        /// 取得會員 By 登入帳號
        /// </summary>
        /// <param name="loginId">識別碼</param>
        /// <returns>會員</returns>
        public Member GetMemberByLoginId(string loginId)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("LoginId", loginId);
            IList<Member> memberList = FTISDao.GetMemberList(conditions);

            if (memberList != null && memberList.Count > 0)
            {
                return memberList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得會員 By 登入帳號
        /// </summary>
        /// <param name="loginId">識別碼</param>
        /// <returns>會員</returns>
        public Member GetMemberByLoginIdNoLazy(string loginId)
        {
            Member member = GetMemberByLoginId(loginId);

            if (member != null && member.Industry != null)
            {
                NHibernateUtil.Initialize(member.Industry);
            }

            return member;
        }
        #endregion

        #region DownloadRecord
        /// <summary>
        /// 會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被新增的會員下載紀錄</param>
        /// <returns>新增後的會員下載紀錄</returns>
        public DownloadRecord CreateDownloadRecord(DownloadRecord downloadRecord)
        {
            return FTISDao.CreateDownloadRecord(downloadRecord);
        }

        /// <summary>
        /// 更新會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被更新的會員下載紀錄</param>
        /// <returns>更新後的會員下載紀錄</returns>
        public DownloadRecord UpdateDownloadRecord(DownloadRecord downloadRecord)
        {
            return FTISDao.UpdateDownloadRecord(downloadRecord);
        }

        /// <summary>
        /// 刪除會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被刪除的會員下載紀錄</param>
        public void DeleteDownloadRecord(DownloadRecord downloadRecord)
        {
            FTISDao.DeleteDownloadRecord(downloadRecord);
        }

        /// <summary>
        /// 取得會員下載紀錄 By 識別碼
        /// </summary>
        /// <param name="downloadRecordId">識別碼</param>
        /// <returns>會員下載紀錄</returns>
        public DownloadRecord GetDownloadRecordById(int downloadRecordId)
        {
            return FTISDao.GetDownloadRecordById(downloadRecordId);
        }

        /// <summary>
        /// 取得會員下載紀錄清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員下載紀錄清單</returns>
        public IList<DownloadRecord> GetDownloadRecordList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetDownloadRecordList(conditions);
        }

        /// <summary>
        /// 取得會員下載紀錄數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetDownloadRecordCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetDownloadRecordCount(conditions);
        }

        ///// <summary>
        ///// 取得會員下載紀錄 By 識別碼
        ///// </summary>
        ///// <param name="downloadRecordId">識別碼</param>
        ///// <returns>會員下載紀錄</returns>
        //public DownloadRecord GetDownloadRecordByIdNoLazy(int downloadRecordId)
        //{
        //    DownloadRecord downloadRecord = FTISDao.GetDownloadRecordById(downloadRecordId);

        //    if (downloadRecord != null && downloadRecord.Member != null)
        //    {
        //        NHibernateUtil.Initialize(downloadRecord.Member);
        //    }

        //    return downloadRecord;
        //}

        /// <summary>
        /// 取得會員下載紀錄統計表清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員下載紀錄統計表清單</returns>
        public IList<DownloadRecord> GetDownloadRecordStatistics(IDictionary<string, string> conditions)
        {
            if (conditions.IsContainsValue("Order"))
            {
                conditions.Remove("Order");
            }
            conditions.Add("Order", "order by d.Name ");
            return FTISDao.GetDownloadRecordStatistics(conditions);
        }
        #endregion

        #region Epaper
        /// <summary>
        /// 新增電子報
        /// </summary>
        /// <param name="epaper">被新增的電子報</param>
        /// <returns>新增後的電子報</returns>
        public Epaper CreateEpaper(Epaper epaper)
        {
            return FTISDao.CreateEpaper(epaper);
        }

        /// <summary>
        /// 更新電子報
        /// </summary>
        /// <param name="epaper">被更新的電子報</param>
        /// <returns>更新後的電子報</returns>
        public Epaper UpdateEpaper(Epaper epaper)
        {
            return FTISDao.UpdateEpaper(epaper);
        }

        /// <summary>
        /// 刪除電子報
        /// </summary>
        /// <param name="epaper">被刪除的電子報</param>
        public void DeleteEpaper(Epaper epaper)
        {
            FTISDao.DeleteEpaper(epaper);
        }

        /// <summary>
        /// 取得電子報 By 識別碼
        /// </summary>
        /// <param name="epaperId">識別碼</param>
        /// <returns>電子報</returns>
        public Epaper GetEpaperById(int epaperId)
        {
            return FTISDao.GetEpaperById(epaperId);
        }

        /// <summary>
        /// 取得電子報清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報清單</returns>
        public IList<Epaper> GetEpaperList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperList(conditions);
        }

        /// <summary>
        /// 取得電子報數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetEpaperCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperCount(conditions);
        }

        /// <summary>
        /// 取得電子報年份清單
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>電子報年份清單</returns>
        public IList<string> GetEpaperYearList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperYearList(conditions);
        }
        #endregion

        #region EpaperEmail
        /// <summary>
        /// 電子報訂閱
        /// </summary>
        /// <param name="epaperEmail">被新增的電子報訂閱</param>
        /// <returns>新增後的電子報訂閱</returns>
        public EpaperEmail CreateEpaperEmail(EpaperEmail epaperEmail)
        {
            return FTISDao.CreateEpaperEmail(epaperEmail);
        }

        /// <summary>
        /// 更新電子報訂閱
        /// </summary>
        /// <param name="epaperEmail">被更新的電子報訂閱</param>
        /// <returns>更新後的電子報訂閱</returns>
        public EpaperEmail UpdateEpaperEmail(EpaperEmail epaperEmail)
        {
            return FTISDao.UpdateEpaperEmail(epaperEmail);
        }

        /// <summary>
        /// 刪除電子報訂閱
        /// </summary>
        /// <param name="member">被刪除的電子報訂閱</param>
        public void DeleteEpaperEmail(EpaperEmail epaperEmail)
        {
            FTISDao.DeleteEpaperEmail(epaperEmail);
        }

        /// <summary>
        /// 取得電子報訂閱 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>電子報訂閱</returns>
        public EpaperEmail GetEpaperEmailById(int epaperEmail)
        {
            return FTISDao.GetEpaperEmailById(epaperEmail);
        }

        /// <summary>
        /// 取得電子報訂閱清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報訂閱清單</returns>
        public IList<EpaperEmail> GetEpaperEmailList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperEmailList(conditions);
        }

        /// <summary>
        /// 取得電子報訂閱數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetEpaperEmailCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperEmailCount(conditions);
        }

        /// <summary>
        /// 取得會員 By 登入帳號
        /// </summary>
        /// <param name="email">識別碼</param>
        /// <returns>會員</returns>
        public EpaperEmail GetEpaperEmailByEmail(string email)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("ByEmail", email);
            IList<EpaperEmail> list = FTISDao.GetEpaperEmailList(conditions);

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region OtherExamination
        /// <summary>
        /// 新增問卷調查
        /// </summary>
        /// <param name="otherExamination">被新增的問卷調查</param>
        /// <returns>新增後的問卷調查</returns>
        public OtherExamination CreateOtherExamination(OtherExamination otherExamination)
        {
            return FTISDao.CreateOtherExamination(otherExamination);
        }

        /// <summary>
        /// 更新問卷調查
        /// </summary>
        /// <param name="otherExamination">被更新的問卷調查</param>
        /// <returns>更新後的問卷調查</returns>
        public OtherExamination UpdateOtherExamination(OtherExamination otherExamination)
        {
            return FTISDao.UpdateOtherExamination(otherExamination);
        }

        /// <summary>
        /// 刪除問卷調查
        /// </summary>
        /// <param name="otherExamination">被刪除的問卷調查</param>
        public void DeleteOtherExamination(OtherExamination otherExamination)
        {
            FTISDao.DeleteOtherExamination(otherExamination);
        }

        /// <summary>
        /// 取得問卷調查 By 識別碼
        /// </summary>
        /// <param name="otherExaminationId">識別碼</param>
        /// <returns>問卷調查</returns>
        public OtherExamination GetOtherExaminationById(int otherExaminationId)
        {
            return FTISDao.GetOtherExaminationById(otherExaminationId);
        }

        /// <summary>
        /// 取得問卷調查清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>問卷調查清單</returns>
        public IList<OtherExamination> GetOtherExaminationList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetOtherExaminationList(conditions);
        }

        /// <summary>
        /// 取得問卷調查數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetOtherExaminationCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetOtherExaminationCount(conditions);
        }
        #endregion

        #region MNorm
        /// <summary>
        /// 新增國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被新增的國際組織標準/系統動態</param>
        /// <returns>新增後的國際組織標準/系統動態</returns>
        public MNorm CreateMNorm(MNorm mNorm)
        {
            return FTISDao.CreateMNorm(mNorm);
        }

        /// <summary>
        /// 更新國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被更新的國際組織標準/系統動態</param>
        /// <returns>更新後的國際組織標準/系統動態</returns>
        public MNorm UpdateMNorm(MNorm mNorm)
        {
            return FTISDao.UpdateMNorm(mNorm);
        }

        /// <summary>
        /// 刪除國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被刪除的國際組織標準/系統動態</param>
        public void DeleteMNorm(MNorm mNorm)
        {
            FTISDao.DeleteMNorm(mNorm);
        }

        /// <summary>
        /// 取得國際組織標準/系統動態 By 識別碼
        /// </summary>
        /// <param name="mNormId">識別碼</param>
        /// <returns>國際組織標準/系統動態</returns>
        public MNorm GetMNormById(int mNormId)
        {
            return FTISDao.GetMNormById(mNormId);
        }

        /// <summary>
        /// 取得國際組織標準/系統動態清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>國際組織標準/系統動態清單</returns>
        public IList<MNorm> GetMNormList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMNormList(conditions);
        }

        /// <summary>
        /// 取得國際組織標準/系統動態數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMNormCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetMNormCount(conditions);
        }
        #endregion

        #region NormClass
        /// <summary>
        /// 標準/規範分類
        /// </summary>
        /// <param name="normClass">被新增的標準/規範分類</param>
        /// <returns>新增後的標準/規範分類</returns>
        public NormClass CreateNormClass(NormClass normClass)
        {
            return FTISDao.CreateNormClass(normClass);
        }

        /// <summary>
        /// 更新標準/規範分類
        /// </summary>
        /// <param name="normClass">被更新的標準/規範分類</param>
        /// <returns>更新後的標準/規範分類</returns>
        public NormClass UpdateNormClass(NormClass normClass)
        {
            return FTISDao.UpdateNormClass(normClass);
        }

        /// <summary>
        /// 刪除標準/規範分類
        /// </summary>
        /// <param name="normClass">被刪除的標準/規範分類</param>
        public void DeleteNormClass(NormClass normClass)
        {
            FTISDao.DeleteNormClass(normClass);
        }

        /// <summary>
        /// 取得標準/規範分類 By 識別碼
        /// </summary>
        /// <param name="normClassId">識別碼</param>
        /// <returns>標準/規範分類</returns>
        public NormClass GetNormClassById(int normClassId)
        {
            return FTISDao.GetNormClassById(normClassId);
        }

        /// <summary>
        /// 取得標準/規範分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範分類清單</returns>
        public IList<NormClass> GetNormClassList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNormClassList(conditions);
        }

        /// <summary>
        /// 取得標準/規範分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範分類清單</returns>
        public IList<NormClass> GetNormClassListNoLazy(IDictionary<string, string> conditions)
        {
            IList<NormClass> list = FTISDao.GetNormClassList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (NormClass normClass in list)
                {
                    if (normClass != null && normClass.ParentNormClass != null)
                    {
                        NHibernateUtil.Initialize(normClass.ParentNormClass);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得標準/規範分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNormClassCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNormClassCount(conditions);
        }

        /// <summary>
        /// 取得標準/規範分類 By 識別碼
        /// </summary>
        /// <param name="normClassId">識別碼</param>
        /// <returns>標準/規範分類</returns>
        public NormClass GetNormClassByIdNoLazy(int normClassId)
        {
            NormClass normClass = FTISDao.GetNormClassById(normClassId);

            if (normClass != null && normClass.ParentNormClass != null)
            {
                NHibernateUtil.Initialize(normClass.ParentNormClass);
            }

            return normClass;
        }
        #endregion

        #region Norm
        /// <summary>
        /// 新增標準/規範資訊
        /// </summary>
        /// <param name="norm">被新增的標準/規範資訊</param>
        /// <returns>新增後的標準/規範資訊</returns>
        public Norm CreateNorm(Norm norm)
        {
            return FTISDao.CreateNorm(norm);
        }

        /// <summary>
        /// 更新標準/規範資訊
        /// </summary>
        /// <param name="norm">被更新的標準/規範資訊</param>
        /// <returns>更新後的標準/規範資訊</returns>
        public Norm UpdateNorm(Norm norm)
        {
            return FTISDao.UpdateNorm(norm);
        }

        /// <summary>
        /// 刪除標準/規範資訊
        /// </summary>
        /// <param name="norm">被刪除的標準/規範資訊</param>
        public void DeleteNorm(Norm norm)
        {
            FTISDao.DeleteNorm(norm);
        }

        /// <summary>
        /// 取得標準/規範資訊 By 識別碼
        /// </summary>
        /// <param name="normId">識別碼</param>
        /// <returns>標準/規範資訊</returns>
        public Norm GetNormById(int normId)
        {
            return FTISDao.GetNormById(normId);
        }

        /// <summary>
        /// 取得標準/規範資訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範資訊清單</returns>
        public IList<Norm> GetNormList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNormList(conditions);
        }

        /// <summary>
        /// 取得標準/規範資訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範資訊清單</returns>
        public IList<Norm> GetNormListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Norm> list = FTISDao.GetNormList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Norm norm in list)
                {
                    if (norm != null && norm.NormClassParent != null)
                    {
                        NHibernateUtil.Initialize(norm.NormClassParent);
                    }

                    if (norm != null && norm.NormClass != null)
                    {
                        NHibernateUtil.Initialize(norm.NormClass);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得標準/規範資訊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNormCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNormCount(conditions);
        }

        /// <summary>
        /// 取得標準/規範資訊 By 識別碼
        /// </summary>
        /// <param name="newsId">識別碼</param>
        /// <returns>標準/規範資訊</returns>
        public Norm GetNormByIdNoLazy(int normId)
        {
            Norm norm = FTISDao.GetNormById(normId);

            if (norm != null && norm.NormClass != null)
            {
                NHibernateUtil.Initialize(norm.NormClass);
            }

            if (norm != null && norm.NormClassParent != null)
            {
                NHibernateUtil.Initialize(norm.NormClassParent);
            }

            return norm;
        }
        #endregion

        #region Examination
        /// <summary>
        /// 新增網站滿意度問卷
        /// </summary>
        /// <param name="examination">被新增的網站滿意度問卷</param>
        /// <returns>新增後的網站滿意度問卷</returns>
        public Examination CreateExamination(Examination examination)
        {
            return FTISDao.CreateExamination(examination);
        }

        /// <summary>
        /// 更新網站滿意度問卷
        /// </summary>
        /// <param name="examination">被更新的網站滿意度問卷</param>
        /// <returns>更新後的網站滿意度問卷</returns>
        public Examination UpdateExamination(Examination examination)
        {
            return FTISDao.UpdateExamination(examination);
        }

        /// <summary>
        /// 刪除網站滿意度問卷
        /// </summary>
        /// <param name="examination">被刪除的網站滿意度問卷</param>
        public void DeleteExamination(Examination examination)
        {
            FTISDao.DeleteExamination(examination);
        }

        /// <summary>
        /// 取得網站滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="examinationId">識別碼</param>
        /// <returns>網站滿意度問卷</returns>
        public Examination GetExaminationById(int examinationId)
        {
            return FTISDao.GetExaminationById(examinationId);
        }

        /// <summary>
        /// 取得網站滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網站滿意度問卷清單</returns>
        public IList<Examination> GetExaminationList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetExaminationList(conditions);
        }

        /// <summary>
        /// 取得網站滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網站滿意度問卷清單</returns>
        public IList<Examination> GetExaminationListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Examination> list = FTISDao.GetExaminationList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Examination examination in list)
                {
                    if (examination != null && examination.Industry != null)
                    {
                        NHibernateUtil.Initialize(examination.Industry);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得網站滿意度問卷數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetExaminationCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetExaminationCount(conditions);
        }

        /// <summary>
        /// 取得網站滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="examinationId">識別碼</param>
        /// <returns>網站滿意度問卷</returns>
        public Examination GetExaminationByIdNoLazy(int examinationId)
        {
            Examination examination = FTISDao.GetExaminationById(examinationId);

            if (examination != null && examination.Industry != null)
            {
                NHibernateUtil.Initialize(examination.Industry);
            }

            return examination;
        }
        #endregion

        #region EpaperExamination
        /// <summary>
        /// 新增電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被新增的電子報滿意度問卷</param>
        /// <returns>新增後的電子報滿意度問卷</returns>
        public EpaperExamination CreateEpaperExamination(EpaperExamination epaperExamination)
        {
            return FTISDao.CreateEpaperExamination(epaperExamination);
        }

        /// <summary>
        /// 更新電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被更新的電子報滿意度問卷</param>
        /// <returns>更新後的電子報滿意度問卷</returns>
        public EpaperExamination UpdateEpaperExamination(EpaperExamination epaperExamination)
        {
            return FTISDao.UpdateEpaperExamination(epaperExamination);
        }

        /// <summary>
        /// 刪除電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被刪除的電子報滿意度問卷</param>
        public void DeleteEpaperExamination(EpaperExamination epaperExamination)
        {
            FTISDao.DeleteEpaperExamination(epaperExamination);
        }

        /// <summary>
        /// 取得電子報滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="epaperExaminationId">識別碼</param>
        /// <returns>電子報滿意度問卷</returns>
        public EpaperExamination GetEpaperExaminationById(int epaperExaminationId)
        {
            return FTISDao.GetEpaperExaminationById(epaperExaminationId);
        }

        /// <summary>
        /// 取得電子報滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報滿意度問卷清單</returns>
        public IList<EpaperExamination> GetEpaperExaminationList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperExaminationList(conditions);
        }

        /// <summary>
        /// 取得電子報滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報滿意度問卷清單</returns>
        public IList<EpaperExamination> GetEpaperExaminationListNoLazy(IDictionary<string, string> conditions)
        {
            IList<EpaperExamination> list = FTISDao.GetEpaperExaminationList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (EpaperExamination epaperExamination in list)
                {
                    if (epaperExamination != null && epaperExamination.Industry != null)
                    {
                        NHibernateUtil.Initialize(epaperExamination.Industry);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得電子報滿意度問卷數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetEpaperExaminationCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetEpaperExaminationCount(conditions);
        }

        /// <summary>
        /// 取得電子報滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="epaperExaminationId">識別碼</param>
        /// <returns>電子報滿意度問卷</returns>
        public EpaperExamination GetEpaperExaminationByIdNoLazy(int epaperExaminationId)
        {
            EpaperExamination epaperExamination = FTISDao.GetEpaperExaminationById(epaperExaminationId);

            if (epaperExamination != null && epaperExamination.Industry != null)
            {
                NHibernateUtil.Initialize(epaperExamination.Industry);
            }

            return epaperExamination;
        }
        #endregion

        #region Post
        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="post">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        public Post CreatePost(Post post)
        {
            return FTISDao.CreatePost(post);
        }

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="post">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        public Post UpdatePost(Post post)
        {
            return FTISDao.UpdatePost(post);
        }

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="post">被刪除的Post</param>
        public void DeletePost(Post post)
        {
            FTISDao.DeletePost(post);
        }

        /// <summary>
        /// 取得Post By 識別碼
        /// </summary>
        /// <param name="postId">識別碼</param>
        /// <returns>Post</returns>
        public Post GetPostById(int postId)
        {
            return FTISDao.GetPostById(postId);
        }

        /// <summary>
        /// 取得Post清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Post清單</returns>
        public IList<Post> GetPostList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetPostList(conditions);
        }

        /// <summary>
        /// 取得Post清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Post清單</returns>
        public IList<Post> GetPostListNoLazy(IDictionary<string, string> conditions)
        {
            IList<Post> list = FTISDao.GetPostList(conditions);

            if (list != null && list.Count > 0)
            {
                foreach (Post post in list)
                {
                    if (post != null && post.Node != null)
                    {
                        NHibernateUtil.Initialize(post.Node);
                        post.Node.ParentNode = null;
                        //if (post.Node.ParentNode != null)
                        //{
                        //    NHibernateUtil.Initialize(post.Node.ParentNode);
                        //}
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 取得Post種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPostCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetPostCount(conditions);
        }

        /// <summary>
        /// 取得Post By 識別碼
        /// </summary>
        /// <param name="postId">識別碼</param>
        /// <returns>Post</returns>
        public Post GetPostByIdNoLazy(int postId)
        {
            Post post = FTISDao.GetPostById(postId);

            if (post != null && post.Node != null)
            {
                NHibernateUtil.Initialize(post.Node);
            }
            else
            {
                post.Node = new Node();
            }

            return post;
        }
        #endregion

        #region Report
        /// <summary>
        /// 新增報告書
        /// </summary>
        /// <param name="report">被新增的報告書</param>
        /// <returns>新增後的報告書</returns>
        public Report CreateReport(Report report)
        {
            return FTISDao.CreateReport(report);
        }

        /// <summary>
        /// 更新報告書
        /// </summary>
        /// <param name="report">被更新的報告書</param>
        /// <returns>更新後的報告書</returns>
        public Report UpdateReport(Report report)
        {
            return FTISDao.UpdateReport(report);
        }

        /// <summary>
        /// 刪除報告書
        /// </summary>
        /// <param name="report">被刪除的報告書</param>
        public void DeleteReport(Report report)
        {
            FTISDao.DeleteReport(report);
        }

        /// <summary>
        /// 取得報告書 By 識別碼
        /// </summary>
        /// <param name="reportId">識別碼</param>
        /// <returns>報告書</returns>
        public Report GetReportById(int reportId)
        {
            return FTISDao.GetReportById(reportId);
        }

        /// <summary>
        /// 取得報告書清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>報告書清單</returns>
        public IList<Report> GetReportList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetReportList(conditions);
        }

        /// <summary>
        /// 取得報告書數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetReportCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetReportCount(conditions);
        }
        #endregion

        #region CountVO
        /// <summary>
        /// 新增計數器
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public CountVO CreateCount(CountVO count)
        {
            return FTISDao.CreateCount(count);
        }

        /// <summary>
        /// 更新計數器
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public CountVO UpdateCount(CountVO count)
        {
            return FTISDao.UpdateCount(count);
        }

        /// <summary>
        /// 取得計數器清單
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IList<CountVO> GetCountList(IDictionary<string, string> conditions)
        {
            return FTISDao.GetCountList(conditions);
        }

        /// <summary>
        /// 取得合計總數
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetSumCountHits(IDictionary<string, string> conditions)
        {
            return FTISDao.GetSumCountHits(conditions);
        }

        /// <summary>
        /// 取得今日的計數器
        /// </summary>
        /// <param name="barId"></param>
        /// <returns></returns>
        public CountVO GetTodayCount(string barId)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("CountDate", DateTime.Today.ToString("yyyy/MM/dd"));
            conditions.Add("BarId", barId);
            IList<CountVO> list = FTISDao.GetCountList(conditions);

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #endregion
    }
}
