using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTIS.Domain.Impl;

namespace FTIS.Persistence
{
    public interface IFTISDao
    {
        #region NewsClass
        /// <summary>
        /// 新增新聞分類
        /// </summary>
        /// <param name="newsClass">被新增的新聞分類</param>
        /// <returns>新增後的新聞分類</returns>
        NewsClass CreateNewsClass(NewsClass newsClass);

        /// <summary>
        /// 更新新聞分類
        /// </summary>
        /// <param name="newsClass">被更新的新聞分類</param>
        /// <returns>更新後的新聞分類</returns>
        NewsClass UpdateNewsClass(NewsClass newsClass);

        /// <summary>
        /// 刪除新聞分類
        /// </summary>
        /// <param name="newsClass">被刪除的新聞分類</param>
        void DeleteNewsClass(NewsClass newsClass);

        /// <summary>
        /// 取得新聞分類 By 識別碼
        /// </summary>
        /// <param name="itemParamId">識別碼</param>
        /// <returns>新聞分類</returns>
        NewsClass GetNewsClassById(int newsClassId);

        /// <summary>
        /// 取得新聞分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞分類清單</returns>
        IList<NewsClass> GetNewsClassList(IDictionary<string, string> conditions);

        int GetNewsClassCount(IDictionary<string, string> conditions);
        #endregion

        #region News
        /// <summary>
        /// 新增新聞
        /// </summary>
        /// <param name="news">被新增的新聞</param>
        /// <returns>新增後的新聞</returns>
        News CreateNews(News news);

        /// <summary>
        /// 更新新聞
        /// </summary>
        /// <param name="newsClass">被更新的新聞</param>
        /// <returns>更新後的新聞</returns>
        News UpdateNews(News news);

        /// <summary>
        /// 刪除新聞
        /// </summary>
        /// <param name="news">被刪除的新聞</param>
        void DeleteNews(News news);

        /// <summary>
        /// 取得新聞 By 識別碼
        /// </summary>
        /// <param name="newsId">識別碼</param>
        /// <returns>新聞</returns>
        News GetNewsById(int newsId);

        /// <summary>
        /// 取得新聞清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞清單</returns>
        IList<News> GetNewsList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得新聞數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetNewsCount(IDictionary<string, string> conditions);
        #endregion

        #region AdminBar
        /// <summary>
        /// 取得全部後台功能清單
        /// </summary>
        /// <returns>後台功能清單</returns>
        IList<AdminBar> GetAllAdminBar();
        #endregion

        #region MasterMember
        /// <summary>
        /// 管理者帳號
        /// </summary>
        /// <param name="masterMember">被新增的管理者帳號</param>
        /// <returns>新增後的管理者帳號</returns>
        MasterMember CreateMasterMember(MasterMember masterMember);

        /// <summary>
        /// 更新管理者帳號
        /// </summary>
        /// <param name="masterMember">被更新的管理者帳號</param>
        /// <returns>更新後的管理者帳號</returns>
        MasterMember UpdateMasterMember(MasterMember masterMember);

        /// <summary>
        /// 刪除管理者帳號
        /// </summary>
        /// <param name="masterMember">被刪除的管理者帳號</param>
        void DeleteMasterMember(MasterMember masterMember);

        /// <summary>
        /// 取得管理者帳號 By 識別碼
        /// </summary>
        /// <param name="masterMemberId">識別碼</param>
        /// <returns>管理者帳號</returns>
        MasterMember GetMasterMemberById(int masterMemberId);

        /// <summary>
        /// 取得管理者帳號清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>管理者帳號清單</returns>
        IList<MasterMember> GetMasterMemberList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得管理者帳號數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetMasterMemberCount(IDictionary<string, string> conditions);
        #endregion

        #region NewsType
        /// <summary>
        /// 新增新聞種類
        /// </summary>
        /// <param name="newsType">被新增的新聞種類</param>
        /// <returns>新增後的新聞種類</returns>
        NewsType CreateNewsType(NewsType newsType);

        /// <summary>
        /// 更新新聞種類
        /// </summary>
        /// <param name="newsClass">被更新的新聞種類</param>
        /// <returns>更新後的新聞種類</returns>
        NewsType UpdateNewsType(NewsType newsType);

        /// <summary>
        /// 刪除新聞種類
        /// </summary>
        /// <param name="newsType">被刪除的新聞種類</param>
        void DeleteNewsType(NewsType newsType);

        /// <summary>
        /// 取得新聞種類 By 識別碼
        /// </summary>
        /// <param name="newsTypeId">識別碼</param>
        /// <returns>新聞種類</returns>
        NewsType GetNewsTypeById(int newsTypeId);

        /// <summary>
        /// 取得新聞種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞種類清單</returns>
        IList<NewsType> GetNewsTypeList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得新聞種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetNewsTypeCount(IDictionary<string, string> conditions);
        #endregion

