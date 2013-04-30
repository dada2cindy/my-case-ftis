using System.Collections.Generic;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications.Persistence;
using System;


namespace WuDada.Core.SystemApplications.Service.Impl
{
    public class LogService : ILogService
    {
        public ILogDao LogDao { get; set; }

        /// <summary>
        /// 新增系統紀錄
        /// </summary>
        /// <param name="logSystemVO">被新增的系統紀錄</param>
        /// <returns>新增後的系統紀錄</returns>
        public LogSystemVO CreateLogSystem(LogSystemVO logSystemVO)
        {
            return LogDao.CreateLogSystem(logSystemVO);
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
            return LogDao.GetLogSystemList(queryString, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得系統紀錄清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>系統紀錄清單</returns>
        public IList<LogSystemVO> GetLogSystemList(string queryString)
        {
            return LogDao.GetLogSystemList(queryString);
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
            return LogDao.GetLogSystemList(fucntion, startDate, endDate, pageIndex, pageSize, sortField, sortDesc);
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
            return LogDao.CountLogSystem(fucntion, startDate, endDate);
        }
    }
}
