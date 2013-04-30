using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;

namespace WuDada.Core.Post.Service
{
    public interface IMessageService
    {
        /// <summary>
        /// 新增訊息
        /// </summary>
        /// <param name="messageVO">被新增的訊息</param>
        /// <returns>新增後的訊息</returns>
        MessageVO CreateMessage(MessageVO messageVO);

        /// <summary>
        /// 取得訊息清單
        /// </summary>
        /// <param name="createName">留言者姓名</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>訊息清單</returns>
        IList<MessageVO> GetMessageList(string createName, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc);

        /// <summary>
        /// 取得訊息筆數
        /// </summary>
        /// <param name="createName">留言者姓名</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>訊息筆數</returns>
        int CountMessage(string createName, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 刪除訊息
        /// </summary>
        /// <param name="messageVO">被刪除的訊息</param>
        void DeleteMessage(MessageVO messageVO);
    }
}