        #region MasterLog
        /// <summary>
        /// 新增後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被新增的後台帳號登入紀錄</param>
        /// <returns>新增後的後台帳號登入紀錄</returns>
        MasterLog CreateMasterLog(MasterLog masterLog);

        /// <summary>
        /// 更新後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被更新的後台帳號登入紀錄</param>
        /// <returns>更新後的後台帳號登入紀錄</returns>
        MasterLog UpdateMasterLog(MasterLog masterLog);

        /// <summary>
        /// 刪除後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被刪除的後台帳號登入紀錄</param>
        void DeleteMasterLog(MasterLog masterLog);

        /// <summary>
        /// 取得後台帳號登入紀錄 By 識別碼
        /// </summary>
        /// <param name="masterLogId">識別碼</param>
        /// <returns>後台帳號登入紀錄</returns>
        MasterLog GetMasterLogById(int masterLogId);

        /// <summary>
        /// 取得後台帳號登入紀錄清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>後台帳號登入紀錄清單</returns>
        IList<MasterLog> GetMasterLogList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得後台帳號登入紀錄數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetMasterLogCount(IDictionary<string, string> conditions);
        #endregion

        #region Node
        /// <summary>
        /// 新增種類
        /// </summary>
        /// <param name="node">被新增的種類</param>
        /// <returns>新增後的種類</returns>
        Node CreateNode(Node node);

        /// <summary>
        /// 更新種類
        /// </summary>
        /// <param name="node">被更新的種類</param>
        /// <returns>更新後的種類</returns>
        Node UpdateNode(Node node);

        /// <summary>
        /// 刪除種類
        /// </summary>
        /// <param name="node">被刪除的種類</param>
        void DeleteNode(Node node);

        /// <summary>
        /// 取得種類 By 識別碼
        /// </summary>
        /// <param name="nodeId">識別碼</param>
        /// <returns>種類</returns>
        Node GetNodeById(int nodeId);

        /// <summary>
        /// 取得種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>種類清單</returns>
        IList<Node> GetNodeList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得新聞種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetNodeCount(IDictionary<string, string> conditions);
        #endregion

        #region Activity
        /// <summary>
        /// 新增活動訊息
        /// </summary>
        /// <param name="activity">被新增的活動訊息</param>
        /// <returns>新增後的活動訊息</returns>
        Activity CreateActivity(Activity activity);

        /// <summary>
        /// 更新活動訊息
        /// </summary>
        /// <param name="activity">被更新的活動訊息</param>
        /// <returns>更新後的活動訊息</returns>
        Activity UpdateActivity(Activity activity);

        /// <summary>
        /// 刪除活動訊息
        /// </summary>
        /// <param name="activity">被刪除的活動訊息</param>
        void DeleteActivity(Activity activity);

        /// <summary>
        /// 取得活動訊息 By 識別碼
        /// </summary>
        /// <param name="activityId">識別碼</param>
        /// <returns>活動訊息</returns>
        Activity GetActivityById(int activityId);

        /// <summary>
        /// 取得活動訊息清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>活動訊息清單</returns>
        IList<Activity> GetActivityList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得活動訊息數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetActivityCount(IDictionary<string, string> conditions);
        #endregion

        #region HomeNews
        /// <summary>
        /// 新增即時訊息
        /// </summary>
        /// <param name="homeNews">被新增的即時訊息</param>
        /// <returns>新增後的即時訊息</returns>
        HomeNews CreateHomeNews(HomeNews homeNews);

        /// <summary>
        /// 更新即時訊息
        /// </summary>
        /// <param name="homeNews">被更新的即時訊息</param>
        /// <returns>更新後的即時訊息</returns>
        HomeNews UpdateHomeNews(HomeNews homeNews);

        /// <summary>
        /// 刪除即時訊息
        /// </summary>
        /// <param name="homeNews">被刪除的即時訊息</param>
        void DeleteHomeNews(HomeNews homeNews);

        /// <summary>
        /// 取得即時訊息 By 識別碼
        /// </summary>
        /// <param name="homeNewsId">識別碼</param>
        /// <returns>即時訊息</returns>
        HomeNews GetHomeNewsById(int homeNewsId);

        /// <summary>
        /// 取得即時訊息清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>即時訊息清單</returns>
        IList<HomeNews> GetHomeNewsList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得即時訊息數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetHomeNewsCount(IDictionary<string, string> conditions);
        #endregion

        #region LinksClass
        /// <summary>
        /// 新增網路資源分類
        /// </summary>
        /// <param name="linksClass">被新增的網路資源分類</param>
        /// <returns>新增後的網路資源分類</returns>
        LinksClass CreateLinksClass(LinksClass linksClass);

