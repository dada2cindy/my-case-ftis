using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate.Support;
using WuDada.Core.Generic.Extension;
using WuDada.Core.Generic.Util;
using WuDada.Core.Helper;

namespace WuDada.Core.Persistence.NHibernate
{
    public class NHibernateDao : HibernateDaoSupport, INHibernateDao
    {
        #region INHibernateDao 成員        

        /// <summary>
        /// 取得VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns>VO的全部資料</returns>
        public IList<T> GetAllVO<T>()
        {
            IList result = HibernateTemplate.LoadAll(typeof(T));

            if (result != null)
            {
                IList<T> typedList = new List<T>();
                foreach (object obj in result)
                {
                    typedList.Add((T)obj);
                }
                return typedList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取出VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>VO的全部資料</returns>
        public IList<T> GetAllVO<T>(int pageIndex, int pageSize)
        {
            int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize);

            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.SetFirstResult(startIndex);
            criteria.SetMaxResults(pageSize);
            return (criteria.List<T>());
        }

        /// <summary>
        /// 取出VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="order">排序條件</param>
        /// <returns>VO的全部資料</returns>
        public IList<T> GetAllVO<T>(int pageIndex, int pageSize, Order order)
        {
            int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize);

            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();            
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.SetFirstResult(startIndex);
            criteria.SetMaxResults(pageSize);
            criteria.AddOrder(order);
            return (criteria.List<T>());
        }        

        /// <summary>
        /// 取得VOBy KeyId
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="keyId">PK</param>
        /// <returns>VO</returns>
        public T GetVOById<T>(object keyId)
        {
            Object obj = HibernateTemplate.Get(typeof(T), keyId);

            if (obj != null)
            {
                return (T)obj;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 執行預先定義好的DetachedCriteria並傳回一筆資料
        /// </summary>
        /// <typeparam name="T">轉型型別</typeparam>
        /// <param name="detachedcriteria">預先準備好的條件</param>
        /// <returns></returns>
        public T GetVOByDetachedCriteria<T>(DetachedCriteria detachedcriteria)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            ICriteria criteria = detachedcriteria.GetExecutableCriteria(session);
            criteria.SetMaxResults(1);
            return (criteria.UniqueResult<T>());
        }

        #region 增,刪,修

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vo">要新增的VO</param>
        public void Insert(Object vo)
        {
            HibernateTemplate.Save(vo);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="vo">被刪除的VO</param>
        public void Delete(Object vo)
        {
            HibernateTemplate.Delete(vo);
        }

        /// <summary>
        /// 更新VO
        /// </summary>
        /// <param name="vo">被更新的VO</param>
        public void Update(Object vo)
        {
            HibernateTemplate.Update(vo);
        }

        /// <summary>
        /// 新增或更新VO
        /// </summary>
        /// <param name="vo">被新增或更新VO</param>
        public void SaveOrUpdate(Object vo)
        {
            HibernateTemplate.SaveOrUpdate(vo);
        }

        /// <summary>
        /// 執行全部更新或新增 
        /// </summary>
        /// <param name="entities">批量VO</param>
        public void SaveOrUpdateAll(ICollection entities)
        {
            HibernateTemplate.SaveOrUpdateAll(entities);
        }

        /// <summary>
        /// 執行全部更新或新增
        /// </summary>
        /// <param name="entities">批量VO</param> 
        public void SaveOrUpdateAll(IEnumerator entities)
        {
            List<object> list = new List<object>();

            while (entities.MoveNext())
            {
                list.Add(entities.Current);
            }

            if (list != null && list.Count > 0)
            {
                HibernateTemplate.SaveOrUpdateAll(list);
            }
        }

        #endregion

        #region 查筆數

        /// <summary>
        /// 動態查詢筆數
        /// </summary>
        /// <returns>總筆數</returns>
        public int CountByDetachedCriteria(DetachedCriteria detachedcriteria)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            DetachedCriteria tmpCriteria = CriteriaTransformer.Clone(detachedcriteria);
            tmpCriteria.ClearOrders();

            ICriteria criteria = tmpCriteria.GetExecutableCriteria(session);

            ProjectionList projection = Projections.ProjectionList();
            projection.Add(Projections.RowCount());
            return (int)criteria.SetProjection(projection).UniqueResult();
        }

        /// <summary>
        /// 動態查詢筆數
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>筆數</returns>
        public int CountByWhere<T>(string where)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            //使用反射
            T obj = (T)System.Reflection.Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());

