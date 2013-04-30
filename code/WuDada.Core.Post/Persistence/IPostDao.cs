using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;

namespace WuDada.Core.Post.Persistence
{
    public interface IPostDao
    {
        /// <summary>
        /// 新增Node
        /// </summary>
        /// <param name="nodeVO">被新增的Node</param>
        /// <returns>新增後的Node</returns>
        NodeVO CreateNode(NodeVO nodeVO);

        /// <summary>
        /// 取得Node By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Node</returns>
        NodeVO GetNodeById(int nodeId);

        /// <summary>
        /// 取得Node By RootNode
        /// </summary>
        /// <returns>Node清單</returns>
        IList<NodeVO> GetNodeListByParentId(int parentId);

        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="postVO">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        PostVO CreatePost(PostVO postVO);

        /// <summary>
        /// 取得Post By PostId
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>Post</returns>
        PostVO GetPostById(int postId);

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Post清單</returns>
        IList<PostVO> GetPostListByNodeId(int nodeId);

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="postVO">被刪除的Post</param>
        void DeletePost(PostVO postVO);

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="postVO">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        PostVO UpdatePost(PostVO postVO);

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, string sortField, bool sortDesc);

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
        IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc);

        /// <summary>
        /// 取得Post筆數 By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// /// <param name="startDate">起始的上架日期</param>
        /// <returns>Post清單</returns>
        int CountPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate);

        /// <summary>
        /// 刪除Node
        /// </summary>
        /// <param name="nodeVO">被刪除的Node</param>
        void DeleteNode(NodeVO nodeVO);

        /// <summary>
        /// 更新Node
        /// </summary>
        /// <param name="nodeVO">被更新的Node</param>
        /// <returns>更新後的Node</returns>
        NodeVO UpdateNode(NodeVO nodeVO);
    }
}