        /// <summary>
        /// 更新網路資源分類
        /// </summary>
        /// <param name="linksClass">被更新的網路資源分類</param>
        /// <returns>更新後的網路資源分類</returns>
        LinksClass UpdateLinksClass(LinksClass linksClass);

        /// <summary>
        /// 刪除網路資源分類
        /// </summary>
        /// <param name="linksClass">被刪除的網路資源分類</param>
        void DeleteLinksClass(LinksClass linksClass);

        /// <summary>
        /// 取得網路資源分類 By 識別碼
        /// </summary>
        /// <param name="linksClassId">識別碼</param>
        /// <returns>網路資源分類</returns>
        LinksClass GetLinksClassById(int linksClassId);

        /// <summary>
        /// 取得網路資源分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源分類清單</returns>
        IList<LinksClass> GetLinksClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得網路資源分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetLinksClassCount(IDictionary<string, string> conditions);
        #endregion

        #region Links
        /// <summary>
        /// 網路資源
        /// </summary>
        /// <param name="links">被新增的網路資源</param>
        /// <returns>新增後的網路資源</returns>
        Links CreateLinks(Links links);

        /// <summary>
        /// 更新網路資源
        /// </summary>
        /// <param name="links">被更新的網路資源</param>
        /// <returns>更新後的網路資源</returns>
        Links UpdateLinks(Links links);

        /// <summary>
        /// 刪除網路資源
        /// </summary>
        /// <param name="links">被刪除的網路資源</param>
        void DeleteLinks(Links links);

        /// <summary>
        /// 取得網路資源 By 識別碼
        /// </summary>
        /// <param name="linksId">識別碼</param>
        /// <returns>網路資源</returns>
        Links GetLinksById(int linksId);

        /// <summary>
        /// 取得網路資源清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源清單</returns>
        IList<Links> GetLinksList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得網路資源數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetLinksCount(IDictionary<string, string> conditions);
        #endregion

        #region GreenFactoryClass
        /// <summary>
        /// 新增綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被新增的綠色工廠分類</param>
        /// <returns>新增後的綠色工廠分類</returns>
        GreenFactoryClass CreateGreenFactoryClass(GreenFactoryClass greenFactoryClass);

        /// <summary>
        /// 更新綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被更新的綠色工廠分類</param>
        /// <returns>更新後的綠色工廠分類</returns>
        GreenFactoryClass UpdateGreenFactoryClass(GreenFactoryClass greenFactoryClass);

        /// <summary>
        /// 刪除綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被刪除的綠色工廠分類</param>
        void DeleteGreenFactoryClass(GreenFactoryClass greenFactoryClass);

        /// <summary>
        /// 取得綠色工廠分類 By 識別碼
        /// </summary>
        /// <param name="greenFactoryClassId">識別碼</param>
        /// <returns>綠色工廠分類</returns>
        GreenFactoryClass GetGreenFactoryClassById(int greenFactoryClassId);

        /// <summary>
        /// 取得綠色工廠分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠分類清單</returns>
        IList<GreenFactoryClass> GetGreenFactoryClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得綠色工廠分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetGreenFactoryClassCount(IDictionary<string, string> conditions);
        #endregion

        #region GreenFactory
        /// <summary>
        /// 綠色工廠
        /// </summary>
        /// <param name="greenFactory">被新增的綠色工廠</param>
        /// <returns>新增後的綠色工廠</returns>
        GreenFactory CreateGreenFactory(GreenFactory greenFactory);

        /// <summary>
        /// 更新綠色工廠
        /// </summary>
        /// <param name="greenFactory">被更新的綠色工廠</param>
        /// <returns>更新後的綠色工廠</returns>
        GreenFactory UpdateGreenFactory(GreenFactory greenFactory);

        /// <summary>
        /// 刪除綠色工廠
        /// </summary>
        /// <param name="greenFactory">被刪除的綠色工廠</param>
        void DeleteGreenFactory(GreenFactory greenFactory);

        /// <summary>
        /// 取得綠色工廠 By 識別碼
        /// </summary>
        /// <param name="greenFactoryId">識別碼</param>
        /// <returns>綠色工廠</returns>
        GreenFactory GetGreenFactoryById(int greenFactoryId);

        /// <summary>
        /// 取得綠色工廠清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠清單</returns>
        IList<GreenFactory> GetGreenFactoryList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得綠色工廠數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetGreenFactoryCount(IDictionary<string, string> conditions);
        #endregion

        #region ApplicationClass
        /// <summary>
        /// 新增企業社會責任分類
        /// </summary>
        /// <param name="applicationClass">被新增的企業社會責任分類</param>
        /// <returns>新增後的企業社會責任分類</returns>
        ApplicationClass CreateApplicationClass(ApplicationClass applicationClass);

