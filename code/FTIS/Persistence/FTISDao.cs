using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using FTIS.Domain.Impl;
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
            AppendNewsClassKeyWord(conditions, whereScript, param);
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
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendNewsClassKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and n.Name like ? or n.NameENG like ? ");
                param.Add("%" + conditions["Name"] + "%");
                param.Add("%" + conditions["Name"] + "%");
            }

            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (n.Name like ? or n.NameENG like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
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
            AppendNewsType(conditions, whereScript, param);
            AppendNewsClass(conditions, whereScript, param);
            AppendNewsKeyWord(conditions, whereScript, param);
            AppendNewsStatus(conditions, whereScript, param);
            AppendNewsIsHome(conditions, whereScript, param);
            AppendNewsTags(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNewsOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendNewsType(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("NewsTypeId"))
            {
                whereScript.Append(" and n.NewsType.NewsTypeId = ? ");
                param.Add(conditions["NewsTypeId"]);
            }
        }

        private void AppendNewsClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("NewsClassId"))
            {
                whereScript.Append(" and n.NewsClass.NewsClassId = ? ");
                param.Add(conditions["NewsClassId"]);
            }
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
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendNewsKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (n.Name like ? or n.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendNewsTags(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Tags"))
            {
                string[] tags = conditions["Tags"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder hql = new StringBuilder();
                for (int i = 0; i < tags.Count(); i++)
                {
                    if (i == 0)
                    {
                        hql.Append(" n.Tag like ? ");
                        param.Add("%" + tags[i] + "%");
                    }
                    else
                    {
                        hql.Append(" or n.Tag like ? ");
                        param.Add("%" + tags[i] + "%");
                    }
                }

                if (!string.IsNullOrEmpty(hql.ToString()))
                {
                    whereScript.AppendFormat(" and ({0}) ", hql.ToString());
                }
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
                order = conditions["Order"];
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

        #region NewsType
        /// <summary>
        /// 新增新聞種類
        /// </summary>
        /// <param name="newsType">被新增的新聞種類</param>
        /// <returns>新增後的新聞種類</returns>
        public NewsType CreateNewsType(NewsType newsType)
        {
            NHibernateDao.Insert(newsType);

            return newsType;
        }

        /// <summary>
        /// 更新新聞種類
        /// </summary>
        /// <param name="newsClass">被更新的新聞種類</param>
        /// <returns>更新後的新聞種類</returns>
        public NewsType UpdateNewsType(NewsType newsType)
        {
            NHibernateDao.Update(newsType);

            return newsType;
        }

        /// <summary>
        /// 刪除新聞種類
        /// </summary>
        /// <param name="newsType">被刪除的新聞種類</param>
        public void DeleteNewsType(NewsType newsType)
        {
            NHibernateDao.Delete(newsType);
        }

        /// <summary>
        /// 取得新聞種類 By 識別碼
        /// </summary>
        /// <param name="newsTypeId">識別碼</param>
        /// <returns>新聞種類</returns>
        public NewsType GetNewsTypeById(int newsTypeId)
        {
            return NHibernateDao.GetVOById<NewsType>(newsTypeId);
        }

        /// <summary>
        /// 取得新聞種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>新聞種類清單</returns>
        public IList<NewsType> GetNewsTypeList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select n from NewsType n ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryNewsType(param, fromScript, whereScript, conditions, true).OfType<NewsType>().ToList();
        }

        /// <summary>
        /// 取得新聞種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNewsTypeCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(n.NewsTypeId) from NewsType n ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryNewsType(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryNewsType(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendNewsTypeKeyWord(conditions, whereScript, param);
            AppendNewsTypeStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNewsTypeOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendNewsTypeOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by n.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendNewsTypeKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and n.Name like ? or n.NameENG like ? ");
                param.Add("%" + conditions["Name"] + "%");
                param.Add("%" + conditions["Name"] + "%");
            }

            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (n.Name like ? or n.NameENG like ? or n.Content like ? or n.ContentENG like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendNewsTypeStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and n.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(masterLog);

            return masterLog;
        }

        /// <summary>
        /// 更新後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被更新的後台帳號登入紀錄</param>
        /// <returns>更新後的後台帳號登入紀錄</returns>
        public MasterLog UpdateMasterLog(MasterLog masterLog)
        {
            NHibernateDao.Update(masterLog);

            return masterLog;
        }

        /// <summary>
        /// 刪除後台帳號登入紀錄
        /// </summary>
        /// <param name="masterLog">被刪除的後台帳號登入紀錄</param>
        public void DeleteMasterLog(MasterLog masterLog)
        {
            NHibernateDao.Delete(masterLog);
        }

        /// <summary>
        /// 取得後台帳號登入紀錄 By 識別碼
        /// </summary>
        /// <param name="masterLogId">識別碼</param>
        /// <returns>後台帳號登入紀錄</returns>
        public MasterLog GetMasterLogById(int masterLogId)
        {
            return NHibernateDao.GetVOById<MasterLog>(masterLogId);
        }

        /// <summary>
        /// 取得後台帳號登入紀錄清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>後台帳號登入紀錄清單</returns>
        public IList<MasterLog> GetMasterLogList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select m from MasterLog m ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryMasterLog(param, fromScript, whereScript, conditions, true).OfType<MasterLog>().ToList();
        }

        /// <summary>
        /// 取得後台帳號登入紀錄數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMasterLogCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(m.MasterLogId) from MasterLog m ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryMasterLog(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryMasterLog(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendMasterLogEnterTime(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendMasterLogOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendMasterLogOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by m.EnterTime desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendMasterLogEnterTime(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("EnterTimeFrom"))
            {
                whereScript.Append(" and m.EnterTime > ? ");
                param.Add(DateTime.Parse(conditions["EnterTimeFrom"]));
            }

            if (conditions.IsContainsValue("EnterTimeTo"))
            {
                whereScript.Append(" and m.EnterTime < ? ");
                param.Add(DateTime.Parse(conditions["EnterTimeTo"]).AddDays(1));
            }
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
            NHibernateDao.Insert(node);

            return node;
        }

        /// <summary>
        /// 更新種類
        /// </summary>
        /// <param name="node">被更新的種類</param>
        /// <returns>更新後的種類</returns>
        public Node UpdateNode(Node node)
        {
            NHibernateDao.Update(node);

            return node;
        }

        /// <summary>
        /// 刪除種類
        /// </summary>
        /// <param name="node">被刪除的種類</param>
        public void DeleteNode(Node node)
        {
            NHibernateDao.Delete(node);
        }

        /// <summary>
        /// 取得種類 By 識別碼
        /// </summary>
        /// <param name="nodeId">識別碼</param>
        /// <returns>種類</returns>
        public Node GetNodeById(int nodeId)
        {
            return NHibernateDao.GetVOById<Node>(nodeId);
        }

        /// <summary>
        /// 取得種類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>種類清單</returns>
        public IList<Node> GetNodeList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select n from Node n ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryNode(param, fromScript, whereScript, conditions, true).OfType<Node>().ToList();
        }

        /// <summary>
        /// 取得新聞種類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNodeCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(n.NodeId) from Node n ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryNode(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryNode(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendNodeParentNode(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNodeOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendNodeOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by n.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendNodeParentNode(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ParentNodeId"))
            {
                whereScript.Append(" and n.ParentNode.NodeId = ? ");
                param.Add(conditions["ParentNodeId"]);
            }
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
            NHibernateDao.Insert(activity);
            return activity;
        }

        /// <summary>
        /// 更新活動訊息
        /// </summary>
        /// <param name="activity">被更新的活動訊息</param>
        /// <returns>更新後的活動訊息</returns>
        public Activity UpdateActivity(Activity activity)
        {
            NHibernateDao.Update(activity);

            return activity;
        }

        /// <summary>
        /// 刪除活動訊息
        /// </summary>
        /// <param name="activity">被刪除的活動訊息</param>
        public void DeleteActivity(Activity activity)
        {
            NHibernateDao.Delete(activity);
        }

        /// <summary>
        /// 取得活動訊息 By 識別碼
        /// </summary>
        /// <param name="activityId">識別碼</param>
        /// <returns>活動訊息</returns>
        public Activity GetActivityById(int activityId)
        {
            return NHibernateDao.GetVOById<Activity>(activityId);
        }

        /// <summary>
        /// 取得活動訊息清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>活動訊息清單</returns>
        public IList<Activity> GetActivityList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select a from Activity a ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryActivity(param, fromScript, whereScript, conditions, true).OfType<Activity>().ToList();
        }

        /// <summary>
        /// 取得活動訊息數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetActivityCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(a.ActivityId) from Activity a ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryActivity(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryActivity(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendActivityKeyWord(conditions, whereScript, param);
            AppendActivityStatus(conditions, whereScript, param);
            AppendActivityIsHome(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendActivityOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendActivityIsHome(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("IsHome"))
            {
                whereScript.Append(" and a.IsHome = ? ");
                param.Add(conditions["IsHome"]);
            }
        }

        private string AppendActivityOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by a.SortId, a.PostDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendActivityKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (a.Name like ? or a.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendActivityStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and a.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(homeNews);

            return homeNews;
        }

        /// <summary>
        /// 更新即時訊息
        /// </summary>
        /// <param name="homeNews">被更新的即時訊息</param>
        /// <returns>更新後的即時訊息</returns>
        public HomeNews UpdateHomeNews(HomeNews homeNews)
        {
            NHibernateDao.Update(homeNews);

            return homeNews;
        }

        /// <summary>
        /// 刪除即時訊息
        /// </summary>
        /// <param name="homeNews">被刪除的即時訊息</param>
        public void DeleteHomeNews(HomeNews homeNews)
        {
            NHibernateDao.Delete(homeNews);
        }

        /// <summary>
        /// 取得即時訊息 By 識別碼
        /// </summary>
        /// <param name="homeNewsId">識別碼</param>
        /// <returns>即時訊息</returns>
        public HomeNews GetHomeNewsById(int homeNewsId)
        {
            return NHibernateDao.GetVOById<HomeNews>(homeNewsId);
        }

        /// <summary>
        /// 取得即時訊息清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>即時訊息清單</returns>
        public IList<HomeNews> GetHomeNewsList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select h from HomeNews h ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryHomeNews(param, fromScript, whereScript, conditions, true).OfType<HomeNews>().ToList();
        }

        /// <summary>
        /// 取得即時訊息數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetHomeNewsCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(h.HomeNewsId) from HomeNews h ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryHomeNews(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryHomeNews(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendHomeNewsKeyWord(conditions, whereScript, param);
            AppendHomeNewsStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendHomeNewsOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendHomeNewsOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by h.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendHomeNewsKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and h.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }

            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (h.Name like ? or h.AUrl like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendHomeNewsStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and h.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(linksClass);

            return linksClass;
        }

        /// <summary>
        /// 更新網路資源分類
        /// </summary>
        /// <param name="linksClass">被更新的網路資源分類</param>
        /// <returns>更新後的網路資源分類</returns>
        public LinksClass UpdateLinksClass(LinksClass linksClass)
        {
            NHibernateDao.Update(linksClass);

            return linksClass;
        }

        /// <summary>
        /// 刪除網路資源分類
        /// </summary>
        /// <param name="linksClass">被刪除的網路資源分類</param>
        public void DeleteLinksClass(LinksClass linksClass)
        {
            NHibernateDao.Delete(linksClass);
        }

        /// <summary>
        /// 取得網路資源分類 By 識別碼
        /// </summary>
        /// <param name="linksClassId">識別碼</param>
        /// <returns>網路資源分類</returns>
        public LinksClass GetLinksClassById(int linksClassId)
        {
            return NHibernateDao.GetVOById<LinksClass>(linksClassId);
        }

        /// <summary>
        /// 取得網路資源分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源分類清單</returns>
        public IList<LinksClass> GetLinksClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select l from LinksClass l ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryLinksClass(param, fromScript, whereScript, conditions, true).OfType<LinksClass>().ToList();
        }

        /// <summary>
        /// 取得網路資源分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetLinksClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(l.LinksClassId) from LinksClass l ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryLinksClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryLinksClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendLinksClassName(conditions, whereScript, param);
            AppendLinksClassStatus(conditions, whereScript, param);
            AppendLinksClassLangId(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendLinksClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendLinksClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by l.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendLinksClassName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and l.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendLinksClassStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and l.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendLinksClassLangId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LangId"))
            {
                whereScript.Append(" and l.LangId = ? ");
                param.Add(conditions["LangId"]);
            }
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
            NHibernateDao.Insert(links);

            return links;
        }

        /// <summary>
        /// 更新網路資源
        /// </summary>
        /// <param name="links">被更新的網路資源</param>
        /// <returns>更新後的網路資源</returns>
        public Links UpdateLinks(Links links)
        {
            NHibernateDao.Update(links);

            return links;
        }

        /// <summary>
        /// 刪除網路資源
        /// </summary>
        /// <param name="links">被刪除的網路資源</param>
        public void DeleteLinks(Links links)
        {
            NHibernateDao.Delete(links);
        }

        /// <summary>
        /// 取得網路資源 By 識別碼
        /// </summary>
        /// <param name="linksId">識別碼</param>
        /// <returns>網路資源</returns>
        public Links GetLinksById(int linksId)
        {
            return NHibernateDao.GetVOById<Links>(linksId);
        }

        /// <summary>
        /// 取得網路資源清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網路資源清單</returns>
        public IList<Links> GetLinksList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select l from Links l ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryLinks(param, fromScript, whereScript, conditions, true).OfType<Links>().ToList();
        }

        /// <summary>
        /// 取得網路資源數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetLinksCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(l.LinksId) from Links l ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryLinks(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryLinks(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendLinksClass(conditions, whereScript, param);
            AppendLinksName(conditions, whereScript, param);
            AppendLinksStatus(conditions, whereScript, param);
            AppendLinksLangId(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendLinksOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendLinksOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by l.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendLinksName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and l.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendLinksStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and l.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendLinksClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LinksClassId"))
            {
                whereScript.Append(" and l.LinksClass.LinksClassId = ? ");
                param.Add(conditions["LinksClassId"]);
            }
        }

        private void AppendLinksLangId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LangId"))
            {
                whereScript.Append(" and l.LangId = ? ");
                param.Add(conditions["LangId"]);
            }
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
            NHibernateDao.Insert(greenFactoryClass);

            return greenFactoryClass;
        }

        /// <summary>
        /// 更新綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被更新的綠色工廠分類</param>
        /// <returns>更新後的綠色工廠分類</returns>
        public GreenFactoryClass UpdateGreenFactoryClass(GreenFactoryClass greenFactoryClass)
        {
            NHibernateDao.Update(greenFactoryClass);

            return greenFactoryClass;
        }

        /// <summary>
        /// 刪除綠色工廠分類
        /// </summary>
        /// <param name="greenFactoryClass">被刪除的綠色工廠分類</param>
        public void DeleteGreenFactoryClass(GreenFactoryClass greenFactoryClass)
        {
            NHibernateDao.Delete(greenFactoryClass);
        }

        /// <summary>
        /// 取得綠色工廠分類 By 識別碼
        /// </summary>
        /// <param name="greenFactoryClassId">識別碼</param>
        /// <returns>綠色工廠分類</returns>
        public GreenFactoryClass GetGreenFactoryClassById(int greenFactoryClassId)
        {
            return NHibernateDao.GetVOById<GreenFactoryClass>(greenFactoryClassId);
        }

        /// <summary>
        /// 取得綠色工廠分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠分類清單</returns>
        public IList<GreenFactoryClass> GetGreenFactoryClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select g from GreenFactoryClass g ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryGreenFactoryClass(param, fromScript, whereScript, conditions, true).OfType<GreenFactoryClass>().ToList();
        }

        public int GetGreenFactoryClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(g.GreenFactoryClassId) from GreenFactoryClass g ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryGreenFactoryClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryGreenFactoryClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendGreenFactoryClassName(conditions, whereScript, param);
            AppendGreenFactoryClassStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendGreenFactoryClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendGreenFactoryClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by g.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendGreenFactoryClassName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and n.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendGreenFactoryClassStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and n.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(greenFactory);

            return greenFactory;
        }

        /// <summary>
        /// 更新綠色工廠
        /// </summary>
        /// <param name="greenFactory">被更新的綠色工廠</param>
        /// <returns>更新後的綠色工廠</returns>
        public GreenFactory UpdateGreenFactory(GreenFactory greenFactory)
        {
            NHibernateDao.Update(greenFactory);

            return greenFactory;
        }

        /// <summary>
        /// 刪除綠色工廠
        /// </summary>
        /// <param name="greenFactory">被刪除的綠色工廠</param>
        public void DeleteGreenFactory(GreenFactory greenFactory)
        {
            NHibernateDao.Delete(greenFactory);
        }

        /// <summary>
        /// 取得綠色工廠 By 識別碼
        /// </summary>
        /// <param name="greenFactoryId">識別碼</param>
        /// <returns>綠色工廠</returns>
        public GreenFactory GetGreenFactoryById(int greenFactoryId)
        {
            return NHibernateDao.GetVOById<GreenFactory>(greenFactoryId);
        }

        /// <summary>
        /// 取得綠色工廠清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>綠色工廠清單</returns>
        public IList<GreenFactory> GetGreenFactoryList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select g from GreenFactory g ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryGreenFactory(param, fromScript, whereScript, conditions, true).OfType<GreenFactory>().ToList();
        }

        /// <summary>
        /// 取得網路資源數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetGreenFactoryCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(g.GreenFactoryId) from GreenFactory g ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryGreenFactory(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryGreenFactory(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendGreenFactoryClass(conditions, whereScript, param);
            AppendGreenFactoryName(conditions, whereScript, param);
            AppendGreenFactoryStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendGreenFactoryOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendGreenFactoryOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by g.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendGreenFactoryName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and g.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendGreenFactoryStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and g.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendGreenFactoryClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("GreenFactoryClassId"))
            {
                whereScript.Append(" and g.GreenFactoryClass.GreenFactoryClassId = ? ");
                param.Add(conditions["GreenFactoryClassId"]);
            }
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
            NHibernateDao.Insert(applicationClass);

            return applicationClass;
        }

        /// <summary>
        /// 更新社會責任分類
        /// </summary>
        /// <param name="applicationClass">被更新的社會責任分類</param>
        /// <returns>更新後的社會責任分類</returns>
        public ApplicationClass UpdateApplicationClass(ApplicationClass applicationClass)
        {
            NHibernateDao.Update(applicationClass);

            return applicationClass;
        }

        /// <summary>
        /// 刪除社會責任分類
        /// </summary>
        /// <param name="applicationClass">被刪除的社會責任分類</param>
        public void DeleteApplicationClass(ApplicationClass applicationClass)
        {
            NHibernateDao.Delete(applicationClass);
        }

        /// <summary>
        /// 取得社會責任分類 By 識別碼
        /// </summary>
        /// <param name="applicationClassId">識別碼</param>
        /// <returns>社會責任分類</returns>
        public ApplicationClass GetApplicationClassById(int applicationClassId)
        {
            return NHibernateDao.GetVOById<ApplicationClass>(applicationClassId);
        }

        /// <summary>
        /// 取得社會責任分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>社會責任分類清單</returns>
        public IList<ApplicationClass> GetApplicationClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select a from ApplicationClass a ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryApplicationClass(param, fromScript, whereScript, conditions, true).OfType<ApplicationClass>().ToList();
        }

        public int GetApplicationClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(a.ApplicationClassId) from ApplicationClass a ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryApplicationClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryApplicationClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendApplicationClassName(conditions, whereScript, param);
            AppendApplicationClassStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendApplicationClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendApplicationClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by a.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendApplicationClassName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and a.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendApplicationClassStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and a.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(application);

            return application;
        }

        /// <summary>
        /// 更新企業社會責任
        /// </summary>
        /// <param name="application">被更新的企業社會責任</param>
        /// <returns>更新後的企業社會責任</returns>
        public Application UpdateApplication(Application application)
        {
            NHibernateDao.Update(application);

            return application;
        }

        /// <summary>
        /// 刪除企業社會責任
        /// </summary>
        /// <param name="application">被刪除的企業社會責任</param>
        public void DeleteApplication(Application application)
        {
            NHibernateDao.Delete(application);
        }

        /// <summary>
        /// 取得企業社會責任 By 識別碼
        /// </summary>
        /// <param name="applicationId">識別碼</param>
        /// <returns>企業社會責任</returns>
        public Application GetApplicationById(int applicationId)
        {
            return NHibernateDao.GetVOById<Application>(applicationId);
        }

        /// <summary>
        /// 取得企業社會責任清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>企業社會責任清單</returns>
        public IList<Application> GetApplicationList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select a from Application a ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryApplication(param, fromScript, whereScript, conditions, true).OfType<Application>().ToList();
        }

        /// <summary>
        /// 取得企業社會責任數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetApplicationCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(a.ApplicationId) from Application a ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryApplication(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryApplication(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendApplicationClass(conditions, whereScript, param);
            AppendApplicationKeyWord(conditions, whereScript, param);
            AppendApplicationStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendApplicationOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendApplicationOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by a.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendApplicationKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (a.Name like ? or a.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendApplicationStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and a.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendApplicationClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ApplicationClassId"))
            {
                whereScript.Append(" and a.ApplicationClass.ApplicationClassId = ? ");
                param.Add(conditions["ApplicationClassId"]);
            }
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
            NHibernateDao.Insert(publicationClass);

            return publicationClass;
        }

        /// <summary>
        /// 更新期刊分類
        /// </summary>
        /// <param name="publicationClass">被更新的期刊分類</param>
        /// <returns>更新後的期刊分類</returns>
        public PublicationClass UpdatePublicationClass(PublicationClass publicationClass)
        {
            NHibernateDao.Update(publicationClass);

            return publicationClass;
        }

        /// <summary>
        /// 刪除期刊分類
        /// </summary>
        /// <param name="publicationClass">被刪除的期刊分類</param>
        public void DeletePublicationClass(PublicationClass publicationClass)
        {
            NHibernateDao.Delete(publicationClass);
        }

        /// <summary>
        /// 取得期刊分類 By 識別碼
        /// </summary>
        /// <param name="publicationClassId">識別碼</param>
        /// <returns>期刊分類</returns>
        public PublicationClass GetPublicationClassById(int publicationClassId)
        {
            return NHibernateDao.GetVOById<PublicationClass>(publicationClassId);
        }

        /// <summary>
        /// 取得期刊分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊分類清單</returns>
        public IList<PublicationClass> GetPublicationClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select p from PublicationClass p ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryPublicationClass(param, fromScript, whereScript, conditions, true).OfType<PublicationClass>().ToList();
        }

        /// <summary>
        /// 取得期刊分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPublicationClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(p.PublicationClassId) from PublicationClass p ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryPublicationClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryPublicationClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendPublicationClassName(conditions, whereScript, param);
            AppendPublicationClassStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendPublicationClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendPublicationClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by p.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendPublicationClassName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and p.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendPublicationClassStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and p.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(publication);

            return publication;
        }

        /// <summary>
        /// 更新期刊
        /// </summary>
        /// <param name="publication">被更新的期刊</param>
        /// <returns>更新後的期刊</returns>
        public Publication UpdatePublication(Publication publication)
        {
            NHibernateDao.Update(publication);

            return publication;
        }

        /// <summary>
        /// 刪除期刊
        /// </summary>
        /// <param name="publication">被刪除的期刊</param>
        public void DeletePublication(Publication publication)
        {
            NHibernateDao.Delete(publication);
        }

        /// <summary>
        /// 取得期刊 By 識別碼
        /// </summary>
        /// <param name="publicationId">識別碼</param>
        /// <returns>期刊</returns>
        public Publication GetPublicationById(int publicationId)
        {
            return NHibernateDao.GetVOById<Publication>(publicationId);
        }

        /// <summary>
        /// 取得期刊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>期刊清單</returns>
        public IList<Publication> GetPublicationList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select p from Publication p ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryPublication(param, fromScript, whereScript, conditions, true).OfType<Publication>().ToList();
        }

        /// <summary>
        /// 取得期刊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPublicationCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(p.PublicationId) from Publication p ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryPublication(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryPublication(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendPublicationClass(conditions, whereScript, param);
            AppendPublicationKeyWord(conditions, whereScript, param);
            AppendPublicationStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendPublicationOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendPublicationOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by p.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendPublicationKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (p.Name like ? or p.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendPublicationStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and p.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendPublicationClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("PublicationClassId"))
            {
                whereScript.Append(" and p.PublicationClass.PublicationClassId = ? ");
                param.Add(conditions["PublicationClassId"]);
            }
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
            NHibernateDao.Insert(questionClass);

            return questionClass;
        }

        /// <summary>
        /// 更新Q&A分類
        /// </summary>
        /// <param name="questionClass">被更新的Q&A分類</param>
        /// <returns>更新後的Q&A分類</returns>
        public QuestionClass UpdateQuestionClass(QuestionClass questionClass)
        {
            NHibernateDao.Update(questionClass);

            return questionClass;
        }

        /// <summary>
        /// 刪除Q&A分類
        /// </summary>
        /// <param name="questionClass">被刪除的Q&A分類</param>
        public void DeleteQuestionClass(QuestionClass questionClass)
        {
            NHibernateDao.Delete(questionClass);
        }

        /// <summary>
        /// 取得Q&A分類 By 識別碼
        /// </summary>
        /// <param name="questionClassId">識別碼</param>
        /// <returns>Q&A分類</returns>
        public QuestionClass GetQuestionClassById(int questionClassId)
        {
            return NHibernateDao.GetVOById<QuestionClass>(questionClassId);
        }

        /// <summary>
        /// 取得Q&A分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A分類清單</returns>
        public IList<QuestionClass> GetQuestionClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select q from QuestionClass q ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryQuestionClass(param, fromScript, whereScript, conditions, true).OfType<QuestionClass>().ToList();
        }

        /// <summary>
        /// 取得Q&A分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetQuestionClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(q.QuestionClassId) from QuestionClass q ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryQuestionClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryQuestionClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendQuestionClassName(conditions, whereScript, param);
            AppendQuestionClassStatus(conditions, whereScript, param);
            AppendQuestionClassLangId(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendQuestionClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendQuestionClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by q.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendQuestionClassName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and 1.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendQuestionClassStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and q.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendQuestionClassLangId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LangId"))
            {
                whereScript.Append(" and q.LangId = ? ");
                param.Add(conditions["LangId"]);
            }
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
            NHibernateDao.Insert(question);

            return question;
        }

        /// <summary>
        /// 更新Q&A
        /// </summary>
        /// <param name="question">被更新的Q&A</param>
        /// <returns>更新後的Q&A</returns>
        public Question UpdateQuestion(Question question)
        {
            NHibernateDao.Update(question);

            return question;
        }

        /// <summary>
        /// 刪除Q&A
        /// </summary>
        /// <param name="question">被刪除的Q&A</param>
        public void DeleteQuestion(Publication question)
        {
            NHibernateDao.Delete(question);
        }

        /// <summary>
        /// 取得Q&A By 識別碼
        /// </summary>
        /// <param name="questionId">識別碼</param>
        /// <returns>Q&A</returns>
        public Question GetQuestionById(int questionId)
        {
            return NHibernateDao.GetVOById<Question>(questionId);
        }

        /// <summary>
        /// 取得Q&A清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>Q&A清單</returns>
        public IList<Question> GetQuestionList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select q from Question q ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryQuestion(param, fromScript, whereScript, conditions, true).OfType<Question>().ToList();
        }

        /// <summary>
        /// 取得Q&A數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetQuestionCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(q.QuestionId) from Question q ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryQuestion(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryQuestion(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendQuestionClass(conditions, whereScript, param);
            AppendQuestionKeyWord(conditions, whereScript, param);
            AppendQuestionStatus(conditions, whereScript, param);
            AppendQuestionLangId(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendQuestionOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendQuestionOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by q.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendQuestionKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (q.Name like ? or q.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendQuestionStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and q.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendQuestionLangId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LangId"))
            {
                whereScript.Append(" and q.LangId = ? ");
                param.Add(conditions["LangId"]);
            }
        }

        private void AppendQuestionClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("QuestionClassId"))
            {
                whereScript.Append(" and q.QuestionClass.QuestionClassId = ? ");
                param.Add(conditions["QuestionClassId"]);
            }
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
            NHibernateDao.Insert(brief);

            return brief;
        }

        /// <summary>
        /// 更新永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被更新的永續產業發展簡訊</param>
        /// <returns>更新後的永續產業發展簡訊</returns>
        public Brief UpdateBrief(Brief brief)
        {
            NHibernateDao.Update(brief);

            return brief;
        }

        /// <summary>
        /// 刪除永續產業發展簡訊
        /// </summary>
        /// <param name="brief">被刪除的永續產業發展簡訊</param>
        public void DeleteBrief(Brief brief)
        {
            NHibernateDao.Delete(brief);
        }

        /// <summary>
        /// 取得永續產業發展簡訊 By 識別碼
        /// </summary>
        /// <param name="briefId">識別碼</param>
        /// <returns>永續產業發展簡訊</returns>
        public Brief GetBriefById(int briefId)
        {
            return NHibernateDao.GetVOById<Brief>(briefId);
        }

        /// <summary>
        /// 取得永續產業發展簡訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>永續產業發展簡訊清單</returns>
        public IList<Brief> GetBriefList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select b from Brief b ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryBrief(param, fromScript, whereScript, conditions, true).OfType<Brief>().ToList();
        }

        /// <summary>
        /// 取得永續產業發展簡訊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetBriefCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(b.BriefId) from Brief b ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryBrief(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryBrief(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendBriefKeyWord(conditions, whereScript, param);
            AppendBriefStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendBriefOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendBriefOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by b.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendBriefKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (b.Name like ? or b.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendBriefStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and b.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(curriculum);

            return curriculum;
        }

        /// <summary>
        /// 更新課程講義
        /// </summary>
        /// <param name="curriculum">被更新的課程講義</param>
        /// <returns>更新後的課程講義</returns>
        public Curriculum UpdateCurriculum(Curriculum curriculum)
        {
            NHibernateDao.Update(curriculum);

            return curriculum;
        }

        /// <summary>
        /// 刪除課程講義
        /// </summary>
        /// <param name="curriculum">被刪除的課程講義</param>
        public void DeleteCurriculum(Curriculum curriculum)
        {
            NHibernateDao.Delete(curriculum);
        }

        /// <summary>
        /// 取得課程講義 By 識別碼
        /// </summary>
        /// <param name="curriculumId">識別碼</param>
        /// <returns>課程講義</returns>
        public Curriculum GetCurriculumById(int curriculumId)
        {
            return NHibernateDao.GetVOById<Curriculum>(curriculumId);
        }

        /// <summary>
        /// 取得課程講義清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>課程講義清單</returns>
        public IList<Curriculum> GetCurriculumList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select c from Curriculum c ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryCurriculum(param, fromScript, whereScript, conditions, true).OfType<Curriculum>().ToList();
        }

        /// <summary>
        /// 取得課程講義數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetCurriculumCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(c.CurriculumId) from Curriculum c ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryCurriculum(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryCurriculum(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendCurriculumKeyWord(conditions, whereScript, param);
            AppendCurriculumStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendCurriculumOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendCurriculumOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by c.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendCurriculumKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (c.Name like ? or c.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendCurriculumStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and c.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(download);

            return download;
        }

        /// <summary>
        /// 更新技術工具/文件下載
        /// </summary>
        /// <param name="download">被更新的技術工具/文件下載</param>
        /// <returns>更新後的技術工具/文件下載</returns>
        public Download UpdateDownload(Download download)
        {
            NHibernateDao.Update(download);

            return download;
        }

        /// <summary>
        /// 刪除技術工具/文件下載
        /// </summary>
        /// <param name="download">被刪除的技術工具/文件下載</param>
        public void DeleteDownload(Download download)
        {
            NHibernateDao.Delete(download);
        }

        /// <summary>
        /// 取得技術工具/文件下載 By 識別碼
        /// </summary>
        /// <param name="downloadId">識別碼</param>
        /// <returns>技術工具/文件下載</returns>
        public Download GetDownloadById(int downloadId)
        {
            return NHibernateDao.GetVOById<Download>(downloadId);
        }

        /// <summary>
        /// 取得技術工具/文件下載清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>技術工具/文件下載清單</returns>
        public IList<Download> GetDownloadList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select d from Download d ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryDownload(param, fromScript, whereScript, conditions, true).OfType<Download>().ToList();
        }

        /// <summary>
        /// 取得課程講義數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetDownloadCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(d.DownloadId) from Download d ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryDownload(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryDownload(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendDownloadKeyWord(conditions, whereScript, param);
            AppendDownloadStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendDownloadOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendDownloadOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by d.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendDownloadKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (d.Name like ? or d.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendDownloadStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and d.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(industry);

            return industry;
        }

        /// <summary>
        /// 更新行業分類
        /// </summary>
        /// <param name="industry">被更新的行業分類</param>
        /// <returns>更新後的行業分類</returns>
        public Industry UpdateIndustry(Industry industry)
        {
            NHibernateDao.Update(industry);

            return industry;
        }

        /// <summary>
        /// 刪除行業分類
        /// </summary>
        /// <param name="industry">被刪除的行業分類</param>
        public void DeleteIndustry(Industry industry)
        {
            NHibernateDao.Delete(industry);
        }

        /// <summary>
        /// 取得行業分類 By 識別碼
        /// </summary>
        /// <param name="industryId">識別碼</param>
        /// <returns>行業分類</returns>
        public Industry GetIndustryById(int industryId)
        {
            return NHibernateDao.GetVOById<Industry>(industryId);
        }

        /// <summary>
        /// 取得行業分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>行業分類清單</returns>
        public IList<Industry> GetIndustryList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select i from Industry i ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryIndustry(param, fromScript, whereScript, conditions, true).OfType<Industry>().ToList();
        }

        /// <summary>
        /// 取得行業分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetIndustryCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(i.IndustryId) from Industry i ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryIndustry(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryIndustry(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendIndustryName(conditions, whereScript, param);
            AppendIndustryStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendIndustryOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendIndustryOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by i.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendIndustryName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and i.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendIndustryStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and i.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(member);
            return member;
        }

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="member">被更新的會員</param>
        /// <returns>更新後的會員</returns>
        public Member UpdateMember(Member member)
        {
            NHibernateDao.Update(member);

            return member;
        }

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="member">被刪除的會員</param>
        public void DeleteMember(Member member)
        {
            NHibernateDao.Delete(member);
        }

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        public Member GetMemberById(int memberId)
        {
            return NHibernateDao.GetVOById<Member>(memberId);
        }

        /// <summary>
        /// 取得會員清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員清單</returns>
        public IList<Member> GetMemberList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select m from Member m ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryMember(param, fromScript, whereScript, conditions, true).OfType<Member>().ToList();
        }

        /// <summary>
        /// 取得會員數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMemberCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(m.MemberId) from Member m ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryMember(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryMember(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendMemberIndustry(conditions, whereScript, param);
            AppendMemberLoginId(conditions, whereScript, param);
            AppendMemberCompany(conditions, whereScript, param);
            AppendMemberStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendMemberOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendMemberIndustry(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("IndustryId"))
            {
                whereScript.Append(" and m.Industry.IndustryId = ? ");
                param.Add(conditions["IndustryId"]);
            }
        }

        private string AppendMemberOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by m.RegDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendMemberLoginId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LoginId"))
            {
                whereScript.Append(" and m.LoginId = ? ");
                param.Add(conditions["LoginId"]);
            }
        }

        private void AppendMemberCompany(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Company"))
            {
                whereScript.Append(" and m.Company like ? ");
                param.Add("%" + conditions["Company"] + "%");
            }
        }

        private void AppendMemberStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and m.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(downloadRecord);
            return downloadRecord;
        }

        /// <summary>
        /// 更新會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被更新的會員下載紀錄</param>
        /// <returns>更新後的會員下載紀錄</returns>
        public DownloadRecord UpdateDownloadRecord(DownloadRecord downloadRecord)
        {
            NHibernateDao.Update(downloadRecord);

            return downloadRecord;
        }

        /// <summary>
        /// 刪除會員下載紀錄
        /// </summary>
        /// <param name="downloadRecord">被刪除的會員下載紀錄</param>
        public void DeleteDownloadRecord(DownloadRecord downloadRecord)
        {
            NHibernateDao.Delete(downloadRecord);
        }

        /// <summary>
        /// 取得會員下載紀錄 By 識別碼
        /// </summary>
        /// <param name="downloadRecordId">識別碼</param>
        /// <returns>會員下載紀錄</returns>
        public DownloadRecord GetDownloadRecordById(int downloadRecordId)
        {
            return NHibernateDao.GetVOById<DownloadRecord>(downloadRecordId);
        }

        /// <summary>
        /// 取得會員下載紀錄清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員下載紀錄清單</returns>
        public IList<DownloadRecord> GetDownloadRecordList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select d from DownloadRecord d ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryDownloadRecord(param, fromScript, whereScript, conditions, true).OfType<DownloadRecord>().ToList();
        }

        /// <summary>
        /// 取得會員下載紀錄數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetDownloadRecordCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(d.DownloadRecordId) from DownloadRecord d ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryDownloadRecord(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        /// <summary>
        /// 取得會員下載紀錄統計表清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員下載紀錄統計表清單</returns>
        public IList<DownloadRecord> GetDownloadRecordStatistics(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select distinct d.Name, d.ClassId, (Select Count(dr.DownloadRecordId) from DownloadRecord dr where dr.Name = d.Name) ";
            fromScript += "  from DownloadRecord d ";
            StringBuilder whereScript = new StringBuilder();

            IList<object[]> recordList = this.QueryDownloadRecord<object[]>(param, fromScript, whereScript, conditions, true);
            IList<DownloadRecord> result = (from record in recordList
                                            select new DownloadRecord
                                            {
                                                Name = record[0] as string,
                                                ClassId = record[1] as string,
                                                Downer = Convert.ToInt32(record[2])
                                            }).ToList();

            return result;
        }

        private IList<T> QueryDownloadRecord<T>(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            IList<T> list = new List<T>();
            AppendDownloadRecordMember(conditions, whereScript, param);
            AppendDownloadRecordName(conditions, whereScript, param);
            AppendDownloadClassId(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendDownloadRecordOrder(conditions);
            }

            list = NHibernateDao.Query<T>(hql, param, conditions);

            return list;
        }

        private IList QueryDownloadRecord(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendDownloadRecordMember(conditions, whereScript, param);
            AppendDownloadRecordName(conditions, whereScript, param);
            AppendDownloadClassId(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendDownloadRecordOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendDownloadRecordMember(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("MemberId"))
            {
                whereScript.Append(" and d.Member.MemberId = ? ");
                param.Add(conditions["MemberId"]);
            }
        }

        private string AppendDownloadRecordOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by convert(datetime, d.PostDate, 111) desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendDownloadRecordName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and d.Name = ? ");
                param.Add(conditions["Name"]);
            }
        }

        private void AppendDownloadClassId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ClassId"))
            {
                whereScript.Append(" and d.ClassId = ? ");
                param.Add(conditions["ClassId"]);
            }
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
            NHibernateDao.Insert(epaper);

            return epaper;
        }

        /// <summary>
        /// 更新電子報
        /// </summary>
        /// <param name="epaper">被更新的電子報</param>
        /// <returns>更新後的電子報</returns>
        public Epaper UpdateEpaper(Epaper epaper)
        {
            NHibernateDao.Update(epaper);

            return epaper;
        }

        /// <summary>
        /// 刪除電子報
        /// </summary>
        /// <param name="epaper">被刪除的電子報</param>
        public void DeleteEpaper(Epaper epaper)
        {
            NHibernateDao.Delete(epaper);
        }

        /// <summary>
        /// 取得電子報 By 識別碼
        /// </summary>
        /// <param name="epaperId">識別碼</param>
        /// <returns>電子報</returns>
        public Epaper GetEpaperById(int epaperId)
        {
            return NHibernateDao.GetVOById<Epaper>(epaperId);
        }

        /// <summary>
        /// 取得電子報清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報清單</returns>
        public IList<Epaper> GetEpaperList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select e from Epaper e ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryEpaper(param, fromScript, whereScript, conditions, true).OfType<Epaper>().ToList();
        }

        /// <summary>
        /// 取得電子報數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetEpaperCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(e.EpaperId) from Epaper e ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryEpaper(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryEpaper(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendEpaperName(conditions, whereScript, param);
            AppendEpaperStatus(conditions, whereScript, param);
            AppendEpaperPostDate(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendEpaperOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendEpaperOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by e.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendEpaperName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and e.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendEpaperStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and e.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }

        private void AppendEpaperPostDate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ByYear"))
            {
                int yearFrom = int.Parse(conditions["ByYear"]);
                DateTime dateFrom = new DateTime(yearFrom, 1, 1);
                DateTime dateTo = new DateTime((yearFrom + 1), 1, 1);
                whereScript.Append(" and e.PostDate >= ? & e.PostDate < ? ");
                param.Add(dateFrom);
                param.Add(dateTo);
            }
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
            NHibernateDao.Insert(epaperEmail);
            return epaperEmail;
        }

        /// <summary>
        /// 更新電子報訂閱
        /// </summary>
        /// <param name="epaperEmail">被更新的電子報訂閱</param>
        /// <returns>更新後的電子報訂閱</returns>
        public EpaperEmail UpdateEpaperEmail(EpaperEmail epaperEmail)
        {
            NHibernateDao.Update(epaperEmail);

            return epaperEmail;
        }

        /// <summary>
        /// 刪除電子報訂閱
        /// </summary>
        /// <param name="member">被刪除的電子報訂閱</param>
        public void DeleteEpaperEmail(EpaperEmail epaperEmail)
        {
            NHibernateDao.Delete(epaperEmail);
        }

        /// <summary>
        /// 取得電子報訂閱 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>電子報訂閱</returns>
        public EpaperEmail GetEpaperEmailById(int epaperEmail)
        {
            return NHibernateDao.GetVOById<EpaperEmail>(epaperEmail);
        }

        /// <summary>
        /// 取得電子報訂閱清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報訂閱清單</returns>
        public IList<EpaperEmail> GetEpaperEmailList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select e from EpaperEmail e ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryEpaperEmail(param, fromScript, whereScript, conditions, true).OfType<EpaperEmail>().ToList();
        }

        /// <summary>
        /// 取得電子報訂閱數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetEpaperEmailCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(e.EpaperEmailId) from EpaperEmail e ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryEpaperEmail(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryEpaperEmail(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendEpaperKeyWord(conditions, whereScript, param);
            AppendEpaperEmailStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendEpaperEmailOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendEpaperEmailOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by e.RegDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendEpaperKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ByEmail"))
            {
                ////退訂/訂閱用
                whereScript.Append(" and e.Email = ? ");
                param.Add(conditions["ByEmail"]);
            }
            else
            {
                ////搜尋用
                if (conditions.IsContainsValue("KeyWord"))
                {
                    whereScript.Append(" and (e.Email like ? or e.Company like ? ) ");
                    param.Add("%" + conditions["KeyWord"] + "%");
                    param.Add("%" + conditions["KeyWord"] + "%");
                }
            }
        }

        private void AppendEpaperEmailStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and e.Status = ? ");
                param.Add(conditions["Status"]);
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
            NHibernateDao.Insert(otherExamination);

            return otherExamination;
        }

        /// <summary>
        /// 更新問卷調查
        /// </summary>
        /// <param name="otherExamination">被更新的問卷調查</param>
        /// <returns>更新後的問卷調查</returns>
        public OtherExamination UpdateOtherExamination(OtherExamination otherExamination)
        {
            NHibernateDao.Update(otherExamination);

            return otherExamination;
        }

        /// <summary>
        /// 刪除問卷調查
        /// </summary>
        /// <param name="otherExamination">被刪除的問卷調查</param>
        public void DeleteOtherExamination(OtherExamination otherExamination)
        {
            NHibernateDao.Delete(otherExamination);
        }

        /// <summary>
        /// 取得問卷調查 By 識別碼
        /// </summary>
        /// <param name="otherExaminationId">識別碼</param>
        /// <returns>問卷調查</returns>
        public OtherExamination GetOtherExaminationById(int otherExaminationId)
        {
            return NHibernateDao.GetVOById<OtherExamination>(otherExaminationId);
        }

        /// <summary>
        /// 取得問卷調查清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>問卷調查清單</returns>
        public IList<OtherExamination> GetOtherExaminationList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select o from OtherExamination o ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryOtherExamination(param, fromScript, whereScript, conditions, true).OfType<OtherExamination>().ToList();
        }

        /// <summary>
        /// 取得問卷調查數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetOtherExaminationCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(o.OtherExaminationId) from OtherExamination o ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryOtherExamination(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryOtherExamination(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendOtherExaminationName(conditions, whereScript, param);
            AppendOtherExaminationStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendOtherExaminationOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendOtherExaminationOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by o.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendOtherExaminationName(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Name"))
            {
                whereScript.Append(" and o.Name like ? ");
                param.Add("%" + conditions["Name"] + "%");
            }
        }

        private void AppendOtherExaminationStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and o.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(mNorm);

            return mNorm;
        }

        /// <summary>
        /// 更新國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被更新的國際組織標準/系統動態</param>
        /// <returns>更新後的國際組織標準/系統動態</returns>
        public MNorm UpdateMNorm(MNorm mNorm)
        {
            NHibernateDao.Update(mNorm);

            return mNorm;
        }

        /// <summary>
        /// 刪除國際組織標準/系統動態
        /// </summary>
        /// <param name="mNorm">被刪除的國際組織標準/系統動態</param>
        public void DeleteMNorm(MNorm mNorm)
        {
            NHibernateDao.Delete(mNorm);
        }

        /// <summary>
        /// 取得國際組織標準/系統動態 By 識別碼
        /// </summary>
        /// <param name="mNormId">識別碼</param>
        /// <returns>國際組織標準/系統動態</returns>
        public MNorm GetMNormById(int mNormId)
        {
            return NHibernateDao.GetVOById<MNorm>(mNormId);
        }

        /// <summary>
        /// 取得國際組織標準/系統動態清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>國際組織標準/系統動態清單</returns>
        public IList<MNorm> GetMNormList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select m from MNorm m ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryMNorm(param, fromScript, whereScript, conditions, true).OfType<MNorm>().ToList();
        }

        /// <summary>
        /// 取得國際組織標準/系統動態數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMNormCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(m.MNormId) from Brief m ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryMNorm(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryMNorm(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendMNormKeyWord(conditions, whereScript, param);
            AppendMNormStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendMNormOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendMNormOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by m.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendMNormKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (m.Name like ? or m.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendMNormStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and b.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(normClass);

            return normClass;
        }

        /// <summary>
        /// 更新標準/規範分類
        /// </summary>
        /// <param name="normClass">被更新的標準/規範分類</param>
        /// <returns>更新後的標準/規範分類</returns>
        public NormClass UpdateNormClass(NormClass normClass)
        {
            NHibernateDao.Update(normClass);

            return normClass;
        }

        /// <summary>
        /// 刪除標準/規範分類
        /// </summary>
        /// <param name="normClass">被刪除的標準/規範分類</param>
        public void DeleteNormClass(NormClass normClass)
        {
            NHibernateDao.Delete(normClass);
        }

        /// <summary>
        /// 取得標準/規範分類 By 識別碼
        /// </summary>
        /// <param name="normClassId">識別碼</param>
        /// <returns>標準/規範分類</returns>
        public NormClass GetNormClassById(int normClassId)
        {
            return NHibernateDao.GetVOById<NormClass>(normClassId);
        }

        /// <summary>
        /// 取得標準/規範分類清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範分類清單</returns>
        public IList<NormClass> GetNormClassList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select n from NormClass n ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryNormClass(param, fromScript, whereScript, conditions, true).OfType<NormClass>().ToList();
        }

        /// <summary>
        /// 取得標準/規範分類數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNormClassCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(n.NormClassId) from NormClass n ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryNormClass(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryNormClass(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendNodeParentNormClass(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNormClassOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendNormClassOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by n.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendNodeParentNormClass(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ParentNormClassId"))
            {
                whereScript.Append(" and n.ParentNormClass.NormClassId = ? ");
                param.Add(conditions["ParentNormClassId"]);
            }
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
            NHibernateDao.Insert(norm);
            return norm;
        }

        /// <summary>
        /// 更新標準/規範資訊
        /// </summary>
        /// <param name="norm">被更新的標準/規範資訊</param>
        /// <returns>更新後的標準/規範資訊</returns>
        public Norm UpdateNorm(Norm norm)
        {
            NHibernateDao.Update(norm);

            return norm;
        }

        /// <summary>
        /// 刪除標準/規範資訊
        /// </summary>
        /// <param name="norm">被刪除的標準/規範資訊</param>
        public void DeleteNorm(Norm norm)
        {
            NHibernateDao.Delete(norm);
        }

        /// <summary>
        /// 取得標準/規範資訊 By 識別碼
        /// </summary>
        /// <param name="normId">識別碼</param>
        /// <returns>標準/規範資訊</returns>
        public Norm GetNormById(int normId)
        {
            return NHibernateDao.GetVOById<Norm>(normId);
        }

        /// <summary>
        /// 取得標準/規範資訊清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>標準/規範資訊清單</returns>
        public IList<Norm> GetNormList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select n from Norm n ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryNorm(param, fromScript, whereScript, conditions, true).OfType<Norm>().ToList();
        }

        /// <summary>
        /// 取得標準/規範資訊數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetNormCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(n.NormId) from Norm n ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryNorm(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryNorm(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendNormClassParent(conditions, whereScript, param);
            AppendNormKeyWord(conditions, whereScript, param);
            AppendNormStatus(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendNormOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendNormClassParent(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("NewsClassParentId"))
            {
                whereScript.Append(" and n.NormClassParent.NormId = ? ");
                param.Add(conditions["NewsClassParentId"]);
            }
        }

        private string AppendNormOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by n.SortId ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendNormKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (n.Name like ? or n.Content like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private void AppendNormStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and n.Status = ? ");
                param.Add(conditions["Status"]);
            }
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
            NHibernateDao.Insert(examination);
            return examination;
        }

        /// <summary>
        /// 更新網站滿意度問卷
        /// </summary>
        /// <param name="examination">被更新的網站滿意度問卷</param>
        /// <returns>更新後的網站滿意度問卷</returns>
        public Examination UpdateExamination(Examination examination)
        {
            NHibernateDao.Update(examination);

            return examination;
        }

        /// <summary>
        /// 刪除網站滿意度問卷
        /// </summary>
        /// <param name="examination">被刪除的網站滿意度問卷</param>
        public void DeleteExamination(Examination examination)
        {
            NHibernateDao.Delete(examination);
        }

        /// <summary>
        /// 取得網站滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="examinationId">識別碼</param>
        /// <returns>網站滿意度問卷</returns>
        public Examination GetExaminationById(int examinationId)
        {
            return NHibernateDao.GetVOById<Examination>(examinationId);
        }

        /// <summary>
        /// 取得網站滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>網站滿意度問卷清單</returns>
        public IList<Examination> GetExaminationList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select e from Examination e ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryExamination(param, fromScript, whereScript, conditions, true).OfType<Examination>().ToList();
        }

        /// <summary>
        /// 取得網站滿意度問卷數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetExaminationCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(e.ExaminationId) from Examination e ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryExamination(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryExamination(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendExaminationKeyWord(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendExaminationOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendExaminationOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by e.PostDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendExaminationKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (e.Name like ? or e.Email like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
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
            NHibernateDao.Insert(epaperExamination);
            return epaperExamination;
        }

        /// <summary>
        /// 更新電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被更新的電子報滿意度問卷</param>
        /// <returns>更新後的電子報滿意度問卷</returns>
        public EpaperExamination UpdateEpaperExamination(EpaperExamination epaperExamination)
        {
            NHibernateDao.Update(epaperExamination);

            return epaperExamination;
        }

        /// <summary>
        /// 刪除電子報滿意度問卷
        /// </summary>
        /// <param name="epaperExamination">被刪除的電子報滿意度問卷</param>
        public void DeleteEpaperExamination(EpaperExamination epaperExamination)
        {
            NHibernateDao.Delete(epaperExamination);
        }

        /// <summary>
        /// 取得電子報滿意度問卷 By 識別碼
        /// </summary>
        /// <param name="epaperExaminationId">識別碼</param>
        /// <returns>電子報滿意度問卷</returns>
        public EpaperExamination GetEpaperExaminationById(int epaperExaminationId)
        {
            return NHibernateDao.GetVOById<EpaperExamination>(epaperExaminationId);
        }

        /// <summary>
        /// 取得電子報滿意度問卷清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子報滿意度問卷清單</returns>
        public IList<EpaperExamination> GetEpaperExaminationList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select e from EpaperExamination e ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryEpaperExamination(param, fromScript, whereScript, conditions, true).OfType<EpaperExamination>().ToList();
        }

        /// <summary>
        /// 取得電子報滿意度問卷數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetEpaperExaminationCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(e.EpaperExaminationId) from EpaperExamination e ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryEpaperExamination(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryEpaperExamination(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendEpaperExaminationKeyWord(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendEpaperExaminationOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private string AppendEpaperExaminationOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by e.PostDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendEpaperExaminationKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (e.Name like ? or e.Email like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }
        #endregion

        #endregion
    }
}
