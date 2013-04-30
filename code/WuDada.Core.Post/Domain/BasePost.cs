using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class BasePost : BaseObject
    {
        public BasePost()
        {

        }

        /// <summary>
        /// 標題
        /// </summary>
        [DataMember]
        public virtual string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        public virtual string Summary { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string HtmlContent { get; set; }

        /// <summary>
        /// 關鍵字
        /// </summary>
        [DataMember]
        public virtual string KeyWord { get; set; }

        ///// <summary>
        ///// Is Discuss
        ///// </summary>
        //public virtual int IsDiscuss { get; set; }

    }
}