        /// <summary>
        /// 更新社會責任分類
        /// </summary>
        /// <param name="applicationClass">被更新的社會責任分類</param>
        /// <returns>更新後的社會責任分類</returns>
        ApplicationClass UpdateApplicationClass(ApplicationClass applicationClass);

        /// <summary>
        /// 刪除社會責任分類
        /// </summary>
        /// <param name="applicationClass">被刪除的社會責任分類</param>
        void DeleteApplicationClass(ApplicationClass applicationClass);

        /// <summary>
        /// 取得社會責任分類 By 識別碼
        /// </summary>
        /// <param name="applicationClassId">識別碼</param>
        /// <returns>社會責任分類</returns>
        ApplicationClass GetApplicationClassById(int applicationClassId);

        /// <summary>
        /// 取得社會責任分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>社會責任分類清單</returns>
        IList<ApplicationClass> GetApplicationClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得社會責任分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetApplicationClassCount(IDictionary<string, string> conditions);
        #endregion

        #region Application
        /// <summary>
        /// 企業社會責任
        /// </summary>
        /// <param name="application">被新增的企業社會責任</param>
        /// <returns>新增後的企業社會責任</returns>
        Application CreateApplication(Application application);

        /// <summary>
        /// 更新企業社會責任
        /// </summary>
        /// <param name="application">被更新的企業社會責任</param>
        /// <returns>更新後的企業社會責任</returns>
        Application UpdateApplication(Application application);

        /// <summary>
        /// 刪除企業社會責任
        /// </summary>
        /// <param name="application">被刪除的企業社會責任</param>
        void DeleteApplication(Application application);

        /// <summary>
        /// 取得企業社會責任 By 識別碼
        /// </summary>
        /// <param name="applicationId">識別碼</param>
        /// <returns>企業社會責任</returns>
        Application GetApplicationById(int applicationId);

        /// <summary>
        /// 取得企業社會責任清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>企業社會責任清單</returns>
        IList<Application> GetApplicationList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得企業社會責任數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetApplicationCount(IDictionary<string, string> conditions);
        #endregion

        #region PublicationClass
        /// <summary>
        /// 新增期刊分類
        /// </summary>
        /// <param name="publicationClass">被新增的期刊分類</param>
        /// <returns>新增後的期刊分類</returns>
        PublicationClass CreatePublicationClass(PublicationClass publicationClass);

        /// <summary>
        /// 更新期刊分類
        /// </summary>
        /// <param name="publicationClass">被更新的期刊分類</param>
        /// <returns>更新後的期刊分類</returns>
        PublicationClass UpdatePublicationClass(PublicationClass publicationClass);

        /// <summary>
        /// 刪除期刊分類
        /// </summary>
        /// <param name="publicationClass">被刪除的期刊分類</param>
        void DeletePublicationClass(PublicationClass publicationClass);

        /// <summary>
        /// 取得期刊分類 By 識別碼
        /// </summary>
        /// <param name="publicationClassId">識別碼</param>
        /// <returns>期刊分類</returns>
        PublicationClass GetPublicationClassById(int publicationClassId);

        /// <summary>
        /// 取得期刊分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊分類清單</returns>
        IList<PublicationClass> GetPublicationClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得期刊分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetPublicationClassCount(IDictionary<string, string> conditions);
        #endregion

        #region Publication
        /// <summary>
        /// 期刊
        /// </summary>
        /// <param name="publication">被新增的期刊</param>
        /// <returns>新增後的期刊</returns>
        Publication CreatePublication(Publication publication);

        /// <summary>
        /// 更新期刊
        /// </summary>
        /// <param name="publication">被更新的期刊</param>
        /// <returns>更新後的期刊</returns>
        Publication UpdatePublication(Publication publication);

        /// <summary>
        /// 刪除期刊
        /// </summary>
        /// <param name="publication">被刪除的期刊</param>
        void DeletePublication(Publication publication);

        /// <summary>
        /// 取得期刊 By 識別碼
        /// </summary>
        /// <param name="publicationId">識別碼</param>
        /// <returns>期刊</returns>
        Publication GetPublicationById(int publicationId);

        /// <summary>
        /// 取得期刊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊清單</returns>
        IList<Publication> GetPublicationList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得期刊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetPublicationCount(IDictionary<string, string> conditions);
        #endregion

        #region QuestionClass
        /// <summary>
        /// 新增Q&A分類
        /// </summary>
        /// <param name="questionClass">被新增的Q&A分類</param>
        /// <returns>新增後的Q&A分類</returns>
        QuestionClass CreateQuestionClass(QuestionClass questionClass);

        /// <summary>
        /// 更新Q&A分類
        /// </summary>
        /// <param name="questionClass">被更新的Q&A分類</param>
        /// <returns>更新後的Q&A分類</returns>
        QuestionClass UpdateQuestionClass(QuestionClass questionClass);

