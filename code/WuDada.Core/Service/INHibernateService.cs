using System.Collections;
using System.Collections.Generic;
using NHibernate.Criterion;
namespace WuDada.Core.Service
{
    public interface INHibernateService
    {
        /// <summary>
        /// 取得VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns>VO的全部資料</returns>
        IList<T> GetAllVO<T>();

        /// <summary>
        /// 取出VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>VO的全部資料</returns>
        IList<T> GetAllVO<T>(int pageIndex, int pageSize);

        /// <summary>
        /// 取出VO的全部資料
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="order">排序條件</param>
        /// <returns>VO的全部資料</returns>
        IList<T> GetAllVO<T>(int pageIndex, int pageSize, Order order);

        /// <summary>
        /// 取得VO By KeyId
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="keyId">PK</param>
        /// <returns>VO</returns>
        T GetVOById<T>(object keyId);

        /// <summary>
        /// 取得VO By DetachedCriteria
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>搜尋的VO</returns>
        T GetVOByDetachedCriteria<T>(DetachedCriteria detachedcriteria);

        #region 增,刪,修

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vo">要新增的VO</param>
        void Insert(object vo);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="vo">被刪除的VO</param>
        void Delete(object vo);

        /// <summary>
        /// 更新VO
        /// </summary>
        /// <param name="vo">被更新的VO</param>
        void Update(object vo);

        /// <summary>
        /// 新增或更新VO
        /// </summary>
        /// <param name="vo">被新增或更新VO</param>
        void SaveOrUpdate(object vo);

        /// <summary>
        /// 執行全部更新或新增 
        /// </summary>
        /// <param name="entities">批量VO</param>
        void SaveOrUpdateAll(ICollection entities);

        #endregion

        #region 查筆數

        /// <summary>
        /// 動態查詢筆數
        /// </summary>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>筆數</returns>
        int CountByDetachedCriteria(DetachedCriteria detachedcriteria);

        /// <summary>
        /// 動態查詢筆數
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>筆數</returns>
        int CountByWhere<T>(string where);

        /// <summary>
        /// 計算VO總筆數
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns>VO總筆數</returns>
        int CountTotal<T>();

        #endregion        
        
        #region 動態查詢

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>搜尋結果</returns>
        IList<T> SearchByWhere<T>(string where);

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>搜尋結果</returns>
        IList<T> SearchByWhere<T>(string where, int pageIndex, int pageSize);

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <returns>搜尋結果</returns>
        IList<T> SearchByDetachedCriteria<T>(DetachedCriteria detachedcriteria);

        /// <summary>
        /// 動態查詢
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <param name="detachedcriteria">搜尋條件</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>搜尋結果</returns>
        IList<T> SearchByDetachedCriteria<T>(DetachedCriteria detachedcriteria, int pageIndex, int pageSize);

        #endregion
    }
}
