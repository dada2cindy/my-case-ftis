using System.Collections.Generic;
using WuDada.Core.SystemApplications.Domain;
using System;

namespace WuDada.Core.SystemApplications.Persistence
{
    public interface ILogDao
    {
        /// <summary>
        /// 新增系統紀錄
        /// </summary>
        /// <param name="logSystemVO">被新增的系統紀錄</param>
        /// <returns>新增後的系統紀錄</returns>
        LogSystemVO CreateLogSystem(LogSystemVO logSystemVO);

        /// <summary>
        /// 取得系統紀錄清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>系統紀錄清單</returns>
        IList<LogSystemVO> GetLogSystemList(string queryString, int pageIndex, int pageSize);

        /// <summary>
        /// 取得系統紀錄清單
        /// </summary>
        /// <param name="queryString">搜尋語法，用Where...order by ...</param>
        /// <returns>系統紀錄清單</returns>
        IList<LogSystemVO> GetLogSystemList(string queryString);

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
        IList<LogSystemVO> GetLogSystemList(string fucntion, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc);

        /// <summary>
        /// 取得系統紀錄筆數
        /// </summary>
        /// <param name="fucntion">功能</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>系統紀錄筆數</returns>
        int CountLogSystem(string fucntion, DateTime? startDate, DateTime? endDate);
    }
}
