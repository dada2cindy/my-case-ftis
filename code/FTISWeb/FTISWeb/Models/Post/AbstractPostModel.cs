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
    public abstract class AbstractPostModel : AbstractEntityModel
    {
        public AbstractPostModel()
        {
        }

        public int PostId { get; set; }

        public Node Node { get; set; }

        /// <summary>
        /// 分類
        /// </summary>
        [DisplayName("分類")]
        [Required(ErrorMessage = "請選擇分類")]
        [Range(1, int.MaxValue, ErrorMessage = "請選擇分類")]
        public int NodeId { get; set; }

        /// <summary>
        /// 廠商
        /// </summary>
        [DisplayName("廠商")]
        [Required]
        public string Company { get; set; }

        /// <summary>
        /// 廠商(英)
        /// </summary>
        [DisplayName("廠商(英)")]
        [Required]
        public string CompanyENG { get; set; }

        /// <summary>
        /// 登錄效期
        /// </summary>
        [DisplayName("登錄效期")]
        [Required]
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 負責人
        /// </summary>
        public string Charge { get; set; }

        /// <summary>
        /// 連絡人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登錄項目明細
        /// </summary>
        [DisplayName("登錄項目明細")]
        [Required]        
        public string Content { get; set; }

        public string Tag { get; set; }

        /// <summary>
        /// 編號
        /// </summary>
        public string No { get; set; }

        protected void LoadPost(int id)
        {
            LoadPost(id, false);
        }

        protected void LoadPost(int id, bool noLazy)
        {
            Post post;
            if (noLazy)
            {
                post = m_FTISService.GetPostByIdNoLazy(id);
            }
            else
            {
                post = m_FTISService.GetPostById(id);
            }

            LoadPost(post);
        }

        protected void LoadPost(Post post)
        {
            if (post != null)
            {
                PostId = post.PostId;
                Name = post.Name;
                Company = post.Company;
                CompanyENG = post.CompanyENG;
                ExpiredDate = post.ExpiredDate;
                Charge = post.Charge;
                Contact = post.Contact;
                Tel = post.Tel;
                Fax = post.Fax;
                Address = post.Address;
                Email = post.Email;
                Content = post.Content;                
                SortId = post.SortId;
                Status = post.Status;
                Tag = post.Tag;
                No = post.No;
                if (post.Node != null)
                {
                    Node = post.Node;
                    NodeId = post.Node.NodeId;
                }
            }
        }

        public void Insert()
        {
            Post post = new Post();
            Save(post);
        }

        public void Update()
        {
            Post post = m_FTISService.GetPostById(NodeId);
            Save(post);
        }

        private void Save(Post post)
        {
            if (NodeId > 0)
            {
                post.Node = m_FTISService.GetNodeById(NodeId);
            }                        
            post.Name = Name;

            post.Company = Company;
            post.CompanyENG = CompanyENG;
            post.ExpiredDate = ExpiredDate;
            post.Charge = Charge;
            post.Contact = Contact;
            post.Tel = Tel;
            post.Fax = Fax;
            post.Address = Address;
            post.Email = Email;
            post.Content = Content;
            post.SortId = SortId;
            post.Status = Status;
            post.Tag = Tag;
            post.No = No;

            if (post.PostId == 0)
            {
                m_FTISService.CreatePost(post);
            }
            else
            {
                m_FTISService.UpdatePost(post);
            }

            LoadPost(post.PostId, false);
        }
    }
}
