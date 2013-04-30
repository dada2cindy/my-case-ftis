using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Persistence;
using NHibernate.Criterion;
using WuDada.Core.Persistence.Ado;

namespace WuDada.Core.Post.Persistence
{
    public class MessageDao : AdoDao, IMessageDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增訊息
        /// </summary>
        /// <param name="messageVO">被新增的訊息</param>
        /// <returns>新增後的訊息</returns>
        public MessageVO CreateMessage(Domain.MessageVO messageVO)
        {
            NHibernateDao.Insert(messageVO);

            return messageVO;
        }

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
        public IList<MessageVO> GetMessageList(string createName, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<MessageVO>();

            if (!string.IsNullOrEmpty(createName))
            {
                dCriteria.Add(Expression.Like("CreateName", createName, MatchMode.Anywhere));
            }

            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("CreatedDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("CreatedDate", endDate));
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

            return NHibernateDao.SearchByDetachedCriteria<MessageVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得訊息筆數
        /// </summary>
        /// <param name="createName">留言者姓名</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>訊息筆數</returns>
        public int CountLogSystem(string createName, DateTime? startDate, DateTime? endDate)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<MessageVO>();

            if (!string.IsNullOrEmpty(createName))
            {
                dCriteria.Add(Expression.Like("CreateName", createName, MatchMode.Anywhere));
            }

            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("CreatedDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("CreatedDate", endDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }

        /// <summary>
        /// 刪除訊息
        /// </summary>
        /// <param name="messageVO">被刪除的訊息</param>
        public void DeleteMessage(MessageVO messageVO)
        {
            NHibernateDao.Delete(messageVO);
        }
    }
}