        /// <summary>
        /// 刪除Q&A分類
        /// </summary>
        /// <param name="questionClass">被刪除的Q&A分類</param>
        void DeleteQuestionClass(QuestionClass questionClass);

        /// <summary>
        /// 取得Q&A分類 By 識別碼
        /// </summary>
        /// <param name="questionClassId">識別碼</param>
        /// <returns>Q&A分類</returns>
        QuestionClass GetQuestionClassById(int questionClassId);

        /// <summary>
        /// 取得Q&A分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A分類清單</returns>
        IList<QuestionClass> GetQuestionClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得Q&A分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetQuestionClassCount(IDictionary<string, string> conditions);
        #endregion

        #region Question
        /// <summary>
        /// Q&A
        /// </summary>
        /// <param name="question">被新增的Q&A</param>
        /// <returns>新增後的Q&A</returns>
        Question CreateQuestion(Question question);

        /// <summary>
        /// 更新Q&A
        /// </summary>
        /// <param name="question">被更新的Q&A</param>
        /// <returns>更新後的Q&A</returns>
        Question UpdateQuestion(Question question);

        /// <summary>
        /// 刪除Q&A
        /// </summary>
        /// <param name="question">被刪除的Q&A</param>
        void DeleteQuestion(Question question);

        /// <summary>
        /// 取得Q&A By 識別碼
        /// </summary>
        /// <param name="questionId">識別碼</param>
        /// <returns>Q&A</returns>
        Question GetQuestionById(int questionId);

        /// <summary>
        /// 取得Q&A清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A清單</returns>
        IList<Question> GetQuestionList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得Q&A數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetQuestionCount(IDictionary<string, string> conditions);
        #endregion

        #region Brief
        /// <summary>
        /// 新增永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被新增的永續產業發展簡訊</param>
        /// <returns>新增後的永續產業發展簡訊</returns>
        Brief CreateBrief(Brief brief);

        /// <summary>
        /// 更新永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被更新的永續產業發展簡訊</param>
        /// <returns>更新後的永續產業發展簡訊</returns>
        Brief UpdateBrief(Brief brief);

        /// <summary>
        /// 刪除永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被刪除的永續產業發展簡訊</param>
        void DeleteBrief(Brief brief);

        /// <summary>
        /// 取得永續產業發展簡訊 By 識別碼
        /// </summary>
        /// <param name="briefId">識別碼</param>
        /// <returns>永續產業發展簡訊</returns>
        Brief GetBriefById(int briefId);

        /// <summary>
        /// 取得永續產業發展簡訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>永續產業發展簡訊清單</returns>
        IList<Brief> GetBriefList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得永續產業發展簡訊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetBriefCount(IDictionary<string, string> conditions);
        #endregion

        #region Curriculum
        /// <summary>
        /// 新增課程講義
        /// </summary>
        /// <param name="curriculum">被新增的課程講義</param>
        /// <returns>新增後的課程講義</returns>
        Curriculum CreateCurriculum(Curriculum curriculum);

        /// <summary>
        /// 更新課程講義
        /// </summary>
        /// <param name="curriculum">被更新的課程講義</param>
        /// <returns>更新後的課程講義</returns>
        Curriculum UpdateCurriculum(Curriculum curriculum);

        /// <summary>
        /// 刪除課程講義
        /// </summary>
        /// <param name="curriculum">被刪除的課程講義</param>
        void DeleteCurriculum(Curriculum curriculum);

        /// <summary>
        /// 取得課程講義 By 識別碼
        /// </summary>
        /// <param name="curriculumId">識別碼</param>
        /// <returns>課程講義</returns>
        Curriculum GetCurriculumById(int curriculumId);

        /// <summary>
        /// 取得課程講義清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>課程講義清單</returns>
        IList<Curriculum> GetCurriculumList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得課程講義數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetCurriculumCount(IDictionary<string, string> conditions);
        #endregion

        #region Download
        /// <summary>
        /// 技術工具/文件下載
        /// </summary>
        /// <param name="download">被新增的技術工具/文件下載</param>
        /// <returns>新增後的技術工具/文件下載</returns>
        Download CreateDownload(Download download);

        /// <summary>
        /// 更新技術工具/文件下載
        /// </summary>
        /// <param name="download">被更新的技術工具/文件下載</param>
        /// <returns>更新後的技術工具/文件下載</returns>
        Download UpdateDownload(Download download);

        /// <summary>
        /// 刪除技術工具/文件下載
        /// </summary>
        /// <param name="download">被刪除的技術工具/文件下載</param>
        void DeleteDownload(Download download);

