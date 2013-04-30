using System.Collections.Generic;
using WuDada.Core.Persistence;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.SystemApplications.Domain;
using NHibernate.Criterion;
using System;

namespace WuDada.Core.SystemApplications.Persistence
{
    public class LogDao : AdoDao, ILogDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增系統紀錄
        /// </summary>
        /// <param name="logSystemVO">被新增的系統紀錄</param>
        /// <returns>新增後的系統紀錄</returns>
        public LogSystemVO CreateLogSystem(LogSystemVO logSystemVO)
        {
            NHibernateDao.Insert(logSystemVO);

            return logSystemVO;
        }

        /// <summary>
        /// 取得系統紀錄清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>系統紀錄清單</returns>
        public IList<LogSystemVO> GetLogSystemList(string queryString, int pageIndex, int pageSize)
        {
            return NHibernateDao.SearchByWhere<LogSystemVO>(queryString, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得系統紀錄清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>系統紀錄清單</returns>
        public IList<LogSystemVO> GetLogSystemList(string queryString)
        {
            return NHibernateDao.SearchByWhere<LogSystemVO>(queryString);
        }

        /// <summary>
        /// 取得系統紀錄清單
        /// </summary>
        /// <param name="fucntion">功能</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>系統紀錄清單</returns>
        public IList<LogSystemVO> GetLogSystemList(string fucntion, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<LogSystemVO>();

            if (!string.IsNullOrEmpty(fucntion))
            {
                dCriteria.Add(Expression.Eq("Fucntion", fucntion));
            }

            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("UpdateDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("UpdateDate", endDate));
            }

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortDesc)
                {
                    dCriteria.AddOrder(Order.Desc(sortField));
                }
                else
                {
                    dCriteria.AddOrder(Order.Asc(sortField));
                }
            }

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<LogSystemVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得系統紀錄筆數
        /// </summary>
        /// <param name="fucntion">功能</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>系統紀錄筆數</returns>
        public int CountLogSystem(string fucntion, DateTime? startDate, DateTime? endDate)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<LogSystemVO>();

            if (!string.IsNullOrEmpty(fucntion))
            {
                dCriteria.Add(Expression.Eq("Fucntion", fucntion));
            }

            if (startDate != null && endDate != null)
            {
                dCriteria.Add(Expression.Ge("UpdateDate", startDate));
                dCriteria.Add(Expression.Le("UpdateDate", endDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }
    }
}
