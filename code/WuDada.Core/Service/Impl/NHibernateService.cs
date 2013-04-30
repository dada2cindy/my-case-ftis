using System.Collections;
using System.Collections.Generic;
using WuDada.Core.Persistence;
using NHibernate.Criterion;

namespace WuDada.Core.Service.Impl
{
    public class NHibernateService : INHibernateService
    {
        public INHibernateDao NHibernateDao { get; set; }

        #region INHibernateService 成員

        /// <summary>
        /// 取得VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns>VO的全部資料</returns>
        public IList<T> GetAllVO<T>()
        {
            return NHibernateDao.GetAllVO<T>();
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
            return NHibernateDao.GetAllVO<T>(pageIndex, pageSize);
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
            return NHibernateDao.GetAllVO<T>(pageIndex, pageSize, order);
        }

        /// <summary>
        /// 取得VO By KeyId
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="keyId">PK</param>
        /// <returns>VO</returns>
        public T GetVOById<T>(object keyId)
        {
            return NHibernateDao.GetVOById<T>(keyId);
        }

        /// <summary>
        /// 取得VO By DetachedCriteria
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>搜尋的VO</returns>   
        public T GetVOByDetachedCriteria<T>(DetachedCriteria detachedcriteria)
        {
            return NHibernateDao.GetVOByDetachedCriteria<T>(detachedcriteria);
        }

        #region 增,刪,修

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vo">要新增的VO</param>
        public void Insert(object vo)
        {
            NHibernateDao.Insert(vo);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="vo">被刪除的VO</param>
        public void Delete(object vo)
        {
            NHibernateDao.Delete(vo);
        }

        /// <summary>
        /// 更新VO
        /// </summary>
        /// <param name="vo">被更新的VO</param>
        public void Update(object vo)
        {
            NHibernateDao.Update(vo);
        }

        /// <summary>
        /// 新增或更新VO
        /// </summary>
        /// <param name="vo">被新增或更新VO</param>
        public void SaveOrUpdate(object vo)
        {
            NHibernateDao.SaveOrUpdate(vo);
        }

        /// <summary>
        /// 執行全部更新或新增 
        /// </summary>
        /// <param name="entities">批量VO</param>
        public void SaveOrUpdateAll(ICollection entities)
        {
            NHibernateDao.SaveOrUpdateAll(entities);
        }

        #endregion

        #region 查筆數

        /// <summary>
        /// 動態查詢筆數
        /// </summary>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>筆數</returns>
        public int CountByDetachedCriteria(DetachedCriteria detachedcriteria)
        {
            return NHibernateDao.CountByDetachedCriteria(detachedcriteria);
        }

        /// <summary>
        /// 動態查詢筆數
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>筆數</returns>
        public int CountByWhere<T>(string where)
        {
            return NHibernateDao.CountByWhere<T>(where);
        }

        /// <summary>
        /// 計算VO總筆數
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns>VO總筆數</returns>
        public int CountTotal<T>()
        {
            return NHibernateDao.CountTotal<T>();
        }

        #endregion        

        #region 動態查詢

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>搜尋結果</returns>
        public IList<T> SearchByWhere<T>(string where)
        {
            return NHibernateDao.SearchByWhere<T>(where);
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
            return NHibernateDao.SearchByWhere<T>(where, pageIndex, pageSize);
        }

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>搜尋結果</returns>
        public IList<T> SearchByDetachedCriteria<T>(DetachedCriteria detachedcriteria)
        {
            return NHibernateDao.SearchByDetachedCriteria<T>(detachedcriteria);
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
            return NHibernateDao.SearchByDetachedCriteria<T>(detachedcriteria, pageIndex, pageSize);
        }
       
        #endregion

        #endregion
    }
}