        /// <summary>
        /// 取得技術工具/文件下載 By 識別碼
        /// </summary>
        /// <param name="downloadId">識別碼</param>
        /// <returns>技術工具/文件下載</returns>
        Download GetDownloadById(int downloadId);

        /// <summary>
        /// 取得技術工具/文件下載清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>技術工具/文件下載清單</returns>
        IList<Download> GetDownloadList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得技術工具/文件下載數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetDownloadCount(IDictionary<string, string> conditions);
        #endregion

        #region Industry
        /// <summary>
        /// 行業分類
        /// </summary>
        /// <param name="industry">被新增的行業分類</param>
        /// <returns>新增後的行業分類</returns>
        Industry CreateIndustry(Industry industry);

        /// <summary>
        /// 更新行業分類
        /// </summary>
        /// <param name="industry">被更新的行業分類</param>
        /// <returns>更新後的行業分類</returns>
        Industry UpdateIndustry(Industry industry);

        /// <summary>
        /// 刪除行業分類
        /// </summary>
        /// <param name="industry">被刪除的行業分類</param>
        void DeleteIndustry(Industry industry);

        /// <summary>
        /// 取得行業分類 By 識別碼
        /// </summary>
        /// <param name="industryId">識別碼</param>
        /// <returns>行業分類</returns>
        Industry GetIndustryById(int industryId);

        /// <summary>
        /// 取得行業分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>行業分類清單</returns>
        IList<Industry> GetIndustryList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得行業分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetIndustryCount(IDictionary<string, string> conditions);
        #endregion

        #region Member
        /// <summary>
        /// 會員
        /// </summary>
        /// <param name="member">被新增的會員</param>
        /// <returns>新增後的會員</returns>
        Member CreateMember(Member member);

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="member">被更新的會員</param>
        /// <returns>更新後的會員</returns>
        Member UpdateMember(Member member);

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="member">被刪除的會員</param>
        void DeleteMember(Member member);

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        Member GetMemberById(int memberId);

        /// <summary>
        /// 取得會員清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員清單</returns>
        IList<Member> GetMemberList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得會員數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetMemberCount(IDictionary<string, string> conditions);
        #endregion

        #region DownloadRecord
        /// <summary>
        /// 會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被新增的會員下載紀錄</param>
        /// <returns>新增後的會員下載紀錄</returns>
        DownloadRecord CreateDownloadRecord(DownloadRecord downloadRecord);

        /// <summary>
        /// 更新會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被更新的會員下載紀錄</param>
        /// <returns>更新後的會員下載紀錄</returns>
        DownloadRecord UpdateDownloadRecord(DownloadRecord downloadRecord);

        /// <summary>
        /// 刪除會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被刪除的會員下載紀錄</param>
        void DeleteDownloadRecord(DownloadRecord downloadRecord);

        /// <summary>
        /// 取得會員下載紀錄 By 識別碼
        /// </summary>
        /// <param name="downloadRecordId">識別碼</param>
        /// <returns>會員下載紀錄</returns>
        DownloadRecord GetDownloadRecordById(int downloadRecordId);

