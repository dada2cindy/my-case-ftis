using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using FTIS;
using FTIS.Domain.Impl;
using FTIS.Service;
using FTISWeb.Security;
using FTISWeb.Helper;
using WuDada.Core.Generic.Util;
using System.ComponentModel;
using Microsoft.Security.Application;

namespace FTISWeb.Models
{
    public abstract class AbstractNodeModel : AbstractShowModel
    {
        public AbstractNodeModel()
        {
        }

        public int NodeId { get; set; }

        public Node ParentNode { get; set; }

        public int ParentNodeId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [DisplayName("名稱")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DisplayName("英文名稱")]
        [Required]
        public string NameENG { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DisplayName("名稱")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 英文內容
        /// </summary>
        [DisplayName("名稱")]
        [Required]
        public string ContentENG { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Required]
        [Range(int.MinValue, int.MinValue, ErrorMessage = "必須為數字")]
        public int SortId { get; set; }

        /// <summary>
        /// 狀態. 0.關閉 1.開啟
        /// </summary>
        [DisplayName("狀態")]
        [Required]
        public string Status { get; set; }

        public string Tag { get; set; }

        protected void LoadNode(int id)
        {
            LoadNode(id, false);
        }

        protected void LoadNode(int id, bool noLazy)
        {
            Node node;
            if (noLazy)
            {
                node = m_FTISService.GetNodeByIdNoLazy(id);
            }
            else
            {
                node = m_FTISService.GetNodeById(id);
            }

            LoadNode(node);
        }

        protected void LoadNode(Node node)
        {
            if (node != null)
            {
                NodeId = node.NodeId;
                Name = node.Name;
                NameENG = node.NameENG;
                Content = node.Content;
                ContentENG = node.ContentENG;
                SortId = node.SortId;
                Status = node.Status;
                Tag = node.Tag;
                if (node.ParentNode != null)
                {
                    ParentNode = node.ParentNode;
                    ParentNodeId = node.ParentNode.NodeId;
                }
            }
        }

        public void Insert()
        {
            Node node = new Node();
            Save(node);
        }

        public void Update()
        {
            Node node = m_FTISService.GetNodeById(NodeId);
            Save(node);
        }

        public IList<News> GetNewsByTags()
        {
            IList<News> result = new List<News>();

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                //查詢
                IDictionary<string, string> conditions = new Dictionary<string, string>();
                conditions.Add("Status", "1");
                conditions.Add("Tags", this.Tag);

                result = m_FTISService.GetNewsList(conditions);
            }
            if (result == null)
            {
                result = new List<News>();
            }

            return result;
        }

        private void Save(Node node)
        {
            if (ParentNodeId > 0)
            {
                node.ParentNode = m_FTISService.GetNodeById(ParentNodeId);
            }
            node.Name = Name;
            node.NameENG = NameENG;
            ////使用AntiXSS的 Sanitizer.GetSageHtmlFragement() 方法，取得安全的HTML區段內容。
            //node.Content = Sanitizer.GetSafeHtmlFragment(Content);
            //node.ContentENG = Sanitizer.GetSafeHtmlFragment(ContentENG);
            node.Content = Content;
            node.ContentENG = ContentENG;
            node.SortId = SortId;
            node.Status = Status;
            node.Tag = Tag;

            if (node.NodeId == 0)
            {
                m_FTISService.CreateNode(node);
            }
            else
            {
                m_FTISService.UpdateNode(node);
            }

            LoadNode(node.NodeId, false);
        }

        public IList<Post> GetPostList(bool onlyOpen, int nodeId = 0)
        {
            if (nodeId > 0)
            {
                NodeId = nodeId;
            }

            IList<Post> result = new List<Post>();

            IDictionary<string, string> conditions = new Dictionary<string, string>();
            if (onlyOpen)
            {
                conditions.Add("Status", "1");
            }
            if (NodeId > 0)
            {
                conditions.Add("NodeId", ((int)NodeId).ToString());
                result = m_FTISService.GetPostList(conditions);                
            }

            if (result == null)
            {
                result = new List<Post>();
            }

            return result;
        }
    }
}