            string hql = string.Format("from {0} {1}",
                obj.GetType().ToString(),
                where.ToUpper().IndexOf("WHERE") != -1 ? where : "WHERE " + where);

            hql = string.Format("select count(*) {0}", hql);

            log.Debug("hql:" + hql);
            IQuery query = session.CreateQuery(hql);

            return (int.Parse(query.UniqueResult().ToString()));
        }

        /// <summary>
        /// 計算VO總筆數
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns>VO總筆數</returns>
        public int CountTotal<T>()
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            ICriteria criteria = session.CreateCriteria(typeof(T));
            return ((int)criteria.SetProjection((Projections.ProjectionList().Add(Projections.RowCount()))).UniqueResult());
        }

        #endregion

        #region 動態查詢

        public IList Query(string hql, ArrayList param, IDictionary<string, string> conditions)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            log.Debug("hql:" + hql);
            IQuery query = session.CreateQuery(hql);
            for (int i = 0; i < param.Count; i++)
            {
                query.SetParameter(i, param[i]);
            }

            //// 處理paging
            if (conditions.IsContainsValue("PageSize") && conditions.IsContainsValue("PageIndex"))
            {
                int pageIndex = Convert.ToInt32(conditions["PageIndex"]);
                int pageSize = Convert.ToInt32(conditions["PageSize"]);
                int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize);
                query.SetFirstResult(startIndex);
                query.SetMaxResults(pageSize);
            }

            return query.List();
        }

        public IList<T> Query<T>(string hql, ArrayList param, IDictionary<string, string> conditions)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            log.Debug("hql:" + hql);
            IQuery query = session.CreateQuery(hql);
            for (int i = 0; i < param.Count; i++)
            {
                query.SetParameter(i, param[i]);
            }

            //// 處理paging
            if (conditions.IsContainsValue("PageSize") && conditions.IsContainsValue("PageIndex"))
            {
                int pageIndex = Convert.ToInt32(conditions["PageIndex"]);
                int pageSize = Convert.ToInt32(conditions["PageSize"]);
                int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize);
                query.SetFirstResult(startIndex);
                query.SetMaxResults(pageSize);
            }

            return query.List<T>();
        }

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>搜尋結果</returns> 
        public IList<T> SearchByWhere<T>(string where)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            //使用反射
            T obj = (T)System.Reflection.Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());

            string hql = string.Format("from {0} {1}",
                obj.GetType().ToString(),
                where.ToUpper().IndexOf("WHERE") != -1 ? where : "WHERE " + where);

            log.Debug("hql:" + hql);
            IQuery query = session.CreateQuery(hql);

            IList<T> list = query.List<T>();
            return list;
        }

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>搜尋結果</returns>
        public IList<T> SearchByWhere<T>(string where, int pageIndex, int pageSize)
        {
            int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize);

            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();

            //使用反射
            T obj = (T)System.Reflection.Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());

            string hql = string.Format("from {0} {1}",
                obj.GetType().ToString(),
                where.ToUpper().IndexOf("WHERE") != -1 ? where : "WHERE " + where);


            log.Debug("hql:" + hql);
            IQuery query = session.CreateQuery(hql);
            query.SetFirstResult(startIndex);
            query.SetMaxResults(pageSize);
            IList<T> list = query.List<T>();
            return list;
        }

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>搜尋結果</returns>   
        public IList<T> SearchByDetachedCriteria<T>(DetachedCriteria detachedcriteria)
        {
            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            ICriteria criteria = detachedcriteria.GetExecutableCriteria(session);
            return (criteria.List<T>());
        }

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>搜尋結果</returns>
        public IList<T> SearchByDetachedCriteria<T>(DetachedCriteria detachedcriteria, int pageIndex, int pageSize)
        {
            int startIndex = ConvertUtil.GetStartIndex(pageIndex, pageSize);

            ISession session = HibernateTemplate.SessionFactory.GetCurrentSession();
            //ISession session = NHibernateHelper.GetCurrentSession();
            ICriteria criteria = detachedcriteria.GetExecutableCriteria(session);
            criteria.SetFirstResult(startIndex);
            criteria.SetMaxResults(pageSize);
            return (criteria.List<T>());
        }

        #endregion

        #endregion
    }
}