        /// <summary>
        /// 取得會員下載紀錄清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員下載紀錄清單</returns>
        IList<DownloadRecord> GetDownloadRecordList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得會員下載紀錄數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetDownloadRecordCount(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得會員下載紀錄統計表清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員下載紀錄統計表清單</returns>
        IList<DownloadRecord> GetDownloadRecordStatistics(IDictionary<string, string> conditions);

        #endregion

        #region Epaper
        /// <summary>
        /// 新增電子報
        /// </summary>
        /// <param name="epaper">被新增的電子報</param>
        /// <returns>新增後的電子報</returns>
        Epaper CreateEpaper(Epaper epaper);

        /// <summary>
        /// 更新電子報
        /// </summary>
        /// <param name="epaper">被更新的電子報</param>
        /// <returns>更新後的電子報</returns>
        Epaper UpdateEpaper(Epaper epaper);

        /// <summary>
        /// 刪除電子報
        /// </summary>
        /// <param name="epaper">被刪除的電子報</param>
        void DeleteEpaper(Epaper epaper);

        /// <summary>
        /// 取得電子報 By 識別碼
        /// </summary>
        /// <param name="epaperId">識別碼</param>
        /// <returns>電子報</returns>
        Epaper GetEpaperById(int epaperId);

        /// <summary>
        /// 取得電子報清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報清單</returns>
        IList<Epaper> GetEpaperList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得電子報數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetEpaperCount(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得電子報年份清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報年份清單</returns>
        IList<string> GetEpaperYearList(IDictionary<string, string> conditions);
        #endregion

        #region EpaperEmail
        /// <summary>
        /// 電子報訂閱
        /// </summary>
        /// <param name="epaperEmail">被新增的電子報訂閱</param>
        /// <returns>新增後的電子報訂閱</returns>
        EpaperEmail CreateEpaperEmail(EpaperEmail epaperEmail);

        /// <summary>
        /// 更新電子報訂閱
        /// </summary>
        /// <param name="epaperEmail">被更新的電子報訂閱</param>
        /// <returns>更新後的電子報訂閱</returns>
        EpaperEmail UpdateEpaperEmail(EpaperEmail epaperEmail);

        /// <summary>
        /// 刪除電子報訂閱
        /// </summary>
        /// <param name="member">被刪除的電子報訂閱</param>
        void DeleteEpaperEmail(EpaperEmail epaperEmail);

        /// <summary>
        /// 取得電子報訂閱 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>電子報訂閱</returns>
        EpaperEmail GetEpaperEmailById(int epaperEmail);

        /// <summary>
        /// 取得電子報訂閱清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報訂閱清單</returns>
        IList<EpaperEmail> GetEpaperEmailList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得電子報訂閱數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetEpaperEmailCount(IDictionary<string, string> conditions);
        #endregion

        #region OtherExamination
        /// <summary>
        /// 新增問卷調查
        /// </summary>
        /// <param name="otherExamination">被新增的問卷調查</param>
        /// <returns>新增後的問卷調查</returns>
        OtherExamination CreateOtherExamination(OtherExamination otherExamination);

        /// <summary>
        /// 更新問卷調查
        /// </summary>
        /// <param name="otherExamination">被更新的問卷調查</param>
        /// <returns>更新後的問卷調查</returns>
        OtherExamination UpdateOtherExamination(OtherExamination otherExamination);

        /// <summary>
        /// 刪除問卷調查
        /// </summary>
        /// <param name="otherExamination">被刪除的問卷調查</param>
        void DeleteOtherExamination(OtherExamination otherExamination);

        /// <summary>
        /// 取得問卷調查 By 識別碼
        /// </summary>
        /// <param name="otherExaminationId">識別碼</param>
        /// <returns>問卷調查</returns>
        OtherExamination GetOtherExaminationById(int otherExaminationId);

        /// <summary>
        /// 取得問卷調查清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>問卷調查清單</returns>
        IList<OtherExamination> GetOtherExaminationList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得問卷調查數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetOtherExaminationCount(IDictionary<string, string> conditions);
        #endregion

        #region MNorm
        /// <summary>
        /// 新增國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被新增的國際組織標準/系統動態</param>
        /// <returns>新增後的國際組織標準/系統動態</returns>
        MNorm CreateMNorm(MNorm mNorm);

        /// <summary>
        /// 更新國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被更新的國際組織標準/系統動態</param>
        /// <returns>更新後的國際組織標準/系統動態</returns>
        MNorm UpdateMNorm(MNorm mNorm);

        /// <summary>
        /// 刪除國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被刪除的國際組織標準/系統動態</param>
        void DeleteMNorm(MNorm mNorm);

        /// <summary>
        /// 取得國際組織標準/系統動態 By 識別碼
        /// </summary>
        /// <param name="mNormId">識別碼</param>
        /// <returns>國際組織標準/系統動態</returns>
        MNorm GetMNormById(int mNormId);

        /// <summary>
        /// 取得國際組織標準/系統動態清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>國際組織標準/系統動態清單</returns>
        IList<MNorm> GetMNormList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得國際組織標準/系統動態數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetMNormCount(IDictionary<string, string> conditions);
        #endregion

        #region NormClass
        /// <summary>
        /// 標準/規範分類
        /// </summary>
        /// <param name="normClass">被新增的標準/規範分類</param>
        /// <returns>新增後的標準/規範分類</returns>
        NormClass CreateNormClass(NormClass normClass);

        /// <summary>
        /// 更新標準/規範分類
        /// </summary>
        /// <param name="normClass">被更新的標準/規範分類</param>
        /// <returns>更新後的標準/規範分類</returns>
        NormClass UpdateNormClass(NormClass normClass);

        /// <summary>
        /// 刪除標準/規範分類
        /// </summary>
        /// <param name="normClass">被刪除的標準/規範分類</param>
        void DeleteNormClass(NormClass normClass);

        /// <summary>
        /// 取得標準/規範分類 By 識別碼
        /// </summary>
        /// <param name="normClassId">識別碼</param>
        /// <returns>標準/規範分類</returns>
        NormClass GetNormClassById(int normClassId);

        /// <summary>
        /// 取得標準/規範分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範分類清單</returns>
        IList<NormClass> GetNormClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得標準/規範分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetNormClassCount(IDictionary<string, string> conditions);
        #endregion

        #region Norm
        /// <summary>
        /// 新增標準/規範資訊
        /// </summary>
        /// <param name="norm">被新增的標準/規範資訊</param>
        /// <returns>新增後的標準/規範資訊</returns>
        Norm CreateNorm(Norm norm);

        /// <summary>
        /// 更新標準/規範資訊
        /// </summary>
        /// <param name="norm">被更新的標準/規範資訊</param>
        /// <returns>更新後的標準/規範資訊</returns>
        Norm UpdateNorm(Norm norm);

        /// <summary>
        /// 刪除標準/規範資訊
        /// </summary>
        /// <param name="norm">被刪除的標準/規範資訊</param>
        void DeleteNorm(Norm norm);

        /// <summary>
        /// 取得標準/規範資訊 By 識別碼
        /// </summary>
        /// <param name="normId">識別碼</param>
        /// <returns>標準/規範資訊</returns>
        Norm GetNormById(int normId);

        /// <summary>
        /// 取得標準/規範資訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範資訊清單</returns>
        IList<Norm> GetNormList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得標準/規範資訊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetNormCount(IDictionary<string, string> conditions);
        #endregion

        #region Examination
        /// <summary>
        /// 新增網站滿意度問卷
        /// </summary>
        /// <param name="examination">被新增的網站滿意度問卷</param>
        /// <returns>新增後的網站滿意度問卷</returns>
        Examination CreateExamination(Examination examination);

        /// <summary>
        /// 更新網站滿意度問卷
        /// </summary>
        /// <param name="examination">被更新的網站滿意度問卷</param>
        /// <returns>更新後的網站滿意度問卷</returns>
        Examination UpdateExamination(Examination examination);

        /// <summary>
        /// 刪除網站滿意度問卷
        /// </summary>
        /// <param name="examination">被刪除的網站滿意度問卷</param>
        void DeleteExamination(Examination examination);

        /// <summary>
        /// 取得網站滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="examinationId">識別碼</param>
        /// <returns>網站滿意度問卷</returns>
        Examination GetExaminationById(int examinationId);

        /// <summary>
        /// 取得網站滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網站滿意度問卷清單</returns>
        IList<Examination> GetExaminationList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得網站滿意度問卷數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetExaminationCount(IDictionary<string, string> conditions);
        #endregion

        #region EpaperExamination
        /// <summary>
        /// 新增電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被新增的電子報滿意度問卷</param>
        /// <returns>新增後的電子報滿意度問卷</returns>
        EpaperExamination CreateEpaperExamination(EpaperExamination epaperExamination);

        /// <summary>
        /// 更新電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被更新的電子報滿意度問卷</param>
        /// <returns>更新後的電子報滿意度問卷</returns>
        EpaperExamination UpdateEpaperExamination(EpaperExamination epaperExamination);

        /// <summary>
        /// 刪除電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被刪除的電子報滿意度問卷</param>
        void DeleteEpaperExamination(EpaperExamination epaperExamination);

        /// <summary>
        /// 取得電子報滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="epaperExaminationId">識別碼</param>
        /// <returns>電子報滿意度問卷</returns>
        EpaperExamination GetEpaperExaminationById(int epaperExaminationId);

        /// <summary>
        /// 取得電子報滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報滿意度問卷清單</returns>
        IList<EpaperExamination> GetEpaperExaminationList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得電子報滿意度問卷數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetEpaperExaminationCount(IDictionary<string, string> conditions);
        #endregion

        #region Post

        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="post">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        Post CreatePost(Post post);

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="post">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        Post UpdatePost(Post post);

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="post">被刪除的Post</param>
        void DeletePost(Post post);

        /// <summary>
        /// 取得Post By 識別碼
        /// </summary>
        /// <param name="postId">識別碼</param>
        /// <returns>Post</returns>
        Post GetPostById(int postId);

        /// <summary>
        /// 取得Post清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Post清單</returns>
        IList<Post> GetPostList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得Post種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetPostCount(IDictionary<string, string> conditions);

        #endregion        

        #region Report
        /// <summary>
        /// 新增報告書
        /// </summary>
        /// <param name="report">被新增的報告書</param>
        /// <returns>新增後的報告書</returns>
        Report CreateReport(Report report);

        /// <summary>
        /// 更新報告書
        /// </summary>
        /// <param name="report">被更新的報告書</param>
        /// <returns>更新後的報告書</returns>
        Report UpdateReport(Report report);

        /// <summary>
        /// 刪除報告書
        /// </summary>
        /// <param name="report">被刪除的報告書</param>
        void DeleteReport(Report report);

        /// <summary>
        /// 取得報告書 By 識別碼
        /// </summary>
        /// <param name="reportId">識別碼</param>
        /// <returns>報告書</returns>
        Report GetReportById(int reportId);

        /// <summary>
        /// 取得報告書清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>報告書清單</returns>
        IList<Report> GetReportList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得報告書數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetReportCount(IDictionary<string, string> conditions);

        #endregion
    }
}
