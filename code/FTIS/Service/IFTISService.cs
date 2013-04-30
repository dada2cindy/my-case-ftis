using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTIS.Domain;

namespace FTIS.Service
{
    public interface IFTISService
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
        /// <param name="newsClassId">識別碼</param>
        /// <returns>新聞分類</returns>
        NewsClass GetNewsClassById(int newsClassId);

        /// <summary>
        /// 取得新聞分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞分類清單</returns>
        IList<NewsClass> GetNewsClassList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得新聞分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
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
        /// 取得一個新的管理者帳號空殼
        /// </summary>
        /// <returns></returns>
        MasterMember MakeMasterMember();

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

        /// <summary>
        /// 取得管理者帳號清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>管理者帳號清單</returns>
        IList<MasterMember> GetMasterMemberListNoLazy(IDictionary<string, string> conditions);
        #endregion
    }
}
