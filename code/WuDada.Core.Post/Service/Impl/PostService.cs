using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Persistence;

namespace WuDada.Core.Post.Service.Impl
{
    public class PostService : IPostService
    {
        public IPostDao PostDao { get; set; }

        public int RootNodeId { get; set; }

        /// <summary>
        /// 新增Node
        /// </summary>
        /// <param name="nodeVO">被新增的Node</param>
        /// <returns>新增後的Node</returns>
        public NodeVO CreateNode(NodeVO nodeVO)
        {
            return PostDao.CreateNode(nodeVO);
        }

        /// <summary>
        /// 取得Node By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Node</returns>
        public NodeVO GetNodeById(int nodeId)
        {
            return PostDao.GetNodeById(nodeId);
        }

        /// <summary>
        /// 取得Node By 父層Id
        /// </summary>
        /// <param name="parentId">父層Id</param>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByParentId(int parentId)
        {
            return PostDao.GetNodeListByParentId(parentId);
        }

        /// <summary>
        /// 取得Node By RootNode
        /// </summary>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByRoot()
        {
            return PostDao.GetNodeListByParentId(this.RootNodeId);
        }

        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="postVO">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        public PostVO CreatePost(PostVO postVO)
        {
            return PostDao.CreatePost(postVO);
        }

        /// <summary>
        /// 取得Post By PostId
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>Post</returns>
        public PostVO GetPostById(int postId)
        {
            return PostDao.GetPostById(postId);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId)
        {
            return PostDao.GetPostListByNodeId(nodeId);
        }

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="postVO">被刪除的Post</param>
        public void DeletePost(PostVO postVO)
        {
            PostDao.DeletePost(postVO);
        }

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="postVO">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        public PostVO UpdatePost(PostVO postVO)
        {
            return PostDao.UpdatePost(postVO);
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
            return PostDao.GetPostListByNodeId(nodeId, onlyShow, sortField, sortDesc);
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
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            return PostDao.GetPostListByNodeId(nodeId, onlyShow, startShowDate, pageIndex, pageSize, sortField, sortDesc);
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
            return PostDao.CountPostListByNodeId(nodeId, onlyShow, startShowDate);
        }

        /// <summary>
        /// 刪除Node
        /// </summary>
        /// <param name="nodeVO">被刪除的Node</param>
        public void DeleteNode(NodeVO nodeVO)
        {
            PostDao.DeleteNode(nodeVO);
        }

        /// <summary>
        /// 更新Node
        /// </summary>
        /// <param name="nodeVO">被更新的Node</param>
        /// <returns>更新後的Node</returns>
        public NodeVO UpdateNode(NodeVO nodeVO)
        {
            return PostDao.UpdateNode(nodeVO);
        }
    }
}
