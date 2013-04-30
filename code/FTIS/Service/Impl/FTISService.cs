using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTIS.Domain;
using FTIS.Persistence;
using NHibernate;

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
        /// 取得新聞分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNewsCount(IDictionary<string, string> conditions)
        {
            return FTISDao.GetNewsCount(conditions);
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
                        NHibernateUtil.Initialize(role.AdminValue);
                    }
                }
            }            
            return list;
        }
        #endregion

        #endregion
    }
}
