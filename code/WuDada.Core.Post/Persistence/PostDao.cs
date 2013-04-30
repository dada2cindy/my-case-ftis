using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using NHibernate.Criterion;

namespace WuDada.Core.Post.Persistence
{
    public class PostDao : AdoDao, IPostDao
    {
        public INHibernateDao NHibernateDao { get; set; }        

        /// <summary>
        /// 新增Node
        /// </summary>
        /// <param name="nodeVO">被新增的Node</param>
        /// <returns>新增後的Node</returns>
        public NodeVO CreateNode(NodeVO nodeVO)
        {
            NHibernateDao.Insert(nodeVO);

            return nodeVO;
        }

        /// <summary>
        /// 取得Node By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Node</returns>
        public NodeVO GetNodeById(int nodeId)
        {
            return NHibernateDao.GetVOById<NodeVO>(nodeId);
        }

        /// <summary>
        /// 取得Node By RootNode
        /// </summary>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByParentId(int parentId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<NodeVO>();
            dCriteria.CreateCriteria("ParentNode").Add(Expression.Eq("NodeId", parentId));
            dCriteria.AddOrder(Order.Asc("SortNo"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<NodeVO>(dCriteria);
        }

        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="postVO">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        public PostVO CreatePost(PostVO postVO)
        {
            NHibernateDao.Insert(postVO);

            return postVO;
        }

        /// <summary>
        /// 取得Post By PostId
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>Post</returns>
        public PostVO GetPostById(int postId)
        {
            return NHibernateDao.GetVOById<PostVO>(postId);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            dCriteria.AddOrder(Order.Asc("SortNo"));
            dCriteria.AddOrder(Order.Desc("UpdatedDate"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="postVO">被刪除的Post</param>
        public void DeletePost(PostVO postVO)
        {
            NHibernateDao.Delete(postVO);
        }

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="postVO">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        public PostVO UpdatePost(PostVO postVO)
        {
            NHibernateDao.Update(postVO);

            return postVO;
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            
            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));                
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// /// <param name="startDate">起始的上架日期</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate, int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            
            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (startShowDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", startShowDate));
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得Post筆數 By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// /// <param name="startDate">起始的上架日期</param>
        /// <returns>Post清單</returns>
        public int CountPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));

            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (startShowDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", startShowDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }

        /// <summary>
        /// 刪除Node
        /// </summary>
        /// <param name="nodeVO">被刪除的Node</param>
        public void DeleteNode(NodeVO nodeVO)
        {
            NHibernateDao.Delete(nodeVO);
        }

        /// <summary>
        /// 更新Node
        /// </summary>
        /// <param name="nodeVO">被更新的Node</param>
        /// <returns>更新後的Node</returns>
        public NodeVO UpdateNode(NodeVO nodeVO)
        {
            NHibernateDao.Update(nodeVO);

            return nodeVO;
        }
    }
}
