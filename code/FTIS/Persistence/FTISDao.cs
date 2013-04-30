using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using FTIS.Domain;
using System.Collections;
using WuDada.Core.Generic.Extension;
using System.Globalization;
using NHibernate;

namespace FTIS.Persistence
{
    public class FTISDao : AdoDao, IFTISDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        #region IFTIDao 成員

        #region NewsClass
        public NewsClass CreateNewsClass(NewsClass newsClass)
        {
            NHibernateDao.Insert(newsClass);

            return newsClass;
        }

        public NewsClass UpdateNewsClass(NewsClass newsClass)
        {
            NHibernateDao.Update(newsClass);

            return newsClass;
        }

        public void DeleteNewsClass(NewsClass newsClass)
        {
            NHibernateDao.Delete(newsClass);
        }

        public NewsClass GetNewsClassById(int newsClassId)
        {
            return NHibernateDao.GetVOById<NewsClass>(newsClassId);
        }

        public IList<NewsClass> GetNewsClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select n from NewsClass n ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryNewsClass(param, fromScript, whereScript, conditions, true).OfType<NewsClass>().ToList();
        }

        public int GetNewsClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(n.NewsClassId) from NewsClass n ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryNewsClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryNewsClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendNewsClassName(conditions, whereScript, param);
            AppendNewsClassStatus(conditions, whereScript, param);            

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNewsClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);            
        }

        private string AppendNewsClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by n.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"].ToLower(CultureInfo.CurrentCulture);
            }

            return order;
        }        

        private void AppendNewsClassName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and n.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendNewsClassStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and n.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }
        #endregion

        #region News
        public News CreateNews(News news)
        {
            NHibernateDao.Insert(news);
            return news;
        }

        public News UpdateNews(News news)
        {
            NHibernateDao.Update(news);

            return news;
        }

        public void DeleteNews(News news)
        {
            NHibernateDao.Delete(news);
        }

        public News GetNewsById(int newsId)
        {
            return NHibernateDao.GetVOById<News>(newsId);
        }

        public IList<News> GetNewsList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select n from News n ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryNews(param, fromScript, whereScript, conditions, true).OfType<News>().ToList();
        }

        public int GetNewsCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(n.NewsId) from News n ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryNews(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryNews(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendNewsName(conditions, whereScript, param);
            AppendNewsStatus(conditions, whereScript, param);
            AppendNewsIsHome(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNewsOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendNewsIsHome(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("IsHome"))
            {
                whereScript.Append(" and n.IsHome = ? ");
                param.Add(conditions["IsHome"]);
            }
        }

        private string AppendNewsOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by n.SortId, n.PostDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"].ToLower(CultureInfo.CurrentCulture);
            }

            return order;
        }

        private void AppendNewsName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and n.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendNewsStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and n.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }
        #endregion

        #region AdminBar
        /// <summary>
        /// 取得全部後台功能清單
        /// </summary>
        /// <returns>後台功能清單</returns>
        public IList<AdminBar> GetAllAdminBar()
        {
            return NHibernateDao.GetAllVO<AdminBar>();
        }
        #endregion

        #region MasterMember
        /// <summary>
        /// 管理者帳號
        /// </summary>
        /// <param name="masterMember">被新增的管理者帳號</param>
        /// <returns>新增後的管理者帳號</returns>
        public MasterMember CreateMasterMember(MasterMember masterMember)
        {
            NHibernateDao.Insert(masterMember);

            return masterMember;
        }

        /// <summary>
        /// 更新管理者帳號
        /// </summary>
        /// <param name="masterMember">被更新的管理者帳號</param>
        /// <returns>更新後的管理者帳號</returns>
        public MasterMember UpdateMasterMember(MasterMember masterMember)
        {
            NHibernateDao.Update(masterMember);

            return masterMember;
        }

        /// <summary>
        /// 刪除管理者帳號
        /// </summary>
        /// <param name="masterMember">被刪除的管理者帳號</param>
        public void DeleteMasterMember(MasterMember masterMember)
        {
            NHibernateDao.Delete(masterMember);
        }

        /// <summary>
        /// 取得管理者帳號 By 識別碼
        /// </summary>
        /// <param name="masterMemberId">識別碼</param>
        /// <returns>管理者帳號</returns>
        public MasterMember GetMasterMemberById(int masterMemberId)
        {
            return NHibernateDao.GetVOById<MasterMember>(masterMemberId);
        }

        /// <summary>
        /// 取得管理者帳號清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>管理者帳號清單</returns>
        public IList<MasterMember> GetMasterMemberList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select m from MasterMember m ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryMasterMember(param, fromScript, whereScript, conditions, true).OfType<MasterMember>().ToList();
        }

        /// <summary>
        /// 取得管理者帳號數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMasterMemberCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(m.MasterMemberId) from MasterMember m ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryMasterMember(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryMasterMember(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendMasterMemberAccount(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendMasterMemberOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendMasterMemberOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by m.MasterMemberId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"].ToLower(CultureInfo.CurrentCulture);
            }

            return order;
        }

        private void AppendMasterMemberAccount(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Account"))
            {
                whereScript.Append(" and m.Account = ? ");
                param.Add(conditions["Account"]);
            }
        }
        #endregion

        #endregion
    }
}
